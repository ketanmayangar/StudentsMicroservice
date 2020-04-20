using StudentsMicroservice.Models;
using System.Collections.Generic;

namespace StudentsMicroservice.Repository
{
  public interface IStudentRepository
  {
    IEnumerable<Student> GetStudents();
    Student GetStudentByID(int student);
    void InsertStudent(Student student);
    void DeleteStudent(int studenttId);
    void UpdateStudent(Student student);
    void Save();
  }
}
