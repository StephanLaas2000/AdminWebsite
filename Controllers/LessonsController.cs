using ImbizoFoundation.Classes;
using ImbizoFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImbizoFoundation.Controllers
{

    public class LessonsController : Controller
    {
        FireBaseStorageWorker storageWorker = new FireBaseStorageWorker();
        private static FireBaseWorker firebaseworker = new FireBaseWorker();

        List<Course> courseList = new List<Course>();
        List<Lesson> lessonCodeList = new List<Lesson>();

        string lessonImageLinks = null;
        string pdfLinks = null;
        string mp3Links = null;

        List<Course> findIdCourseList = new List<Course>();
        List<Lesson> findIdLessonList = new List<Lesson>();
        string courseID = null;
        // GET: LessonController
        public ActionResult Index()
        {
            FireBaseWorker f = new FireBaseWorker();
            List<Lesson> lessonList = new List<Lesson>();
            lessonList = f.getAllLesson();

            return View(lessonList);
        }


        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(string id)
        {
            List<Lesson> lessonList = firebaseworker.getAllLesson();
            Lesson lesson = new Lesson();

            lesson = lessonList.Where(x => x.lessonID.Equals(id)).FirstOrDefault();

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            populateViewBagCreate();
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string lessonName, string duration, string lessonShortDescription, string lessonSupportingText, string lessonLectures, IFormFile pdfLink, IFormFile mp3Link, string videoLink, string courseID, string language, IFormFile lessonImageLink)
        {

            populateViewBagCreate();

            List<Course> courses = firebaseworker.getAllCourse();
            
            courseList = firebaseworker.getAllCourse();
            Worker.courseIDs = courseID;
            
            int count = 0;

            for (int i = 0; i < courseList.Count + 1; i++)
            {
                count++;
            }

            string lessonID = "L" + "0" + count++;

            courseList = courseList.Where(x => x.courseID.Equals(courseID)).ToList();

            int test = courses.Count();
            //ViewData["courseID"] = new SelectList(courses, "courseID", "courseName");

            List<string> listOfLecturers = new List<string>();
            listOfLecturers.Add(lessonLectures);

            DateTime lessonCreated = DateTime.Now;

           
            var imageS = await storageWorker.UploadImage(lessonImageLink);
            string lessonImageLinks = imageS.ToString();

            var pdfLinkS = await storageWorker.UploadPdf(pdfLink);
            string pdfLinks = pdfLinkS.ToString();

            var mp3LinkS = await storageWorker.UploadMp3(mp3Link);
            string mp3Links = mp3LinkS.ToString();         

            foreach (Course item in courseList)
            {
               
                if (item.listOfLessonID == null)
                {
                    item.listOfLessonID = new List<string>();              
                }

                item.listOfLessonID.Add(lessonID);

                Course updateCourse = new Course(item.courseID, item.courseName, item.recommendedCourseID, item.courseImageLink, item.courseAuthor, item.courseLanguage, item.listCourseIDAltLan, item.dateCreated, item.listOfLessonID);

                await firebaseworker.updateCourse(updateCourse);
            }

            Lesson l = new Lesson(lessonID, lessonName, listOfLecturers, duration, lessonCreated.ToString(), lessonShortDescription, lessonSupportingText, pdfLinks, mp3Links, videoLink,Worker.courseIDs, lessonImageLinks,language);

            await firebaseworker.writeLesson(l);
            return RedirectToAction("Index");
        }

        public void populateViewBagCreate()
        {
            ListHandler.languagesList = new List<string>();
            if (firebaseworker.getAllCourse() != null)
            {
                List<Course> courses = firebaseworker.getAllCourse();

                ViewData["courseID"] = new SelectList(courses, "courseID", "courseName");
            }
            else
            {
                ViewData["courseID"] = new SelectList("No Courses", "courseID", "courseName");
            }

            List<string> languagesList = new List<string>();
            languagesList.Add("English");
            languagesList.Add("IsiZulu");
            languagesList.Add("Xhosa");

            ViewBag.languagesList = new SelectList(languagesList);

        }


        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            List<Lesson> lessonList = firebaseworker.getAllLesson();
            Lesson lesson = new Lesson();

            lesson = lessonList.Where(x => x.lessonID.Equals(id)).FirstOrDefault();

            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string lessonID,string lessonName, string duration, string lessonShortDescription, string lessonSupportingText, string lessonLectures, IFormFile pdfLink, IFormFile mp3Link, string videoLink, IFormFile Image)
        {
            List<Lesson> editList = new List<Lesson>();
            editList = firebaseworker.getAllLesson();

            editList = editList.Where(x => x.lessonID.Equals(lessonID)).ToList();

            if (pdfLink != null)
            {
                var pdfLinkS = await storageWorker.UploadPdf(pdfLink);
                pdfLinks = pdfLinkS.ToString();             
            }
            if (mp3Link != null)
            {
                var mp3LinkS = await storageWorker.UploadMp3(mp3Link);
                mp3Links = mp3LinkS.ToString();
            }
            if (Image != null)
            {

                var imageS = await storageWorker.UploadImage(Image);
                lessonImageLinks = imageS.ToString();
            }  

            foreach (Lesson item in editList)
            {
                Worker.lessonIDs = lessonID;

                if (lessonLectures != null)
                {
                    item.lessonLectures.Add(lessonLectures);
                }

                Lesson l = new Lesson(lessonID, lessonName != null ? lessonName : item.lessonName, item.lessonLectures, duration != null ? duration : item.duration, 
                    item.dateCreated.ToString(), lessonShortDescription != null ? lessonShortDescription : item.lessonShortDescription, lessonSupportingText != null ? lessonSupportingText : item.lessonSupportingText,
                    pdfLinks != null ? pdfLinks : item.pdfLinks, mp3Links != null ? mp3Links : item.mp3Links, videoLink != null ? videoLink : item.videoLink, item.courseID, lessonImageLinks != null ? lessonImageLinks : item.lessonImageLink,item.language);

                firebaseworker.updateLesson(l);
            }

            return View();
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            findIdCourseList = firebaseworker.getAllCourse();
            findIdLessonList = firebaseworker.getAllLesson();

            findIdLessonList = findIdLessonList.Where(x => x.lessonID.Equals(id)).ToList();

            foreach (Lesson item in findIdLessonList)
            {
                courseID = item.courseID;
            }

            findIdCourseList = findIdCourseList.Where(x => x.courseID.Equals(courseID)).ToList();
           
            foreach (Course item in findIdCourseList)
            {
                Worker.courseIDs = item.courseID;
                foreach (string item1 in item.listOfLessonID.ToList())
                {         
                        item.listOfLessonID.Remove(id);
                }

                Course updateCourse = new Course(item.courseID, item.courseName, item.recommendedCourseID, item.courseImageLink, item.courseAuthor, item.courseLanguage, item.listCourseIDAltLan, item.dateCreated, item.listOfLessonID);

                await firebaseworker.updateCourse(updateCourse);
            }

            await firebaseworker.deleteLesson(id);
            return RedirectToAction("Index");
        }



    }
}
