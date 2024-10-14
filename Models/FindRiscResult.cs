using DiabetWebSite.Models;

public class FindRiscResult
{
    public int Id { get; set; }
    public int UserId { get; set; } // Kullanıcıyı tanımlamak için
    public DateTime TestDate { get; set; }
    public int TotalRiskPoints { get; set; }
    public string DegreeOfRisk { get; set; }
    public string TenYearRiskRating { get; set; }
    public string RoutineScreening { get; set; }

    public List<UserAnswer> UserAnswers { get; set; }
}
