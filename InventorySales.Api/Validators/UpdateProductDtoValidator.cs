using System;
using FluentValidation;
using InventorySales.Application.DTOs;
namespace InventorySales.Api.Validators;

public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MaximumLength(200).WithMessage("Product name cannot exceed 200 characters.");

        RuleFor(x => x.PurchasePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Purchase price cannot be negative.");

        RuleFor(x => x.SalePrice)
            .GreaterThan(0).WithMessage("Sale price must be greater than 0.")
            .GreaterThanOrEqualTo(x => x.PurchasePrice)
            .WithMessage("Sale price must be greater than or equal to purchase price.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
    }

}
