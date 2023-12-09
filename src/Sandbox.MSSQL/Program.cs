using Microsoft.EntityFrameworkCore;
using Sandbox.MSSQL;

#pragma warning disable CS8321 // Local function is declared but never used
static void Steps()
{
    Console.WriteLine("Add-Migration \"Add_Employee_Table\"");
    Console.WriteLine("Update-Database");
}
#pragma warning restore CS8321 // Local function is declared but never used

var dbContext = new AppDbContext();

dbContext.Database.EnsureCreated();

var employees = await dbContext.Employees.ToListAsync().ConfigureAwait(false);

foreach (var employee in employees)
{
    Console.WriteLine($"{employee.Id} - {employee.Name} - {employee.Title}");
}
