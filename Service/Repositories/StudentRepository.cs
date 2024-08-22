using DLA.Data;
using DomainEntities.Model;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repositories
{
    public class StudentRepository:GenericClass<Student>,IStudent
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context):base(context) { }  
       
        public async void Add(StudentDto studentDto )
        {
             var studentData = Entities(studentDto);
               await Add(studentData);
           
        }
        public List<Student> GetStudent()
        {
           // var executeStoreProcedure = _context.Database.ExecuteSqlRaw("exec GetCustomers"); 
               // var studentData = _context.Students.Include(x => x.Address).Include(x => x.Books).ToList();
                var studentData = GetAllIncluding(x=>x.Address).ToList();
                    studentData = GetAllIncluding(x=>x.Books).ToList();
            if (studentData != null)
            {
                return studentData;
            }
            else
            {
                return new List<Student>();
            }
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await GetById(id);
        }
        public void Update(StudentDto studentDto)
        {
            var studentData = Entities(studentDto);
            if(studentData!= null) 
            Update(studentData);
         
        }
        public void Delete(int id)
        {
            var studentData = GetById(id).Result;
            Delete(studentData);
           
        }
        private Student Entities(StudentDto studentDto)
        {
            if (studentDto.Id == 0)
            {
                Student student = new Student();
                student.Name = studentDto.Name;
                student.AddressId = studentDto.AddressId;
                return student;
            }
            else
            {
                var studentData = GetById(studentDto.Id).Result;
                studentData.Name=studentDto.Name;
                studentData.AddressId=studentDto.AddressId;
                return studentData;
            }
        }
    }
}
