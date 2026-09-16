using System.Collections.Generic;

namespace QuanLysinhvien
{
    public class StudentService
    {
        private readonly StudentRepository repo = new();

        public List<Student> GetAll() => repo.GetAll();

        public Student Add(string name, string email, string address, int age, double grade)
        {
            var s = new Student
            {
                Name = name,
                Email = email,
                Address = address,
                Age = age,
                Grade = grade
            };
            return repo.Add(s);
        }

        public bool Update(int id, string name, string email, string address, int age, double grade)
        {
            var s = repo.GetById(id);
            if (s == null) return false;
            s.Name = name;
            s.Email = email;
            s.Address = address;
            s.Age = age;
            s.Grade = grade;
            return repo.Update(s);
        }

        public bool Delete(int id) => repo.Delete(id);

        public List<Student> Search(string keyword) => repo.Search(keyword);

        public Student GetById(int id) => repo.GetById(id);
    }
}
