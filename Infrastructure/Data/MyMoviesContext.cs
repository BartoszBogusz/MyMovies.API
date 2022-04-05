namespace Infrastructure.Data;

public class MyMoviesContext : DbContext
{
    public MyMoviesContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }

    public DbSet<Disk> Disks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Movie

        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<Movie>().HasKey(x => x.Id);
        modelBuilder.Entity<Movie>()
            .Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Movie>()
            .HasOne(x => x.Detail)
            .WithOne(y => y.Movie)
            .HasForeignKey<MovieDetail>(md => md.MovieId);
        modelBuilder.Entity<Movie>()
            .HasOne(x => x.Disk)
            .WithMany(y => y.Movies)
            .HasForeignKey(md => md.DiskId);

        #endregion

        #region Disk

        modelBuilder.Entity<Disk>().ToTable("Disks");
        modelBuilder.Entity<Disk>().HasKey(x => x.Id);
        modelBuilder.Entity<Disk>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Disk>()
            .Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        #endregion

        #region MovieDetails

        modelBuilder.Entity<MovieDetail>().ToTable("MovieDetails");
        modelBuilder.Entity<MovieDetail>().HasKey(x => x.Id);
        modelBuilder.Entity<MovieDetail>()
            .Property(p => p.Created)
            .HasColumnType("datetime2").HasPrecision(0)
            .IsRequired();
        modelBuilder.Entity<MovieDetail>()
            .Property(p => p.LastModified)
            .HasColumnType("datetime2").HasPrecision(0)
            .IsRequired();

        #endregion
    }
}
