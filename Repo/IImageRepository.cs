namespace Bloggie.Web.Repo
{
    public interface IImageRepository
    {

        Task<string> UploadAsync(IFormFile file);
    }
}
