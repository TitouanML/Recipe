using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Recette> Recettes { get; set; } = new List<Recette>();
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
    }

    public class Etape
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Commentaire
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class Recette
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public TimeSpan TempsPreparation { get; set; }
        public TimeSpan TempsCuisson { get; set; }
        public string Difficulte { get; set; }

        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Etape> Etapes { get; set; } = new List<Etape>();
        public ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();
    }

}
