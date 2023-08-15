using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CashControlBack.Models;
using Microsoft.EntityFrameworkCore;
using CashControl.Context;
using CashControl.Models;

namespace CashControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext; // Replace YourDbContext with your actual DbContext class

        public CompanyProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/CompanyProduct
        [HttpGet]
        public IActionResult GetAll()
        {
            List<CompanyProduct> products = _dbContext.CompanyProducts.ToList();
            return Ok(products);
        }


        // GET api/CompanyProduct/{id}
        [HttpGet("Get/{id}")]
        public IActionResult Get(int id)
        {
            CompanyProduct product = _dbContext.CompanyProducts.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("sales-records")]
        public IActionResult SaveSalesRecords([FromBody] List<SalesRecord> salesRecords)
        {
            foreach (var salesRecord in salesRecords)
            {
                // Save salesRecord to the database using Entity Framework or your preferred ORM
                _dbContext.SalesRecords.Add(salesRecord);
            }

            _dbContext.SaveChanges();

            return Ok();
        }

        [HttpGet("sales-records")]
        public IActionResult GetSalesRecordsByDate(DateTime date)
        {
            var salesRecords = _dbContext.SalesRecords
                .Where(s => s.Date.Date == date.Date)
                .ToList();

            return Ok(salesRecords);
        }
        // POST api/CompanyProduct
        [HttpPost]
        public IActionResult Post([FromBody] CompanyProduct companyProduct)
        {
            // Validate the received data if necessary
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Perform any necessary processing or validation on the companyProduct object
            // For example, you can calculate the profit by calling the CalculateProfit() method
            decimal profit = companyProduct.CalculateProfit();
            companyProduct.Profit = profit;

            // Save the companyProduct to the database
            _dbContext.CompanyProducts.Add(companyProduct);
            _dbContext.SaveChanges();

            // Return a success response with the created companyProduct
            return Ok(companyProduct);
        }

        // PUT api/CompanyProduct/Update/{id}
        [HttpPut("Update/{id}")]
        public IActionResult Put(int id, [FromBody] CompanyProduct updatedProduct)
        {
            // Validate the received data if necessary
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the existing product by its ID in the database
            CompanyProduct existingProduct = _dbContext.CompanyProducts.FirstOrDefault(p => p.ProductId == id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the existing product properties with the values from the updatedProduct object
            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.ProductQuantity = updatedProduct.ProductQuantity;
            existingProduct.ProductRemainings = updatedProduct.ProductRemainings;
            existingProduct.SellingPrice = updatedProduct.SellingPrice;

            // Calculate the updated profit
            decimal profit = existingProduct.CalculateProfit();
            existingProduct.Profit = profit;

            // Save the updated product to the database
            _dbContext.SaveChanges();

            // Return a success response with the updated product
            return Ok(existingProduct);
        }

        // DELETE api/CompanyProduct/Delete/{id}
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            // Find the product to delete by its ID in the database
            CompanyProduct productToDelete = _dbContext.CompanyProducts.FirstOrDefault(p => p.ProductId == id);

            if (productToDelete == null)
            {
                return NotFound();
            }

            // Delete the product from the database
            _dbContext.CompanyProducts.Remove(productToDelete);
            _dbContext.SaveChanges();

            // Return a success response
            return NoContent();
        }
    }
}
