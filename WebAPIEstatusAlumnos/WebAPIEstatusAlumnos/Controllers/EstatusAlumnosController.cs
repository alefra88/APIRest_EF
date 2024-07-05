using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIEstatusAlumnos.Models.Context;
using WebAPIEstatusAlumnos.Models.Entities;

namespace WebAPIEstatusAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusAlumnosController : ControllerBase
    {
        private readonly EstatusContext _context;

        public EstatusAlumnosController(EstatusContext context)
        {
            _context = context;
        }

        // GET: api/EstatusAlumnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstatusAlumnos>>> GetEstatusAlumnos()
        {
          if (_context.EstatusAlumnos == null)
          {
              return NotFound();
          }
            return await _context.EstatusAlumnos.ToListAsync();
        }

        // GET: api/EstatusAlumnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstatusAlumnos>> GetEstatusAlumnos(short id)
        {
          if (_context.EstatusAlumnos == null)
          {
              return NotFound();
          }
            var estatusAlumnos = await _context.EstatusAlumnos.FindAsync(id);

            if (estatusAlumnos == null)
            {
                return NotFound();
            }

            return estatusAlumnos;
        }

        // PUT: api/EstatusAlumnos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstatusAlumnos(short id, EstatusAlumnos estatusAlumnos)
        {
            if (id != estatusAlumnos.Id)
            {
                return BadRequest();
            }

            _context.Entry(estatusAlumnos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstatusAlumnosExists(id))
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

        // POST: api/EstatusAlumnos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstatusAlumnos>> PostEstatusAlumnos(EstatusAlumnos estatusAlumnos)
        {
          if (_context.EstatusAlumnos == null)
          {
              return Problem("Entity set 'EstatusContext.EstatusAlumnos'  is null.");
          }
            _context.EstatusAlumnos.Add(estatusAlumnos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstatusAlumnos", new { id = estatusAlumnos.Id }, estatusAlumnos);
        }

        // DELETE: api/EstatusAlumnos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstatusAlumnos(short id)
        {
            if (_context.EstatusAlumnos == null)
            {
                return NotFound();
            }
            var estatusAlumnos = await _context.EstatusAlumnos.FindAsync(id);
            if (estatusAlumnos == null)
            {
                return NotFound();
            }

            _context.EstatusAlumnos.Remove(estatusAlumnos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstatusAlumnosExists(short id)
        {
            return (_context.EstatusAlumnos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
