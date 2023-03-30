using DCT1205.Entity;
using DCT1205.persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT1205.Services.implementation
{
    public class EmployeeService : IEmployeeSevice
    {
        private ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsSync(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var employee = GetById(id);
            _context.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsSync(Employee employee)
        {
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employee.ToList();
        }

        public Employee GetById(int id)
        {
            return _context.Employee.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task UpdateAsSync(Employee employee)
        {
            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(int id)
        {
            var employee = GetById(id);
            if (employee != null)
            {
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
            }
        }
    }
}
