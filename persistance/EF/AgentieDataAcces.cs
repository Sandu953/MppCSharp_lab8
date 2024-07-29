using lab8.Domain;
using System.Data.Entity;

namespace lab8.Repository
{
    public class AppDbContext : System.Data.Entity.DbContext
    {
        public System.Data.Entity.DbSet<Agentie> Agenties { get; set; }

        public AppDbContext() : base("name=connectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agentie>()
                .ToTable("Agentie")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Agentie>()
                .Property(u => u.Username)
                .HasMaxLength(50);

            modelBuilder.Entity<Agentie>()
                .Property(u => u.Password)
                .HasMaxLength(50);

            //modelBuilder.Entity<Agentie>()
            //    .Property(u => u.Pa)
            //    .HasMaxLength(50);
            //base.OnModelCreating(modelBuilder);
        }
    }
}