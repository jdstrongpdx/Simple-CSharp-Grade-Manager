using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Stores a list of all Student data
        List<Student> studentList = new List<Student>();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n1. Add a New Student");
            Console.WriteLine("2. Assign Grades to a Student");
            Console.WriteLine("3. List Average Grades of All Students");
            Console.WriteLine("4. Display All Student Records and Grades");
            Console.WriteLine("5. Exit");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        // ADD A NEW STUDENT
                        Console.Write("Enter a Student Name: ");
                        string studentName;
                        while (true)
                        {
                            studentName = Console.ReadLine() ?? "";
                            // Ensure subject is not empty
                            if (!string.IsNullOrWhiteSpace(studentName))
                            {
                                break; // Valid student entered
                            }
                            Console.WriteLine("Student name cannot be empty.");
                        }
                        Student newStudent = new Student(studentName);
                        studentList.Add(newStudent);
                        break;

                    case 2:
                        // ASSIGN GRADES TO A STUDENT
                        // 0. Check that students exist, else exit.
                        if (studentList.Count == 0)
                        {
                            Console.WriteLine("No Students Exist");
                            break;
                        }
                        // 1. List student IDs and names
                                Console.WriteLine("Available Students:");
                                foreach (Student student in studentList)
                                {
                                    Console.WriteLine($"{student.StudentId}. {student.StudentName}");
                                }

                                // 2. Get valid student ID
                                int studentId;
                                while (true)
                                {
                                    Console.Write("Enter the ID of the student to add a grade to: ");
                                    if (int.TryParse(Console.ReadLine(), out studentId) && studentList.Any(s => s.StudentId == studentId))
                                    {
                                        break; // Valid ID entered
                                    }

                                    Console.WriteLine("Invalid student ID. Please try again.");
                                }

                                // 3. Get subject name
                                Console.Write("Enter the Subject Name: ");
                                string subjectName;
                                while (true)
                                {
                                    subjectName = Console.ReadLine() ?? "";
                                    // Ensure subject is not empty
                                    if (!string.IsNullOrWhiteSpace(subjectName))
                                    {
                                        break; // Valid subject entered
                                    }
                                    Console.WriteLine("Subject name cannot be empty.");
                                }

                                // 4. Get valid grade (0-100)
                                int gradeScore;
                                while (true)
                                {
                                    Console.Write("Enter the grade (0-100): ");
                                    if (int.TryParse(Console.ReadLine(), out gradeScore) && gradeScore >= 0 && gradeScore <= 100)
                                    {
                                        break; // Valid grade entered
                                    }

                                    Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
                                }

                                // 5. Add grade to student's grade list
                                Student selectedStudent = studentList.First(s => s.StudentId == studentId);
                                selectedStudent.StudentGrades.Add(new Grade(gradeScore, subjectName));

                                Console.WriteLine($"Grade added successfully for {selectedStudent.StudentName}"); Console.WriteLine("");
                        break;

                    case 3:
                        // LIST AVERAGE GRADES OF ALL STUDENTS
                        if (studentList.Count == 0)
                        {
                            Console.WriteLine("No Students have been entered.");
                        }
                        else
                        {
                            foreach (Student student in studentList)
                            {
                                // ternary grade evaluation
                                double averageGrade = student.StudentGrades.Count > 0 ? 
                                    student.StudentGrades.Average(g => g.GradeScore) 
                                    : 0;
                                string gradeText = averageGrade == 0 ? "No Grades Entered" : "Average Grade: " + averageGrade.ToString("F0") + "%";
                                Console.WriteLine($"ID: {student.StudentId} -- Student Name: {student.StudentName} -- {gradeText}");
                            }
                        }
                        break;

                    case 4:
                        // DISPLAY ALL STUDENT NAMES AND GRADES
                        if (studentList.Count == 0)
                        {
                            Console.WriteLine("No Students have been entered.");
                        }
                        else
                        {
                            foreach (Student student in studentList)
                            {
                                // write student name
                                Console.WriteLine($"ID: {student.StudentId} -- Student Name: {student.StudentName}");
                                if (student.StudentGrades.Count == 0)
                                {
                                    Console.WriteLine($"     No Grades Entered");
                                }
                                else
                                {
                                    // write all student subjects and grades
                                    foreach (Grade grade in student.StudentGrades)
                                    {
                                        Console.WriteLine($"     Subject: {grade.GradeSubject} -- Grade: {grade.GradeScore}%");
                                    }
                                }
                            }
                        }
                        break;
                    case 5:
                        // EXIT PROGRAM
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid choice.");
            }
       }
    }

    class Student(string studentName)
    {
        private static int idCounter = 1;
        public int StudentId { get; private set; } = idCounter++;
        public string StudentName { get; private set; } = studentName;
        public List<Grade> StudentGrades { get; private set; } = new List<Grade>();
    }

    class Grade(int gradeScore, string gradeSubject)
    {
        public int GradeScore { get; private set; } = gradeScore;
        public string GradeSubject { get; private set; } = gradeSubject;
    }
}