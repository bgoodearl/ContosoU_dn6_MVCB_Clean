using Ardalis.GuardClauses;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.ViewModels;
using CU.Application.Shared.ViewModels.Courses;
using CU.Application.Shared.ViewModels.Departments;
using CU.Application.Shared.ViewModels.Instructors;
using CU.Application.Shared.ViewModels.Students;
using Microsoft.EntityFrameworkCore;
using CM = ContosoUniversity.Models;

namespace CU.Infrastructure.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private ISchoolDbContext SchoolDbContext { get; }

        public SchoolRepository(ISchoolDbContext schoolDbContext)
        {
            Guard.Against.Null(schoolDbContext, nameof(schoolDbContext));
            SchoolDbContext = schoolDbContext;
        }

        #region ISchoolRepository

        public async Task<CourseActionResult> AddNewCourseAsync(CourseEditDto course)
        {
            Guard.Against.Null(course, nameof(course));

            CourseActionResult result = new CourseActionResult
            {
                Action = "AddNewCourseAsync",
                CourseID = course.CourseID
            };

            CM.Department? department = null;
            if (course.DepartmentID != 0)
            {
                department = SchoolDbContext.Departments.Where(d => d.DepartmentID == course.DepartmentID).FirstOrDefault();
            }

            if (department == null)
            {
                result.ErrorMessage = $"Department {course.DepartmentID} not found";
            }
            else
            {
                CM.Course persistentCourse = new CM.Course(course.CourseID, course.Title, department)
                {
                    Credits = course.Credits
                };
                SchoolDbContext.Courses.Add(persistentCourse);
                result.ChangeCount = await SaveChangesAsync();
                result.CourseIDNew = persistentCourse.CourseID;
            }
            return result;
        }

        public async Task<CourseEditDto?> GetCourseEditDtoNoTrackingAsync(int courseID)
        {
            CourseEditDto? course = await SchoolDbContext.Courses
                .AsNoTracking()
                .Where(c => c.CourseID == courseID)
                .Select(c => new CourseEditDto
                {
                    CourseID = c.CourseID,
                    Credits = c.Credits,
                    DepartmentID = c.DepartmentID,
                    Title = c.Title
                })
                .SingleOrDefaultAsync();

            return course;
        }

        public async Task<List<IdItem>> GetCourseInstructorsNoTrackingAsync(int courseID)
        {
            CM.Course? course = await SchoolDbContext.Courses
                .Include(c => c.Instructors)
                .AsNoTracking()
                .Where(c => c.CourseID == courseID)
                .SingleOrDefaultAsync();
            if (course != null)
            {
                List<IdItem> instructorList = course.Instructors
                    .OrderBy(i => i.FullName)
                    .Select(i => new IdItem
                    {
                        Id = i.ID,
                        Name = i.FullName
                    })
                    .ToList();
                return instructorList;
            }
            return new List<IdItem>();
        }

        public async Task<CourseListItem?> GetCourseListItemNoTrackingAsync(int courseID)
        {
            CourseListItem? course = await SchoolDbContext.Courses
                .AsNoTracking()
                .Where(c => c.CourseID == courseID)
                .Select(c => new CourseListItem
                {
                    CourseID = c.CourseID,
                    Credits = c.Credits,
                    Department = c.Department.Name,
                    Title = c.Title
                })
                .SingleOrDefaultAsync();
            return course;
        }

        public async Task<List<CourseListItem>> GetCourseListItemsNoTrackingAsync()
        {
            List<CourseListItem> courses = await SchoolDbContext.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .OrderBy(c => c.CourseID)
                .Select(c => new CourseListItem
                {
                    CourseID = c.CourseID,
                    Credits = c.Credits,
                    Department = c.Department.Name,
                    Title = c.Title
                })
                .ToListAsync();

            return courses;
        }

        public async Task<List<DepartmentListItem>> GetDepartmentListItemsNoTrackingAsync()
        {
            List<DepartmentListItem> departments = await SchoolDbContext.Departments
                .Include(d => d.Administrator)
                .AsNoTracking()
                .OrderBy(d => d.Name)
                .Select(d => new DepartmentListItem
                {
                    Administrator = d.Administrator != null ? d.Administrator.LastName + ", " + d.Administrator.FirstMidName : string.Empty,
                    Budget = d.Budget,
                    DepartmentID = d.DepartmentID,
                    InstructorID = d.InstructorID,
                    Name = d.Name,
                    StartDate = d.StartDate
                })
                .ToListAsync();
            return departments;
        }

        public IQueryable<CM.Department> GetDepartmentsQueryable()
        {
            return SchoolDbContext.Departments;
        }

        public async Task<List<InstructorListItem>> GetInstructorListItemsNoTrackingAsync()
        {
            List<InstructorListItem> instructors = await SchoolDbContext.Instructors
                .Include(i => i.OfficeAssignment)
                .OrderBy(i => i.LastName)
                .ThenBy(i => i.FirstMidName)
                .Select(i => new InstructorListItem
                {
                    ID = i.ID,
                    FirstMidName = i.FirstMidName,
                    HireDate = i.HireDate,
                    LastName = i.LastName,
                    OfficeAssignment = i.OfficeAssignment != null ? i.OfficeAssignment.Location : string.Empty,
                })
                .ToListAsync();

            return instructors;
        }

        public IQueryable<CM.Course> GetCoursesQueryable()
        {
            return (IQueryable<CM.Course>)SchoolDbContext.Courses;
        }

        public async Task<CM.Department?> GetDepartmentById(int id)
        {
            return await SchoolDbContext.Departments
                .Where(d => d.DepartmentID == id)
                .SingleOrDefaultAsync();
        }

        public async Task<List<IdItem>> GetDepartmentsListAsync()
        {
            List<IdItem> departmentList = await SchoolDbContext.Departments
                .OrderBy(x => x.Name)
                .Select(x => new IdItem
                {
                    Id = x.DepartmentID,
                    Name = x.Name
                })
                .ToListAsync();
            return departmentList;
        }

        public async Task<List<StudentListItem>> GetStudentListItemsNoTrackingAsync()
        {
            List<StudentListItem> students = await SchoolDbContext.Students
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstMidName)
                .Select(s => new StudentListItem
                {
                    ID = s.ID,
                    EnrollmentDate = s.EnrollmentDate,
                    FirstMidName = s.FirstMidName,
                    LastName = s.LastName
                })
                .ToListAsync();

            return students;
        }

        public object RemoveCourse(CM.Course course)
        {
            return SchoolDbContext.Courses.Remove(course);
        }

        public async Task<CourseActionResult> SaveCourseChangesAsync(CourseEditDto course)
        {
            Guard.Against.Null(course, nameof(course));
            Guard.Against.Zero(course.CourseID, nameof(course.CourseID));

            CourseActionResult result = new CourseActionResult
            {
                Action = "SaveCourseChangesAsync",
                CourseID = course.CourseID
            };

            CM.Course? dbCourse = await SchoolDbContext.Courses
                .Where(c => c.CourseID == course.CourseID)
                .SingleOrDefaultAsync();
            if (dbCourse == null)
            {
                result.ErrorMessage = "Course not found";
            }
            else
            {
                dbCourse.Credits = course.Credits;
                dbCourse.DepartmentID = course.DepartmentID;
                dbCourse.Title = course.Title;
                result.ChangeCount = await SaveChangesAsync();
            }
            return result;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await SchoolDbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion


        #region IDisposable

        public void Dispose()
        {
            SchoolDbContext.Dispose();
        }

        #endregion IDisposable
    }
}
