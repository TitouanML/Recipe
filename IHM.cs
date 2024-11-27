using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    class IHM
    {
        public static async Task Start()
        {
            using var context = new AppDbContext();

            // Debug BDD
            Console.WriteLine("Création de la base de données si elle n'existe pas...");
            await context.Database.EnsureCreatedAsync();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n--- Menu Principal ---");
                Console.WriteLine("1. Ajouter une recette");
                Console.WriteLine("2. Afficher les recettes");
                Console.WriteLine("3. Supprimer une recette");
                Console.WriteLine("4. Quitter");
                Console.Write("Choix : ");

                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        Console.Clear();
                        await AjouterRecette(context);
                        break;
                    case "2":
                        Console.Clear();
                        AfficherRecettes(context);
                        break;
                    case "3":
                        Console.Clear();
                        await SupprimerRecette(context);
                        break;
                    case "4":
                        Console.WriteLine("Au revoir !");
                        return;
                    default:
                        Console.WriteLine("Choix invalide, réessayez.");
                        break;
                }
            }
        }

        static async Task AjouterRecette(AppDbContext context)
        {
            // La logique d'ajout d'une recette
            Console.Clear();
            Console.WriteLine("--- Ajouter une recette ---");

            Console.Write("Nom de la recette : ");
            var nom = Console.ReadLine();

            int tempsPrep;
            while (true)
            {
                Console.Write("Temps de préparation (en minutes) : ");
                if (int.TryParse(Console.ReadLine(), out tempsPrep) && tempsPrep >= 0)
                    break;
                Console.WriteLine("Veuillez entrer un nombre valide pour le temps de préparation.");
            }

            int tempsCuisson;
            while (true)
            {
                Console.Write("Temps de cuisson (en minutes) : ");
                if (int.TryParse(Console.ReadLine(), out tempsCuisson) && tempsCuisson >= 0)
                    break;
                Console.WriteLine("Veuillez entrer un nombre valide pour le temps de cuisson.");
            }

            string difficulte;
            while (true)
            {
                Console.Write("Difficulté (Facile/Moyen/Difficile) : ");
                difficulte = Console.ReadLine();
                if (new[] { "Facile", "Moyen", "Difficile" }.Contains(difficulte, StringComparer.OrdinalIgnoreCase))
                    break;
                Console.WriteLine("Veuillez entrer une difficulté valide (Facile, Moyen, Difficile).");
            }

            Console.Write("Nom de la catégorie : ");
            var categorieNom = Console.ReadLine();
            var categorie = context.Categories.FirstOrDefault(c => c.Nom == categorieNom)
                            ?? new Categorie { Nom = categorieNom };

            var recette = new Recette
            {
                Nom = nom,
                TempsPreparation = TimeSpan.FromMinutes(tempsPrep),
                TempsCuisson = TimeSpan.FromMinutes(tempsCuisson),
                Difficulte = difficulte,
                Categorie = categorie
            };

            Console.WriteLine("Ajoutez des ingrédients (séparés par une virgule) : ");
            var ingredientsInput = Console.ReadLine();
            var ingredients = ingredientsInput?.Split(',')
                                   .Select(i => i.Trim())
                                   .Where(i => !string.IsNullOrWhiteSpace(i))
                                   .Select(i => new Ingredient { Nom = i })
                                   .ToList();
            recette.Ingredients = ingredients;

            context.Recettes.Add(recette);
            await context.SaveChangesAsync();

            Console.WriteLine("Recette ajoutée avec succès !");
            Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
        }

        static void AfficherRecettes(AppDbContext context)
        {
            Console.Clear();
            Console.WriteLine("--- Liste des recettes ---");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Choix : ");

            var retour = Console.ReadLine();
            if (retour == "0") return;

            var recettes = context.Recettes
                .Include(r => r.Categorie)
                .Include(r => r.Ingredients)
                .ToList();

            if (!recettes.Any())
            {
                Console.WriteLine("Aucune recette disponible.");
            }
            else
            {
                foreach (var recette in recettes)
                {
                    Console.WriteLine($"\n- {recette.Nom} (Difficulté : {recette.Difficulte})");
                    Console.WriteLine($"  Préparation : {recette.TempsPreparation.TotalMinutes} min, Cuisson : {recette.TempsCuisson.TotalMinutes} min");
                    Console.WriteLine($"  Catégorie : {recette.Categorie?.Nom}");
                    Console.WriteLine($"  Ingrédients : {string.Join(", ", recette.Ingredients.Select(i => i.Nom))}");
                }
            }

            Console.WriteLine("\nAppuyez sur une touche pour revenir au menu...");
            Console.ReadKey();
        }

        static async Task SupprimerRecette(AppDbContext context)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Supprimer une recette ---");
                Console.WriteLine("0. Retour au menu principal");
                Console.Write("Choix : ");

                var retour = Console.ReadLine();
                if (retour == "0") return;

                Console.WriteLine("Recettes disponibles :");

                var recettes = context.Recettes.ToList();
                if (!recettes.Any())
                {
                    Console.WriteLine("Aucune recette n'est disponible.");
                    Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                    Console.ReadKey();
                    return;
                }

                foreach (var recette in recettes)
                {
                    Console.WriteLine($"{recette.Id} - {recette.Nom}");
                }

                int id;
                while (true)
                {
                    Console.Write("ID de la recette à supprimer : ");
                    if (int.TryParse(Console.ReadLine(), out id) && id > 0)
                        break;
                    Console.WriteLine("Veuillez entrer un ID valide (nombre entier positif).");
                }

                var recetteToDelete = await context.Recettes.FindAsync(id);
                if (recetteToDelete != null)
                {
                    context.Recettes.Remove(recetteToDelete);
                    await context.SaveChangesAsync();
                    Console.WriteLine("Recette supprimée avec succès !");
                }
                else
                {
                    Console.WriteLine("Recette introuvable.");
                }

                Console.WriteLine("Appuyez sur une touche pour revenir au menu...");
                Console.ReadKey();
                return;
            }
        }

        static async Task Main(string[] args)
        {
            await Start();
        }
    }
}
