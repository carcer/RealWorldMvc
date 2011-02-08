using System.Collections.Generic;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Castle.Services.Transaction;
using NHibernate;
using NHibernate.Linq;

namespace RealWorldMvc3.Core.Repositories
{
    [Transactional]
    public class NHibernateRepository : IRepository
    {
        private readonly ISessionManager sessionManager;

        public NHibernateRepository(ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
        }

        protected internal virtual ISession Session
        {
            get
            {
                return sessionManager.OpenSession();
            }
        }

        public T FindById<T>(object id)
        {
            return Session.Get<T>(id);
        }

        public T LoadForId<T>(object id)
        {
            return Session.Load<T>(id);
        }

        public IEnumerable<T> FindAll<T>()
        {
            return Session.CreateCriteria(typeof(T)).Future<T>();
        }

        public IQueryable<T> Query<T>()
        {
            return Session.Query<T>();
        }

        [Transaction(TransactionMode.Requires)]
        public void Save<T>(T entity)
        {
            Session.SaveOrUpdate(entity);
        }

        [Transaction(TransactionMode.Requires)]
        public void Delete<T>(T entity)
        {
            Session.Delete(entity);
        }
    }
}