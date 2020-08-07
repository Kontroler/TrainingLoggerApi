using System;
using TrainingLogger.API.Models;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainingLogger.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingExerciseSetRep> TrainingExerciseSetReps { get; set; }
        public DbSet<TrainingExerciseSet> TrainingExerciseSets { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }
        public DbSet<Training> Trainings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UnitConfiguration());
            builder.ApplyConfiguration(new ExerciseConfiguration());
            builder.ApplyConfiguration(new TrainingExerciseSetRepConfiguration());
            builder.ApplyConfiguration(new TrainingConfiguration());
        }
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(u => u.Username)
                .IsUnique();

            builder
                .Property(u => u.Username)
                .IsRequired();
            builder
                .Property(u => u.PasswordHash)
                .IsRequired();
            builder
                .Property(u => u.PasswordSalt)
                .IsRequired();
            builder
                .Property(u => u.Created)
                .IsRequired();
        }
    }

    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder
                .HasIndex(u => u.Code)
                .IsUnique();
            builder.HasData(
                new Unit { Id = 1, Code = "kg" },
                new Unit { Id = 2, Code = "lbs" }
            );
        }
    }

    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>
        where TBase : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TBase> builder)
        {
            builder
                .Property(b => b.Created)
                .IsRequired();
            builder
                .Property(b => b.CreatedById)
                .IsRequired();
            builder
                .Property(b => b.LastUpdated)
                .IsRequired();
            builder
                .Property(b => b.LastUpdatedById)
                .IsRequired();
        }
    }

    public class ExerciseConfiguration : BaseEntityTypeConfiguration<Exercise>
    {
        public override void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
                .HasIndex(b => b.Name)
                .IsUnique();

            base.Configure(builder);
        }
    }

    public class TrainingExerciseSetRepConfiguration : BaseEntityTypeConfiguration<TrainingExerciseSetRep>
    {
        public override void Configure(EntityTypeBuilder<TrainingExerciseSetRep> builder)
        {
            builder
                .Property(b => b.Value)
                .IsRequired();
            builder
                .Property(b => b.Weight)
                .IsRequired();

            base.Configure(builder);
        }
    }

    public class TrainingConfiguration : BaseEntityTypeConfiguration<Training>
    {
        public override void Configure(EntityTypeBuilder<Training> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired();
            builder
                .Property(b => b.Date)
                .IsRequired();

            base.Configure(builder);
        }
    }
}