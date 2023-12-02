using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SOCIAL_NETWORK_API.Models;

public partial class SocialNetworkContext : DbContext
{
    public SocialNetworkContext()
    {
    }

    public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Network> Networks { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostLiked> PostLikeds { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Network>(entity =>
        {
            entity.HasKey(e => e.FriendshipId).HasName("PRIMARY");

            entity.ToTable("network");

            entity.Property(e => e.FriendshipId)
                .HasColumnType("int(11)")
                .HasColumnName("friendship_id");
            entity.Property(e => e.RelationType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'friendship'")
                .HasColumnName("relation_type");
            entity.Property(e => e.User1Id)
                .HasColumnType("int(11)")
                .HasColumnName("user1_id");
            entity.Property(e => e.User2Id)
                .HasColumnType("int(11)")
                .HasColumnName("user2_id");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PRIMARY");

            entity.ToTable("post");

            entity.Property(e => e.PostId)
                .HasColumnType("int(11)")
                .HasColumnName("post_id");
            entity.Property(e => e.PostedOn)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("postedOn");
            entity.Property(e => e.Text)
                .HasMaxLength(50)
                .HasColumnName("text");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Visibility)
                .HasMaxLength(50)
                .HasDefaultValueSql("'private'")
                .HasColumnName("visibility");
        });

        modelBuilder.Entity<PostLiked>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("post_liked");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.LikeStatus).HasColumnName("like_status");
            entity.Property(e => e.PostLiked1)
                .HasColumnType("int(11)")
                .HasColumnName("post_liked");
            entity.Property(e => e.UserLike)
                .HasColumnType("int(11)")
                .HasColumnName("user_like");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .HasColumnName("fullName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
