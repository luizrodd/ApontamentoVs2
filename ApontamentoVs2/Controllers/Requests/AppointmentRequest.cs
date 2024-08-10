namespace ApontamentoVs2.Controllers.Requests
{
    public class AppointmentRequest
    {
        public Guid UserID { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
    }
}
