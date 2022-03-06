using CU.Application.Shared.ViewModels.Students;
using MediatR;

namespace CU.Application.Shared.DataRequests.SchoolItems.Commands
{
    public class CreateStudentItemCommand : IRequest<int>
    {
        public CreateStudentItemCommand()
        {
        }

        public CreateStudentItemCommand(StudentEditDto dto)
        {
            EnrollmentDate = dto.EnrollmentDate;
            FirstMidName = dto.FirstMidName;
            LastName = dto.LastName;
        }

        public DateTime EnrollmentDate { get; set; }
        public string FirstMidName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
