using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Globalization;

public class FilterPeopleFromXmlSolution
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        try
        {
            
            if (string.IsNullOrWhiteSpace(xmlData))
            {
                return CreateEmptyResult();
            }

            
            XDocument doc = XDocument.Parse(xmlData);
            
            
            var people = doc.Descendants("Person");
            
            
            var filteredPeople = people.Where(person => MeetsCriteria(person)).ToList();
            
            
            if (!filteredPeople.Any())
            {
                return CreateEmptyResult();
            }
            
            
            var names = filteredPeople
                .Select(p => p.Element("Name")?.Value ?? "")
                .Where(name => !string.IsNullOrEmpty(name))
                .OrderBy(name => name)
                .ToList();
            
            var salaries = filteredPeople
                .Select(p => ParseDecimal(p.Element("Salary")?.Value))
                .Where(salary => salary.HasValue)
                .Select(salary => salary.Value)
                .ToList();
            
            
            decimal totalSalary = salaries.Sum();
            decimal averageSalary = salaries.Any() ? salaries.Average() : 0;
            decimal maxSalary = salaries.Any() ? salaries.Max() : 0;
            int count = filteredPeople.Count;
            
            
            var result = new
            {
                Names = names,
                TotalSalary = (int)totalSalary,
                AverageSalary = (int)averageSalary,
                MaxSalary = (int)maxSalary,
                Count = count
            };
            
            return JsonSerializer.Serialize(result);
        }
        catch (Exception)
        {
            
            return CreateEmptyResult();
        }
    }
    
    private static bool MeetsCriteria(XElement person)
    {
        try
        {
            
            var ageElement = person.Element("Age");
            if (ageElement == null || !int.TryParse(ageElement.Value, out int age) || age <= 30)
                return false;
            
            
            var departmentElement = person.Element("Department");
            if (departmentElement == null || departmentElement.Value != "IT")
                return false;
            
            
            var salaryElement = person.Element("Salary");
            if (salaryElement == null || !ParseDecimal(salaryElement.Value).HasValue || ParseDecimal(salaryElement.Value).Value <= 5000)
                return false;
            
            
            var hireDateElement = person.Element("HireDate");
            if (hireDateElement == null || !DateTime.TryParse(hireDateElement.Value, out DateTime hireDate) || hireDate >= new DateTime(2019, 1, 1))
                return false;
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    private static decimal? ParseDecimal(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        
        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result))
            return result;
        
        return null;
    }
    
    private static string CreateEmptyResult()
    {
        var emptyResult = new
        {
            Names = new List<string>(),
            TotalSalary = 0,
            AverageSalary = 0,
            MaxSalary = 0,
            Count = 0
        };
        
        return JsonSerializer.Serialize(emptyResult);
    }
}
