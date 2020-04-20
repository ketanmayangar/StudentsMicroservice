using Microsoft.EntityFrameworkCore;
using StudentsMicroservice.DBContexts;
using StudentsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentsMicroservice.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext _dbContext;

        public StudentRepository(StudentContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteStudent(int studentId)
        {
            var student = _dbContext.Students.Find(studentId);
            _dbContext.Students.Remove(student);
            Save();
        }

        public Student GetStudentByID(int studentId)
        {
            return _dbContext.Students.Find(studentId);
        }


        public IEnumerable<Student> GetStudents()
        {
            return _dbContext.Students.ToList();
        }

        public void InsertStudent(Student student)
        {
            _dbContext.Add(student);
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            _dbContext.Entry(student).State = EntityState.Modified;
            Save();
        }
    }
}
