namespace Sandbox.DataProtection
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [SensitiveData]
        public string FirstName { get; set; }

        [SensitiveData]
        public string LastName { get; set; }

        [EmailAddress]
        [PiiData]
        public string Email { get; set; }

        [Phone]
        [PiiData]
        public string PhoneNumber { get; set; }

        [PiiData]
        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
