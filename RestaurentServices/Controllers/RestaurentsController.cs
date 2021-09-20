using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurentServices.Data;
using RestaurentServices.Models;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace RestaurentServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        
        
        public RestaurentsController(DataContext context, IWebHostEnvironment en)
        {
            _context = context;
            _env = en;
        }

       
        [HttpGet]
        public ActionResult GetRestaurents()
        {
            try
            {
                              var result = (from r in _context.Restaurents
                              join m in _context.Menus on r.RestaurentId equals m.RestaurentId
                              join c in _context.Categories on m.MenuId equals c.MenuId
                              join t in _context.items on c.CategoryId equals t.CategoryId
                          select new Restaurent
                          { 
                             RestaurentId = r.RestaurentId,
                             RestaurentName=r.RestaurentName,
                             Address = r.Address,
                             RestaurentImage=r.RestaurentImage,
                             menu= new List<Menu>
                             {
                               new Menu
                               {
                                 MenuId=m.MenuId,
                                 RestaurentId=m.RestaurentId,
                                 
                                 categories = new List<Category>
                                 {
                                     new Category
                                     {
                                         CategoryId=c.CategoryId,
                                         Name=c.Name,
                                         MenuId=c.MenuId,
                                         Items=new List<Item>
                                         {
                                             new Item
                                             {
                                                Id=t.Id,
                                                item=t.item,
                                                price=t.price,
                                                veg=t.veg,
                                                nonveg=t.nonveg,
                                                IsAvailable=t.IsAvailable,
                                                Ingredians=t.Ingredians,
                                                Description=t.Description,
                                                CategoryId=t.CategoryId,
                                             }
                                         }
                                     }
                                 }
                               }
                             }
                          }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpGet("filter")]
        public ActionResult Filter(int id,string ItemName)
        {
                //if (ItemName == "NonVeg-Starter")
                //{
                    var result = (from r in _context.Restaurents
                                  join m in _context.Menus on r.RestaurentId equals m.RestaurentId
                                  where m.RestaurentId == id
                                  join c in _context.Categories on m.MenuId equals c.MenuId where c.Name== ItemName
                                  join t in _context.items on c.CategoryId equals t.CategoryId 
                                  select new Restaurent
                                  {
                                      RestaurentId = r.RestaurentId,
                                      RestaurentName = r.RestaurentName,
                                      Address = r.Address,
                                      RestaurentImage = r.RestaurentImage,
                                      menu = new List<Menu>
                                      {
                                           new Menu
                                           {
                                             MenuId=m.MenuId,
                                             RestaurentId=m.RestaurentId,

                                                categories = new List<Category>
                                                {
                                                    new Category
                                                    {
                                                     CategoryId=c.CategoryId,
                                                     Name=c.Name,
                                                     MenuId=c.MenuId,
                                                        Items=new List<Item>
                                                        {
                                                           new Item
                                                           {
                                                                Id=t.Id,
                                                                item=t.item,
                                                                price=t.price,
                                                                veg=t.veg,
                                                                nonveg=t.nonveg,
                                                                IsAvailable=t.IsAvailable,
                                                                Ingredians=t.Ingredians,
                                                                Description=t.Description,
                                                                CategoryId=t.CategoryId,
                                                           }
                                                        }
                                                    }
                                                }
                                           }
                                      }
                                  }).ToList();

                    return Ok(result);

                    //var a = _context.Categories.Include(x => x.Items).ToList().Where(x => x.Name == ItemName);

                    //return new JsonResult(a);

                
                //else if (ItemName == "Nonveg-Maincourse")
                //{
                //    var result = (from r in _context.Restaurents
                //                  join m in _context.Menus on r.RestaurentId equals m.RestaurentId
                //                  where m.RestaurentId == id
                //                  join c in _context.Categories on m.MenuId equals c.MenuId
                //                  where c.Name == ItemName
                //                  join t in _context.items on c.CategoryId equals t.CategoryId
                //                  select new Restaurent
                //                  {
                //                      RestaurentId = r.RestaurentId,
                //                      RestaurentName = r.RestaurentName,
                //                      Address = r.Address,
                //                      RestaurentImage = r.RestaurentImage,
                //                      menu = new List<Menu>
                //              {
                //               new Menu
                //               {
                //                 MenuId=m.MenuId,
                //                 RestaurentId=m.RestaurentId,

                //                 categories = new List<Category>
                //                 {
                //                     new Category
                //                     {
                //                         CategoryId=c.CategoryId,
                //                         Name=c.Name,
                //                         MenuId=c.MenuId,
                //                         Items=new List<Item>
                //                         {
                //                             new Item
                //                             {
                //                                Id=t.Id,
                //                                item=t.item,
                //                                price=t.price,
                //                                veg=t.veg,
                //                                nonveg=t.nonveg,
                //                                IsAvailable=t.IsAvailable,
                //                                Ingredians=t.Ingredians,
                //                                Description=t.Description,
                //                                CategoryId=t.CategoryId,
                //                             }
                //                         }
                //                     }
                //                 }
                //               }
                //             }
                //                  }).ToList();

                //    return Ok(result);

                //    //var a = _context.Categories.Include(x => x.Items).ToList().Where(x => x.Name == ItemName);


                //    //return new JsonResult(a);
                //}
                //else if (ItemName == "Veg-Maincourse")
                //{
                //    var a = _context.Categories.Include(x => x.Items).ToList().Where(x => x.Name == ItemName);


                //    return new JsonResult(a);
                //}

                //else if (ItemName == "Veg-Starter")
                //{

                //    var a = _context.Categories.Include(x => x.Items).ToList().Where(x => x.Name == ItemName);


                //    return new JsonResult(a);

                //}
                //else if (ItemName == "drinks")
                //{

                //    var a = _context.Categories.Include(x => x.Items).ToList().Where(x => x.Name == ItemName);

                //    return new JsonResult(a);

                //}

                //else if (ItemName == "Deserts")
                //{

                //    var a = _context.Categories.Include(x => x.Items).ToList().Where(x => x.Name == ItemName);

                //    return new JsonResult(a);

                //}
                //else
                //{
                //    return Content("invaliddata");
                //}
                return Ok();
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurent>> GetRestaurent(int id)
        {
            try
            {


                var result = (from r in _context.Restaurents
                              join m in _context.Menus on r.RestaurentId equals m.RestaurentId where m.RestaurentId == id
                              join c in _context.Categories on m.MenuId equals c.MenuId
                              join t in _context.items on c.CategoryId equals t.CategoryId
                              select new Restaurent
                              {
                                  RestaurentId = r.RestaurentId,
                                  RestaurentName = r.RestaurentName,
                                  Address = r.Address,
                                  RestaurentImage = r.RestaurentImage,
                                  menu = new List<Menu>
                                  {
                               new Menu
                               {
                                 MenuId=m.MenuId,
                                 RestaurentId=m.RestaurentId,

                                 categories = new List<Category>
                                 {
                                     new Category
                                     {
                                         CategoryId=c.CategoryId,
                                         Name=c.Name,
                                         MenuId=c.MenuId,
                                         Items=new List<Item>
                                         {
                                             new Item
                                             {
                                                Id=t.Id,
                                                item=t.item,
                                                price=t.price,
                                                veg=t.veg,
                                                nonveg=t.nonveg,
                                                IsAvailable=t.IsAvailable,
                                                Ingredians=t.Ingredians,
                                                Description=t.Description,
                                                CategoryId=t.CategoryId,
                                             }
                                         }
                                     }
                                 }
                               }
                                  }
                              }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurent(int id, Restaurent restaurent)
        {
            if (id != restaurent.RestaurentId)
            {
                return BadRequest();
            }

            _context.Entry(restaurent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public ActionResult PostRestaurent(Restaurent restaurent)
        {
            _context.Restaurents.Add(restaurent);
            _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurent", new { id = restaurent.RestaurentId }, restaurent);
        }
        [HttpDelete("MenuDelete")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("SaveFile/{itemId}")]
        public async Task<IActionResult> SaveFileAsync(IFormFile formFile,int itemId)
        {
            var item = _context.items.FirstOrDefault(e => e.Id == itemId);
             var filestream = formFile.OpenReadStream();
            var containerName = "restaurentimages";
            FileInfo fileInfo = new FileInfo(formFile.FileName);
            var fileName = item.item.ToString() + fileInfo.Extension;
            var storageAccount_connectionString = "DefaultEndpointsProtocol=https;AccountName=restaurentstoragesccount;AccountKey=4jdgry0uPaptWalRREWDXwB1Lt4YbavBFc3aXJRopyhw+TCZQQYvpQvnhB+b0y88Sk8SYerXzzrAa89vJzTIXg==;EndpointSuffix=core.windows.net";
            var blobServiceClient = new BlobClient(connectionString: storageAccount_connectionString, blobContainerName: containerName, blobName:fileName);
            await blobServiceClient.UploadAsync(filestream);
            item.ImageTitle = fileName;
            await _context.SaveChangesAsync();
            return Ok("Uploaded Success");
        }
        
        [HttpPost("Menus")]
        public ActionResult Post(Menu menu)
        {
            {
                _context.Menus.Add(menu);
                _context.SaveChanges();

                return Ok();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurent(int id)
        {
            var restaurent = await _context.Restaurents.FindAsync(id);
            if (restaurent == null)
            {
                return NotFound();
            }

            _context.Restaurents.Remove(restaurent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurentExists(int id)
        {
            return _context.Restaurents.Any(e => e.RestaurentId == id);
        }
    }
}
