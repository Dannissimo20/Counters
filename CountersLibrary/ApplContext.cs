using Microsoft.EntityFrameworkCore;

namespace CountersLibrary;

public class ApplContext : DbContext
{
    public DbSet<Record> Record { get; set; } = null!;
    public DbSet<Cost> Cost { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<Tarif> Tarif { get; set; } = null!;

    public ApplContext(DbContextOptions<ApplContext> options) : base(options)
        {
            Database.Migrate();
            Context.AddDb(this);
        }

    public ApplContext() : this(GetDb())
    {
        
    }
        public static string ConnectionString = "Host=localhost;Port=5432;Database=counters;Username=postgres;Password=denchik2702";
        public static DbContextOptions<ApplContext> GetDb()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplContext>();
            return optionsBuilder.UseNpgsql(ConnectionString).UseLazyLoadingProxies().Options;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
}