namespace ImbizoFoundation.Models
{
    public class LanCourse
    {
        public LanCourse(string? courseLanguage, string? courseID)
        {
            this.courseLanguage = courseLanguage;
            this.courseID = courseID;
        }

        public LanCourse()
        {

        }

        public string? courseLanguage { get; set; }
        public string? courseID { get; set; }

    }
}
