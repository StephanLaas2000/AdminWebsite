using ImbizoFoundation.Classes;
using ImbizoFoundation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ImbizoFoundation.Controllers
{
    public class CoursesController : Controller
    {
        string? courseID;
        List<String>? listOfLessonID = null;
        List<Course>? courseListId = null;
        List<string> noRelatedCourseList = new List<string>();
        FireBaseWorker fireBaseWorker = new FireBaseWorker();
        FireBaseStorageWorker storageWorker = new FireBaseStorageWorker();
        List<LanCourse> lLanCourse = new List<LanCourse>();
        // GET: Courses
        public async Task<IActionResult> Index()
        {
            List<Course> courseList = new List<Course>();
            courseList = fireBaseWorker.getAllCourse();
            ListHandler.languagesList.Clear();

            return View(courseList);

        }
 

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(string id)
        {
            List<Course> courses = fireBaseWorker.getAllCourse();

            Course course = new Course();

            course = courses.Where(x => x.courseID.Equals(id)).FirstOrDefault();

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            populateViewBags();
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string courseName, string recommendedCourseID, string courseAuthor, string listCourseIDAltLan, IFormFile Image, string courseRelated, string noRelatedCourse)
        {
            if (noRelatedCourse == null & courseRelated == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                //ListHandler.courseList = fireBaseWorker.getAllCourse();
                ListHandler.allcourseList = fireBaseWorker.getAllCourse();

                noRelatedCourseList.Add("No Related Course");

                populateViewBags();

                //getting a new ID 

                //Population ListOfLesson 
                
                if (fireBaseWorker.getAllCourse() != null)
                {
                    courseListId = fireBaseWorker.getAllCourse();

                    int count = 0;

                    for (int i = 0; i < courseListId.Count; i++)
                    {
                        count++;
                    }
                    courseID = "C" + "0" + count;
                }
                else
                {
                    courseID = "C01";
                }

                //Temporarily Populating the languages 

                string courseLanguage = listCourseIDAltLan;

                //Population of dateCreated
                DateTime dateCreated = DateTime.Now;
       
                var s = await storageWorker.UploadImage(Image);
                string courseImageLinks = s.ToString();

                ListHandler.allcourseList = ListHandler.allcourseList.Where(x => x.courseID.Equals(courseRelated)).ToList();     

                if (courseRelated != null & noRelatedCourse == null)
                {
                    lLanCourse = ListHandler.allcourseList.Select(x => x.listCourseIDAltLan).First().ToList();

                    LanCourse course = new LanCourse(courseLanguage, courseID);

                    foreach (Course item1 in ListHandler.allcourseList)
                    {
                        item1.listCourseIDAltLan.Add(course);

                        Course C = new Course(courseID, courseName, recommendedCourseID, courseImageLinks, courseAuthor, courseLanguage, item1.listCourseIDAltLan, dateCreated.Date.ToString(), listOfLessonID);

                        await fireBaseWorker.writeCourse(C);
                    }


                    ListHandler.allcourseList.Clear();

                    foreach (LanCourse lanCourse in lLanCourse)
                    {
                        foreach (Course item in courseListId)
                        {
                            if (lanCourse.courseID.Equals(item.courseID))
                            {
                                ListHandler.allcourseList.Add(item);
                            }
                        }

                    }

                    foreach (Course item in ListHandler.allcourseList)
                    {
                        List<Course> ccourses = new List<Course>();
                        Worker.courseIDs = item.courseID;

                        item.listCourseIDAltLan.Add(course);

                        Course updateCourse = new Course(item.courseID, item.courseName, item.recommendedCourseID, item.courseImageLink, item.courseAuthor, item.courseLanguage, item.listCourseIDAltLan, item.dateCreated, item.listOfLessonID);

                        await fireBaseWorker.updateCourse(updateCourse);
                    }

                }

                if (noRelatedCourse != null & courseRelated == null)
                {
                    LanCourse course = new LanCourse(courseLanguage, courseID);

                    ListHandler.lanCourseList.Add(course);

                    Course C = new Course(courseID, courseName, recommendedCourseID, courseImageLinks, courseAuthor, courseLanguage, ListHandler.lanCourseList, dateCreated.Date.ToString(), listOfLessonID);

                    await fireBaseWorker.writeCourse(C);
                }

                return RedirectToAction("Index");
            }         
          
        }

        public void populateViewBags()
        {
            ListHandler.languagesList = new List<string>();
            List<string> noRelatedCourseList = new List<string>();

            noRelatedCourseList.Add("No Related Course");

            if (fireBaseWorker.getAllCourse() != null)
            {
                List<Course> courses = fireBaseWorker.getAllCourse();

                ViewData["courseID"] = new SelectList(courses, "courseID", "courseName");
                ViewData["courseRelatedName"] = new SelectList(courses, "courseID", "courseName");
                ViewData["courseRelatedNot"] = new SelectList(noRelatedCourseList);
            }
            else
            {
                ViewData["courseID"] = new SelectList("No Courses", "courseID", "courseName");
                ViewData["courseRelatedName"] = new SelectList("No Courses", "courseID", "courseName");
                ViewData["courseRelatedNot"] = new SelectList(noRelatedCourseList);
            }
            ListHandler.languagesList.Add("English");
            ListHandler.languagesList.Add("IsiZulu");
            ListHandler.languagesList.Add("Afrikaans");
            ListHandler.languagesList.Add("Xhosa");

            ViewBag.languagesList = new SelectList(ListHandler.languagesList);

        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            List<Course> courses = fireBaseWorker.getAllCourse();

            populateViewBagsEdit();

            Course course = new Course();

            course = courses.Where(x => x.courseID.Equals(id)).FirstOrDefault();

            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(string courseID, string courseName, string recommendedCourseID, string courseAuthor, IFormFile Image)
        {
            populateViewBagsEdit();
            //New
            List<Course> editList = new List<Course>();
            editList = fireBaseWorker.getAllCourse();
            editList = editList.Where(x => x.courseID.Equals(courseID)).ToList();
            string? courseImageLinks = null;

            FireBaseStorageWorker storageWorker = new FireBaseStorageWorker();

            if (Image != null)
            {
                var s = await storageWorker.UploadImage(Image);
                courseImageLinks = s.ToString();
            }

            Worker.courseIDs = courseID;

            foreach (Course item in editList)
            {

                Course c = new Course(courseID,courseName != null ? courseName : item.courseName, recommendedCourseID != null ? recommendedCourseID : item.recommendedCourseID, courseImageLinks != null ? courseImageLinks : item.courseImageLink, courseAuthor != null ? courseAuthor : item.courseAuthor
                    , item.courseLanguage, item.listCourseIDAltLan, item.dateCreated, item.listOfLessonID);

     
                await fireBaseWorker.updateCourse(c);
            }

            return View();
        }

        public void populateViewBagsEdit()
        {
            ListHandler.languagesList = new List<string>();
            List<Course> courses = fireBaseWorker.getAllCourse();

            ViewData["courseID"] = new SelectList(courses, "courseID", "courseName");

            ListHandler.languagesList.Add("English");
            ListHandler.languagesList.Add("Zulu");

            ViewBag.languagesList = new SelectList(ListHandler.languagesList);
        }

        //// GET: Courses/Delete/5
        //public IActionResult Delete(string id)
        //{
        //    fireBaseWorker.deleteCourse(id);
        //    return RedirectToAction("Index");
        //}

        // GET: AdminController/Delete/5
        public ActionResult Delete(string id)
        {
            List<Course> CourseList = fireBaseWorker.getAllCourse();
            Course c = new Course();
            c = CourseList.Where(x => x.courseID.Equals(id)).FirstOrDefault();
            return View(c);
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id,string courseID)
        {

            fireBaseWorker.deleteCourse(courseID);

            return RedirectToAction("Index");
        }

    }
}
