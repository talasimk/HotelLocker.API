using HotelLocker.Common.DTOs.HotelsDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Interfaces
{
    public interface IHotelService
    {
        Task<HotelDTO> CreateHotel(HotelCreateDTO hotelCreateDTO);
        Task<HotelDTO> EditHotel(HotelDTO hotelDTO, int userId);
        List<HotelDTO> GetAllHotels(string name, string sort, bool sortDir,int minStars, int maxStars,  string country, string city, string address);
    }
}
