﻿using Microsoft.AspNetCore.Mvc;
using ShoeSalesAPI.Models;
using ShoeSalesAPI.Services;

namespace ShoeSalesAPI.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/v{v:apiVersion}/products")]
    public class ShoeV1Controller(ShoeService shoeService) : ControllerBase
    {
        private readonly ShoeService _shoeService = shoeService;

        #region Get Requests
        // Finds all products   
        [Route("getproducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts(string? sortedBy, string? productName)
        {
            // passes a boolean that decides whether to sort by SKU or price
            var allProducts = await _shoeService.GetAllProducts(sortedBy, productName);

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


        [Route("sku={sku}")]
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


        [Route("sku={sku}")]
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
    [Route("/v{v:apiVersion}/products")]
    public class ShoeV2Controller(ShoeService shoeService) : ControllerBase
    {
        private readonly ShoeService _shoeService = shoeService;


        #region Get Requests
        // Finds all products   
        [Route("getproducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts(string? sortedBy, string? productName)
        {
            // passes a boolean that decides whether to sort by SKU or price
            var allProducts = await _shoeService.GetAllProducts(sortedBy, productName);

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


        [Route("sku={sku}")]
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


        [Route("sku={sku}")]
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
