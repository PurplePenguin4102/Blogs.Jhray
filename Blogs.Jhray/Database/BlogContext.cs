using Microsoft.EntityFrameworkCore;
using Blogs.Jhray.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Diagnostics;

namespace Blogs.Jhray.Database
{
    public partial class BlogContext : IdentityDbContext<BlogsJhrayUser>
    {
        public BlogContext(){}

        public BlogContext(DbContextOptions<BlogContext> options) : base(options){}

        public virtual DbSet<ArInternalMetadata> ArInternalMetadata { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<SchemaMigrations> SchemaMigrations { get; set; }
        public new virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<BlogsJhrayUser> BlogsJhrayUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArInternalMetadata>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("ar_internal_metadata_pkey");

                entity.ToTable("ar_internal_metadata");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("character varying");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.PublishDate).HasColumnName("publish_date");

                entity.Property(e => e.Published).HasColumnName("published");

                entity.Property(e => e.Subtitle)
                    .HasColumnName("subtitle")
                    .HasColumnType("character varying");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("character varying");

                entity.Property(e => e.TopPost)
                    .HasColumnName("top_post")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            modelBuilder.Entity<SchemaMigrations>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("schema_migrations_pkey");

                entity.ToTable("schema_migrations");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasDatabaseName("index_users_on_email")
                    .IsUnique();

                entity.HasIndex(e => e.ResetPasswordToken)
                    .HasDatabaseName("index_users_on_reset_password_token")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.CreatedAt).HasColumnName("created_at");

                entity.Property(e => e.CurrentSignInAt).HasColumnName("current_sign_in_at");

                entity.Property(e => e.CurrentSignInIp).HasColumnName("current_sign_in_ip");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("character varying")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.EncryptedPassword)
                    .IsRequired()
                    .HasColumnName("encrypted_password")
                    .HasColumnType("character varying")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.LastSignInAt).HasColumnName("last_sign_in_at");

                entity.Property(e => e.LastSignInIp).HasColumnName("last_sign_in_ip");

                entity.Property(e => e.RememberCreatedAt).HasColumnName("remember_created_at");

                entity.Property(e => e.ResetPasswordSentAt).HasColumnName("reset_password_sent_at");

                entity.Property(e => e.ResetPasswordToken)
                    .HasColumnName("reset_password_token")
                    .HasColumnType("character varying");

                entity.Property(e => e.SignInCount).HasColumnName("sign_in_count");

                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
