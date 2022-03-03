# Contoso University Clean 6 - Dev Notes

<table>
    <tr>
        <th>Date</th><th>Dev</th>
		<th>Notes</th>
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
            Added test CanGetCoursesWithInstructorsAsync()
		</td>
    </tr>
    <tr>
        <td></td><td></td>
		<td>
		</td>
    </tr>
</table>
