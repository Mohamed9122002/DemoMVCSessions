using AutoMapper;
using Demo.BLL.DataTransferObject.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<DateTime, DateOnly>().ConvertUsing(src => DateOnly.FromDateTime(src));
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dist => dist.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dist => dist.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dist => dist.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null));
            // Convert Employee To EmployeeDetailsDTO
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dist => dist.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dist => dist.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType))
                .ForMember(dist => dist.HiringDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ForMember(dist => dist.Department, Options => Options.MapFrom(src => src.Department != null ? src.Department.Name : null)).ReverseMap()
                .ForMember(dist => dist.ImageName,opt=>opt.MapFrom(src=>src.Image));
            //  Convert CreateEmployeeDTO To Employee
            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dist => dist.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));
            // Convert UpdatedEmployeeDTO To Employee
            CreateMap<UpdatedEmployeeDto, Employee>()
                                .ForMember(dist => dist.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
