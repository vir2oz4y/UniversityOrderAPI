﻿using System.Net.Http.Json;
using Newtonsoft.Json;
using UniversityOrderAPI.BLL.Category;
using UniversityOrderAPI.BLL.Client;
using UniversityOrderAPI.BLL.Manufacturer;
using UniversityOrderAPI.BLL.Order;
using UniversityOrderAPI.BLL.Product;
using UniversityOrderAPI.BLL.Purchase;
using UniversityOrderAPI.Middleware.Models;
using UniversityOrderAPI.Models;
using UniversityOrderAPI.Models.Category;
using UniversityOrderAPI.Models.Client;
using UniversityOrderAPI.Models.Manufacturer;
using UniversityOrderAPI.Models.Order;
using UniversityOrderAPI.Models.Product;
using UniversityOrderAPI.Models.Purchase;
using UniversityOrderAPI.Models.User;

namespace UniversityOrderAPI.HttpClient;

public class CustomHttpClient : System.Net.Http.HttpClient
{
    private CustomHttpClient() 
    {
        BaseAddress = new Uri("http://canstudy.ru/orderapi/");
    }

    private CustomHttpClient(string token) : this()
    {
        DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    }

    public static CustomHttpClient Create(string identifier)
    {
        using var httpClient = new CustomHttpClient();

        var response = httpClient.PostAsJsonAsync("User/login", new LoginRequest
        {
            Identifier = identifier
        }).GetAwaiter().GetResult();

        var responseObject = response.ReadAsJsonAsync<LoginResponse>().GetAwaiter().GetResult();
        
        return new CustomHttpClient(responseObject.AuthToken);
    }

