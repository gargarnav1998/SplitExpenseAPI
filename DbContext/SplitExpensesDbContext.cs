using Microsoft.EntityFrameworkCore;
using SplitExpenses.Entities;

namespace SplitExpenses
{
    public class SplitExpensesDbContext : DbContext
    {
        public SplitExpensesDbContext(DbContextOptions<SplitExpensesDbContext> options) 
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("");
        }
        //entities
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<GroupParticipant> Transactions { get; set; }
        public DbSet<Transaction> GroupParticipantExpenses { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
