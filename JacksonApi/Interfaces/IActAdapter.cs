using JacksonApi.Responses;
using System;
using System.Collections.Generic;
namespace JacksonApi.Interfaces
{
    public interface IActAdapter
    {
        void DeleteAct(Guid id);
        List<Models.Act> GetActs();
        Models.Act UpsertAct(Models.Act newAct);
    }
}
