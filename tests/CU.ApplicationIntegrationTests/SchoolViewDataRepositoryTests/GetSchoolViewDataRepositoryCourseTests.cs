using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CU.Application.Shared.Interfaces;
using CU.Application.Shared.ViewModels.Courses;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.SchoolViewDataRepositoryTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetSchoolViewDataRepositoryCourseTests : DbContextTestBase
    {
        public GetSchoolViewDataRepositoryCourseTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task CanGetCourseListNoTrackingAsync()
        {
            string itemType = nameof(CourseListItem);

            ISchoolViewDataRepository repo = _fixture.GetSchoolViewDataRepositoryFactory(_testOutputHelper).GetViewDataRepository();
            IList<CourseListItem> courseListItems = await repo.GetCourseListNoTrackingAsync();
            courseListItems.Should().NotBeNullOrEmpty();
            _testOutputHelper.WriteLine($"Have {courseListItems.Count} items of type [{itemType}]");
            _testOutputHelper.WriteLine("");

            CourseListItem firstItem = courseListItems.First();
            firstItem.Should().NotBeNull();
            _testOutputHelper.WriteLine($"First [{itemType}] CourseID = ({firstItem.CourseID}) [{firstItem.Title}]");

            CourseListItem lastItem = courseListItems.Last();
            lastItem.Should().NotBeNull();
            _testOutputHelper.WriteLine($"Last  [{itemType}] CourseID = ({lastItem.CourseID}) [{lastItem.Title}]");
        }

        [Fact]
        public async Task CanGetCourseDetailsAsync()
        {
            string itemType = nameof(CourseListItem);
            string itemType2 = nameof(CourseItem);

            ISchoolViewDataRepository repo = _fixture.GetSchoolViewDataRepositoryFactory(_testOutputHelper).GetViewDataRepository();
            IList<CourseListItem> listItems = await repo.GetCourseListNoTrackingAsync();
            listItems.Should().NotBeNullOrEmpty();
            _testOutputHelper.WriteLine($"Have {listItems.Count} items of type [{itemType}]");
            _testOutputHelper.WriteLine("");

            int firstItemId = listItems.Select(cl => cl.CourseID).FirstOrDefault();
            firstItemId.Should().NotBe(0);

            CourseItem? firstDetails = await repo.GetCourseDetailsNoTrackingAsync(firstItemId);
            firstDetails.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference. - handled by Fluent assert
            _testOutputHelper.WriteLine($"First [{itemType2}] CourseID = {firstDetails.CourseID}, Title = [{firstDetails.Title}], Credits = {firstDetails.Credits}, Dept = [{firstDetails.Department}]");
#pragma warning restore CS8602
            firstDetails.Instructors.Should().NotBeNull();
            if (firstDetails.Instructors.Count == 0)
                _testOutputHelper.WriteLine($"\tNo Instructors");
            foreach (var i in firstDetails.Instructors)
            {
                _testOutputHelper.WriteLine($"\tInstructor Id = {i.Id} [{i.Name}]");
            }
            firstDetails.PresentationTypes.Should().NotBeNull();
            if (firstDetails.PresentationTypes.Count == 0)
                _testOutputHelper.WriteLine($"\tNo PresentationTypes");
            foreach(var p in firstDetails.PresentationTypes)
            {
                _testOutputHelper.WriteLine($"\tPresentation Type Code = [{p.Code}] [{p.Name}]");
            }
            _testOutputHelper.WriteLine("");

            int lastItemId = listItems.Select(cl => cl.CourseID).LastOrDefault();
            lastItemId.Should().NotBe(0);

            CourseItem? lastDetails = await repo.GetCourseDetailsNoTrackingAsync(lastItemId);
            lastDetails.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference. - handled by Fluent assert
            _testOutputHelper.WriteLine($"Last  [{itemType2}] CourseID = {lastDetails.CourseID}, Title = [{lastDetails.Title}], Credits = {lastDetails.Credits}, Dept = [{lastDetails.Department}]");
#pragma warning restore CS8602
            lastDetails.Instructors.Should().NotBeNull();
            if (lastDetails.Instructors.Count == 0)
                _testOutputHelper.WriteLine($"\tNo Instructors");
            foreach (var i in lastDetails.Instructors)
            {
                _testOutputHelper.WriteLine($"\tInstructor Id = {i.Id} [{i.Name}]");
            }
            lastDetails.PresentationTypes.Should().NotBeNull();
            if (lastDetails.PresentationTypes.Count == 0)
                _testOutputHelper.WriteLine($"\tNo PresentationTypes");
            foreach (var p in lastDetails.PresentationTypes)
            {
                _testOutputHelper.WriteLine($"\tPresentation Type Code = [{p.Code}] [{p.Name}]");
            }
        }
    }
}
