namespace Benchmarks.Net48Net10;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Linq;
using System.Collections.Generic;

[SimpleJob(RuntimeMoniker.Net48, baseline: true)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class MultipleWhere
{
    public sealed class User
    {
        public string Name { get; set; }

        public string City { get; set; }

        public int Age { get; set; }

        public User(string name, string city, int age)
        {
            Name = name;
            City = city;
            Age = age;
        }
    }

    private List<User> users = new List<User>();

    public MultipleWhere()
    {
        this.Setup();
    }

    public void Setup()
    {
        Console.WriteLine("This should only be called once");
        this.users.Clear();
        this.users.Add(new User("Shaun", "Texas", 32));

        var rnd = new Random();
        string[] names = ["Olivia", "Liam", "Emma", "Noah", "Ava", "Oliver", "Sophia", "Elijah", "Isabella", "Lucas"];
        string[] cities = ["New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "Dallas"];

        for (int i = 0; i < 50; i++)
        {
            var name = names[rnd.Next(names.Length)];
            var city = cities[rnd.Next(cities.Length)];
            var age = rnd.Next(18, 81); // ages between 18 and 80
            this.users.Add(new User(name, city, age));
        }
    }

    [Benchmark(Baseline = true)]
    public List<User> SingleWhere()
    {
        return this.users.Where(x => x.Name.StartsWith("Ol") && x.City.Equals("Chicago") && x.Age < 40).ToList();
    }

    [Benchmark]
    public List<User> MultipleWheres()
    {
        return this.users
            .Where(x => x.Name.StartsWith("Ol"))
            .Where(x => x.City.Equals("Chicago"))
            .Where(x => x.Age < 40)
            .ToList();
    }
}
