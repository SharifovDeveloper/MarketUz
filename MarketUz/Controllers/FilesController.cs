using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DiyorMarket.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        private Dictionary<int, string> _fileNames = new Dictionary<int, string>
        {
            { 1, "Dars rejasi(C#).pdf" },
            { 2, "txt.txt" },
            { 3, "ErrorViewModel.cs" }
        };

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        [HttpGet("{id}")]
        public ActionResult GetFileById(int id)
        {
            if (!_fileNames.TryGetValue(id, out string fileName))
            {
                fileName = "Dars rejasi(C#).pdf";
            }

            if (!System.IO.File.Exists(fileName))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(fileName);
            return File(bytes, contentType, fileName);
        }
    }
}
