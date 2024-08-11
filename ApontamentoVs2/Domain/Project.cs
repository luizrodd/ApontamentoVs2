namespace ApontamentoVs2.Domain
{
    public class Project : Entity<Guid>
    {
        public Project(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; set; }
    }
}
