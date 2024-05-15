using Microsoft.AspNetCore.Mvc;
using ShoeSalesAPI.Models;
using ShoeSalesAPI.Services;

namespace ShoeSalesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoeController : Controller
    {
        private readonly ShoeService _shoeService;

        public ShoeController(ShoeService shoeService) =>
            _shoeService = shoeService;


        #region Get Requests
        // Finds all products
        [Route("allproducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var allProducts = await _shoeService.GetAllProducts();

            if (allProducts == null)
            {
                return NotFound();
            }
            return Ok(allProducts);

        }

        // Filters shoes by their prices, with min and maximum
        [Route("filterprice")]
        [HttpGet]
        public async Task<IActionResult> GetPrice(double minPrice, double maxPrice)
        {
            var rangeProducts = await _shoeService.GetShoesByPrice(minPrice, maxPrice);

            if (rangeProducts == null)
            {
                return NotFound();
            }
            return Ok(rangeProducts);

        }

        // Finds all availible stock and displays it
        [Route("isAvailable={available}")]
        [HttpGet]
        public async Task<IActionResult> GetStock(bool available)
        {
            var stock = await _shoeService.GetShoesByAvailibility(available);

            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
        #endregion

        [Route("sku={sku}")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int sku, Shoe updatedShoe)
        {
            var product = await _shoeService.UpdateProduct(sku, updatedShoe);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        [Route("delete={sku}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int sku)
        {
            var product = await _shoeService.DeleteProduct(sku);

            if (product == null)
            {
                return BadRequest(product);
            }
            return NoContent();
        }


        [Route("post={sku}")]
        [HttpPost]
        public async Task<IActionResult> Post(int sku, Shoe shoe)
        {
            var post = await _shoeService.AddProduct(sku, shoe);

            if (post == null)
            {
                return BadRequest(post);
            }
            return Ok(post);



        }
    }

}
