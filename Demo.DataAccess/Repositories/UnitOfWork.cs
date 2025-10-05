using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.DepartmentRepo;
using Demo.DataAccess.Repositories.EmployeeRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly Lazy<IDepartmentRepository> _departmentRepository ;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
           this._dbContext = dbContext;
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));

        }

        public IEmployeeRepository employeeRepository => _employeeRepository.Value;
        public IDepartmentRepository departmentRepository => _departmentRepository.Value;

        public int SaveChange()
        {
            return _dbContext.SaveChanges();
        }
    }
}
