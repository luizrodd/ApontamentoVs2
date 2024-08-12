using ApontamentoVs2.Domain;
using ApontamentoVs2.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class AppointmentRuleEngine
{
    private readonly List<UserTypeRules> _rules;
    private readonly IAppointmentRepository _repository; // Adiciona uma referência ao repositório

    public AppointmentRuleEngine(string jsonRulesPath, IAppointmentRepository repository)
    {
        var json = File.ReadAllText(jsonRulesPath);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _rules = JsonSerializer.Deserialize<List<UserTypeRules>>(json, options);
        _repository = repository; // Inicializa o repositório
    }

    public void Validate(Appointment appointment)
    {
        var userTypeRules = _rules.FirstOrDefault(r => r.UserType == appointment.User.Type);
        if (userTypeRules == null)
        {
            appointment.IsApproved = false;
            return;
        }

        // Verifica as regras gerais do usuário
        foreach (var rule in userTypeRules.Rules)
        {
            if (!EvaluateRule(appointment, rule))
            {
                appointment.IsApproved = false;
                return;
            }
        }

        // Verifica regras específicas para estagiários
        if (appointment.User.Type == "Estagiario")
        {
            var totalHours = GetTotalHoursForUserOnDate(appointment.User.Id, appointment.StartTime.Date);
            var appointmentDuration = (appointment.EndTime - appointment.StartTime).TotalHours;

            if (totalHours + appointmentDuration > 5)
            {
                throw new InvalidOperationException("Estagiário não pode registrar mais de 5 horas no mesmo dia.");
            }
        }

        appointment.IsApproved = true;
    }

    private bool EvaluateRule(Appointment appointment, Rule rule)
    {
        switch (rule.Field)
        {
            case "StartTime":
                var startTime = TimeSpan.Parse(rule.Value);
                if (rule.Condition == "greater_than" && appointment.StartTime.TimeOfDay <= startTime)
                    return false;
                break;

            case "EndTime":
                var endTime = TimeSpan.Parse(rule.Value);
                if (rule.Condition == "less_than" && appointment.EndTime.TimeOfDay >= endTime)
                    return false;
                break;

            case "Duration":
                var duration = TimeSpan.Parse(rule.Value);
                var appointmentDuration = appointment.EndTime - appointment.StartTime;
                if (rule.Condition == "greater_than_or_equal" && appointmentDuration < duration)
                    return false;
                if (rule.Condition == "less_than_or_equal" && appointmentDuration > duration)
                    return false;
                break;

            default:
                throw new Exception("Campo não reconhecido na regra.");
        }

        return true;
    }

    private double GetTotalHoursForUserOnDate(Guid userId, DateTime date)
    {
        // Recupera os apontamentos para o usuário na data especificada
        var appointments = _repository.GetAppointmentsForUserOnDate(userId, date);

        // Soma as horas registradas
        return appointments.Sum(a => (a.EndTime - a.StartTime).TotalHours);
    }
}

public class UserTypeRules
{
    public string UserType { get; set; }
    public List<Rule> Rules { get; set; }
}

public class Rule
{
    public string Field { get; set; }
    public string Condition { get; set; }
    public string Value { get; set; }
}
