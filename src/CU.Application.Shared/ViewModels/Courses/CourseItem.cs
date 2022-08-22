using Ardalis.GuardClauses;

namespace CU.Application.Shared.ViewModels.Courses
{
    public class CourseItem : CourseListItem
    {
        public CourseItem()
        {
            PresentationTypes = new List<CodeItem>();
            Instructors = new List<IdItem>();
        }

        public CourseItem(CourseListItem listItem)
            : this()
        {
            if ((listItem != null) && (listItem != this))
            {
                this.CourseID = listItem.CourseID;
                this.Credits = listItem.Credits;
                this.Department = listItem.Department;
                this.Title = listItem.Title;
            }
        }

        public void SetInstructors(List<IdItem> instructors)
        {
            Guard.Against.Null(instructors, nameof(instructors));
            Instructors = instructors;
        }

        public void SetPresentationTypes(List<CodeItem> presentationTypes)
        {
            Guard.Against.Null(presentationTypes, nameof(presentationTypes));
            PresentationTypes = presentationTypes;
        }

        public List<IdItem> Instructors { get; private set; }

        public List<CodeItem> PresentationTypes { get; private set; }
    }
}
