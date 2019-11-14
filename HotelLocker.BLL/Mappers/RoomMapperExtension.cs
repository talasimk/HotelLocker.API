using HotelLocker.Common.DTOs.RoomDTOs;
using HotelLocker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.BLL.Mappers
{
    public static class RoomMapperExtension
    {
        public static Room ToRoom(this RoomCreateDTO dto)
        {
            return new Room()
            {
                IsAvailable = dto.IsAvailable,
                NumberInHotel = dto.NumberInHotel,
                Category = dto.Category,
                Beds = dto.Beds,
                HotelId = dto.HotelId,
                Price = dto.Price
            };
        }

        public static RoomDTO ToRoomDTO(this Room room)
        {
            return new RoomDTO()
            {
                Id = room.Id,
                IsAvailable = room.IsAvailable,
                NumberInHotel = room.NumberInHotel,
                Category = room.Category,
                Beds = room.Beds,
                HotelId = room.HotelId,
                Price = room.Price,
                Hotel = room.Hotel.ToHotelDTO()
            };
        }
    }
}
