using AutoMapper;
using RealWorldMvc3.Areas.Admin.Models;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Repositories;
using RealWorldMvc3.Models;

namespace RealWorldMvc3.Core.ObjectMapping
{
    public class ProductProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Product, ProductDetail>();
            CreateMap<Category, BrowseProducts>();

            CreateMap<Product, ProductUpdate>();

            CreateMap<ProductUpdate, Product>()
                .ConvertUsing<ProductUpdateProductConverter>();
        }
    }

    public class ProductUpdateProductConverter : TypeConverter<ProductUpdate, Product>
    {
        private readonly IRepository repository;

        public ProductUpdateProductConverter(IRepository repository)
        {
            this.repository = repository;
        }

        protected override Product ConvertCore(ProductUpdate source)
        {
            var product = repository.FindById<Product>(source.Id);
            product.Name = source.Name;

            return product;
        }
    }
}