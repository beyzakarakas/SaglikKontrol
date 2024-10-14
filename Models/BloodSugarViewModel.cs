using System;
using System.Collections.Generic;

namespace DiabetWebSite.Models.ViewModels
{
    public class BloodSugarViewModel
    {
        public List<BloodSugar> BloodSugarRecords { get; set; }
        public decimal NewMeasurementValue { get; set; }
    }
}
