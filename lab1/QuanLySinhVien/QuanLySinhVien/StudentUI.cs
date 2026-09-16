using System;

namespace QuanLysinhvien
{
    public class StudentUI
    {
        private readonly StudentService service = new();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowMenu();
                Console.Write("Chọn: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ShowAll(); break;
                    case "2": Add(); break;
                    case "3": Edit(); break;
                    case "4": Delete(); break;
                    case "5": Search(); break;
                    case "0": return;
                    default: Console.WriteLine("Lựa chọn không hợp lệ!"); break;
                }
                Console.WriteLine("Nhấn Enter để tiếp tục...");
                Console.ReadLine();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== QUẢN LÝ SINH VIÊN ===");
            Console.WriteLine("1. Hien thi danh sach");
            Console.WriteLine("2. Them sinh vien");
            Console.WriteLine("3. Sua sinh vien");
            Console.WriteLine("4. Xoa sinh vien");
            Console.WriteLine("5. Tim kiem");
            Console.WriteLine("0. Thoat");
        }

        private void ShowAll()
        {
            var list = service.GetAll();
            Console.WriteLine($"{"Id",-5} {"Name",-20} {"Email",-25} {"Address",-20} {"Age",-5} {"Grade",-5}");
            foreach (var s in list)
                Console.WriteLine(s);
            if (list.Count == 0)
                Console.WriteLine("Chưa có sinh viên nào.");
        }

        private void Add()
        {
            Console.Write("Tên: "); var name = Console.ReadLine();
            Console.Write("Email: "); var email = Console.ReadLine();
            Console.Write("Địa chỉ: "); var address = Console.ReadLine();
            Console.Write("Tuổi: "); int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Điểm: "); double.TryParse(Console.ReadLine(), out double grade);
            service.Add(name, email, address, age, grade);
        }

        private void Edit()
        {
            Console.Write("Nhập Id cần sửa: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;
            var s = service.GetById(id);
            if (s == null) { Console.WriteLine("Không tìm thấy!"); return; }
            Console.Write($"Tên ({s.Name}): "); var name = Console.ReadLine();
            Console.Write($"Email ({s.Email}): "); var email = Console.ReadLine();
            Console.Write($"Địa chỉ ({s.Address}): "); var address = Console.ReadLine();
            Console.Write($"Tuổi ({s.Age}): "); int.TryParse(Console.ReadLine(), out int age);
            Console.Write($"Điểm ({s.Grade}): "); double.TryParse(Console.ReadLine(), out double grade);
            service.Update(id,
                string.IsNullOrWhiteSpace(name) ? s.Name : name,
                string.IsNullOrWhiteSpace(email) ? s.Email : email,
                string.IsNullOrWhiteSpace(address) ? s.Address : address,
                age == 0 ? s.Age : age,
                grade == 0 ? s.Grade : grade
            );
        }

        private void Delete()
        {
            Console.Write("Nhập Id cần xoá: ");
            if (int.TryParse(Console.ReadLine(), out int id))
                service.Delete(id);
        }

        private void Search()
        {
            Console.Write("Nhập từ khoá (Id, Tên, Địa chỉ, Điểm): ");
            var keyword = Console.ReadLine();
            var list = service.Search(keyword);
            Console.WriteLine($"{"Id",-5} {"Name",-20} {"Email",-25} {"Address",-20} {"Age",-5} {"Grade",-5}");
            foreach (var s in list)
                Console.WriteLine(s);
            if (list.Count == 0)
                Console.WriteLine("Không tìm thấy sinh viên nào.");
        }
    }
}
