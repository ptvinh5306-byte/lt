using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement
{
    class Student
    {
        public string Name { get; set; }
        public int Semester { get; set; }
        public string CourseName { get; set; }

        public Student(string name, int semester, string courseName)
        {
            Name = name;
            Semester = semester;
            CourseName = courseName;
        }

        public override string ToString()
        {
            return $"{Name} | {CourseName} | Semester {Semester}";
        }
    }

    class Program
    {
        static List<Student> students = new List<Student>();

        static void Main(string[] args)
        {
            int choice;
            do
            {
                Console.WriteLine("\n===== MENU QUAN LY SINH VIEN =====");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Tim kiem sinh vien theo ten");
                Console.WriteLine("3. Sua thong tin sinh vien");
                Console.WriteLine("4. Xoa sinh vien");
                Console.WriteLine("5. Thong ke so lan dang ky");
                Console.WriteLine("0. Thoat");
                Console.Write("Nhap lua chon: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        SearchStudent();
                        break;
                    case 3:
                        UpdateStudent();
                        break;
                    case 4:
                        DeleteStudent();
                        break;
                    case 5:
                        Report();
                        break;
                    case 0:
                        Console.WriteLine("Thoat chuong trinh!");
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }

            } while (choice != 0);
        }

        static void AddStudent()
        {
            Console.Write("Nhap ten sinh vien: ");
            string name = Console.ReadLine();

            Console.Write("Nhap hoc ky: ");
            int semester = int.Parse(Console.ReadLine());

            Console.Write("Nhap ten khoa hoc (Java, .Net, C/C++): ");
            string course = Console.ReadLine();

            students.Add(new Student(name, semester, course));
            Console.WriteLine("Da them sinh vien thanh cong!");
        }

        static void SearchStudent()
        {
            Console.Write("Nhap ten sinh vien can tim: ");
            string name = Console.ReadLine();

            var result = students.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (result.Count > 0)
            {
                Console.WriteLine("Ket qua tim kiem:");
                foreach (var s in result)
                    Console.WriteLine(s);
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien!");
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Nhap ten sinh vien can sua: ");
            string name = Console.ReadLine();

            var student = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (student != null)
            {
                Console.Write("Nhap ten moi: ");
                student.Name = Console.ReadLine();

                Console.Write("Nhap hoc ky moi: ");
                student.Semester = int.Parse(Console.ReadLine());

                Console.Write("Nhap khoa hoc moi (Java, .Net, C/C++): ");
                student.CourseName = Console.ReadLine();

                Console.WriteLine("Cap nhat thong tin thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien!");
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Nhap ten sinh vien can xoa: ");
            string name = Console.ReadLine();

            var student = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Da xoa sinh vien!");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien!");
            }
        }

        static void Report()
        {
            Console.WriteLine("===== Thong ke so lan dang ky =====");
            var report = students
                .GroupBy(s => new { s.Name, s.CourseName })
                .Select(g => new { g.Key.Name, g.Key.CourseName, Count = g.Count() });

            foreach (var item in report)
            {
                Console.WriteLine($"{item.Name} | {item.CourseName} | {item.Count}");
            }
        }
    }
}
