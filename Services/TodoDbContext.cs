using EFCore_ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore_ConsoleApp.Services
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Todo.db;");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskPriority>()
                .ToTable("TASK_PRIORITY")
                .HasMany(p => p.Tasks)
                .WithOne(t => t.TaskPriority);

            modelBuilder.Entity<TaskPriority>()
                .Property(p => p.TaskPriorityId)
                .HasColumnName("ID")
                .HasColumnType("INTEGER")
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<TaskPriority>()
                .Property(p => p.Name)
                .HasColumnName("NAME")
                .HasColumnType("TEXT")
                .HasDefaultValue("(Nueva prioridad de tarea sin nombre)")
                .IsRequired();

            modelBuilder.Entity<Task>()
                .ToTable("TASK")
                .HasOne(t => t.TaskPriority)
                .WithMany(p => p.Tasks);

            modelBuilder.Entity<Task>()
                .Property(t => t.TaskId)
                .HasColumnName("ID")
                .HasColumnType("INTEGER")
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.Name)
                .HasColumnName("NAME")
                .HasColumnType("TEXT")
                .HasDefaultValue("(Nueva tarea sin nombre)")
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.Done)
                .HasColumnName("DONE")
                .HasColumnType("INTEGER")
                .HasDefaultValue(false)
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.DueDate)
                .HasColumnName("DUE_DATE")
                .HasColumnType("TEXT")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            modelBuilder.Entity<Task>()
                .Property(t => t.TaskPriorityId)
                .HasColumnName("TASK_PRIORITY_ID")
                .HasColumnType("INTEGER")
                .IsRequired();
        }
    }
}