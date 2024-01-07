using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models;

public partial class Comp2001malTjiatianContext : DbContext
{
    public Comp2001malTjiatianContext()
    {
    }

    public Comp2001malTjiatianContext(DbContextOptions<Comp2001malTjiatianContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Trail> Trails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=dist-6-505.uopnet.plymouth.ac.uk;Database=COMP2001MAL_TJiatian;User Id=TJiatian;Password=TkuC238+;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.ActivityId).HasName("PK__Activity__45F4A7F1B2469F09");

            entity.ToTable("Activity", "CW1", tb =>
                {
                    tb.HasTrigger("UpdateTotalActivities");
                    tb.HasTrigger("UpdateTrailDuration");
                });

            entity.Property(e => e.ActivityId).HasColumnName("ActivityID");
            entity.Property(e => e.ActivityDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActivityTime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ActivityType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TrailId).HasColumnName("TrailID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Activities)
                .HasForeignKey(d => d.TrailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Activity__TrailI__3493CFA7");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF5241CD7F5");

            entity.ToTable("Favorite", "CW1", tb =>
                {
                    tb.HasTrigger("UpdateFavoriteTrailCount");
                    tb.HasTrigger("UpdateUserRoleOnFavorite");
                });

            entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");
            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Trail).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.TrailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__TrailI__3587F3E0");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Favorite__UserID__367C1819");
        });

        modelBuilder.Entity<Trail>(entity =>
        {
            entity.HasKey(e => e.TrailId).HasName("PK__Trail__8F5236EE1EFDD0EC");

            entity.ToTable("Trail", "CW1");

            entity.Property(e => e.TrailId).HasColumnName("TrailID");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Distance)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TotalActivities).HasDefaultValue(0);
            entity.Property(e => e.TrailLevel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TrailName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__1788CCAC0672FD6D");

            entity.ToTable("users", "CW1", tb => tb.HasTrigger("UpdateUserRole"));

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FavoriteTrailCount).HasDefaultValue(0);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
