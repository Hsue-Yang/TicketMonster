using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Interface;
using TicketMonster.Admin.Models.Create;

namespace TicketMonster.Admin.WebApi
{
 
    public class CreateController : BaseApiController
    {
        private readonly IEventRepo eventRepo;

        public CreateController(IEventRepo eventRepo) => this.eventRepo = eventRepo;
      

        [HttpPost]
        public async Task<IActionResult> CreateNewEvents(EventAndPicDto eventAndPicDto)
        {
            var result = await eventRepo.CreateNewEvents(eventAndPicDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryNameByCategoryId(int id)
        {
            var result = await eventRepo.GetCategoryNameByCategoryId(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryName()
        {
            var result = await eventRepo.GetAllCategoryName();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubCategoryNameAndCategoryId()
        {
            var result = await eventRepo.GetAllSubCategoryNameAndCategoryId();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVenueName()
        {
            var result = await eventRepo.GetAllVenueName();
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPerformerName()
        {
            var result = await eventRepo.GetAllPerformerName();
            return Ok(result);
        }
    }
}
