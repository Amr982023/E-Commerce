using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.Dtos.AccountDTOs;

namespace E_commerce_Application.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public AccountDto Account { get; set; } = null!;
    }
}
