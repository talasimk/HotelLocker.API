using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelLocker.BLL.Extensions;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs.UserBlackListDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelLocker.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBlackListController : ControllerBase
    {
        private readonly IUserBlackListService blackListService;

        public UserBlackListController(IUserBlackListService blackListService)
        {
            this.blackListService = blackListService;
        }

        [HttpPost]
        [Authorize(Roles = "hotel-admin")]
        public async Task<UserBlackListDTO> CreateUserBlackList([FromBody]UserBLCreateDTO userBLCreateDTO)
        {
            return await blackListService.CreateUserBlackList(userBLCreateDTO, User.ConvertToUserData().Id);
        }

        [HttpPut]
        [Authorize(Roles = "hotel-admin")]
        public async Task<UserBlackListDTO> EditBlackList(UserBLEditDTO userBLEditDTO)
        {
            return await blackListService.EditBlackList(userBLEditDTO, User.ConvertToUserData().Id);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "hotel-admin")]
        public async Task<UserBlackListDTO> DeleteBlackList(int id)
        {
            return await blackListService.DeleteBlackList(id, User.ConvertToUserData().Id);
        }

        [HttpGet]
        [Authorize(Roles = "hotel-admin")]
        public List<UserBlackListDTO> GetAllBlackList()
        {
            return blackListService.GetAllBlackLists();
        }

        [HttpGet("hotel")]
        [Authorize(Roles = "hotel-admin")]
        public List<UserBlackListDTO> GetHotellBlackList()
        {
            return blackListService.GetBlackLists(User.ConvertToUserData().Id);
        }



    }
}