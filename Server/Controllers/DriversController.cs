using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController: ControllerBase
    {
        private readonly ApiDbContext _context;

        public DriversController(ApiDbContext context)
        {
            _context = context;  
        }

        [HttpGet]
        public async Task<ActionResult<List<Driver>>> GetDrivers()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Driver>>> GetDriverDetails(int id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
             
            if(driver == null) {
                return NotFound();
            } 

            return Ok(driver);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDriver(Driver driver)
        {
           _context.Drivers.Add(driver);
           await _context.SaveChangesAsync();

           return CreatedAtAction(nameof(GetDriverDetails), driver, driver.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDriver(Driver driver, int id)
        {
            var driverExist = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
             
            if(driverExist == null) {
                return NotFound();
            } 

            driverExist.Name = driver.Name;
            driverExist.RacingNumber = driver.RacingNumber;
            driverExist.TeamName = driverExist.TeamName;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDriver(int id)
        {
            var driverExist = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
             
            if(driverExist == null) {
                return NotFound();
            } 

            _context.Drivers.Remove(driverExist);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
