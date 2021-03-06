﻿using System;
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
               Name = "Cook",
               NormalizedName = "COOK"
           }
           );

           

            builder.Entity<Tags>().HasData(
                new Models.Tags
                {
                    TagsId = 1,
                    Name = "Vegan"
                },
                new Tags
                {
                    TagsId = 2,
                    Name = "Nut-Free"
                },
                new Tags
                {
                    TagsId = 3,
                    Name = "Dairy"
                },
                new Tags
                {
                    TagsId = 4,
                    Name = "Paleo"
                },
                new Tags
                {
                    TagsId = 5,
                    Name = "Pescatarian"
                });

            builder.Entity<RecipeTags>()
                .HasKey(bc => new { bc.RecipeId, bc.TagsId });
            builder.Entity<RecipeTags>()
                .HasOne(bc => bc.Recipe)
                .WithMany(b => b.RecipeTags)
                .HasForeignKey(bc => bc.RecipeId);
            builder.Entity<RecipeTags>()
                .HasOne(bc => bc.Tags)
                .WithMany(c => c.RecipeTags)
                .HasForeignKey(bc => bc.TagsId);
            

            builder.Entity<CookTag>()
                .HasKey(bc => new { bc.CookId, bc.TagsId });
            builder.Entity<CookTag>()
                .HasOne(bc => bc.Cook)
                .WithMany(b => b.CookTag)
                .HasForeignKey(bc => bc.CookId);
            builder.Entity<CookTag>()
                .HasOne(bc => bc.Tag)
                .WithMany(c => c.CookTag)
                .HasForeignKey(bc => bc.TagsId);




            //builder.Entity<CookSavedRecipes>()
            //   .HasKey(bc => new { bc.CookId, bc.RecipeId });
            //builder.Entity<CookSavedRecipes>()
            //    .HasOne(bc => bc.Cook)
            //    .WithMany(b => b.CookSavedRecipes)
            //    .HasForeignKey(bc => bc.CookId);
            //builder.Entity<CookSavedRecipes>()
            //    .HasOne(bc => bc.Recipe)
            //    .WithMany(c => c.CookSavedRecipes)
            //    .HasForeignKey(bc => bc.RecipeId);

        }
        

        public DbSet<Cook> Cook { get; set; }
        public DbSet<Followers> Followers { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<RecipeTags> RecipeTags { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<CookTag> CookTags { get; set; }
        public DbSet<CookSavedRecipes> CookSavedRecipes { get; set; }
        public DbSet<RecipeEdits> RecipeEdits { get; set; }
       

        
    }

    

}