using HotelLocker.Common.DTOs;
using HotelLocker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelLocker.BLL.Mappers
{
    public static class RoomAccessMapperExtension
    {
        public static RoomAccessDTO ToRoomAccessDTO(this RoomAccess room)
        {
            return new RoomAccessDTO()
            {
                Id = room.Id,
                RoomId = room.RoomId,
                DateTime = room.DateTime,
                IsOpen = room.IsOpen,
                UserId = room.UserId,
                Reason = room.ReasonOfVisit
            };
        }
    }
}
