namespace DatabaseReservation.Service
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IFormFile file, IWebHostEnvironment webHostEnvironment);
    }
}
