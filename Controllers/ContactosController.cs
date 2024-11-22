using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibretaContactosAPI.Data;
using LibretaContactosAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibretaContactosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly LibretaContactosAPIContext _context;

        public ContactosController(LibretaContactosAPIContext context)
        {
            _context = context;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contactos>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        // GET {id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contactos>> GetContactos(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);

            if (contactos == null)
            {
                return NotFound();
            }

            return contactos;
        }

        // PUT {id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactos(int id, Contactos contactos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactos.id)
            {
                return BadRequest();
            }

            _context.Entry(contactos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactosExists(id))
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

        // POST
        [HttpPost]
        public async Task<ActionResult<Contactos>> PostContactos(Contactos contactos)
        {
            //Verifica si el modelo es valido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contactos.Add(contactos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactos", new { contactos.id }, contactos);
        }

        // DELETE {id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactos(int id)
        {
            var contactos = await _context.Contactos.FindAsync(id);
            if (contactos == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contactos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactosExists(int id)
        {
            return _context.Contactos.Any(e => e.id == id);
        }
    }
}
