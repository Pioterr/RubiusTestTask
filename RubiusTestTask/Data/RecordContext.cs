using RubiusTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace RubiusTestTask.Data
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions<RecordContext> options) : base(options)
        {
        }
        
       public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<Record>().ToTable("Record");
        }
    }
}