using Microsoft.EntityFrameworkCore;
using RoomMate.Models;

namespace RoomMate.Data;

public class RoomMateDbContext : DbContext
{
    public RoomMateDbContext(DbContextOptions<RoomMateDbContext> options)
        : base(options)
    {
    }

    // Database tables
    public DbSet<TempUser> Users => Set<TempUser>();
    public DbSet<Profile> Profiles => Set<Profile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One User can have one Profile
        modelBuilder.Entity<TempUser>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);
    }
}
