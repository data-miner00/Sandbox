using Bogus;
using static Bogus.DataSets.Name;

var countryIso = new[] { "MYS", "SGP", "IDN", "PHP", "VNM", "THA" };

var personGenerator = new Faker<Person>()
    .RuleFor(u => u.FirstName, (f, u) => f.Name.FirstName(Gender.Female))
    .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(Gender.Female))
    .RuleFor(u => u.Avatar, (f, u) => f.Internet.Avatar())
    .RuleFor(u => u.CountryISO, (f, u) => f.PickRandom(countryIso))
    .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName));

var countryGenerator = new Faker<Country>()
    .RuleFor(u => u.IsoCode, (f, u) => f.PickRandom(countryIso))
    .RuleFor(u => u.Name, (f, u) => f.Address.Country())
    .RuleFor(u => u.Continent, (f, u) => f.Address.Random.ClampString("abcderfg"));

var people = personGenerator.GenerateBetween(30, 60);
var countries = countryGenerator.GenerateBetween(10, 20);

var query =
    from person in people
    join country in countries on person.CountryISO equals country.IsoCode
    select new
    {
        FirstName = person.FirstName,
        LastName = person.LastName,
        CountryIso = country.IsoCode,
        CountryName = country.Name,
        Continent = country.Continent,
    };

var table = query.ToList();

Console.WriteLine();

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Avatar { get; set; }
    public string Email { get; set; }
    public string CountryISO { get; set; }
    public string FullName => $"{this.FirstName} {this.LastName}";
}

public class Country
{
    public string Name { get; set; }

    public string IsoCode { get; set; }

    public string Continent { get; set; }
}
