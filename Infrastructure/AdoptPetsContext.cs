using AdoptPets.Application.Contracts.Interfaces;
using AdoptPets.Domain.Common;
using AdoptPets.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdoptPets.Infrastructure
{
    public class AdoptPetsContext : DbContext
    {
        private readonly ICurrentUserService currentUserService;

        public AdoptPetsContext(
            DbContextOptions<AdoptPetsContext> options, ICurrentUserService currentUserService) :
            base(options)
        {
            this.currentUserService = currentUserService;
        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<Deworming> Dewormings { get; set; }
        public DbSet<Observation> Observations { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUserService.GetCurrentUserId();
                    
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    //entry.Entity.LastModifiedBy = currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedBy = currentUserService.GetCurrentUserId();

                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
           .HasOne(a => a.MedicalHistory)
           .WithOne(m => m.Animal)
           .HasForeignKey<MedicalHistory>(m => m.AnimalId);

            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Vaccinations)
                .WithOne(v => v.Animal)
                .HasForeignKey(v => v.AnimalId);

            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Observations)
                .WithOne(o => o.Animal)
                .HasForeignKey(o => o.AnimalId);

            modelBuilder.Entity<Animal>()
                .HasMany(a => a.Dewormings)
                .WithOne(d => d.Animal);
       
        }
    }
}
