public class BloodPressure
{
    public int BloodPressureId { get; set; }
    public int UserId { get; set; }
    public int Systolic { get; set; }
    public int Diastolic { get; set; }
    public DateTime MeasurementTime { get; set; }
    public string Notes { get; set; }
    public User User { get; set; }
}