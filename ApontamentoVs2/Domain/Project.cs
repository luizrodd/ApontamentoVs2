namespace ApontamentoVs2.Domain
{
    public class Project : Entity<Guid>
    {
        private Project() { }
        public string Name { get; set; }
    }
}
