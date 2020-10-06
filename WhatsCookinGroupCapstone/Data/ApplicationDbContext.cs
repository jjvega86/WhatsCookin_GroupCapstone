using System;
using System.Collections.Generic;
using System.Text;
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
        public DbSet<Cook> Cook { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeTags> RecipeTags { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Tags> Tags { get; set; }
        //public DbSet<User> User { get; set; }

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