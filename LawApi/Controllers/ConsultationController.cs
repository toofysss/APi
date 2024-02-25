using LawApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // الاستشارة القانونية
    public class ConsultationController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ConsultationController(ApplicationDbContext logger) => _context = logger;
        
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Consultation>> Get()
        {
            return Ok(_context.Consultation.OrderByDescending(x => x.ID));
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert(IFormFile file,[FromForm] Consultation Consultation)
        {
            var result = await Insertfile(file);
            Consultation.Files = result;
            Consultation.Status = 0;
            Consultation.ID = 0;
            _context.Consultation.Add(Consultation);
            _context.SaveChanges();
            return Ok("success");
        }
        
        [HttpGet("{filename}")]
        public ActionResult GetFile(string filename)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\BookLegal");
            var filepath = Path.Combine(path, filename);

            if (System.IO.File.Exists(filepath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                string contentType = GetContentType(filepath);
                return File(fileBytes, contentType, filename);
            }

            return NotFound();
        }

        private static string GetContentType(string filepath)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        private static async Task<string> Insertfile(IFormFile File)
        {
            var extension = "." + File.FileName.Split('.')[^1];
            string Filename = DateTime.Now.Ticks.ToString() + extension;

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", Filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await File.CopyToAsync(stream);
                }
            
         
            return Filename;
        }

        private static async Task<string> UpdateFile(IFormFile file,string oldimage)
        {
            var extension = "." + file.FileName.Split('.')[^1];
            string filename = DateTime.Now.Ticks.ToString() + extension;

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(filepath, filename);

            // Check if the file with the specified filename exists
            string fileToDelete = oldimage;
            var filePathToDelete = Path.Combine(filepath, fileToDelete);

            if (System.IO. File.Exists(filePathToDelete))
            {
                // Delete the file if it exists
                System.IO.File.Delete(filePathToDelete);
            }

            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UploadFiles(IFormFile file, [FromForm] Consultation Consultation)
        {
            var data = _context.Consultation.FirstOrDefault(x => x.ID == Consultation.ID);
            if (data == null) return NotFound();
            var result = await UpdateFile(file, data.Files);
            data.Files = result;
           if(Consultation.Phone !=null) data.Phone = Consultation.Phone;
            if (Consultation.Type != null) data.Type = Consultation.Type;
            if (Consultation.Address != null) data.Address = Consultation.Address;
            if (Consultation.Dscrp != null) data.Dscrp = Consultation.Dscrp;
            _context.Consultation.Update(data);
            _context.SaveChanges();
            return Ok("success");
        }
        private static string RemoveFile(string oldImage)
        {

            var extension = "." + oldImage.Split('.')[^1];
            string filename = DateTime.Now.Ticks.ToString() + extension;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");


            // Delete the old file if it exists
            if (System.IO.File.Exists(Path.Combine(filePath, oldImage)))
            {
                System.IO.File.Delete(Path.Combine(filePath, oldImage));
            }

            return filename;

        }

        [HttpPut("UpdateData")]
        public ActionResult<IEnumerable<Consultation>> Update([FromBody] Consultation blog)
        {
            var blogdata = _context.Consultation.FirstOrDefault(x => x.ID == blog.ID);
            if (blogdata == null) return NotFound();
            if (blog.Status > 0) blogdata.Status = blog.Status;
            _context.Consultation.Update(blogdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpDelete("DeleteAll")]
        public ActionResult DeleteAll(int id )
        {
            var data = _context.Consultation.FirstOrDefault(x => x.ID == id);
            if (data == null) return NotFound();
            _ = RemoveFile(data.Files);
            _context.Consultation.Remove(data);
            _context.SaveChanges();
            return Ok("success");
        }
    }
}
