using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.Models;
using Ecommerce_Project.Models.Images;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
//using static System.Net.Mime.MediaTypeNames;

namespace Ecommerce_Project.Services.ImageServices
{
    public class ImageServices : IImageServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ImageServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddProductImage(int productId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var imageData = memoryStream.ToArray();

                    //Get Product by id
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);

                    // Save the imageData to the database using your data access logic
                    // For example, using Entity Framework Core:
                    ProductImage image = new ProductImage
                    {
                        // Set other properties of the model
                        ImageData = imageData,
                        FileName = file.FileName,
                        ContentType = GetImageContentType(file.FileName),
                    };

                    if (product != null)
                    {
                        image.BodyProduct = product;
                    }

                    // Save the model to the database
                    _context.ProductImages.Add(image);
                    await _context.SaveChangesAsync();
                }
            }
        }

        //public async Task<ServiceResponse<List<GetProductImageDTO>>> GetImage()
        //{
        //    var serviceResponse = new ServiceResponse<List<GetProductImageDTO>>();
        //    var images = await _context.ProductImages.ToListAsync();

        //    serviceResponse.Data = images.Select(i => _mapper.Map<GetProductImageDTO>(i)).ToList();

        //    return serviceResponse;

        //}


        private string GetImageContentType(string fileName)
        {
            return Path.GetExtension(fileName)?.ToLowerInvariant();

            //return string extension = Path.GetExtension(fileName)?.ToLowerInvariant();

            //switch (extension)
            //{
            //    case ".jpg":
            //    case ".jpeg":
            //        return "image/jpeg";
            //    case ".png":
            //        return "image/png";
            //    case ".gif":
            //        return "image/gif";
            //    // Add more cases for other image formats if needed
            //    default:
            //        return "application/octet-stream"; // Default to binary data if the format is unknown
            //}
        }
    }
}
