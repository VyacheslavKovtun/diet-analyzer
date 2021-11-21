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
    [Route("api/base-info")]
    [ApiController]
    public class BaseInfoController : ControllerBase
    {
        BaseInfoService baseInfoService;

        public BaseInfoController(BaseInfoService baseInfoService)
        {
            this.baseInfoService = baseInfoService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<BaseInfoDTO>> Get()
        {
            var info = await baseInfoService.GetAllBaseInfoAsync();
            return info;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<BaseInfoDTO> Get(int id)
        {
            var info = await baseInfoService.GetBaseInfoByIdAsync(id);
            return info;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var baseInfoDTO = JsonConvert.DeserializeObject<BaseInfoDTO>(jsonObject.ToString());
                await this.baseInfoService.CreateNewBaseInfoAsync(baseInfoDTO);

                return Ok(baseInfoDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var baseInfoDTO = JsonConvert.DeserializeObject<BaseInfoDTO>(jsonObject.ToString());
                await this.baseInfoService.UpdateBaseInfoAsync(baseInfoDTO);

                return Ok(baseInfoDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.baseInfoService.GetBaseInfoByIdAsync(id).Result != null)
            {
                await this.baseInfoService.DeleteBaseInfoAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
