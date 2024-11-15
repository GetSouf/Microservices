using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProviderMicroservice;
using ProviderMicroservice.Models;

namespace ProviderMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly ProviderContext _context;

        public ProvidersController(ProviderContext context)
        {
            _context = context;
        }

        // GET: api/Providers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> Getproviders()
        {
            return await _context.providers.ToListAsync();
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(long id)
        {
            var provider = await _context.providers.FindAsync(id);

            if (provider == null)
            {
                return NotFound();
            }

            return provider;
        }

        // PUT: api/Providers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvider(long id, Provider provider)
        {
            if (id != provider.Id)
            {
                return BadRequest();
            }

            _context.Entry(provider).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProviderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Providers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Provider>> PostProvider(Provider provider)
        {
            _context.providers.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvider", new { id = provider.Id }, provider);
        }

        // DELETE: api/Providers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(long id)
        {
            var provider = await _context.providers.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }

            _context.providers.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProviderExists(long id)
        {
            return _context.providers.Any(e => e.Id == id);
        }
    }
}
