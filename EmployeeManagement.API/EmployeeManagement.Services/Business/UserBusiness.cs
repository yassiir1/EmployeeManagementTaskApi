using AutoMapper;
using EmployeeManagement.Core.BusinessInterfaces;
using EmployeeManagement.Core.DTOs;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Services.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddNewUser(AuthRequest inputDto)
        {
            await _unitOfWork.Users.AddNewUser(new User()
            {
                Password = HashPassword(inputDto.Password),
                Username = inputDto.Username,
            });
            await _unitOfWork.CompleteAsync();
        }

        public Task<User> Login(AuthRequest inputDto)
        {
            var User = _unitOfWork.Users.Login(inputDto);
            return User;
        }
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12); 
        }
    }
}
