using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace ApontamentoVs2.Domain
{
    public class AppointmentValidator
    {
        private readonly Dictionary<string, Action<Appointment, Rule>> _ruleValidators;
        private readonly RulesConfig _rulesConfig;
        private readonly Func<Appointment, DateTime> _startTimeAccessor;
        private readonly Func<Appointment, DateTime> _endTimeAccessor;

        public AppointmentValidator(string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            _rulesConfig = JsonConvert.DeserializeObject<RulesConfig>(json);

            _ruleValidators = new Dictionary<string, Action<Appointment, Rule>>()
            {
                { "comparison", ValidateComparisonRule },
                { "duration", ValidateDurationRule },
                { "maxDailyHours", ValidateMaxDailyHoursRule },
                { "workHours", ValidateWorkHoursRule }
            };

            _startTimeAccessor = PropertyAccessor.CreateGetter<Appointment, DateTime>("StartTime");
            _endTimeAccessor = PropertyAccessor.CreateGetter<Appointment, DateTime>("EndTime");
        }

        public void Validate(Appointment appointment)
        {
            foreach (var rule in _rulesConfig.Rules)
            {
                if (_ruleValidators.TryGetValue(rule.Type, out var validate))
                {
                    validate(appointment, rule);
                }
                else
                {
                    throw new InvalidOperationException($"Tipo de regra desconhecido: {rule.Type}");
                }
            }
        }

        private void ValidateComparisonRule(Appointment appointment, Rule rule)
        {
            var fieldValue = _startTimeAccessor(appointment);
            var compareToValue = _endTimeAccessor(appointment);

            if (rule.Operator == "lessThan" && fieldValue >= compareToValue)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
        }

        private void ValidateDurationRule(Appointment appointment, Rule rule)
        {
            var startTime = _startTimeAccessor(appointment);
            var endTime = _endTimeAccessor(appointment);
            var duration = (endTime - startTime).TotalMinutes;

            if (rule.Operator == "minDuration" && duration < rule.Duration)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
        }

        private void ValidateMaxDailyHoursRule(Appointment appointment, Rule rule)
        {
            var startTime = _startTimeAccessor(appointment);
            var endTime = _endTimeAccessor(appointment);
            var totalHours = (endTime - startTime).TotalHours;

            if (totalHours > rule.MaxHours)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
        }

        private void ValidateWorkHoursRule(Appointment appointment, Rule rule)
        {
            var timeValue = _startTimeAccessor(appointment).TimeOfDay;
            var targetTime = TimeSpan.Parse(rule.Operator == "start" ? rule.Start : rule.End);

            if (rule.Operator == "start" && timeValue < targetTime)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
            if (rule.Operator == "end" && timeValue > targetTime)
            {
                throw new InvalidOperationException(rule.ErrorMessage);
            }
        }
    }

    public static class PropertyAccessor
    {
        public static Func<T, TProp> CreateGetter<T, TProp>(string propertyName)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(param, propertyName);
            var lambda = Expression.Lambda<Func<T, TProp>>(property, param);
            return lambda.Compile();
        }
    }
}
