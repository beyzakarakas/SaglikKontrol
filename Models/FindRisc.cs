using System;
using System.Collections.Generic;

namespace DiabetWebSite.Models
{
    public class FindRisc
    {
        public int Id { get; set; }
        public string UserId { get; set; }  // Kullanıcının ID'si
        public DateTime DateTaken { get; set; }  // Anketin tamamlandığı tarih
        public int TotalRiskPoints { get; set; }  // Toplam Risk Puanı
        public string DegreeOfRisk { get; set; }  // Risk Derecesi
        public string TenYearRiskRating { get; set; }  // 10 Yıllık Risk Oranı
        public string RoutineScreening { get; set; }  // Rutin Taramalar

        public List<UserAnswer> UserAnswers { get; set; }  // Kullanıcının verdiği cevaplar
    }
}
