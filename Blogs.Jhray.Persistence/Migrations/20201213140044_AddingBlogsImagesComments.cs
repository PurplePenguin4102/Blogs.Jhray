﻿using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Blogs.Jhray.Persistence.Migrations
{
    public partial class AddingBlogsImagesComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<long>(
                name: "BlogId",
                table: "posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DirectLink",
                table: "posts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    LeadIn = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    HomeUrl = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: true),
                    PostsId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ToolTip = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    DirectUrl = table.Column<string>(type: "text", nullable: true),
                    FileLocation = table.Column<string>(type: "text", nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileCheckSum = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_BlogId",
                table: "posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostsId",
                table: "Comments",
                column: "PostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_Blogs_BlogId",
                table: "posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_Blogs_BlogId",
                table: "posts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_posts_BlogId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "DirectLink",
                table: "posts");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.ArInternalMetadata", b =>
            {
                b.Property<string>("Key")
                    .HasColumnType("character varying")
                    .HasColumnName("key");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                b.Property<string>("Value")
                    .HasColumnType("character varying")
                    .HasColumnName("value");

                b.HasKey("Key")
                    .HasName("ar_internal_metadata_pkey");

                b.ToTable("ar_internal_metadata");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Blog", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .UseIdentityByDefaultColumn();

                b.Property<string>("Author")
                    .HasColumnType("text");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("HomeUrl")
                    .HasColumnType("text");

                b.Property<string>("LeadIn")
                    .HasColumnType("text");

                b.Property<string>("Title")
                    .HasColumnType("text");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp without time zone");

                b.HasKey("Id");

                b.ToTable("Blogs");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.BlogsJhrayUser", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("text");

                b.Property<int>("AccessFailedCount")
                    .HasColumnType("integer");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text");

                b.Property<string>("Email")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<bool>("EmailConfirmed")
                    .HasColumnType("boolean");

                b.Property<bool>("LockoutEnabled")
                    .HasColumnType("boolean");

                b.Property<DateTimeOffset?>("LockoutEnd")
                    .HasColumnType("timestamp with time zone");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<string>("PasswordHash")
                    .HasColumnType("text");

                b.Property<string>("PhoneNumber")
                    .HasColumnType("text");

                b.Property<bool>("PhoneNumberConfirmed")
                    .HasColumnType("boolean");

                b.Property<string>("SecurityStamp")
                    .HasColumnType("text");

                b.Property<bool>("TwoFactorEnabled")
                    .HasColumnType("boolean");

                b.Property<string>("UserName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasDatabaseName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasDatabaseName("UserNameIndex");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Comment", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .UseIdentityByDefaultColumn();

                b.Property<string>("Content")
                    .HasColumnType("text");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp without time zone");

                b.Property<long>("PostsId")
                    .HasColumnType("bigint");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("UserId")
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("PostsId");

                b.HasIndex("UserId");

                b.ToTable("Comments");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Image", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .UseIdentityByDefaultColumn();

                b.Property<string>("Author")
                    .HasColumnType("text");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp without time zone");

                b.Property<string>("DirectUrl")
                    .HasColumnType("text");

                b.Property<string>("FileCheckSum")
                    .HasColumnType("text");

                b.Property<string>("FileLocation")
                    .HasColumnType("text");

                b.Property<long>("FileSize")
                    .HasColumnType("bigint");

                b.Property<string>("Name")
                    .HasColumnType("text");

                b.Property<string>("ToolTip")
                    .HasColumnType("text");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp without time zone");

                b.HasKey("Id");

                b.ToTable("Images");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Posts", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("id")
                    .UseIdentityByDefaultColumn();

                b.Property<long>("BlogId")
                    .HasColumnType("bigint");

                b.Property<string>("Content")
                    .HasColumnType("character varying")
                    .HasColumnName("content");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                b.Property<string>("DirectLink")
                    .HasColumnType("text");

                b.Property<DateTime?>("PublishDate")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("publish_date");

                b.Property<bool?>("Published")
                    .HasColumnType("boolean")
                    .HasColumnName("published");

                b.Property<string>("Subtitle")
                    .HasColumnType("character varying")
                    .HasColumnName("subtitle");

                b.Property<string>("Title")
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                b.Property<bool?>("TopPost")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasColumnName("top_post")
                    .HasDefaultValueSql("false");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                b.HasKey("Id");

                b.HasIndex("BlogId");

                b.ToTable("posts");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.SchemaMigrations", b =>
            {
                b.Property<string>("Version")
                    .HasColumnType("character varying")
                    .HasColumnName("version");

                b.HasKey("Version")
                    .HasName("schema_migrations_pkey");

                b.ToTable("schema_migrations");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Users", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("bigint")
                    .HasColumnName("id")
                    .UseIdentityByDefaultColumn();

                b.Property<bool?>("Admin")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("boolean")
                    .HasColumnName("admin")
                    .HasDefaultValueSql("false");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at");

                b.Property<DateTime?>("CurrentSignInAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("current_sign_in_at");

                b.Property<IPAddress>("CurrentSignInIp")
                    .HasColumnType("inet")
                    .HasColumnName("current_sign_in_ip");

                b.Property<string>("Email")
                    .IsRequired()
                    .ValueGeneratedOnAdd()
                    .HasColumnType("character varying")
                    .HasColumnName("email")
                    .HasDefaultValueSql("''::character varying");

                b.Property<string>("EncryptedPassword")
                    .IsRequired()
                    .ValueGeneratedOnAdd()
                    .HasColumnType("character varying")
                    .HasColumnName("encrypted_password")
                    .HasDefaultValueSql("''::character varying");

                b.Property<DateTime?>("LastSignInAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("last_sign_in_at");

                b.Property<IPAddress>("LastSignInIp")
                    .HasColumnType("inet")
                    .HasColumnName("last_sign_in_ip");

                b.Property<DateTime?>("RememberCreatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("remember_created_at");

                b.Property<DateTime?>("ResetPasswordSentAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("reset_password_sent_at");

                b.Property<string>("ResetPasswordToken")
                    .HasColumnType("character varying")
                    .HasColumnName("reset_password_token");

                b.Property<int>("SignInCount")
                    .HasColumnType("integer")
                    .HasColumnName("sign_in_count");

                b.Property<DateTime>("UpdatedAt")
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at");

                b.HasKey("Id");

                b.HasIndex("Email")
                    .IsUnique()
                    .HasDatabaseName("index_users_on_email");

                b.HasIndex("ResetPasswordToken")
                    .IsUnique()
                    .HasDatabaseName("index_users_on_reset_password_token");

                b.ToTable("users");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
            {
                b.Property<string>("Id")
                    .HasColumnType("text");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken()
                    .HasColumnType("text");

                b.Property<string>("Name")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256)
                    .HasColumnType("character varying(256)");

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasDatabaseName("RoleNameIndex");

                b.ToTable("AspNetRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .UseIdentityByDefaultColumn();

                b.Property<string>("ClaimType")
                    .HasColumnType("text");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text");

                b.Property<string>("RoleId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .UseIdentityByDefaultColumn();

                b.Property<string>("ClaimType")
                    .HasColumnType("text");

                b.Property<string>("ClaimValue")
                    .HasColumnType("text");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("character varying(128)");

                b.Property<string>("ProviderKey")
                    .HasMaxLength(128)
                    .HasColumnType("character varying(128)");

                b.Property<string>("ProviderDisplayName")
                    .HasColumnType("text");

                b.Property<string>("UserId")
                    .IsRequired()
                    .HasColumnType("text");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("text");

                b.Property<string>("RoleId")
                    .HasColumnType("text");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.Property<string>("UserId")
                    .HasColumnType("text");

                b.Property<string>("LoginProvider")
                    .HasMaxLength(128)
                    .HasColumnType("character varying(128)");

                b.Property<string>("Name")
                    .HasMaxLength(128)
                    .HasColumnType("character varying(128)");

                b.Property<string>("Value")
                    .HasColumnType("text");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Comment", b =>
            {
                b.HasOne("Blogs.Jhray.Database.Entities.Posts", "Post")
                    .WithMany("Comments")
                    .HasForeignKey("PostsId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Blogs.Jhray.Database.Entities.BlogsJhrayUser", "User")
                    .WithMany()
                    .HasForeignKey("UserId");

                b.Navigation("Post");

                b.Navigation("User");
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Posts", b =>
            {
                b.HasOne("Blogs.Jhray.Database.Entities.Blog", "Blog")
                    .WithMany()
                    .HasForeignKey("BlogId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Blog");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
            {
                b.HasOne("Blogs.Jhray.Database.Entities.BlogsJhrayUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
            {
                b.HasOne("Blogs.Jhray.Database.Entities.BlogsJhrayUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Blogs.Jhray.Database.Entities.BlogsJhrayUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
            {
                b.HasOne("Blogs.Jhray.Database.Entities.BlogsJhrayUser", null)
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Blogs.Jhray.Database.Entities.Posts", b =>
            {
                b.Navigation("Comments");
            });
        }
    }
}
