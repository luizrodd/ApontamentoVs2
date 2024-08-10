namespace ApontamentoVs2.Domain
{
    public class User : Entity<Guid>
    {
        private User() { }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
