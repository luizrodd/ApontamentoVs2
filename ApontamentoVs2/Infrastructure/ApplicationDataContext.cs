using Microsoft.EntityFrameworkCore;

namespace ApontamentoVs2.Infrastructure
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options) { }

        public DbSet<Domain.User> Users { get; set; }
        public DbSet<Domain.Project> Projects { get; set; }
        public DbSet<Domain.Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.User>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.User>()
                .HasMany(x => x.Projects)
                .WithOne()
                .HasForeignKey("UserId");

            modelBuilder.Entity<Domain.Project>().HasKey(x => x.Id);

            modelBuilder.Entity<Domain.Appointment>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Appointment>()
                .Property<Guid>("_projectId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ProjectId")
                .IsRequired();
            modelBuilder.Entity<Domain.Appointment>()
                .Ignore(x => x.Project)
                .HasOne<Domain.Project>()
                .WithMany()
                .HasForeignKey("_projectId")
                .HasConstraintName("FK_Appointment_Project");
        }
    }
}
