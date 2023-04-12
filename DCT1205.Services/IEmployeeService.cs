using DCT1205.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT1205.Services
{
    public interface IEmployeeSevice
    {
        Task CreateAsSync (Employee employee);
        Task UpdateById(int  id);
        Task UpdateAsSync (Employee employee);
        Task DeleteById(int id);   
        Task DeleteAsSync (int Id);
        Employee GetById(int id);
        IEnumerable<Employee> GetAll(); 

    }
}
