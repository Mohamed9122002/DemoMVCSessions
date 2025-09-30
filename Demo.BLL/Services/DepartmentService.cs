using Demo.DataAccess.Repositories.DepartmentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository)
    {
        private readonly IDepartmentRepository departmentRepository = _departmentRepository;
    }
}
