using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CU.Application.Common.Interfaces;
using CU.Application.Shared.ViewModels.Courses;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CU.ApplicationIntegrationTests.SchoolRepositoryTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetSchoolRepositoryCoursesTests : DbContextTestBase
    {
        public GetSchoolRepositoryCoursesTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {
        }

        [Fact]
        public async Task CanGetCourseListAsync()
        {
            string itemType = nameof(CourseListItem);

            using (ISchoolRepository repo = _fixture.GetSchoolRepositoryFactory(_testOutputHelper).GetSchoolRepository())
            {
                List<CourseListItem> courseList = await repo.GetCourseListItemsNoTrackingAsync();
                courseList.Should().NotBeNullOrEmpty();
                _testOutputHelper.WriteLine($"Have {courseList.Count} items of type [{itemType}]");
                _testOutputHelper.WriteLine("");

                CourseListItem firstItem = courseList.First();
                firstItem.Should().NotBeNull();
                _testOutputHelper.WriteLine($"First [{itemType}] ({firstItem.CourseID}) [{firstItem.Title}]");
                _testOutputHelper.WriteLine("");

                CourseListItem lastItem = courseList.Last();
                lastItem.Should().NotBeNull();
                _testOutputHelper.WriteLine($"Last  [{itemType}] ({lastItem.CourseID}) [{lastItem.Title}]");
            }
        }

        [Fact]
        public async Task CanGetCourseDetailsAsync()
        {
            string itemType = nameof(CourseListItem);
            string itemType2 = nameof(CourseEditDto);

            using (ISchoolRepository repo = _fixture.GetSchoolRepositoryFactory(_testOutputHelper).GetSchoolRepository())
            {
                List<CourseListItem> courseList = await repo.GetCourseListItemsNoTrackingAsync();
                courseList.Should().NotBeNullOrEmpty();
                _testOutputHelper.WriteLine($"Have {courseList.Count} items of type [{itemType}]");
                _testOutputHelper.WriteLine("");

                int firstItemId = courseList.Select(cl => cl.CourseID).FirstOrDefault();
                firstItemId.Should().NotBe(0);

                CourseEditDto? firstDetails = await repo.GetCourseEditDtoNoTrackingAsync(firstItemId);
                firstDetails.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference. - handled by Fluent assert
                _testOutputHelper.WriteLine($"First [{itemType2}] CourseID = {firstDetails.CourseID}, Title = [{firstDetails.Title}], Credits = {firstDetails.Credits}");
#pragma warning restore CS8602
                _testOutputHelper.WriteLine($"");

                int lastItemId = courseList.Select(cl => cl.CourseID).LastOrDefault();
                lastItemId.Should().NotBe(0);

                CourseEditDto? lastDetails = await repo.GetCourseEditDtoNoTrackingAsync(lastItemId);
                lastDetails.Should().NotBeNull();
#pragma warning disable CS8602 // Dereference of a possibly null reference. - handled by Fluent assert
                _testOutputHelper.WriteLine($"Last  [{itemType2}] CourseID = {lastDetails.CourseID}, Title = [{lastDetails.Title}], Credits = {lastDetails.Credits}");
#pragma warning restore CS8602
            }
        }
    }
}
