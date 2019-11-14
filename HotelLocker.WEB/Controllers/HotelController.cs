using System.Collections.Generic;
using System.Threading.Tasks;
using HotelLocker.BLL.Extensions;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs.HotelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelLocker.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;

        public HotelController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpGet]
        public List<HotelDTO> GetAllHotels([FromQuery(Name = "name")] string name,
                                                 [FromQuery(Name = "sort")] string sort,
                                                 [FromQuery(Name = "sortDir")] bool sortDir,
                                                 [FromQuery(Name = "minStars")] int minStars,
                                                 [FromQuery(Name = "maxStars")] int maxStars,
                                                 [FromQuery(Name = "country")] string country,
                                                 [FromQuery(Name = "city")] string city,
                                                 [FromQuery(Name = "address")] string address)
        {
            return hotelService.GetAllHotels(name, sort, sortDir, minStars, maxStars, country, city, address);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<HotelDTO> RegisterHotel([FromBody] HotelCreateDTO hotelCreateDTO)
        {
            return await hotelService.CreateHotel(hotelCreateDTO);
        }

        [Authorize(Roles = "hotel-admin")]
        [HttpPut]
        public async Task<HotelDTO> EditHotel([FromBody] HotelDTO hotelDTO)
        {
            return await hotelService.EditHotel(hotelDTO, User.ConvertToUserData().Id);
        }
    }
}