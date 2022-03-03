﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CU.Application.Common.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using CM = ContosoUniversity.Models;

namespace CU.ApplicationIntegrationTests.DbContextTests
{
    [Collection(TestFixture.DbCollectionName)]
    public class GetDbContextCoursesTests : DbContextTestBase
    {
        public GetDbContextCoursesTests(ITestOutputHelper testOutputHelper, TestFixture fixture)
            : base(testOutputHelper, fixture)
        {

        }

        [Fact]
        public async Task CanGetCoursesAsync()
        {
            int maxCourseCount = 5;

            using (ISchoolDbContext cuContext = await _fixture.GetISchoolDbContext(_testOutputHelper))
            {
                cuContext.Should().NotBeNull();

                List<CM.Course> courses = await cuContext.Courses
                    .Take(maxCourseCount).ToListAsync();
                courses.Should().NotBeNullOrEmpty();
                courses.Count.Should().BePositive();
                _testOutputHelper.WriteLine($"courses.Count = {courses.Count}");
            }
        }
    }
}