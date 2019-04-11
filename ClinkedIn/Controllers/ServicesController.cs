using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        [HttpPost("services")]
        public ActionResult AddService(CreateServiceRequest createService)
        {
            var newService = ServiceRepository.AddService(createService.Services);
            return Created($"api/users/{newService.Id}", newService);
        }

    }
}
