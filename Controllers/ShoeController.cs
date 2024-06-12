using Microsoft.AspNetCore.Mvc;
using ShoeSalesAPI.Models;
using ShoeSalesAPI.Services;

namespace ShoeSalesAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/products")]
    public class ShoeV1Controller(ShoeService shoeService) : ControllerBase
    {
        private readonly ShoeService _shoeService = shoeService;

        #region Get Requests
        // Finds all products   
        [HttpGet]
        public async Task<IActionResult> GetProducts(string? sortedBy, string? productName)
        {
            bool isAvailable = false;
            // passes a boolean that decides whether to sort by SKU or price
            var allProducts = await _shoeService.GetAllProducts(sortedBy, productName, isAvailable);

			if (allProducts.Count == 0)
			{
				return NoContent();
			}
			return Ok(allProducts);
		}

        // Filters shoes by their prices, with min and maximum
        [Route("{minPrice}to{maxPrice}")]
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
        [Route("{available}")]
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

        [HttpPost]
        public async Task<IActionResult> Post(int sku, Shoe shoe)
        {
            var post = await _shoeService.AddProduct(sku, shoe);

            if (post == null)
            {
                return Conflict();
            }
            return Ok(post);
        }
    }
	[ApiVersion("2.0")]
	[ApiController]
	[Route("/products")]
	public class ShoeV2Controller(ShoeService shoeService) : ControllerBase
	{
		private readonly ShoeService _shoeService = shoeService;

		#region Get Requests
		// Finds all products   
		[HttpGet]
		public async Task<IActionResult> GetProducts(string? sortedBy, string? productName)
		{
			bool isAvailable = true;
			// passes a boolean that decides whether to sort by SKU or price
			var allProducts = await _shoeService.GetAllProducts(sortedBy, productName, isAvailable);

			if (allProducts.Count == 0)
			{
				return NoContent();
			}
			return Ok(allProducts);
		}

		// Filters shoes by their prices, with min and maximum
		[Route("{minPrice}to{maxPrice}")]
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
		[Route("{available}")]
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

		[HttpPost]
		public async Task<IActionResult> Post(int sku, Shoe shoe)
		{
			var post = await _shoeService.AddProduct(sku, shoe);

			if (post == null)
			{
				return Conflict();
			}
			return Ok(post);
		}
	}
}

