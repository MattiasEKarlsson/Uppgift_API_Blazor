using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public ClientsController(SqlDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.Include(c=>c.Cases).ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        
        

       
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (!_context.Clients.Any(u => u.Phone == client.Phone))
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClient", new { id = client.Id }, client);

            }
            return new BadRequestResult();
        }

        

        
    }
}
