using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

    public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
    {
        _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ??
                                            throw new System.ArgumentNullException(
                                                nameof(fileExtensionContentTypeProvider));
    }
    
    
    
    
    
    
    
    
    
    
    
    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
        string pathToFile = "test.pdf";

        if (!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }

        byte[] bytes = System.IO.File.ReadAllBytes(pathToFile);
        return File(bytes , "text/plain", Path.GetFileName(pathToFile));
    }
    
}