namespace MVCprog.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public string Description { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;

        public decimal TotalAmount => HoursWorked * HourlyRate;

        public string Status { get; set; } = "Pending";
        public List<Document>? Documents { get; set; }
    }
}
