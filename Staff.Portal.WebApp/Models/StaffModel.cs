using System.ComponentModel.DataAnnotations;

namespace Staff.Portal.Models
{
    public class StaffModel
    {
        public string? employment_number { get; set; } = "";
        public string? first_name { get; set; } = "";
        public string? last_name { get; set; } = "";

        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
        public DateTime? birth_date { get; set; }
        public double? salary { get; set; }
        public int? years_work_experience { get; set; }
        public int? gender_id { get; set; }
        public int? qualification_id { get; set; }
        public string? gender { get; set; }
        public string? qualification { get; set; }

    }

   
}
