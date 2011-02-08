using System.Web.Mvc;
using AutoMapper;
using RealWorldMvc3.Areas.Admin.Models;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Models;

namespace RealWorldMvc3.Core.ObjectMapping
{
    public class CategoryProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Category, CategoryDetail>();
            CreateMap<Category, CategoryInput>();

            CreateMap<Category, SelectListItem>()
                .ConstructUsing(x => new SelectListItem
                                         {
                                             Value = x.Id.ToString(),
                                             Text = x.Name
                                         });

            CreateMap<CategoryInput, Category>();
        }
    }
}