using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.ServicesInterfaces
{
    public interface IUserRepository
    {
        Task AddNewUser(User User);
        Task<User?> Login(AuthRequest inputDto);
    }
}
