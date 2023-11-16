namespace DatabaseReservation.Service
{
    public class FileUpload : IFileUpload
    {
        public async Task<string> UploadFile(IFormFile file, IWebHostEnvironment webHostEnvironment)
        {
            string path = string.Empty;
            string fileUniqueName = Guid.NewGuid()+ "_" + file.FileName;
            try
            {
                path = Path.GetFullPath(Path.Combine(Path.Combine(Environment.CurrentDirectory, "wwwroot/images"), fileUniqueName));
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return fileUniqueName;
            }catch (Exception ex)
            {
                File.Delete(path);
                throw new Exception("File could not be uploaded ", ex);
            }
        }
    }
}
