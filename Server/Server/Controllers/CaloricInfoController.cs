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
    [Route("api/caloric-info")]
    [ApiController]
    public class CaloricInfoController : ControllerBase
    {
        CaloricInfoService caloricInfoService;

        public CaloricInfoController(CaloricInfoService caloricInfoService)
        {
            this.caloricInfoService = caloricInfoService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<CaloricInfoDTO>> Get()
        {
            var info = await caloricInfoService.GetAllCaloricInfoAsync();
            return info;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<CaloricInfoDTO> Get(int id)
        {
            var info = await caloricInfoService.GetCaloricInfoByIdAsync(id);
            return info;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var caloricInfoDTO = JsonConvert.DeserializeObject<CaloricInfoDTO>(jsonObject.ToString());
                await this.caloricInfoService.CreateNewCaloricInfoAsync(caloricInfoDTO);

                return Ok(caloricInfoDTO);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put(object jsonObject)
        {
            if (ModelState.IsValid)
            {
                var caloricInfoDTO = JsonConvert.DeserializeObject<CaloricInfoDTO>(jsonObject.ToString());
                await this.caloricInfoService.UpdateCaloricInfoAsync(caloricInfoDTO);

                return Ok(caloricInfoDTO);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (this.caloricInfoService.GetCaloricInfoByIdAsync(id).Result != null)
            {
                await this.caloricInfoService.DeleteCaloricInfoAsync(id);

                return Ok();
            }
            return BadRequest();
        }
    }
}
