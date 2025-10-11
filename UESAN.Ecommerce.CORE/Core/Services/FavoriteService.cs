using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UESAN.Ecommerce.CORE.Core.DTOs;
using UESAN.Ecommerce.CORE.Core.Entities;
using UESAN.Ecommerce.CORE.Core.Interfaces;

namespace UESAN.Ecommerce.CORE.Core.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;
        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        // GET FAVORITES
        public async Task<IEnumerable<FavoriteDetailDTO>> GetFavorites()
        {
            var favorites = await _favoriteRepository.GetAllFavorites();
            var favoriteDTOs = new List<FavoriteDetailDTO>();
            foreach (var favorite in favorites)
            {
                var favoriteDTO = new FavoriteDetailDTO();
                favoriteDTO.Id = favorite.Id;

                var favoriteUserDTO = new UserFavoriteDTO();
                favoriteUserDTO.Id = favorite.Id;
                favoriteUserDTO.FirstName = favorite.User.FirstName;
                favoriteUserDTO.LastName = favorite.User.LastName;

                favoriteDTO.User = favoriteUserDTO;

                var productFavoriteDTO = new ProductFavoriteDTO();
                productFavoriteDTO.Id = favorite.Id;
                productFavoriteDTO.Description = favorite.Product.Description;
                productFavoriteDTO.Price = favorite.Product.Price;
                productFavoriteDTO.Stock = favorite.Product.Stock;

                favoriteDTO.Product = productFavoriteDTO;

                favoriteDTOs.Add(favoriteDTO);

            }
            return favoriteDTOs;
        }

        // GET FAVORITES BY ID
        public async Task<FavoriteDetailDTO> GetFavoriteById(int id)
        {
            var favorite = await _favoriteRepository.GetFavoriteById(id);
            if (favorite == null) return null;

            var favoriteDTO = new FavoriteDetailDTO();
            favoriteDTO.Id = favorite.Id;

            var favoriteUserDTO = new UserFavoriteDTO();
            favoriteUserDTO.Id = favorite.User.Id;
            favoriteUserDTO.FirstName = favorite.User.FirstName;
            favoriteUserDTO.LastName = favorite.User.LastName;
            favoriteDTO.User = favoriteUserDTO;

            var productFavoriteDTO = new ProductFavoriteDTO();
            productFavoriteDTO.Id = favorite.Product.Id;
            productFavoriteDTO.Description = favorite.Product.Description;
            productFavoriteDTO.Price = favorite.Product.Price;
            productFavoriteDTO.Stock = favorite.Product.Stock;
            favoriteDTO.Product = productFavoriteDTO;

            return favoriteDTO;
        }


        // INSERT FAVORITE
        public async Task<int> InsertFavorite(FavoriteCreateDTO favoriteCreateDTO)
        {
            var favorite = new Favorite();
            favorite.UserId = favoriteCreateDTO.UserId;
            favorite.ProductId = favoriteCreateDTO.ProductId;
            favorite.CreatedAt = DateTime.Now;
            var newFavoriteId = await _favoriteRepository.InsertFavorite(favorite);
            return newFavoriteId;
        }

        // DELETE FAVORITE
        public async Task DeleteFavorite(int id)
        {
            await _favoriteRepository.DeleteFavorite(id);
        }

    }
}
