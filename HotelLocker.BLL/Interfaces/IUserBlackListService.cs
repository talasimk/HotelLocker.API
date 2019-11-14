using HotelLocker.Common.DTOs.UserBlackListDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Interfaces
{
    public interface IUserBlackListService
    {
        Task<UserBlackListDTO> CreateUserBlackList(UserBLCreateDTO userBLCreateDTO, int userId);
        Task<UserBlackListDTO> EditBlackList(UserBLEditDTO userBLEditDTO, int userId);
        Task<UserBlackListDTO> DeleteBlackList(int questId, int userId);
        List<UserBlackListDTO> GetBlackLists(int userId);
        List<UserBlackListDTO> GetAllBlackLists();
    }
}
