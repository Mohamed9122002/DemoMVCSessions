using Demo.BLL.DataTransferObject;

namespace Demo.BLL.Services
{
    public interface IDepartmentService
    {
        int AddDepartment(CreatedDepartmentDto departmentDto);
        bool DepleteDepartment(int id);
        IEnumerable<DepartmentDto> GetAll();
        DepartmentDetailsDto GetDepartmentById(int id);
        int UpdatedDepartment(UpdatedDepartmentDto departmentDto);
    }
}