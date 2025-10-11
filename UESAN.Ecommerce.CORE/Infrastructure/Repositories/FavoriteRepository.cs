using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;
using UESAN.Ecommerce.CORE.Infrastructure.Data;

namespace UESAN.Ecommerce.CORE.Infrastructure.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly StoreDbContext _context;
        public FavoriteRepository(StoreDbContext context)
        {
            _context = context;
        }

        //GET Favorites 
        public async Task<IEnumerable<Favorite>> GetAllFavorites()
        {
            return await _context.Favorite.ToListAsync();
        }

        //GET Favorite by ID
        public async Task<Favorite> GetFavoriteById(int id)
        {
            return await _context.Favorite.
                Include(f => f.Product).
                Include(f => f.User).
                Where(f => f.Id == id).
                FirstOrDefaultAsync();
        }

        //INSERT Favorite
        public async Task<int> InsertFavorite(Favorite favorite)
        {
            await _context.Favorite.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return favorite.Id;
        }

        //DELETE Favorite
        public async Task DeleteFavorite(int id)
        {
            var favorite = await _context.Favorite.FindAsync(id);
            if (favorite != null)
            {
                _context.Favorite.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }





    }
}
