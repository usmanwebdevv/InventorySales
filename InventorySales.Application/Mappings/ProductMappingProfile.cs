using System;
using InventorySales.Application.DTOs;
using InventorySales.Domain.Entities;
namespace InventorySales.Application.Mappings;

public static class ProductMapping
{
    public static Product ToEntity(this CreateProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            PurchasePrice = dto.PurchasePrice,
            SalePrice = dto.SalePrice,
            StockQuantity = dto.StockQuantity
        };
        
    }
     public static Product ToEntity(this UpdateProductDto dto, int id)
    {
        return new Product
        {
            Id = id,
            Name = dto.Name,
            Description = dto.Description,
            PurchasePrice = dto.PurchasePrice,
            SalePrice = dto.SalePrice,
            StockQuantity = dto.StockQuantity
        };
        
    }
     public static ProductDto ToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            PurchasePrice = product.PurchasePrice,
            SalePrice = product.SalePrice,
            StockQuantity = product.StockQuantity,
            CreatedAt = product.CreatedAt
        };
        
    }
    public static List<ProductDto> ToDtoList(this List<Product> products)
    {
        return products.Select(p => p.ToDto()).ToList();
    }
}