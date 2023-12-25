namespace Sandbox.Core.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vehicle
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 6)]
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [RegularExpression(@"^[A-Z]_[0-9]$")]
        [Required]
        public string Code { get; set; }

        [Range(1, double.MaxValue)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
