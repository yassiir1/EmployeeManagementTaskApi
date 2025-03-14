using EmployeeManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.ServicesInterfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
