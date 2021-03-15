using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuearyController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public QuearyController(SqlDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetCases([FromQuery] string status, int Id, int clientid, DateTime date)
        {
            var quarable = _context.Cases.AsQueryable();
            if (!string.IsNullOrEmpty(status))
                quarable = quarable.Where(q => q.StatusCode == status);

            if (Id >= 1)
                quarable = quarable.Where(q => q.Id == Id);

            if (clientid >= 1)
                quarable = quarable.Where(q => q.ClientId == clientid);

            if (date != DateTime.MinValue)
                quarable = quarable.Where(q => q.Created.Date == date);

            return new OkObjectResult(await quarable.ToListAsync());
        }
    }
}
