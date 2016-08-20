
using JacksonApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Interfaces
{
    public interface IActRepository
    {
        List<Act> GetActs();

        Act GetActByNumber(int actNumber);

        Act GetActById(Guid actId);

        Act InsertAct(Act newAct);

        Act UpdateAct(Act updateAct);

        void DeleteAct(Guid id);
    }
}
