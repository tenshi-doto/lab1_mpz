using System;
using System.Collections.Generic;
using System.Linq; // Потрібно для FirstOrDefault в репозиторії

namespace lab1_Bilous
{
    public class Adress
    {
        public int ID { get; set; }
        public string StudentAddress { get; set; }

        // Ключ для звязку 1-до-1 з Student
        public int StudentID { get; set; }
        // Навігаційна властивість
        public Student Student { get; set; }
    }
    
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        // Ключ для зв'язку "багато-до-1" з Group
        public int GroupID { get; set; }
        // Навігаційна властивість
        public Group Group { get; set; }

        // Навігаційна властивість для зв'язку 1-до-1
        public Adress Adress { get; set; }
    }
    
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }

        // Навігаційна властивість для зв'язку 1-до-багатьох
        public List<Student> Students { get; set; } = new List<Student>();

        // Навігаційна властивість для зв'язку багато-до-багатьох
        public List<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();
    }
    
    public class Subject
    {
        public int ID { get; set; }
        public string SubjectName { get; set; }
        public int Hours { get; set; }

        // Навігаційна властивість для звязку багато-до-багатьох
        public List<GroupSubject> GroupSubjects { get; set; } = new List<GroupSubject>();
    }
    
    public class GroupSubject
    {
        public int ID { get; set; }

        // Ключ для зв'язку з Group
        public int GroupID { get; set; }
        public Group Group { get; set; }

        // Ключ для зв'язку з Subject
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
    }

    // КЛАС-РЕПОЗИТОРІЙ 
    
    public class StudentRepository
    {
        private List<Student> _students = new List<Student>();
        private int _nextId = 1;
        
        public Student AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student), "Студент не може бути null");
            }
            student.ID = _nextId++;
            _students.Add(student);
            Console.WriteLine($"[Репозиторій] Додано студента: {student.Name} {student.Surname} (ID: {student.ID})");
            return student;
        }
        
        public bool DeleteStudent(int studentId)
        {
            Student studentToRemove = GetStudentById(studentId);
            if (studentToRemove != null)
            {
                _students.Remove(studentToRemove);
                Console.WriteLine($"[Репозиторій] Видалено студента: {studentToRemove.Name} (ID: {studentId})");
                return true;
            }
            Console.WriteLine($"[Репозиторій] Помилка: Студента з ID {studentId} не знайдено.");
            return false;
        }

     
        public Student GetStudentById(int studentId)
        {
            return _students.FirstOrDefault(s => s.ID == studentId);
        }

        
        public List<Student> GetAllStudents()
        {
            return _students;
        }
    }
    
    public class Program
    {
        // Це точка входу вашої програми
        public static void Main(string[] args)
        {
            // Створюємо репозиторій
            StudentRepository studentRepo = new StudentRepository();

            // Створюємо та додаємо студентів
            Console.WriteLine("--- Додавання студентів ---");
            Student student1 = new Student { Name = "Іван", Surname = "Петренко", Age = 20, GroupID = 1 };
            Student student2 = new Student { Name = "Марія", Surname = "Коваль", Age = 19, GroupID = 1 };
            Student student3 = new Student { Name = "Андрій", Surname = "Шевченко", Age = 21, GroupID = 2 };

            studentRepo.AddStudent(student1);
            studentRepo.AddStudent(student2);
            studentRepo.AddStudent(student3);

            // Вивід списку
            Console.WriteLine("\n--- Повний список студентів ---");
            List<Student> allStudents = studentRepo.GetAllStudents();
            foreach (var s in allStudents)
            {
                Console.WriteLine($"- ID: {s.ID}, Ім'я: {s.Name} {s.Surname}, Вік: {s.Age}");
            }

            // Пошук студента
            Console.WriteLine("\n--- Пошук студента з ID 2 ---");
            Student foundStudent = studentRepo.GetStudentById(2);
            if (foundStudent != null)
            {
                Console.WriteLine($"Знайдено: {foundStudent.Name} {foundStudent.Surname}");
            }
            else
            {
                Console.WriteLine("Студента не знайдено.");
            }

            // Видалення студента
            Console.WriteLine("\n--- Видалення студента з ID 1 ---");
            studentRepo.DeleteStudent(1);

            // Повторний вивід списку (перевірка)
            Console.WriteLine("\n--- Оновлений список студентів ---");
            allStudents = studentRepo.GetAllStudents(); // Отримуємо список знову
            foreach (var s in allStudents)
            {
                Console.WriteLine($"- ID: {s.ID}, Ім'я: {s.Name} {s.Surname}, Вік: {s.Age}");
            }
            
            // Спроба знайти видаленого студента
            Console.WriteLine("\n--- Пошук студента з ID 1 (після видалення) ---");
            Student deletedStudent = studentRepo.GetStudentById(1);
            if (deletedStudent == null)
            {
                Console.WriteLine("Студента з ID 1 коректно не знайдено.");
            }
        }
    }
}