    public static async Task CheckResponse(HttpResponseMessage httpResponseMessage)
    {
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            var errorJson = await httpResponseMessage.Content.ReadAsStringAsync();
            var errorObj = JsonConvert.DeserializeObject<ErrorModel>(errorJson);

            throw new ApiException(errorObj.Message);
        }
    }

    // Category

    public async Task<ISingleResult<CategoryAPIDTO>> GetCategoryByIdAsync(int id)
    {
        var response = await GetAsync($"Category/{id}");
        
        var entry = await response.ReadAsJsonAsync<ISingleResult<CategoryAPIDTO>>();
        
        return entry;
    }

    public async Task<IManyResult<CategoryAPIDTO>> GetCategoryListAsync()
    {
        var response = await GetAsync("Category/list");

        var entryList = await response.ReadAsJsonAsync<IManyResult<CategoryAPIDTO>>();

        return entryList; 
    }

    public async Task<ISingleResult<CategoryAPIDTO>?> CreateCategoryAsync(CategoryDTO item)
    {
        var response = await this.PostAsJsonAsync("Category", item);
        
        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<CategoryAPIDTO>>();

        return entry;
    }
    
    public async Task<ISingleResult<CategoryAPIDTO>?> PatchCategoryAsync(ISingleResult<CategoryDTO> item)
    {
        var response = await this.PatchAsJsonAsync("Category", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<CategoryAPIDTO>>();

        return entry;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var response = await DeleteAsync($"Category/{id}");
        
        await CheckResponse(response);
    }
    
    // Client
    
    public async Task<ISingleResult<ClientAPIDTO>> GetClientByIdAsync(int id)
    {
        var response = await GetAsync($"Client/{id}");

        var entry = await response.ReadAsJsonAsync<ISingleResult<ClientAPIDTO>>();

        return entry;
    }

    public async Task<IManyResult<ClientAPIDTO>> GetClientListAsync()
    {
        var response = await GetAsync("Client/list");

        var entryList = await response.ReadAsJsonAsync<IManyResult<ClientAPIDTO>>();

        return entryList; 
    }

    public async Task<ISingleResult<ClientAPIDTO>?> CreateClientAsync(ClientDTO item)
    {
        var response = await this.PostAsJsonAsync("Client", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<ClientAPIDTO>>();

        return entry;
    }
    
    public async Task<ISingleResult<ClientAPIDTO>?> PatchClientAsync(ISingleResult<ClientDTO> item)
    {
        var response = await this.PatchAsJsonAsync("Client", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<ClientAPIDTO>>();

        return entry;
    }

    public async Task DeleteClientAsync(int id)
    {
        var response = await DeleteAsync($"Client/{id}");
        
        await CheckResponse(response);
    }
    
    // Manufacturer
    
    public async Task<ISingleResult<ManufacturerAPIDTO>> GetManufacturerByIdAsync(int id)
    {
        var response = await GetAsync($"Manufacturer/{id}");

        var entry = await response.ReadAsJsonAsync<ISingleResult<ManufacturerAPIDTO>>();

        return entry;
    }

    public async Task<IManyResult<ManufacturerAPIDTO>> GetManufacturerListAsync()
    {
        var response = await GetAsync("Manufacturer/list");

        var entryList = await response.ReadAsJsonAsync<IManyResult<ManufacturerAPIDTO>>();

        return entryList; 
    }

    public async Task<ISingleResult<ManufacturerAPIDTO>?> CreateManufacturerAsync(ManufacturerDTO item)
    {
        var response = await this.PostAsJsonAsync("Manufacturer", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<ManufacturerAPIDTO>>();

        return entry;
    }
    
    public async Task<ISingleResult<ManufacturerAPIDTO>?> PatchManufacturerAsync(ISingleResult<ManufacturerDTO> item)
    {
        var response = await this.PatchAsJsonAsync("Manufacturer", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<ManufacturerAPIDTO>>();

        return entry;
    }

    public async Task DeleteManufacturerAsync(int id)
    {
        var response = await DeleteAsync($"Manufacturer/{id}");
        
        await CheckResponse(response);
    }
    
    // Order
    
    public async Task<ISingleResult<OrderAPIDTO>> GetOrderByIdAsync(int id)
    {
        var response = await GetAsync($"Order/{id}");

        var entry = await response.ReadAsJsonAsync<ISingleResult<OrderAPIDTO>>();

        return entry;
    }

    public async Task<IManyResult<OrderAPIDTO>> GetOrderListAsync()
    {
        var response = await GetAsync("Order/list");

        var entryList = await response.ReadAsJsonAsync<IManyResult<OrderAPIDTO>>();

        return entryList; 
    }

    public async Task<ISingleResult<OrderAPIDTO>?> CreateOrderAsync(OrderDTO item)
    {
        var response = await this.PostAsJsonAsync("Order", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<OrderAPIDTO>>();

        return entry;
    }
    
    public async Task<ISingleResult<OrderAPIDTO>?> PatchOrderAsync(ISingleResult<OrderDTO> item)
    {
        var response = await this.PatchAsJsonAsync("Order", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<OrderAPIDTO>>();

        return entry;
    }

    public async Task DeleteOrderAsync(int id)
    {
        var response = await DeleteAsync($"Order/{id}");
        
        await CheckResponse(response);
    }
    
    // Product
    
    public async Task<ISingleResult<ProductAPIDTO>> GetProductByIdAsync(int id)
    {
        var response = await GetAsync($"Product/{id}");

        var entry = await response.ReadAsJsonAsync<ISingleResult<ProductAPIDTO>>();

        return entry;
    }

    public async Task<IManyResult<ProductAPIDTO>> GetProductListAsync()
    {
        var response = await GetAsync("Product/list");

        var entryList = await response.ReadAsJsonAsync<IManyResult<ProductAPIDTO>>();

        return entryList; 
    }

    public async Task<ISingleResult<ProductAPIDTO>?> CreateProductAsync(ProductDTO item)
    {
        var response = await this.PostAsJsonAsync("Product", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<ProductAPIDTO>>();

        return entry;
    }
    
    public async Task<ISingleResult<ProductAPIDTO>?> PatchProductAsync(ISingleResult<ProductDTO> item)
    {
        var response = await this.PatchAsJsonAsync("Product", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<ProductAPIDTO>>();

        return entry;
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await DeleteAsync($"Product/{id}");
        
        await CheckResponse(response);
    }
    
    // Purchase
    
    public async Task<ISingleResult<PurchaseAPIDTO>> GetPurchaseByIdAsync(int id)
    {
        var response = await GetAsync($"Purchase/{id}");

        var entry = await response.ReadAsJsonAsync<ISingleResult<PurchaseAPIDTO>>();

        return entry;
    }

    public async Task<IManyResult<PurchaseAPIDTO>> GetPurchaseListAsync()
    {
        var response = await GetAsync("Purchase/list");

        var entryList = await response.ReadAsJsonAsync<IManyResult<PurchaseAPIDTO>>();

        return entryList; 
    }

    public async Task<ISingleResult<PurchaseAPIDTO>?> CreatePurchaseAsync(PurchaseDTO item)
    {
        var response = await this.PostAsJsonAsync("Purchase", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<PurchaseAPIDTO>>();

        return entry;
    }
    
    public async Task<ISingleResult<PurchaseAPIDTO>?> PatchPurchaseAsync(ISingleResult<PurchaseDTO> item)
    {
        var response = await this.PatchAsJsonAsync("Purchase", item);

        var entry = await response.Content.ReadFromJsonAsync<ISingleResult<PurchaseAPIDTO>>();

        return entry;
    }

    public async Task DeletePurchaseAsync(int id)
    {
        var response = await DeleteAsync($"Purchase/{id}");
        
        await CheckResponse(response);
    }
}