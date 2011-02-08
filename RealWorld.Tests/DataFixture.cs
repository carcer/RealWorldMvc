using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using RealWorldMvc3.Core.Domain;
using RealWorldMvc3.Core.Persistence;

namespace RealWorld.Tests
{
    [TestFixture]
    public class DataFixture
    {
        [Test]
        public void createSchema()
        {
            var defaultNHibernateConfiguration = new DefaultNHibernateConfiguration();
            var configuration = defaultNHibernateConfiguration.GetConfiguration();
            var schemaExport = new SchemaExport(configuration);
            schemaExport.Drop(true, true);
            schemaExport.Execute(true, true, false);
            var buildSessionFactory = configuration.BuildSessionFactory();
            var session = buildSessionFactory.OpenSession();


            IEnumerable<Category> categories = CreateCategories(session, "Books", "DVDs", "CDs");
            var random = new Random();

            foreach (var category in categories)
            {
                AddSomeProducts(session, category, random.Next(1, 20));
            }
        }

        private void AddSomeProducts(ISession session, Category category, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var product = new Product("Product " + i);
                category.AddProduct(product);
            }
            WithTransaction(session, () => session.SaveOrUpdate(category));
        }

        private IEnumerable<Category> CreateCategories(ISession session, params string[] categoryNames)
        {
            return categoryNames.Select(x =>
                                            {
                                                var cat = new Category(x);
                                                WithTransaction(session, () => session.SaveOrUpdate(cat));
                                                return cat;
                                            });
        }

        private void WithTransaction(ISession session, Action action)
        {
            using (var tx = session.BeginTransaction())
            {
                action();
                tx.Commit();
            }
        }
    }
}