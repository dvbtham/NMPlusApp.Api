using Microsoft.EntityFrameworkCore;
using NMPlusApp.Api.Models;

namespace NMPlusApp.Api.Data
{
    public class PokeAppDataContext : DbContext
    {
        public PokeAppDataContext(DbContextOptions<PokeAppDataContext> options)
            : base(options)
        { }

        public DbSet<LogEntry> LogEntries { get; set; }
    }
}
