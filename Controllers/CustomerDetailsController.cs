using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AFI_CustomerRegistration.API.Models;
using Microsoft.Data.SqlClient;

namespace AFI_CustomerRegistration.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly CustomerDetailContext _context;

        // TODO - need to figure out how to use the DBContext properly so that the settings come from the json file
        private readonly string _conString = "Server=tcp:webapplication220200311095927dbserver.database.windows.net,1433;Initial Catalog=WebApplication220200311095927_db;Persist Security Info=False;User ID=AFI_Customer;Password=^9o3Wq34SLDpR7D;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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
        public async Task<ActionResult<CustomerID>> PostCustomerDetail(CustomerDetail customerDetail)
        {
            /*
             * will need to do some validation here
             * are they over 18?
             * is there an email or a DOB (need at least one)
             * 
             */
            Boolean bln = false;

            if (customerDetail.Email is null && customerDetail.DOB is null) { return ValidationProblem(); }

            else {
                // now if null then set to empty string
                if (customerDetail.Email is null) customerDetail.Email = "";
                if (customerDetail.DOB is null) customerDetail.DOB = "";

                // if both empty then not valid
                if (customerDetail.DOB == "" && customerDetail.Email == "") { return ValidationProblem(); }

                // are they over 18?
                if (customerDetail.DOB != "" && !(customerDetail.DOB is null))
                {
                    if (DateTime.Parse(customerDetail.DOB) > DateTime.Now.AddYears(-18)) { bln = true; } // too young
                }

                // email has .co.uk or .com?
                if (customerDetail.Email != "")
                {
                    if (!customerDetail.Email.Contains(".co.uk") && !customerDetail.Email.Contains(".com")) { bln = true; }
                    else if (customerDetail.Email.Length > 6) { 
                        if (customerDetail.Email.ToString().Substring(customerDetail.Email.Length - 6, 6) != ".co.uk" && customerDetail.Email.ToString().Substring(customerDetail.Email.Length - 4, 4) != ".com") { bln = true; }
                    } else if (customerDetail.Email.Length > 4)
                    {
                        if (customerDetail.Email.ToString().Substring(customerDetail.Email.Length - 4, 4) != ".com") { bln = true; }
                    }
                }

                if (bln)
                {
                    return ValidationProblem(); // TODO - possibly could give more feedback here..?
                }
            }

            CustomerID customerID = new CustomerID(); // generate a new instance of customerID model to hold the return value in 

            try { // TODO - could use finer detail error trapping here - for example one for the db connection; one for the execute of the SQL .. 
            using (SqlConnection sql = new SqlConnection(_conString)) // connect to database
            {
                using (SqlCommand cmd = new SqlCommand("Create_New_AFI_Customer", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fname", customerDetail.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@sname", customerDetail.Surname));
                    cmd.Parameters.Add(new SqlParameter("@reference", customerDetail.ReferenceNumber));
                    DateTime newDate;
                    if (DateTime.TryParse(customerDetail.DOB,out newDate))
                        { 
                            cmd.Parameters.Add(new SqlParameter("@dob", DateTime.Parse(customerDetail.DOB)));
                        }
                    cmd.Parameters.Add(new SqlParameter("@email", customerDetail.Email));
                    
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            customerID.ID = Int32.Parse(reader["Customer_ID"].ToString());
                        }
                    }

                    return customerID;
                }
            }
            }
            catch(Exception ex)
            {
                return BadRequest("There was a problem sending to the database" + ex.Message);

            }

            //_context.CustomerDetails.Add(customerDetail);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCustomerDetail", new { id = customerDetail.CustomerID }, customerDetail);
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
