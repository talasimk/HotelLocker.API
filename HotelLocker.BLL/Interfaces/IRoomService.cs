using HotelLocker.Common.DTOs.RoomDTOs;
using HotelLocker.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDTO> CreateRoom(RoomCreateDTO roomCreateDTO, int userID);
        Task<RoomDTO> EditRoomAvailable(int roomId, int userId, bool isAvailable);
        Task<RoomDTO> EditRoom(RoomEditDTO roomDTO, int userId);
        List<RoomDTO> GetAllRooms(decimal minPrice,
                                                     decimal maxPrice,
                                                     int beds,
                                                     string sort,
                                                     bool sortDir,
                                                     RoomCategory roomCategory,
                                                     DateTime from,
                                                     int hotelId,
                                                     DateTime to);
    }
}
