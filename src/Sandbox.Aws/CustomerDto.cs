namespace Sandbox.Aws
{
    using System;

    internal class CustomerDto
    {
        public string PartitionKey { get; set; }

        public string SortKey { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
