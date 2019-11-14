using HotelLocker.BLL.Interfaces;
using HotelLocker.BLL.Mappers;
using HotelLocker.Common.DTOs.UserBlackListDTOs;
using HotelLocker.Common.Exceptions;
using HotelLocker.Common.Validators;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Services
{
    public class UserBlackListService : IUserBlackListService
    {
        private readonly IUnitOfWork context;

        public UserBlackListService(IUnitOfWork context)
        {
            this.context = context;
        }

        public async Task<UserBlackListDTO> CreateUserBlackList(UserBLCreateDTO  userBLCreateDTO, int userId)
        {
            ValidationResults result = ModelValidator.IsValid(userBLCreateDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            HotelAdmin hotelAdmin = context.HotelAdmins.Get(userId);
            if (hotelAdmin == null)
                throw new NotFoundException("No such hotel");

            Guest guest = context.Guests.Get(userBLCreateDTO.GuestId);
            if (guest == null)
                throw new NotFoundException("No such user");

            int hotelId = hotelAdmin.Hotel.Id;
            var countInDB = context.UserBlackLists
                .GetAll()
                .Where(x => x.HotelId == hotelId 
                            && x.GuestId == userBLCreateDTO.GuestId)
                .Count();
            if(countInDB != 0)
                throw new ValidationException("This user is already in a black list");

            var blackList = userBLCreateDTO.ToUserBlackList();
            blackList.HotelId = hotelId;
            context.UserBlackLists.Create(blackList);
            await context.SaveAsync();
            return blackList.ToUserBlackListDTO();
        }

        public async Task<UserBlackListDTO> EditBlackList(UserBLEditDTO userBLEditDTO, int userId)
        {
            ValidationResults result = ModelValidator.IsValid(userBLEditDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            HotelAdmin hotelAdmin = context.HotelAdmins.Get(userId);
            if (hotelAdmin == null)
                throw new NotFoundException("No such hotel");

            Guest guest = context.Guests.Get(userBLEditDTO.GuestId);
            if (guest == null)
                throw new NotFoundException("No such user");
            int hotelId = hotelAdmin.Hotel.Id;
            var userBlackList = context.UserBlackLists.GetAll()
                .Where(x => x.HotelId == hotelId
                            && x.GuestId == userBLEditDTO.GuestId).FirstOrDefault();
            if (userBlackList == null)
                throw new NotFoundException("No such user in the hotel's black list");
            userBlackList.Reason = userBLEditDTO.Reason;
            context.UserBlackLists.Update(userBlackList);
            await context.SaveAsync();
            return userBlackList.ToUserBlackListDTO();
        }

        public async Task<UserBlackListDTO> DeleteBlackList(int questId, int userId)
        {
            HotelAdmin hotelAdmin = context.HotelAdmins.Get(userId);
            if (hotelAdmin == null)
                throw new NotFoundException("No such hotel");

            int hotelId = hotelAdmin.Hotel.Id;
            var userBlackList = context.UserBlackLists.GetAll()
                .Where(x => x.HotelId == hotelId
                            && x.GuestId == questId).FirstOrDefault();
            if (userBlackList == null)
                throw new NotFoundException("No such user in the hotel's black list");
            
            context.UserBlackLists.Delete(userBlackList);
            await context.SaveAsync();
            return userBlackList.ToUserBlackListDTO();
        }

        public List<UserBlackListDTO> GetBlackLists(int userId)
        {
            HotelAdmin hotelAdmin = context.HotelAdmins.Get(userId);
            if (hotelAdmin == null)
                throw new NotFoundException("No such hotel");

            int hotelId = hotelAdmin.Hotel.Id;
            var userBlackList = context.UserBlackLists.GetAll()
                .Where(x => x.HotelId == hotelId).Select(x => x.ToUserBlackListDTO()).ToList();
            return userBlackList;
        }

        public List<UserBlackListDTO> GetAllBlackLists()
        {
            return context.UserBlackLists
                .GetAll()
                .Select(x => x.ToUserBlackListDTO())
                .ToList();
        }
    }
}
