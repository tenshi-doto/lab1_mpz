using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversityApp
{

    public class Address
    {
        public int Id { get; set; }
        public string StudentAddress { get; set; }
        public int StudentId { get; set; }

        public override string ToString() => $"ID: {Id}, Адреса: {StudentAddress}";
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"Група: {Name} (ID: {Id})";
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public int GroupId { get; set; }

        public override string ToString()
        {
            return $"Студент: {Surname} {Name}, Вік: {Age}, GroupID: {GroupId}";
        }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public int Hours { get; set; }

        public override string ToString() => $"Предмет: {SubjectName}, Годин: {Hours}";
    }

    public class GroupSubject
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
    }
    
    public class StudentRepository
    {
        private readonly List<Student> _students = new List<Student>();

        public void Add(Student student)
        {
            if (_students.Any(s => s.Id == student.Id))
            {
                Console.WriteLine($"Помилка: Студент з ID {student.Id} вже існує.");
                return;
            }
            _students.Add(student);
            Console.WriteLine($"Студента {student.Surname} {student.Name} успішно додано.");
        }

        public void Delete(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _students.Remove(student);
                Console.WriteLine($"Студента з ID {id} видалено.");
            }
            else
            {
                Console.WriteLine($"Помилка: Студента з ID {id} не знайдено.");
            }
        }

        public Student FindBySurname(string surname)
        {
            return _students.FirstOrDefault(s => s.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase));
        }

        public void PrintAll()
        {
            Console.WriteLine("\n--- Список студентів ---");
            foreach (var student in _students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine("------------------------\n");
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            StudentRepository repository = new StudentRepository();
            
            Group groupPd = new Group { Id = 32, Name = "ПД-32" };
            
            Student s1 = new Student { Id = 1, Name = "Назар", Surname = "Білоус", Age = 19, GroupId = groupPd.Id };
            Student s2 = new Student { Id = 2, Name = "Богдан", Surname = "Худік", Age = 20, GroupId = groupPd.Id };
            Student s3 = new Student { Id = 3, Name = "Микита", Surname = "Треньов", Age = 19, GroupId = groupPd.Id };
            
            Console.WriteLine(">>> Додавання студентів:");
            repository.Add(s1);
            repository.Add(s2);
            repository.Add(s3);
            
            repository.PrintAll();
            
            Console.WriteLine(">>> Пошук студента за прізвищем 'Худік':");
            var foundStudent = repository.FindBySurname("Худік");
            if (foundStudent != null)
            {
                Console.WriteLine($"Знайдено: {foundStudent}");
            }
            
            Console.WriteLine("\n>>> Видалення студента з ID 1 (Білоус):");
            repository.Delete(1);
            
            repository.PrintAll();
            
            Console.ReadLine(); 
        }
    }
}

