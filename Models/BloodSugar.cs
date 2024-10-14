public class BloodSugar
{
    public int BloodSugarId { get; set; }
    public int UserId { get; set; }
    public decimal MeasurementValue { get; set; }
    public DateTime MeasurementTime { get; set; }
    public User User { get; set; }
}