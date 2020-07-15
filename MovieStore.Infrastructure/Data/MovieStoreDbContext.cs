﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

// Whenever you change your model/entity always make sure you add new Migration
// With CF approach never change the Database directly, always change the entities using DataAnnotations or Fluent API and add new migration  then update database
// IN EF we have 2 ways to create our entities and model our database using Code-First approach
// 1. Data Annotations which is nothing but bunch of C# attributes that we can use
// 2. Fluent API is more syntax and more powerful and usually uses lambdas
// Combine both DataAnnotations and Fluent API

namespace MovieStore.Infrastructure.Data
{
    // Steps:
    // Install all the EF Core libraries using Nuget package Manger
    // Create a class that inherits from DbContext class
    // DbContext kinda represents your Database
    // Create a connection string which EF is gonna use to create/access the Database, should include server name, Database Name and any credentials
    // Create an Entity Class, Genre table
    // Make sure to add your Entity class as a DbSet property inside your DbContext class
    // In EF we have thing called Migrations, we are gonna use Migrations to create our Database
    // We need to add Migration commands to Create the tables, database etc
    // When running Migration commands, make sure the project selected is the one project which has DbContext class
    // Make sure you add reference for library that has DbContext to your startup project, in this case MVC
    // Tell MVC project that we are using Entity Framework in startup file
    // Add DbContext options as constructor parameter for our DbContext
    // Add-Migration MigrationName, make sure your migration names are meaningful, don't use names such as xyz, abc, migration1 like that
    // Make sure you have Migrations folder created, and check the created migration file
    // After Creating Migration file and verifying it we need to use update-database command
    public class MovieStoreDbContext : DbContext
    {
        // Multiple DbSets, all the DbSets you create are gonna reside in your DbContext class
        // This DbSet, is gonna represent out Table based on Entity class which is Genre in this case
        public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }
        public void ConfigureRole(EntityTypeBuilder<Role> modelBuilder)
        {
            modelBuilder.ToTable("Role");
            modelBuilder.HasKey(r => r.Id);
            modelBuilder.Property(r => r.Name).HasMaxLength(20);
        }
        public void ConfigureUserRole(EntityTypeBuilder<UserRole> modelBuilder)
        {
            modelBuilder.ToTable("UserRole");
            modelBuilder.HasKey(ur => new { ur.UserId,ur.RoleId});
            modelBuilder.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(mc => mc.RoleId);
            modelBuilder.HasOne(ur => ur.User).WithMany(r => r.UserRoles).HasForeignKey(mc => mc.UserId);

        }
        public void ConfigureFavorite(EntityTypeBuilder<Favorite> modelBuilder)
        {
            modelBuilder.ToTable("Favorite");
            modelBuilder.HasKey(f => f.Id);
        }
        private void ConfigurePurchase(EntityTypeBuilder<Purchase> modelBuilder)
        {
            modelBuilder.ToTable("Purchase");
            modelBuilder.HasKey(p => p.Id);
            modelBuilder.Property(p=>p.PurchaseDateTime).HasDefaultValueSql("getdate()");
            modelBuilder.HasIndex(p => p.PurchaseNumber).IsUnique();
            modelBuilder.Property(p => p.TotalPrice).HasColumnType("decimal(5,2)");


        }
        private void ConfigureCast(EntityTypeBuilder<Cast> modelBuilder)
        {
            modelBuilder.ToTable("Cast");
            modelBuilder.HasKey(n => n.Id);
            modelBuilder.Property(n => n.Name).HasMaxLength(128);
            modelBuilder.Property(n => n.Gender).HasMaxLength(4096);
            modelBuilder.Property(n => n.TmdbUrl).HasMaxLength(4096);
            modelBuilder.Property(n => n.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> modelBuilder)
        {
            modelBuilder.ToTable("MovieCast");
            modelBuilder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character});
            modelBuilder.Property(mc => mc.Character).HasMaxLength(450);
            modelBuilder.HasOne(mc => mc.Movie).WithMany(n => n.MovieCasts).HasForeignKey(mc => mc.MovieId);
            modelBuilder.HasOne(mc => mc.Cast).WithMany(n => n.MovieCasts).HasForeignKey(mc => mc.CastId);
        }
        private void ConfigureUser(EntityTypeBuilder<User> modelBuilder)
        { 
            modelBuilder.ToTable("User");
            modelBuilder.HasKey(u => u.Id);
            modelBuilder.Property(u => u.FirstName).HasMaxLength(256);
            modelBuilder.Property(u => u.LastName).HasMaxLength(256);
            modelBuilder.Property(u => u.Email).HasMaxLength(256);
            modelBuilder.Property(u => u.DateOfBirth).HasDefaultValueSql("getdate()");
            modelBuilder.Property(u => u.LockoutEndDate).HasDefaultValueSql("getdate()");
            modelBuilder.Property(u => u.LastLoginDateTime).HasDefaultValueSql("getdate()");
            modelBuilder.Property(u => u.PhoneNumber).HasMaxLength(4096);


        }
        private void ConfigureReview(EntityTypeBuilder<Review> modelBuilder)
        {
            modelBuilder.ToTable("Review");
            modelBuilder.HasKey(r => new { r.MovieId,r.UserId});
            modelBuilder.Property(r => r.Rating).IsRequired().HasColumnType("decimal(3, 2)");
            modelBuilder.Property(r => r.ReviewText).HasMaxLength(4096);

        }
        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> modelBuilder)
        {
            modelBuilder.ToTable("MovieGenre");
            modelBuilder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            modelBuilder.HasOne(mg => mg.Genre).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.GenreId);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> modelBuilder)
        {
            modelBuilder.ToTable("Trailer");
            modelBuilder.HasKey(t => t.Id);
            modelBuilder.Property(t => t.Name).HasMaxLength(2084);
            modelBuilder.Property(t => t.TrailerUrl).HasMaxLength(2084);
        }
            private void ConfigureMovie(EntityTypeBuilder<Movie> modelBuilder)
        {
            //we can use Fluent API Configurations to model our database
            modelBuilder.ToTable("Movie");
            modelBuilder.HasKey(m => m.Id);
            modelBuilder.Property(m => m.Title).IsRequired().HasMaxLength(256);
            modelBuilder.Property(m => m.Overview).HasMaxLength(4096);
            modelBuilder.Property(m => m.Tagline).HasMaxLength(512);
            modelBuilder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.PosterUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            modelBuilder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            modelBuilder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            modelBuilder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            //we want to show rating property in our entity so could show in UI
            //we dont want to create a column called Rating
            modelBuilder.Ignore(m => m.Rating);
        }
  
    }
}

