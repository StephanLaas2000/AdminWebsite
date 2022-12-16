namespace ImbizoFoundation.Controllers
{
    public class FireBaseStorageWorker
    {
        private static string ApiKey = "AIzaSyAhzp3HnUs_sgt6TfrEperZO5wpMdZxui4";
        private static string Bucket = "xbcad7319-a18ef.appspot.com";
        private static string AuthEmail = "severadminImbizo@gmail.com";
        private static string AuthPassword = "imbizofoundationstar1234";

        public FireBaseStorageWorker()
        {

        }

        public async Task<string> UploadImage(IFormFile file)
        {
            //Return string uploaded file 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var s = await UploadImages(new FileStream(Path.Combine(filePath), FileMode.Open), file.FileName);
            s = s.ToString();
            return s;
        }
        private async Task<String> UploadImages(FileStream stream, string filename)
        {
            var auth = new Firebase.Auth.FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            //

            var cancelation = new CancellationTokenSource();

            var task = new Firebase.Storage.FirebaseStorage(
                Bucket,
                    new Firebase.Storage.FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }
                ).Child("images")
                .Child(filename)
                .PutAsync(stream, cancelation.Token);
            try
            {
                return await task;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> UploadPdf(IFormFile file)
        {
            //Return string uploaded file 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var s = await UploadPdfs(new FileStream(Path.Combine(filePath), FileMode.Open), file.FileName);
            s = s.ToString();
            return s;
        }
        private async Task<String> UploadPdfs(FileStream stream, string filename)
        {
            var auth = new Firebase.Auth.FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            //

            var cancelation = new CancellationTokenSource();

            var task = new Firebase.Storage.FirebaseStorage(
                Bucket,
                    new Firebase.Storage.FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }
                ).Child("pdf")
                .Child(filename)
                .PutAsync(stream, cancelation.Token);
            try
            {
                return await task;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> UploadMp3(IFormFile file)
        {
            //Return string uploaded file 
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var s = await UploadMp3s(new FileStream(Path.Combine(filePath), FileMode.Open), file.FileName);
            s = s.ToString();
            return s;
        }
        private async Task<String> UploadMp3s(FileStream stream, string filename)
        {
            var auth = new Firebase.Auth.FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            //

            var cancelation = new CancellationTokenSource();

            var task = new Firebase.Storage.FirebaseStorage(
                Bucket,
                    new Firebase.Storage.FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    }
                ).Child("mp3")
                .Child(filename)
                .PutAsync(stream, cancelation.Token);
            try
            {
                return await task;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
