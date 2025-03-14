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
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        public EmployeeBusiness(IUnitOfWork unitOfWork, IMapper mapper, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _mapper = mapper;
        }
        public async Task CreateEmployeeAsync(CreateEmployeeDto createDto)
        {
            var employee = _mapper.Map<Employee>(createDto);

            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
            await _cacheService.RemoveAsync("employees_list");

        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return false;

            await _unitOfWork.Employees.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            await _cacheService.RemoveAsync("employees_list");

            return true;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            string cacheKey = "employees_list";
            var cachedEmployees = await _cacheService.GetAsync<IEnumerable<EmployeeDto>>(cacheKey);

            if (cachedEmployees != null)
            {
                return cachedEmployees;
            }
            var employees = await _unitOfWork.Employees.GetAllAsync();
            var ReturnDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            await _cacheService.SetAsync(cacheKey, ReturnDto, TimeSpan.FromMinutes(10));

            return ReturnDto;
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            return _mapper.Map<EmployeeDto>(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return false;

            _mapper.Map(updateDto, employee);

            await _unitOfWork.Employees.UpdateAsync(employee);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
