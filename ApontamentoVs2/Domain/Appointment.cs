namespace ApontamentoVs2.Domain
{
    public class Appointment : Entity<Guid>
    {
        public Appointment(Guid projectId, Guid userId, DateTime startTime, DateTime endTime)
        {
            _userId = userId;
            _projectId = projectId;
            StartTime = startTime;
            EndTime = endTime;
            CreatedAt = DateTime.Now;

            var validator = new AppointmentValidator("rules.json");
            validator.Validate(this);
        }

        public Project Task { get; set; }
        public Guid _projectId;
        public User User { get; set; }
        public Guid _userId;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
