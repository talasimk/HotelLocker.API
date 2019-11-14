using HotelLocker.Common.DTOs.UserBlackListDTOs;
using HotelLocker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.BLL.Mappers
{
    public static class UserBlackListMapperExtension
    {
        public static UserBlackList ToUserBlackList(this UserBLCreateDTO userBLCreateDTO)
        {
            return new UserBlackList()
            {
                GuestId = userBLCreateDTO.GuestId,
                Reason = userBLCreateDTO.Reason
            };
        }

        public static UserBlackListDTO ToUserBlackListDTO(this UserBlackList userBlackList)
        {
            return new UserBlackListDTO()
            {
                GuestId = userBlackList.GuestId,
                GuestName = userBlackList.Guest.FirstName + " " + userBlackList.Guest.LastName,
                HotelId = userBlackList.HotelId,
                HotelName = userBlackList.Hotel.Name,
                Reason = userBlackList.Reason
            };
        }

    }
}
