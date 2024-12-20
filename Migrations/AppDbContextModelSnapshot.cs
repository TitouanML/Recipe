﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipe;

#nullable disable

namespace Recipe.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("IngredientRecette", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecetteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IngredientsId", "RecetteId");

                    b.HasIndex("RecetteId");

                    b.ToTable("IngredientRecette");
                });

            modelBuilder.Entity("Recipe.Categorie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Recipe.Commentaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RecetteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecetteId");

                    b.ToTable("Commentaires");
                });

            modelBuilder.Entity("Recipe.Etape", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RecetteId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RecetteId");

                    b.ToTable("Etapes");
                });

            modelBuilder.Entity("Recipe.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Recipe.Recette", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategorieId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Difficulte")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("TempsCuisson")
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("TempsPreparation")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategorieId");

                    b.ToTable("Recettes");
                });

            modelBuilder.Entity("IngredientRecette", b =>
                {
                    b.HasOne("Recipe.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipe.Recette", null)
                        .WithMany()
                        .HasForeignKey("RecetteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipe.Commentaire", b =>
                {
                    b.HasOne("Recipe.Recette", null)
                        .WithMany("Commentaires")
                        .HasForeignKey("RecetteId");
                });

            modelBuilder.Entity("Recipe.Etape", b =>
                {
                    b.HasOne("Recipe.Recette", null)
                        .WithMany("Etapes")
                        .HasForeignKey("RecetteId");
                });

            modelBuilder.Entity("Recipe.Recette", b =>
                {
                    b.HasOne("Recipe.Categorie", "Categorie")
                        .WithMany("Recettes")
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");
                });

            modelBuilder.Entity("Recipe.Categorie", b =>
                {
                    b.Navigation("Recettes");
                });

            modelBuilder.Entity("Recipe.Recette", b =>
                {
                    b.Navigation("Commentaires");

                    b.Navigation("Etapes");
                });
#pragma warning restore 612, 618
        }
    }
}
