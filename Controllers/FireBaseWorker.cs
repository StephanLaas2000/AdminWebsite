namespace ImbizoFoundation.Controllers
{
    using FireSharp.Config;
    using FireSharp.Interfaces;
    using FireSharp.Response;
    using ImbizoFoundation.Classes;
    using ImbizoFoundation.Models;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public class FireBaseWorker
    {
        private IFirebaseClient client;
        public FirebaseResponse response;
        public FireBaseWorker()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                AuthSecret = "FCUDOqDisMc0NWWt1wZkYZ9q1PgscUCK84uDeu4d",
                BasePath = "https://xbcad7319-a18ef-default-rtdb.firebaseio.com/"
            };

            try
            {
                client = new FireSharp.FirebaseClient(config);

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> writeCourse(Course course)
        {
            try
            {
                SetResponse setter = await client.SetAsync("Courses/" + course.courseID, course);
                if (setter.ResultAs<Course>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> writeAdmin(User user)
        {
            try
            {
                SetResponse setter = await client.SetAsync("Admin/" + user.username, user);
                if (setter.ResultAs<User>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> writeCompany(MarketPlace marketPlaceModel)
        {
            try
            {
                SetResponse setter = await client.SetAsync("MarketPlace/" + marketPlaceModel.companyName, marketPlaceModel);
                if (setter.ResultAs<MarketPlace>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
                throw;
            }

        }
        //public async Task<Course> getCourse(String id)
        //{
        //    try
        //    {
        //        FirebaseResponse getCourse = await client.GetAsync("Courses/" + id);
        //        return getCourse.ResultAs<Course>();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }

        //}
        public List<Course> getCourse(String id)
        {
            try
            {
                FirebaseResponse getCourse = client.Get("Courses/"+id);

                string jsondata = getCourse.Body.ToString();

                var items = JsonConvert.DeserializeObject<Dictionary<string, Course>>(jsondata);

                List<Course> corList = items.Select(x => x.Value).ToList();

                return corList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Course> getAllCourse()
        {
            try
            {
                FirebaseResponse getCourse = client.Get("Courses/");

                string jsondata = getCourse.Body.ToString();

                var items = JsonConvert.DeserializeObject<Dictionary<string, Course>>(jsondata);

                if (items != null)
                {
                    List<Course> corList = items.Select(x => x.Value).ToList();

                    return corList;
                }
                else
                {
                    List<Course> corList = new List<Course>();
                    return corList;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<User> getAllAdmin()
        {
            try
            {
                FirebaseResponse getCourse = client.Get("Admin/");

                string jsondata = getCourse.Body.ToString();

                var items = JsonConvert.DeserializeObject<Dictionary<string, User>>(jsondata);

                if (items != null)
                {
                    List<User> corList = items.Select(x => x.Value).ToList();

                    return corList;
                }
                else
                {
                    List<User> corList = new List<User>();
                    return corList;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<MarketPlace> getAllCompany()
        {
            try
            {
                FirebaseResponse getCompany = client.Get("MarketPlace/");

                string jsondata = getCompany.Body.ToString();

                var items = JsonConvert.DeserializeObject<Dictionary<string, MarketPlace>>(jsondata);

                if (items != null)
                {
                    List<MarketPlace> corList = items.Select(x => x.Value).ToList();

                    return corList;
                }
                else
                {
                    List<MarketPlace> corList = new List<MarketPlace>();
                    return corList;
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> updateCourse(Course course)
        {
            try
            {

                response = await client.UpdateAsync("Courses/" + Worker.courseIDs, course);

                if (response.ResultAs<Course>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> updateCompany(MarketPlace marketPlaceModel)
        {
            try
            {

                response = await client.UpdateAsync("MarketPlace/" + Worker.companyIDs, marketPlaceModel);

                if (response.ResultAs<MarketPlace>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public async Task<bool> deleteCourse(String id)
        {
            try
            {
                FirebaseResponse response = await client.DeleteAsync("Courses/" + id);
                if (response.ResultAs<Course>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> deleteAdmin(String id)
        {
            try
            {
                FirebaseResponse response = await client.DeleteAsync("Admin/" + id);
                if (response.ResultAs<User>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public async Task<bool> deleteCompany(String id)
        {
            try
            {
                FirebaseResponse response = await client.DeleteAsync("MarketPlace/" + id);
                if (response.ResultAs<MarketPlace>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }


        public async Task<bool> writeLesson(Lesson lesson)
        {
            try
            {
                SetResponse setter = await client.SetAsync("Lesson/" + lesson.lessonID, lesson);
                if (setter.ResultAs<Course>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
                throw;
            }

        }
        public List<Lesson> getLesson(String id)
        {
            try
            {
                FirebaseResponse getLesson = client.Get("Courses/");

                string jsonData = getLesson.Body.ToString();

                var items = JsonConvert.DeserializeObject<Dictionary<string, Lesson>>(jsonData);

                List<Lesson> lesList = items.Select(x => x.Value).ToList();

                return lesList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> updateLesson(Lesson lesson)
        {
            try
            {
                FirebaseResponse response = await client.UpdateAsync("Lesson/" + Worker.lessonIDs, lesson);
                if (response.ResultAs<Course>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public async Task<bool> deleteLesson(String id)
        {
            try
            {
                FirebaseResponse response = await client.DeleteAsync("Lesson/" + id);
                if (response.ResultAs<Course>() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public List<Lesson> getAllLesson()
        {

            try
            {
                FirebaseResponse getLesson = client.Get("Lesson/");

                string jsonData = getLesson.Body.ToString();

                var items = JsonConvert.DeserializeObject<Dictionary<string, Lesson>>(jsonData);

                if (items != null)
                {
                    List<Lesson> lessonList = items.Select(x => x.Value).ToList();

                    return lessonList;
                }
                else
                {
                    List<Lesson> lessonList = new List<Lesson>();
                    return lessonList;
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
