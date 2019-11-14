﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelLocker.BLL.Extensions;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs;
using HotelLocker.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelLocker.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService accessService;

        public AccessController(IAccessService accessService)
        {
            this.accessService = accessService;
        }

        [Authorize(Roles = "hotel-staff")]
        [HttpPost]
        public async Task GetAccessByHotelStaff([FromBody] AccessByWorkerDTO accessByWorkerDTO)
        {
            await accessService.GetAccessByHotelStaff(User.ConvertToUserData().Id, accessByWorkerDTO);
        }

        [Authorize(Roles = "hotel-admin")]
        [HttpPost]
        public async Task GetAccessByHotelAdmin([FromBody] AccessByWorkerDTO accessByWorkerDTO)
        {
            await accessService.GetAccessByHotelStaff(User.ConvertToUserData().Id, accessByWorkerDTO);
        }

        [Authorize(Roles = "user")]
        [HttpPost("{id}")]
        public async Task GetAccessByUser(int id)
        {
            await accessService.GetAccessByUser(User.ConvertToUserData().Id, id);
        }

    }
}