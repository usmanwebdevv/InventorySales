using System;
using InventorySales.Domain.Entities;

namespace InventorySales.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken (User user) ;

}
