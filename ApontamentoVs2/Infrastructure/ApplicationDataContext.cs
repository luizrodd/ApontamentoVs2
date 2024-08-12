using ApontamentoVs2.Domain;
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
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.EndTime).IsRequired();

                entity.HasOne(e => e.Project)
                      .WithMany()
                      .HasForeignKey(e => e._projectId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                      .WithMany()
                      .HasForeignKey(e => e._userId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}
