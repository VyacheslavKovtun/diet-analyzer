using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Infrastructure.Business.DTO;
using Server.Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/api-users")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        ApiUsersService apiUsersService;

        public ApiUsersController(ApiUsersService apiUsersService)
        {
            this.apiUsersService = apiUsersService;
        }

        [HttpGet]
        public async Task<IEnumerable<ApiUserDTO>> Get()
        {
            var users = await apiUsersService.GetAllApiUsersAsync();
            return users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ApiUserDTO> Get(string id)
        {
            Guid gId = Guid.Parse(id);
            var user = await apiUsersService.GetApiUserByIdAsync(gId);
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var userDTO = JsonConvert.DeserializeObject<ApiUserDTO>(jsonObject.ToString());
                await this.apiUsersService.CreateNewApiUserAsync(userDTO);

                return Ok(userDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var gId = Guid.Parse(id);

            if (this.apiUsersService.GetApiUserByIdAsync(gId).Result != null)
            {
                await this.apiUsersService.DeleteApiUserAsync(gId);

                return Ok();
            }
            return BadRequest();
        }
    }
}
