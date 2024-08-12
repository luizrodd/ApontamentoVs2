namespace ApontamentoVs2.Domain
{
    public class User : Entity<Guid>
    {
        public User(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public string Name { get; set; }
        public string Type { get; set; }
    }
}
