using System.ComponentModel.DataAnnotations;

namespace ImbizoFoundation.Models
{
    public class Course
    {                   //C
        public Course()
        {
        }

        public Course(string? courseID, string? courseName, string? recommendedCourseID, string? courseImageLink, string? courseAuthor, string? courseLanguage, List<LanCourse>? listCourseIDAltLan, string? dateCreated, List<string>? listOfLessonID)
        {
            this.courseID = courseID;
            this.courseName = courseName;
            this.recommendedCourseID = recommendedCourseID;
            this.courseImageLink = courseImageLink;
            this.courseAuthor = courseAuthor;
            this.courseLanguage = courseLanguage;
            this.listCourseIDAltLan = listCourseIDAltLan;
            this.dateCreated = dateCreated;
            this.listOfLessonID = listOfLessonID;
        }

        public string? courseID { get; set; }
        public string? courseName { get; set; }
        public string? recommendedCourseID { get; set; }
        public string? courseImageLink { get; set; }
        public string? courseAuthor { get; set; }
        public string? courseLanguage { get; set; }
        public string? courseRelated { get; set; }
        public string? noRelatedCourse { get; set; }
        public List<LanCourse>? listCourseIDAltLan { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string? dateCreated { get; set; }
        public List<string>? listOfLessonID { get; set; }
        public IFormFile? Image { get; set; }

    }
}
