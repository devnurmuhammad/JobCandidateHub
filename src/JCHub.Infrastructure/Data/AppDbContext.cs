using JCHub.Application.DbInterfaces;
using JCHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JCHub.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public new DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    #region DbSet

    public virtual DbSet<Candidate> Candidates { get; set; }
    public virtual DbSet<State> States { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<State>().HasData(
            new State { Id = 1, Name = "Active" },
            new State { Id = 2, Name = "Passive" }
        );
        
        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}