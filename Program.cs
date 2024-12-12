using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;  // Needed for List<T>
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
                        Console.Write("Enter student name: ");
                        string studentName = Console.ReadLine();
                        
                        // Ensure task is not empty
                        if (!string.IsNullOrWhiteSpace(studentName))
                        {
                            Student newStudent = new Student(studentName);
                            studentList.Add(newStudent); 
                        }
                        else
                        {
                            Console.WriteLine("Student name cannot be empty.");
                        }
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
                                    Console.WriteLine($"{student.studentId}. {student.studentName}");
                                }

                                // 2. Get valid student ID
                                int studentId;
                                while (true)
                                {
                                    Console.Write("Enter the ID of the student to add a grade to: ");
                                    if (int.TryParse(Console.ReadLine(), out studentId) && studentList.Any(s => s.studentId == studentId))
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
                                    subjectName = Console.ReadLine();
                                    
                                    // Ensure task is not empty
                                    if (!string.IsNullOrWhiteSpace(subjectName))
                                    {
                                        break; // Valid grade entered
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
                                Student selectedStudent = studentList.First(s => s.studentId == studentId);
                                selectedStudent.studentGrades.Add(new Grade(gradeScore, subjectName));

                                Console.WriteLine($"Grade added successfully for {selectedStudent.studentName}"); Console.WriteLine("");
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
                                double averageGrade = student.studentGrades.Count > 0 ? 
                                    student.studentGrades.Average(g => g.gradeScore) 
                                    : 0;
                                string gradeText = averageGrade == 0 ? "No Grades Entered" : "Average Grade: " + averageGrade.ToString("F0") + "%";
                                Console.WriteLine($"ID: {student.studentId} -- Student Name: {student.studentName} -- {gradeText}");
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
                                Console.WriteLine($"ID: {student.studentId} -- Student Name:{student.studentName}");
                                if (student.studentGrades.Count == 0)
                                {
                                    Console.WriteLine($"     No Grades Entered");
                                }
                                else
                                {
                                    // write all student subjects and grades
                                    foreach (Grade grade in student.studentGrades)
                                    {
                                        Console.WriteLine($"     Subject: {grade.gradeSubject} -- Grade: {grade.gradeScore}%");
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

    class Student
    {
        private static int idCounter = 1;
        public int studentId { get; private set;}
        public string studentName { get; private set;}
        public List<Grade> studentGrades { get; private set;}

        public Student(string studentName)
        {
            this.studentId = idCounter++;
            this.studentName = studentName;
            this.studentGrades = new List<Grade>();
        }
    }

    class Grade {
        public int gradeScore { get; private set;}
        public string gradeSubject { get; private set;}

        public Grade(int gradeScore, string gradeSubject)
        {
            this.gradeScore = gradeScore;
            this.gradeSubject = gradeSubject;
        }
    }
}