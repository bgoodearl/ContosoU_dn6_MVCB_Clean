# Contoso University Clean 6 - Dev Notes

<table>
    <tr>
        <th>Date</th><th>Dev</th>
		<th>Notes</th>
    </tr>
    <tr>
        <td>5/18/2022</td><td>bg</td>
		<td>
            Branch - with_lookups2: backed out migration CU6_M04_AddLookups<br/>
            Commented out DepartmentFacilityType, added RandomLookupType so there are at least 2 lookups<br/>
            Ran migration CU6_M04_AddLookups, generated SQL scripts for migration<br/>
            Added seeding of lookup data, tweaked SeedData in CoursesController<br/>
		</td>
    </tr>
    <tr>
        <td>5/17/2022</td><td>bg</td>
		<td>
            Started experiments with many-to-many Course and CoursePresentationType<br/>
		</td>
    </tr>
    <tr>
        <td>5/16/2022</td><td>bg</td>
		<td>
            Branch - with_lookups:
            Added CoursePresentationType and DepartmentFacilityType<br/>
		</td>
    </tr>
    <tr>
        <td>5/9/2022</td><td>bg</td>
		<td>
            Fix problem with first instructor's courses<br/>
		</td>
    </tr>
    <tr>
        <td>5/1/2022</td><td>bg</td>
		<td>
            Added bootstrap formatting for tables<br/>
		</td>
    </tr>
    <tr>
        <td>4/1/2022</td><td>bg</td>
		<td>
            Changed render-mode for Blazor components to "Server"<br/>
		</td>
    </tr>
    <tr>
        <td>3/6/2022</td><td>bg</td>
		<td>
            Added GetCourseListItemsQuery with handler and test<br/>
            Extended SchoolItemEventArgs with additional properties<br/>
            Minor code cleanup<br/>
            Added component CourseList4Instructor, 
            use optionally when Instructors list is visible<br/>
		</td>
    </tr>
    <tr>
        <td>3/5/2022</td><td>bg</td>
		<td>
            Tweaked use of ISchoolDbContextFactory<br/>
            Refactored Components - moved Course components to Courses folder,
            share SchoolItemEventArgs, SchoolItemViewModel<br/>
            Fixed problem with saving changed DepartmentID when saving Course edit<br/>
            Added StudentEdit component - Create New<br/>
            Completed StudentEdit - Edit, MediatR command
            UpdateStudentItemCommand<br/>
            Added GetInstructorListItemsWithPaginationQuery, test<br/>
            Added razor components: Instructors, InstructorList<br/>
            Updated Instructors Index MVC page to use razor components by default<br/>
            Added optional InstructorID to GetCourseListItemsWithPaginationQuery,
            updated handler, added test<br/>
		</td>
    </tr>
    <tr>
        <td>3/4/2022</td><td>bg</td>
		<td>
            Minor re-org:
            Removed DomainEvent artifacts - deferring implementation of DomainEvent
            until after a rexamination of the topic<br/>
            Moved ISchoolDbContext and ISchoolDbContextFactory from Application
            to CU.Application.Data.Common so Infrastructure doesn't depend on Application<br/>
            Reverted to MediatR v9<br/>
            Added unit test for MediatR requests having handlers<br/>
            Integrated additional code from Jason Taylor's Clean sample to support
            MediatR commands<br/>
            Added first command: CreateStudentItemCommand with validation, handler and test<br/>
            Moved ValidationException from CU.Application.Common to CU.Application.Shared<br/>
		</td>
    </tr>
    <tr>
        <td>3/3/2022</td><td>bg</td>
		<td>
            Added ASP.NET 6 Core MVC web app - ContosoUniversity<br/>
            Switched web app to attribute routing<br/>
            Added CUControllerBase, _TopNav.cshtml, changed content for home page<br/>
            Wired up Infrastructure, etc. to web app<br/>
            Added Departments controller with index page<br/>
            Added logging with NLog<br/>
            Added Instructors and Students controllers and index views<br/>
            Added Courses controller and views from .NET Core 3.1 version,
            tweaked controller for .NET 6<br/>
            See changes in SchoolDbContext with new method InitializeDbSets()<br/>
            Added Demo.Components project w/Blazor demo components<br/>
            Wired up Blazor Server<br/>
            Migrated ContosoUniversity.Components from .NET Core 3.1 Clean app<br/>
            Wired up CU Blazor components for Courses<br/>
            Added first MediatR query - GetDepartmentListItemsQuery
            with handler in CU.Application, and with integration test<br/>
            Dto model, Query in CU.Application.Shared<br/>
            Handler, AutoMapper mapping in CU.Application<br/>
            Added DependencyInjection to CU.Application<br/>
            Integration test GetDepartmentsTests.CanGetDepartmentsList()<br/>
            Wired up CU.Application to CU web app,
            use MediatR query for Departments Index MVC page<br/>
            Added MediatR query GetInstructorListItemsQuery,
            added integration test, wired up to Instructors Index page<br/>
            Incorporated Jason Taylor's PaginatedListHandler.
            Added MediatR query GetCourseListItemsWithPaginationQuery,
            handler and integration test<br/>
            Added Pager component to ContosoUniversity.Components<br/>
            Use Pager and MediatR query GetCourseListItemsWithPaginationQuery
            with Blazor CourseList<br/>
            Added initial Student MediatR queries and tests<br/>
            Added Students Blazor component, use it as default
            view on Students Index page<br/>
		</td>
    </tr>
    <tr>
        <td>3/2/2022</td><td>bg</td>
		<td>
            Started with CU.SharedKernel<br/>
            Added ContosoUniversity.Models - persistent
            object models - from ContosoUniversity_dnc31_MVC<br/>
            Wired up entity models to EntityBaseT<br/>
            Added CU.Application.Shared and CU.Application.Common<br/>
            Added CU.Application, CU.Infrastructure - adapted 
            SchoolDbContext from EF 6 version<br/>
            Added CU.EFDataApp to use for running Migrations from Package Manager Console<br/>
            Simplified CU.EFDataApp<br/>
            Added SchoolContextFactory to CU.EFDataApp.Data<br/>
            Added SchoolDbContextFactory to CU.Infrastructure<br/>
            Added first EF migration: CU6_M01_ExistingSchemaBase_2022 -
            see _MigrationNotes.md in CU.Infrastructure<br/>
            Added Infrastructure DependencyInjection<br/>
            Added CU.ApplicationIntegrationTests, first test:
            CanGetCoursesAsync - using ISchoolDbContext<br/>
            Added private constructors and public constructors for
            persistent objects<br/>
            Added persistent Enrollment w/links to Course and Student<br/>
            Added migration CU6_M02_AddEnrollment, tweaked SQL scripts<br/>
            Added migration CU6_M03_AddCourseInstructorLink, tweaked SQL<br/>
            Added test CanGetCoursesWithInstructorsAsync()<br/>
            Added SchoolRepository and related test<br/>
            Added SchoolViewDataRepository and related test<br/>
		</td>
    </tr>
    <tr>
        <td></td><td></td>
		<td>
		</td>
    </tr>
</table>
