using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RealWorldMvc3.Areas.Admin.Models
{
    public class ProductUpdate
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [UIHint("Category")]
        public Guid CategoryId { get; set; }
    }
}