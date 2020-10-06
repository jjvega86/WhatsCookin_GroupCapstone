using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WhatsCookinGroupCapstone.Models;

namespace WhatsCookinGroupCapstone.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            }
            );
        }

        public DbSet<Cook> Cook { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeTags> RecipeTags { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Tags> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RecipeTags>()
                .HasKey(bc => new { bc.RecipeId, bc.TagsId });
            modelBuilder.Entity<RecipeTags>()
                .HasOne(bc => bc.Recipe)
                .WithMany(b => b.RecipeTags)
                .HasForeignKey(bc => bc.RecipeId);
            modelBuilder.Entity<RecipeTags>()
                .HasOne(bc => bc.Tags)
                .WithMany(c => c.RecipeTags)
                .HasForeignKey(bc => bc.TagsId);
        }
    }

    

}