using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.LoginCQ.Querries;

namespace Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Core.Entitites.User, GetUsersQuerry>().ReverseMap();
            /*CreateMap<Employee.Core.Entitites.Employee, CreateEmployeeCommand>().ReverseMap();*/
        }
    }
}
