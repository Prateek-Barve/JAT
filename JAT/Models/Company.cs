using System.ComponentModel.DataAnnotations;

namespace JAT.Models
{
    public enum Status
    {
        Pending = 0,
        Selected = 1,
        Rejected = 2
    }
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public DateTime AppliedDate { get; set; } = DateTime.Now;
    }
}
