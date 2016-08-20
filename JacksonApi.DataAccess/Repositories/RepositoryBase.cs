using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Gateways
{
    public class RepositoryBase
    {
        /// <summary>
        /// Polymorphism via a lambda! This wraps all queries and performs setup
        /// and teardown on all calls that hit DB.
        /// </summary>
        /// <param name="query">Query, non-query, whatever</param>
        protected void SetupContext(Action<JacksonSiteEntities> query)
        {
            try
            {
                using (JacksonApi.DataAccess.JacksonSiteEntities context = new JacksonSiteEntities())
                {
                    query(context);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
