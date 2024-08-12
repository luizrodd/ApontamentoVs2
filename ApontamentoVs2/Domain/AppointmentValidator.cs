using Newtonsoft.Json;

namespace ApontamentoVs2.Domain
{
    public class AppointmentValidator
    {
        private readonly RulesConfig _rulesConfig;

        public AppointmentValidator(string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            _rulesConfig = JsonConvert.DeserializeObject<RulesConfig>(json);
        }

        public void Validate(Appointment appointment)
        {
            foreach (var rule in _rulesConfig.Rules)
            {
                switch (rule.Type)
                {
                    case "comparison":
                        ValidateComparisonRule(appointment, rule);
                        break;
                    case "duration":
                        ValidateDurationRule(appointment, rule);
                        break;
                    default:
                        throw new InvalidOperationException($"Tipo de regra desconhecido: {rule.Type}");
                }
            }
        }

        private void ValidateComparisonRule(Appointment appointment, Rule rule)
        {
            var appointmentType = typeof(Appointment);
            var fieldValue = (DateTime)appointmentType.GetProperty(rule.Field).GetValue(appointment);
            var compareToValue = (DateTime)appointmentType.GetProperty(rule.CompareTo).GetValue(appointment);

            if (rule.Operator == "lessThan" && fieldValue >= compareToValue)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
        }

        private void ValidateDurationRule(Appointment appointment, Rule rule)
        {
            var appointmentType = typeof(Appointment);
            var startTime = (DateTime)appointmentType.GetProperty(rule.Field).GetValue(appointment);
            var endTime = (DateTime)appointmentType.GetProperty("EndTime").GetValue(appointment);
            var duration = endTime - startTime;

            if (rule.Operator == "minDuration" && duration.TotalMinutes < rule.Duration)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
        }
    }
}
