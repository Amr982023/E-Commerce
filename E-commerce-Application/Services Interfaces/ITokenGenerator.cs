using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AccountDTOs;
using E_commerce_Core.Models;

namespace E_commerce_Application.Services_Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(AccountDto account);
    }
}
