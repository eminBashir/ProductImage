using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductImageApi.Model.DTO;
using ProductImageApi.Services.Implement;
using ProductImageApi.Services.Interface;

namespace ProductImageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateDTO productCreateDTO)
        {
            await _productService.Create(productCreateDTO);
            return Ok();

        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            return Ok(product);
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetImageBase64(int id)
        {
            try
            {
                var base64Image = await _productService.GetImageBase64(id);
                return Ok(new { ImageBase64 = base64Image });

            }
            catch (FileNotFoundException)
            {

                return NotFound("Image not found");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductUpdateDTO productUpdateDTO)
        {
            await _productService.Update(id, productUpdateDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.Delete(id);

            return NoContent();
        }



    }
}
