namespace DatabaseReservation.Service
{
    public class FileUpload : IFileUpload
    {
        /// <summary>
        /// A method to help upload files and store them in the wwwroot/Images folder
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> UploadFile(IFormFile file)
        {
            string path = string.Empty;
            // generate a unique name for the file uploaded
            string fileUniqueName = Guid.NewGuid()+ "_" + file.FileName;
            
            try
            {
                // get the path of the wwwroot folder
                path = Path.GetFullPath(Path.Combine(Path.Combine(Environment.CurrentDirectory, "wwwroot/images"), fileUniqueName));
                using (var fileStream = new FileStream(path,FileMode.Create))
                {
                    // copy the file to the new directory
                    await file.CopyToAsync(fileStream);
                }
                // return the name of the file to be used later
                return fileUniqueName;
            }catch (Exception ex)
            {
                File.Delete(path);
                throw new Exception("File could not be uploaded ", ex);
            }
        }
    }
}
