namespace RealWorldMvc3.Core.Domain
{
    public class Product : Entity
    {
        public virtual string Name { get; set; }

        public virtual Category Category { get; set; }

        public Product()
        {
        }

        public Product(string name)
        {
            Name = name;
        }
    }
}