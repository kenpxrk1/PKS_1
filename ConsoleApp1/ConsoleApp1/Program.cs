using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

public class Student
{
    public required string student_code { get; set; }
    public required string last_name { get; set; }
    public required string first_name { get; set; }
    public required string middle_name { get; set; }
}

public class Subject
{
    public required string subject_code { get; set; }
    public required string name { get; set; }

}

public class Curriculum
{
    public required string student_code { get; set; }
    public required string subject_code { get; set; }
    public int grade { get; set; }
}

public class SchoolData
{
    public required List<Student> students { get; set; }
    public required List<Subject> subjects { get; set; }
    public required List<Curriculum> curriculum { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Чтение данных из JSON-файла
        string json = File.ReadAllText("C:\\Users\\blood\\OneDrive\\Рабочий стол\\progks\\ConsoleApp1\\ConsoleApp1\\data.json");

        


        SchoolData? schoolData = JsonSerializer.Deserialize<SchoolData>(json);


        void addGrade(string studentCode, string subjectCode, int grade)
        {
            Curriculum newGrade = new Curriculum
            {
                student_code = studentCode,
                subject_code = subjectCode,
                grade = grade
            };
            schoolData?.curriculum.Add(newGrade);
            // Сохранение данных в JSON-файл
            string newJson = JsonSerializer.Serialize(schoolData);
            File.WriteAllText("C:\\Users\\blood\\OneDrive\\Рабочий стол\\progks\\ConsoleApp1\\ConsoleApp1\\data.json", newJson);
        }


        void showGrades(string studentCode)
        {
            var studentGrades = from sp in schoolData?.curriculum
                                join s in schoolData?.subjects on sp.subject_code equals s.subject_code
                                where sp.student_code == "001"
                                select new { SubjectName = s.name, Grade = sp.grade };

            int totalGrades = studentGrades.Count();
            int excellentPercentage = studentGrades.Count(g => g.Grade == 5) * 100 / totalGrades;
            int goodPercentage = studentGrades.Count(g => g.Grade == 4) * 100 / totalGrades;
            int satisfactoryPercentage = studentGrades.Count(g => g.Grade == 3) * 100 / totalGrades;

            foreach (var grade in studentGrades)
            {
                Console.WriteLine($"Предмет: {grade.SubjectName}, Оценка: {grade.Grade}");
            }

            Console.WriteLine($"Процент отличных оценок от общего кол-ва: {excellentPercentage}%");
            Console.WriteLine($"Процент хороших оценок от общего кол-ва: {goodPercentage}%");
            Console.WriteLine($"Процент удовлетворительных оценок от общего кол-ва: {satisfactoryPercentage}%");
        }
        showGrades("003");
    }
}
    
