using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class RecetteService
    {
        private readonly AppDbContext _context;

        public RecetteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AjouterRecetteAsync(Recette recette)
        {
            _context.Recettes.Add(recette);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recette>> ObtenirRecettesAsync()
        {
            return await _context.Recettes
                .Include(r => r.Ingredients)
                .Include(r => r.Etapes)
                .Include(r => r.Commentaires)
                .Include(r => r.Categorie)
                .ToListAsync();
        }

        public async Task SupprimerRecetteAsync(int recetteId)
        {
            var recette = await _context.Recettes.FindAsync(recetteId);
            if (recette != null)
            {
                _context.Recettes.Remove(recette);
                await _context.SaveChangesAsync();
            }
        }
    }

}
