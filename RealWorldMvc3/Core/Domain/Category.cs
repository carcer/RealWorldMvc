using Iesi.Collections.Generic;

namespace RealWorldMvc3.Core.Domain
{
    public class Category : Entity
    {
        public virtual string Name { get; private set; }

        public virtual ISet<Product> Products { get; set; }

        public Category()
        {
            Products = new HashedSet<Product>();
        }

        public Category(string name) : this()
        {
            Name = name;
        }

        public virtual void AddProduct(Product product)
        {
            Products.Add(product);
            product.Category = this;
        }
    }
}