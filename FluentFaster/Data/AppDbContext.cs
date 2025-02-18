using FluentFaster.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluentFaster.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions onptions): base(onptions) {
        
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<GrammarTopic> GrammarTopics { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<Word> Words { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>().HasKey(g => g.GameID);

            modelBuilder.Entity<GrammarTopic>()
                .HasKey(gt => gt.TopicID);
            modelBuilder.Entity<GrammarTopic>()
                .HasOne(gt => gt.Game)
                .WithMany(g => g.GrammarTopics)
                .HasForeignKey(gt => gt.GameID);

            modelBuilder.Entity<Test>()
                .HasKey(t => t.TestID);
            modelBuilder.Entity<Test>()
                .HasOne(t => t.GrammarTopic)
                .WithMany(gt => gt.Tests)
                .HasForeignKey(t => t.TopicID);

            modelBuilder.Entity<Question>()
                .HasKey(q => q.QuestionID);
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TestID);

            modelBuilder.Entity<AnswerOption>()
                .HasKey(a => a.OptionID);
            modelBuilder.Entity<AnswerOption>()
                .HasOne(a => a.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(a => a.QuestionID);

            modelBuilder.Entity<Word>().HasKey(w => w.WordID);
            modelBuilder.Entity<Word>()
                .HasOne(w => w.Game)
                .WithMany(g => g.Words)
                .HasForeignKey(w => w.GameID);
        }
    }
}
