namespace Benchmarks.Net8Net10;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;

[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class MultipleWhere
{
    public sealed record User(string Name, string City, int Age);

    private List<User> users = [];

    public MultipleWhere()
    {
        this.Setup();
    }

    public void Setup()
    {
        Console.WriteLine("This should only be called once");
        this.users.Clear();
        this.users.Add(new User("Shaun", "Texas", 32));

        var rnd = Random.Shared;
        string[] names = { "Olivia", "Liam", "Emma", "Noah", "Ava", "Oliver", "Sophia", "Elijah", "Isabella", "Lucas" };
        string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "Dallas" };

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
