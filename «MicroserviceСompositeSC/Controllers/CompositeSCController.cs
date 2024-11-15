using Microsoft.AspNetCore.Mvc;
using MicroserviceСompositeSC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace MicroserviceСompositeSC.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CompositeSCController : ControllerBase
{
    private readonly string _productServiceAddress = "https://localhost:7186/api/products";
    private readonly string _categoryServiceAddress = "https://localhost:7051/api/categories";
    private readonly string _providerServiceAddress = "https://localhost:7273/api/providers";

    [HttpGet]
    public string Start()
    {
        return "Composite is run!";
    }
    [HttpGet("products/{categoryId}")]
    public async Task<List<Product>> GetProductsByCategoryAsync(long categoryId)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();

        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        using (HttpClient client = new HttpClient(clientHandler))
        {
            HttpResponseMessage response = await
           client.GetAsync($"{_productServiceAddress}");
            if (response.IsSuccessStatusCode)
            {
                List<Product> products = await
               JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
                return products.Where(product => product.CategoryId == categoryId).ToList();
            }
        }
        return null;
    }
    [HttpGet("productsCount/{categoryId}")]
    public async Task<NumberOfProducts> GetProductsCountAsync(long categoryId)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        using (HttpClient client = new HttpClient(clientHandler))
        {
            HttpResponseMessage response = await
           client.GetAsync($"{_productServiceAddress}");
            if (response.IsSuccessStatusCode)
            {
                List<Product> products = await
               JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
                var collection = products.Where(p => p.CategoryId == categoryId).ToList();
                HttpResponseMessage productResponse = await client.GetAsync($"{_categoryServiceAddress}/{categoryId}");
                if (productResponse.IsSuccessStatusCode)
                {
                    var category = await JsonSerializer.DeserializeAsync<Product>(await productResponse.Content.ReadAsStreamAsync());

                    return new NumberOfProducts()
                    {
                        CategoryName = category.Name,
                        Count = collection.Count
                    };
                }
            }
        }
        return null;
    }
    [HttpGet("averageRating/{categoryId}")]
    public async Task<ActionResult<RatingOfCategory>> GetAverageRatingByCategoryAsync(long categoryId)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        using (HttpClient client = new HttpClient(clientHandler))
        {
            HttpResponseMessage response = await client.GetAsync(_productServiceAddress);

            if (response.IsSuccessStatusCode)
            {
                List<Product> products = await JsonSerializer.DeserializeAsync<List<Product>>(await response.Content.ReadAsStreamAsync());
                var categoryProducts = products.Where(p => p.CategoryId == categoryId).ToList();
                if (!categoryProducts.Any())
                {
                    return NotFound("Товары данной категории не найдены.");
                }
                double averageRating = categoryProducts.Average(p => p.Rating);
                HttpResponseMessage categoryResponse = await client.GetAsync($"{_categoryServiceAddress}/{categoryId}");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var category = await JsonSerializer.DeserializeAsync<Category>(await categoryResponse.Content.ReadAsStreamAsync());
                    return Ok(new RatingOfCategory
                    {
                        CategoryName = category.Name,
                        AverageRating = averageRating
                    });
                }

                return StatusCode((int)categoryResponse.StatusCode, "Не удалось получить категорию.");
            }

            return StatusCode((int)response.StatusCode, "Не удалось получить список товаров.");
        }
    }
    [HttpGet("providerByProduct/{productId}")]
    public async Task<ActionResult<Provider>> GetProviderByProductIdAsync(long productId)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        using (HttpClient client = new HttpClient(clientHandler))
        {
            HttpResponseMessage productResponse = await client.GetAsync($"{_productServiceAddress}/{productId}");

            if (productResponse.IsSuccessStatusCode)
            {
                var product = await JsonSerializer.DeserializeAsync<Product>(await productResponse.Content.ReadAsStreamAsync());
                HttpResponseMessage providerResponse = await client.GetAsync($"{_providerServiceAddress}/{product.ProviderId}");
                if (providerResponse.IsSuccessStatusCode)
                {
                    var provider = await JsonSerializer.DeserializeAsync<Provider>(await providerResponse.Content.ReadAsStreamAsync());
                    return Ok(provider);
                }
                return StatusCode((int)providerResponse.StatusCode, "Не удалось получить информацию о поставщике.");
            }
            return StatusCode((int)productResponse.StatusCode, "Не удалось получить информацию о товаре.");
        }
    }

}
