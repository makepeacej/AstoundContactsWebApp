using AstoundContactsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace AstoundContactsWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static List<FilesViewModel> LoadDocs = new List<FilesViewModel>();
        

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //ToDo Fix loading of files in visible format
        public IActionResult Index()
        {
            LoadDocs.Clear();
            foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(),@"wwwroot\publicDocs\"))
            {
                Console.WriteLine(item);
                GetFile(item);
            }
            
            
            return View(LoadDocs);
        }

        public void GetFile(string file)
        {
           
            
            using (var stream = System.IO.File.OpenRead(file))
            {
                FilesViewModel fileView = new FilesViewModel
                {
                    FileName = System.IO.Path.GetFileName(file),

                    File = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
                    {
                        Headers = new HeaderDictionary()
                    }
                };
                LoadDocs.Add(fileView);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Upload(FilesViewModel model)
        {
            
            model.IsResponse = true;

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/publicDocs");

            //create folder if not exist
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //get file extension
            FileInfo fileInfo = new FileInfo(model.File.FileName);
            string fileName = model.FileName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                
                model.File.CopyTo(stream);
            }
            model.IsSuccess = true;
            model.Message = "File upload successfully";
            
            return View("Index", model);
        }

        // Download file from the server
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename is not availble");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "upload", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        // Get content type
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        // Get mime types
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
    {
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
        {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"}
    };
        }


        public IActionResult UploadDocs()
        {
            FilesViewModel filesViewModel = new FilesViewModel();
            return View(filesViewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}