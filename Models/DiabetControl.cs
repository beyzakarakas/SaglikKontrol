public class DiabetesControl
{
    public int DiabetesControlId { get; set; }
    public int UserId { get; set; }
    public decimal Hba1c { get; set; }
    public DateTime MeasurementTime { get; set; }
    public string Notes { get; set; }

    public User User { get; set; }
}
