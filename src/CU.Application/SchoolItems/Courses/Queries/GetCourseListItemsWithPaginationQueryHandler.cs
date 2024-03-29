﻿using Ardalis.GuardClauses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using CU.Application.Common.Mapping;
using CU.Application.Data.Common.Interfaces;
using CU.Application.Shared.Common.Models;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using CU.Application.Shared.Models.SchoolDtos;
using Microsoft.EntityFrameworkCore;
using CM = ContosoUniversity.Models;

namespace CU.Application.SchoolItems.Courses.Queries
{
    public class GetCourseListItemsWithPaginationQueryHandler : IRequestHandler<GetCourseListItemsWithPaginationQuery, PaginatedList<CourseListItemDto>>
    {
        ISchoolDbContext Context { get; }
        IMapper Mapper { get; }

        public GetCourseListItemsWithPaginationQueryHandler(ISchoolDbContext context, IMapper mapper)
        {
            Guard.Against.Null(context, nameof(context));
            Guard.Against.Null(mapper, nameof(mapper));
            Context = context;
            Mapper = mapper;
        }

        public async Task<PaginatedList<CourseListItemDto>> Handle(GetCourseListItemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            IQueryable<CM.Course>? coursesQueryable = null;

            if (request.InstructorID.HasValue)
            {
                var instructorCourses = await Context.Instructors.Where(i => i.ID == request.InstructorID.Value)
                    .Select(i => new
                    {
                        InstructorID = i.ID,
                        CourseIDs = i.Courses.Select(c => c.CourseID).ToList()
                    }).SingleOrDefaultAsync();
                List<int> courseIds = instructorCourses != null ? instructorCourses.CourseIDs : new List<int>();
                coursesQueryable = Context.Courses
                    .Include(c => c.Department)
                    .Where(c => courseIds.Contains(c.CourseID));
            }
            else
            {
                coursesQueryable = Context.Courses
                    .Include(c => c.Department);
            }

            if (request.SortOrder == CourseSortOrder.ByCourseTitle)
            {
                coursesQueryable = coursesQueryable
                    .OrderBy(c => c.Title)
                    .ThenBy(c => c.CourseID);
            }
            else //request.SortOrder == CourseSortOrder.ByCourseID
            {
                coursesQueryable = coursesQueryable
                    .OrderBy(c => c.CourseID);
            }
            return await coursesQueryable
                .ProjectTo<CourseListItemDto>(Mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
