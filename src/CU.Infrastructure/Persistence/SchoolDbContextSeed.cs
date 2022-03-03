using Ardalis.GuardClauses;
using ContosoUniversity.Models;
using CU.Application.Common.Interfaces;

namespace CU.Infrastructure.Persistence
{
    internal class SchoolDbContextSeed
    {
        public static async Task<int> SeedDefaultDataAsync(ISchoolDbContext context)
        {
            Guard.Against.Null(context, nameof(context));

            int saveCount = 0;

            bool haveStudents = false;
            if (context.Students.Any())
            {
                haveStudents = true;
            }
            else
            {
                var students = new List<Student>
                {
                    new Student("Alexander", "Carson",DateTime.Parse("2010-09-01")),
                    new Student("Alonso", "Meredith", DateTime.Parse("2020-09-01")),
                    new Student("Anand", "Arturo", DateTime.Parse("2021-09-01")),
                    new Student("Barzdukas", "Gytis", DateTime.Parse("2020-09-01")),
                    new Student("Li", "Yan", DateTime.Parse("2020-09-01")),
                    new Student("Justice", "Peggy", DateTime.Parse("2019-09-01")),
                    new Student("Norman", "Laura", DateTime.Parse("2021-09-01")),
                    new Student("Olivetto", "Nino", DateTime.Parse("2016-08-11"))
                };
                await context.Students.AddRangeAsync(students);
                saveCount += await context.SaveChangesAsync(new CancellationToken());
                haveStudents = true;
            }
            bool haveInstructors = false;
            if (context.Instructors.Any())
            {
                haveInstructors = true;
            }
            else
            {
                var instructors = new List<Instructor>
                {
                    new Instructor("Abercrombie", "Kim", DateTime.Parse("1995-03-11")),
                    new Instructor("Fakhouri", "Fadi", DateTime.Parse("2002-07-06")),
                    new Instructor("Harui", "Roger", DateTime.Parse("1998-07-01")),
                    new Instructor("Kapoor", "Candace", DateTime.Parse("2001-01-15")),
                    new Instructor("Zheng", "Roger", DateTime.Parse("2004-02-12"))
                };
                await context.Instructors.AddRangeAsync(instructors);
                saveCount += await context.SaveChangesAsync(new CancellationToken());
                haveInstructors = true;

                var departments = new List<Department>
                {
                    new Department("English", 350000, DateTime.Parse("2007-09-01")) 
                    {
                        InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID
                    },
                    new Department("Mathematics", 100000, DateTime.Parse("2007-09-01"))
                    {
                        InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID
                    },
                    new Department("Engineering", 350000, DateTime.Parse("2007-09-01"))
                    {
                        InstructorID  = instructors.Single( i => i.LastName == "Harui").ID
                    },
                    new Department("Economics", 100000, DateTime.Parse("2007-09-01"))
                    {
                        InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID
                    }
                };
                await context.Departments.AddRangeAsync(departments);
                saveCount += await context.SaveChangesAsync(new CancellationToken());

                var instructor_Fak = instructors.Single(i => i.LastName == "Fakhouri");
                var instructor_Har = instructors.Single(i => i.LastName == "Harui");
                var instructor_Kap = instructors.Single(i => i.LastName == "Kapoor");
                var officeAssignments = new List<OfficeAssignment>
                {
                    new OfficeAssignment(instructor_Fak, "Smith 17"),
                    new OfficeAssignment(instructor_Har, "Gowan 27"),
                    new OfficeAssignment(instructor_Kap, "Thompson 304")
                };
                await context.OfficeAssignments.AddRangeAsync(officeAssignments);
                saveCount += await context.SaveChangesAsync(new CancellationToken());
            }
            bool haveCourses = false;
            if (context.Courses.Any())
            {
                haveCourses = true;
            }
            else
            {
                var dept_Engineering = context.Departments.Single(s => s.Name == "Engineering");
                var dept_Economics = context.Departments.Single(s => s.Name == "Economics");
                var dept_Math = context.Departments.Single(s => s.Name == "Mathematics");
                var dept_English = context.Departments.Single(s => s.Name == "English");
                var courses = new List<Course>
                {
                    new Course(1050, "Chemistry", dept_Engineering) { Credits = 3 },
                    new Course(4022, "Microeconomics", dept_Economics) { Credits = 3 },
                    new Course(4041, "Macroeconomics", dept_Economics) { Credits = 3 },
                    new Course(1045, "Calculus", dept_Math) { Credits = 4 },
                    new Course(3141, "Trigonometry", dept_Math) { Credits = 4 },
                    new Course(2021, "Composition", dept_English) { Credits = 3 },
                    new Course(2042, "Literature", dept_English) { Credits = 4 }
                };
                await context.Courses.AddRangeAsync(courses);
                saveCount += await context.SaveChangesAsync(new CancellationToken());
                haveCourses = true;
            }

            if (haveCourses && haveStudents)
            {
                if (!context.Enrollments.Any())
                {
                    Course chemistry = context.Courses.Single(c => c.Title == "Chemistry");
                    Course composition = context.Courses.Single(c => c.Title == "Composition");
                    Course microeconomics = context.Courses.Single(c => c.Title == "Microeconomics");
                    Course macroeconomics = context.Courses.Single(c => c.Title == "Macroeconomics");

                    Student alexander = context.Students.Single(s => s.LastName == "Alexander");
                    Student alonso = context.Students.Single(s => s.LastName == "Alonso");
                    Student anand = context.Students.Single(s => s.LastName == "Anand");

                    List<Enrollment> enrollments = new List<Enrollment>
                    {
                        new Enrollment(chemistry, alexander) {Grade = Grade.A},
                        new Enrollment(microeconomics, alexander) {Grade = Grade.C},
                        new Enrollment(macroeconomics, alexander) {Grade = Grade.B},
                        new Enrollment(context.Courses.Single(c => c.Title == "Calculus" ), alonso) {Grade = Grade.B},
                        new Enrollment(context.Courses.Single(c => c.Title == "Trigonometry" ), alonso) {Grade = Grade.B},
                        new Enrollment(composition, alonso) {Grade = Grade.B},
                        new Enrollment(chemistry, anand),
                        new Enrollment(microeconomics, anand) {Grade = Grade.B},
                        new Enrollment(chemistry, context.Students.Single(s => s.LastName == "Barzdukas")) {Grade = Grade.B},
                        new Enrollment(composition, context.Students.Single(s => s.LastName == "Li")) {Grade = Grade.B},
                        new Enrollment(context.Courses.Single(c => c.Title == "Literature"),
                            context.Students.Single(s => s.LastName == "Justice")) {Grade = Grade.B}
                    };
                    await context.Enrollments.AddRangeAsync(enrollments);
                    saveCount += await context.SaveChangesAsync(new CancellationToken());
                }
            }

            if (haveCourses && haveInstructors)
            {
                int coursesWithInstructorsCount = context.Courses
                    .Where(c => c.Instructors.Count > 0)
                    .Count();

                if (coursesWithInstructorsCount == 0)
                {
                    int courseInstructorAddCount = 0;
                    if (AddInstructorToCourse(context, "Chemistry", "Kapoor")) { courseInstructorAddCount++; }
                    if (AddInstructorToCourse(context, "Chemistry", "Harui")) { courseInstructorAddCount++; }
                    if (AddInstructorToCourse(context, "Microeconomics", "Zheng")) { courseInstructorAddCount++; }
                    if (AddInstructorToCourse(context, "Macroeconomics", "Zheng")) { courseInstructorAddCount++; }

                    if (AddInstructorToCourse(context, "Calculus", "Fakhouri")) { courseInstructorAddCount++; }
                    if (AddInstructorToCourse(context, "Trigonometry", "Harui")) { courseInstructorAddCount++; }
                    if (AddInstructorToCourse(context, "Composition", "Abercrombie")) { courseInstructorAddCount++; }
                    if (AddInstructorToCourse(context, "Literature", "Abercrombie")) { courseInstructorAddCount++; }

                    if (courseInstructorAddCount > 0)
                    {
                        saveCount += await context.SaveChangesAsync(new CancellationToken());
                    }
                }
            }
            return saveCount;
        }

        static bool AddInstructorToCourse(ISchoolDbContext context, string courseTitle, string instructorName)
        {
            var course = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            if (course != null)
            {
                var instructor = course.Instructors.SingleOrDefault(i => i.LastName == instructorName);
                if (instructor == null)
                {
                    course.Instructors.Add(context.Instructors.Single(i => i.LastName == instructorName));
                    return true;
                }
            }
            return false;
        }
    }
}
