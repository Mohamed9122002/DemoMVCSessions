﻿using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObject
{
    public class DepartmentDetailsDto
    {
        // Constructor Based Mapping 
        //public DepartmentDetailsDto( Department department)
        //{
        //    Id = department.Id; 
        //    Name = department.Name; 
        //    CreatedOn = DateOnly.FromDateTime((DateTime)department.CreatedOn);
        //}
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateOnly? CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateOnly? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }


    }
}
