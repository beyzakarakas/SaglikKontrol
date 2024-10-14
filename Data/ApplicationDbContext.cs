using DiabetWebSite.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<BloodSugar> BloodSugars { get; set; }
    public DbSet<BloodPressure> BloodPressures { get; set; }
    public DbSet<DiabetesControl> DiabetesControls { get; set; }
    public DbSet<BodyMassIndex> BodyMassIndexes { get; set; }
    public DbSet<FindRiscResult> FindRiscResults { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // BodyMassIndex tablo adını ve sütun adlarını tanımlayın
        modelBuilder.Entity<BodyMassIndex>(entity =>
        {
            entity.ToTable("BodyMassIndexes");

            entity.HasKey(e => e.BodyMassIndexId);

            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.HeightCm).IsRequired();
            entity.Property(e => e.WeightKg).IsRequired();
            entity.Property(e => e.MeasurementTime).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(500);

            // Foreign Key Relationship
            entity.HasOne(e => e.User)
                .WithMany(u => u.BodyMassIndexes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

}
