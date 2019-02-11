using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetAngular.Models;

namespace AspNetAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public SupplierController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: api/Supplier
        [HttpGet]
        public IEnumerable<Suppliers> GetSuppliers()
        {
            return _context.Suppliers;
        }

        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuppliers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suppliers = await _context.Suppliers.FindAsync(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            return Ok(suppliers);
        }

        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuppliers([FromRoute] int id, [FromBody] Suppliers suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != suppliers.SupplierId)
            {
                return BadRequest();
            }

            _context.Entry(suppliers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(id))
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

        // POST: api/Supplier
        [HttpPost]
        public async Task<IActionResult> PostSuppliers([FromBody] Suppliers suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Suppliers.Add(suppliers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSuppliers", new { id = suppliers.SupplierId }, suppliers);
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuppliers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var suppliers = await _context.Suppliers.FindAsync(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(suppliers);
            await _context.SaveChangesAsync();

            return Ok(suppliers);
        }

        private bool SuppliersExists(int id)
        {
            return _context.Suppliers.Any(e => e.SupplierId == id);
        }
    }
}