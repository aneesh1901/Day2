using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Day2;



namespace Day2.Tests;

public class ProductsApiTests : IClassFixture<TestApiFactory>
{
    private readonly HttpClient _client;

    public ProductsApiTests(TestApiFactory factory)
    {
        // This creates an HttpClient that talks to the in-memory API server
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_Products_Returns200Ok()
    {
        // Act: call the real API endpoint
        var response = await _client.GetAsync("/api/Products");

        // Assert: verify HTTP 200
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_Product_CreatesProduct_AndReturnsIt()
    {
        // Arrange: this is the product we want to create
        var newProduct = new { name = "Phone", price = 999.99m };

        // Act: send POST with JSON body
        var response = await _client.PostAsJsonAsync("/api/Products", newProduct);

        // Assert: 200 OK
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        // Assert: response body has created product with Id
        var created = await response.Content.ReadFromJsonAsync<Product>();

        Assert.NotNull(created);
        Assert.True(created!.Id > 0);
        Assert.Equal("Phone", created.Name);
        Assert.Equal(999.99m, created.Price);
    }

    [Fact]
    public async Task Post_Then_GetById_ReturnsSameProduct()
    {
        // Arrange
        var newProduct = new { name = "Mouse", price = 25.50m };

        // Act 1: Create product
        var postResponse = await _client.PostAsJsonAsync("/api/Products", newProduct);
        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(created);
        Assert.True(created!.Id > 0);

        // Act 2: Fetch product back using id
        var getResponse = await _client.GetAsync($"/api/Products/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var fetched = await getResponse.Content.ReadFromJsonAsync<Product>();

        // Assert
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
        Assert.Equal("Mouse", fetched.Name);
        Assert.Equal(25.50m, fetched.Price);
    }


}
