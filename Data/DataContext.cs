using System;
using TrainingLogger.API.Models;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.Models;

namespace TrainingLogger.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingExerciseSetRep> TrainingExerciseSetReps { get; set; }
        public DbSet<TrainingExerciseSet> TrainingExerciseSets { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }
        public DbSet<Training> Trainings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<Unit>().HasData(
                new Unit
                {
                    Id = 1,
                    Code = "kg"
                },
                new Unit
                {
                    Id = 2,
                    Code = "lbs"
                }
            );
        }
    }
}