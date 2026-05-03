using E_CommerceApplication.Dtos.FavoritesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Interfaces.IFavoritesRepository
{
    public interface IFavoritesRepository
    {
        Task<List<AdminFavoritesDto>> GetFavoritesGroupUserId();
    }
}
