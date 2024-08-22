using DomainEntities.Model;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IStudent
    {
        void Add(StudentDto studentDto);
        List<Student> GetStudent();
        Task<Student> GetStudentById(int id);
        void Update(StudentDto studentDto);
        void Delete(int id);
    }
}
