namespace ApontamentoVs2.Domain
{
    public class Appointment : Entity<Guid>
    {
        private Appointment() { }
        public Appointment(Guid projectId, DateTime startTime, DateTime endTime, string description)
        {
            _projectId = projectId;
            StartTime = startTime;
            EndTime = endTime;
            Description = description;
        }
        public Project Project { get; private set; }
        public Guid _projectId;
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Description { get; private set; }

    }
}
