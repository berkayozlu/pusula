using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class FilterEmployeesSolution
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        
        if (employees == null)
        {
            return CreateEmptyResult();
        }
        
        
        var filteredEmployees = employees.Where(emp => MeetsCriteria(emp)).ToList();
        
        
        if (!filteredEmployees.Any())
        {
            return CreateEmptyResult();
        }
        
        
        var sortedNames = filteredEmployees
            .Select(emp => emp.Name)
            .OrderByDescending(name => name.Length)
            .ThenBy(name => name)
            .ToList();
        
        
        var salaries = filteredEmployees.Select(emp => emp.Salary).ToList();
        decimal totalSalary = salaries.Sum();
        decimal averageSalary = salaries.Average();
        decimal minSalary = salaries.Min();
        decimal maxSalary = salaries.Max();
        int count = filteredEmployees.Count;
        
        
        var result = new
        {
            Names = sortedNames,
            TotalSalary = totalSalary,
            AverageSalary = Math.Round(averageSalary, 2),
            MinSalary = minSalary,
            MaxSalary = maxSalary,
            Count = count
        };
        
        return JsonSerializer.Serialize(result);
    }
    
    private static bool MeetsCriteria((string Name, int Age, string Department, decimal Salary, DateTime HireDate) employee)
    {
        
        if (employee.Age < 25 || employee.Age > 40)
            return false;
        
        
        if (employee.Department != "IT" && employee.Department != "Finance")
            return false;
        
        
        if (employee.Salary < 5000m || employee.Salary > 9000m)
            return false;
        
        
        if (employee.HireDate < new DateTime(2017, 1, 1))
            return false;
        
        return true;
    }
    
    private static string CreateEmptyResult()
    {
        var emptyResult = new
        {
            Names = new List<string>(),
            TotalSalary = 0m,
            AverageSalary = 0m,
            MinSalary = 0m,
            MaxSalary = 0m,
            Count = 0
        };
        
        return JsonSerializer.Serialize(emptyResult);
    }
}
