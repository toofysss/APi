using LawApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LaweysControll:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LaweysControll(ApplicationDbContext logger) => _context = logger;
        
       [HttpGet("GetAllS")]
        public ActionResult<IEnumerable<Laweys>> GetAll()
        {         
            return Ok(_context.Laweys.OrderByDescending(x=>x.ID));
        }
        [HttpGet("GetBy")]
        public ActionResult<IEnumerable<Laweys>> GetBy(string depart, string govermentID, string room)
        {
            IQueryable<Laweys> laweys = _context.Laweys;
            if (!string.IsNullOrEmpty(depart))laweys = laweys.Where(l => l.Depart == depart);   
            if (!string.IsNullOrEmpty(room)) laweys = laweys.Where(l => l.Room == room);
            if (!string.IsNullOrEmpty(govermentID)) laweys = laweys.Where(l => l.CityID.ToString() == govermentID);            
            return Ok(laweys.Where(x => x.Status == 1));
            
        }
        [HttpPost("InsertLaweys")]
        public async Task<ActionResult<IEnumerable<Laweys>>> Insert(IFormFile img, IFormFile imgCardLaw, [FromForm] Laweys Consultation)
        {
            var Img = await Insertfiles(img);
            var ImgCardLaw = await Insertfiles(imgCardLaw);

            Consultation.Img = Img;
            Consultation.Imgcardlaw = ImgCardLaw;
            Consultation.ID = 0;
            _context.Laweys.Add(Consultation);
            _context.SaveChanges();
            return Ok(Consultation.ID);
        }

        [HttpGet("{filename}")]
        public ActionResult GetImage(string filename)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\laweys");
            var filepath = Path.Combine(path, filename);

            if (System.IO.File.Exists(filepath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filepath);
                return File(b, "image/png");
            }

            return NotFound();
        }

        private static async Task<string> Insertfiles(IFormFile File)
        {
            var extension = "." + File.FileName.Split('.')[^1];
            string Filename = DateTime.Now.Ticks.ToString() + extension;

            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\laweys");

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\laweys", Filename);
            using (var stream = new FileStream(exactpath, FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }


            return Filename;
        }

        private static string RemoveFile(string oldImage)
        {

            var extension = "." + oldImage.Split('.')[^1];
            string filename = DateTime.Now.Ticks.ToString() + extension;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\laweys");


            // Delete the old file if it exists
            if (System.IO.File.Exists(Path.Combine(filePath, oldImage)))
            {
                System.IO.File.Delete(Path.Combine(filePath, oldImage));
            }

            return filename;

        }

        [HttpDelete("DeleteAll")]
        public ActionResult DeleteAll(int id)
        {
            var data = _context.Laweys.FirstOrDefault(x => x.ID == id);
            if (data == null) return NotFound();
            _ = RemoveFile(data.Img);
            _ = RemoveFile(data.Imgcardlaw);

            _context.Laweys.Remove(data);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<Laweys>>Update([FromBody] Laweys laweys)
        {
            var data = _context.Laweys.FirstOrDefault(x => x.ID == laweys.ID);
            if (data == null) return NotFound();
            if (laweys.Rating >0) data.Rating = laweys.Rating;
             data.Status = laweys.Status;
            _context.Laweys.Update(data);
            _context.SaveChanges();
            return Ok(laweys.ID);
        }
    }
}
