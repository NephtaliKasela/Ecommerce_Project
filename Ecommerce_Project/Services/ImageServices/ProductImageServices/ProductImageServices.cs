using AutoMapper;
using Ecommerce_Project.Data;
using Ecommerce_Project.DTOs.Images.ProductImage;
using Ecommerce_Project.DTOs.Product;
using Ecommerce_Project.Models;
using Ecommerce_Project.Models.Images;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_Project.Services.ImageServices.ProductImageServices
{
    public class ProductImageServices : IProductImageServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductImageServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<GetProductDTO>> AddProductImage(AddProductImageDTO newProductImage)
        {
            var serviceResponse = new ServiceResponse<GetProductDTO>();
            if (newProductImage.files.Count > 0)
            {
                foreach (var file in newProductImage.files)
                {
                    if (file != null && file.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            file.CopyTo(memoryStream);
                            var imageData = memoryStream.ToArray();

                            //Get Product by id
                            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == newProductImage.ProductId);

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
                                image.Product = product;
                                // add the image to the list of product images
                                serviceResponse.Data = _mapper.Map<GetProductDTO>(product);
                            }
                            //image.Product = newProductImage.;
                            await _context.ProductImages.AddAsync(image);
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return serviceResponse;
        }

        private string GetImageContentType(string fileName)
        {
            return Path.GetExtension(fileName)?.ToLowerInvariant();

            //string extension = Path.GetExtension(fileName)?.ToLowerInvariant();

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
