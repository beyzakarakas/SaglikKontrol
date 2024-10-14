namespace DiabetWebSite.Models
{
    public class BodyMassIndex
    {
        public int BodyMassIndexId { get; set; }
        public int UserId { get; set; }
        public decimal HeightCm { get; set; }
        public decimal WeightKg { get; set; }
        public decimal BMICalculated { get; set; } // Yeni eklenen sütun
        public DateTime MeasurementTime { get; set; }
        public string Notes { get; set; }

        public User User { get; set; }
    }
}
