using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuanLysinhvien
{
    public class StudentRepository
    {
        private readonly string filePath = "students.txt";
        private List<Student> students = new();
        private int nextId = 1;

        public StudentRepository()
        {
            LoadFromFile();
            if (students.Count > 0)
                nextId = students.Max(s => s.Id) + 1;
        }

        public List<Student> GetAll() => new(students);

        public Student Add(Student student)
        {
            student.Id = nextId++;
            students.Add(student);
            SaveToFile();
            return student;
        }

        public bool Update(Student student)
        {
            var idx = students.FindIndex(s => s.Id == student.Id);
            if (idx == -1) return false;
            students[idx] = student;
            SaveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            var s = students.FirstOrDefault(x => x.Id == id);
            if (s == null) return false;
            students.Remove(s);
            SaveToFile();
            return true;
        }

        public List<Student> Search(string keyword)
        {
            keyword = keyword?.ToLower() ?? "";
            return students.Where(s =>
                s.Id.ToString() == keyword ||
                s.Name.ToLower().Contains(keyword) ||
                s.Address.ToLower().Contains(keyword) ||
                s.Grade.ToString() == keyword
            ).ToList();
        }

        public Student GetById(int id) => students.FirstOrDefault(s => s.Id == id);

        private void LoadFromFile()
        {
            students.Clear();
            if (!File.Exists(filePath)) return;
            foreach (var line in File.ReadAllLines(filePath))
            {
                var s = Student.FromFileString(line);
                if (s != null) students.Add(s);
            }
        }

        private void SaveToFile()
        {
            File.WriteAllLines(filePath, students.Select(s => s.ToFileString()));
        }
    }
}
