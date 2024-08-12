namespace ApontamentoVs2.Domain
{
    public class Appointment : Entity<Guid>
    {

        public Appointment(Guid projectId, Guid userId, DateTime startTime, DateTime endTime, DateTime createdAt) 
        { 
            _userId = userId;
            _projectId = projectId;
            StartTime = startTime;
            EndTime = endTime;
            CreatedAt = createdAt;
        }

        public Project Project { get; set; }
        public Guid _projectId; 
        public User User { get; set; }
        public Guid _userId;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
