using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.BusinessInterfaces
{
    public interface IUserBusiness
    {
        Task AddNewUser(AuthRequest inputDto);
        Task<User> Login(AuthRequest inputDto);

    }
}
