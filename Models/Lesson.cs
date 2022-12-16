namespace ImbizoFoundation.Models
{
    public class Lesson
    {
        public Lesson()
        {

        }

        public Lesson(string? lessonID, string? lessonName, List<string>? lessonLectures, string? duration,
            string? dateCreated, string? lessonShortDescription, string? lessonSupportingText, string? pdfLinks,
            string? mp3Links, string? videoLink, string? courseID, string? lessonImageLink,string? language)
        {
            this.lessonID = lessonID;
            this.lessonName = lessonName;
            this.lessonLectures = lessonLectures;
            this.duration = duration;
            this.dateCreated = dateCreated;
            this.lessonShortDescription = lessonShortDescription;
            this.lessonSupportingText = lessonSupportingText;
            this.pdfLinks = pdfLinks;
            this.mp3Links = mp3Links;
            this.videoLink = videoLink;
            this.courseID = courseID;
            this.lessonImageLink = lessonImageLink;
            this.language = language;
        }

        public string? lessonID { get; set; }
        public string? lessonName { get; set; }
        public List<string>? lessonLectures { get; set; }
        public string? duration { get; set; }
        public string? dateCreated { get; set; }
        public string? lessonShortDescription { get; set; }
        public string? lessonSupportingText { get; set; }
        public string? pdfLinks { get; set; }
        public string? mp3Links { get; set; }
        public string? videoLink { get; set; }
        public string? courseID { get; set; }
        public string? language { get; set; }
        public string? lessonImageLink { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? pdfLink { get; set; }
        public IFormFile? mp3Link { get; set; }



    }
}
