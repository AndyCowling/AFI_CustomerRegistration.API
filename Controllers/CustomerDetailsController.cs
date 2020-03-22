using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AFI_CustomerRegistration.API.Models;

namespace AFI_CustomerRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly CustomerDetailContext _context;

        public CustomerDetailsController(CustomerDetailContext context)
        {
            _context = context;
        }

        // GET: api/CustomerDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDetail>>> GetCustomerDetails()
        {
            return BadRequest("HttpGet not enabled for this API");
         //   return await _context.CustomerDetails.ToListAsync();
        }

        // GET: api/CustomerDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetail>> GetCustomerDetail(int id)
        {
            return BadRequest("HttpGet{id} not enabled for this API");
            /*
            var customerDetail = await _context.CustomerDetails.FindAsync(id);

            if (customerDetail == null)
            {
                return NotFound();
            }

            return customerDetail;
            */
        }

        // PUT: api/CustomerDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDetail(int id, CustomerDetail customerDetail)
        {
            return BadRequest("HttpPut{id} not enabled for this API");
            /*
            if (id != customerDetail.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customerDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            */
        }

        // POST: api/CustomerDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CustomerDetail>> PostCustomerDetail(CustomerDetail customerDetail)
        {
            _context.CustomerDetails.Add(customerDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerDetail", new { id = customerDetail.CustomerID }, customerDetail);
        }

        // DELETE: api/CustomerDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerDetail>> DeleteCustomerDetail(int id)
        {

            return BadRequest("HttpDelete{id} not enabled for this API");
            /*
            var customerDetail = await _context.CustomerDetails.FindAsync(id);
            if (customerDetail == null)
            {
                return NotFound();
            }

            _context.CustomerDetails.Remove(customerDetail);
            await _context.SaveChangesAsync();

            return customerDetail;
            */
        }

        private bool CustomerDetailExists(int id)
        {
            return _context.CustomerDetails.Any(e => e.CustomerID == id);
        }
    }
}
