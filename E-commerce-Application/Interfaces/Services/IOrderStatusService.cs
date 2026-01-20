using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_commerce_Application.DTOs.OrderStatusDTOs;

namespace E_commerce_Application.Interfaces.Services
{
    public interface IOrderStatusService
    {
        Task<IEnumerable<OrderStatusDto>> GetAllAsync();
    }

}
