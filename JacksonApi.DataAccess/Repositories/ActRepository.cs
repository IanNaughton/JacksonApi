using JacksonApi.DataAccess;
using JacksonApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Gateways
{
    public class ActRepository : RepositoryBase, IActRepository
    {     
        /// <summary>
        /// Resturns a list of all acts in the Jackson site DB
        /// </summary>
        /// <returns>A list of all current act entities</returns>
        public List<Act> GetActs()
        {
            List<Act> result = new List<Act>();

            // It's a closure! :D 
            SetupContext(context =>
            {
                result = (from Act item in context.Acts orderby item.Number descending select item).ToList<Act>();
            });             
            return result;
        }

        /// <summary>
        /// Retrieves an act by act number
        /// </summary>
        /// <param name="actNumber">The number of the act that will be retrieved</param>
        /// <returns>An act with the specified number (or null)</returns>
        public Act GetActByNumber(int actNumber)
        {
            Act result = null;

            SetupContext(context =>
            {
                result = (from Act item in context.Acts where item.Number == actNumber select item).FirstOrDefault<Act>();
            });
            return result;
        }

        /// <summary>
        /// Retrieves an act by act Id
        /// </summary>
        /// <param name="actId">The Id of the act that will be retrieved</param>
        /// <returns>An act with the specified Id (or null)</returns>
        public Act GetActById(Guid actId)
        {
            Act result = null;

            SetupContext(context =>
            {
                result = (from Act item in context.Acts where item.Id == actId select item).FirstOrDefault<Act>();
            });
            return result;
        }

        /// <summary>
        /// Inserts a new act into the Jackson site DB
        /// </summary>
        /// <param name="newAct">An Act entity to insert into the Jackson site DB</param>
        public Act InsertAct(Act newAct)
        {
            SetupContext(context =>
            {
                context.Acts.Add(newAct);
                context.SaveChanges();
            });

            return GetActByNumber(newAct.Number);
        }

        /// <summary>
        /// Updates an existing act in the Jackson site DB
        /// </summary>
        /// <param name="updatedAct">An Act entity to update in the Jackson site DB</param>
        public Act UpdateAct(Act updatedAct)
        {
            SetupContext(context =>
            {
                context.Acts.Attach(updatedAct);
                var entry = context.Entry<Act>(updatedAct);
                entry.State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            });

            return GetActByNumber(updatedAct.Number);
        }

        /// <summary>
        /// Deletes an act from the DB
        /// </summary>
        /// <param name="id">The id of the act we want to delete</param>
        public void DeleteAct(Guid id)
        {
            SetupContext(context =>
                {
                    // UGHHHHH WAIIIIIIIII FUUUUUUUUUUUUU
                    Act actToDelete = (from Act act in context.Acts where act.Id == id select act).First<Act>();
                    context.Acts.Remove(actToDelete);
                    context.SaveChanges();
                }
            );
        }
    }
}
