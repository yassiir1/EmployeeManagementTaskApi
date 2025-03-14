using Azure.Core;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Helpers;
using EmployeeManagement.Core.ServicesInterfaces;
using EmployeeManagement.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Infrastructure.ServicesImplementaions
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddNewUser(User inputDto)
        {
            await _context.Users.AddAsync(inputDto);
        }

        public async Task<User?> Login(AuthRequest inputDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == inputDto.Username);
            if (user == null || !BCryptHasher.VerifyPassword(inputDto.Password, user.Password))
                return null;
            return user;
        }
    }
}
