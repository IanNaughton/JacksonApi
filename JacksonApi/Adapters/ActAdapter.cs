using JacksonApi.DataAccess;
using JacksonApi.DataAccess.Interfaces;
using JacksonApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JacksonApi.Adapters
{
    public class ActAdapter : JacksonApi.Interfaces.IActAdapter
    {
        public IActRepository _myActRepository;

        public ActAdapter(IActRepository repository)
        {
            _myActRepository = repository;
            AutoMapper.Mapper.CreateMap<DataAccess.Act, Models.Act>();
            AutoMapper.Mapper.CreateMap<Models.Act, DataAccess.Act>();
        }

        /// <summary>
        /// Returns a list of all Jackson Acts stored in the DB
        /// </summary>
        /// <returns>All stored Jackson Acts</returns>
        public List<Models.Act> GetActs()
        {

            List<Models.Act> Volumes = new List<Models.Act>();

            try
            {
                List<DataAccess.Act> retrievedVolumes = _myActRepository.GetActs();

                foreach (DataAccess.Act item in retrievedVolumes)
                {
                    Volumes.Add(AutoMapper.Mapper.Map<Models.Act>(item));
                }
                return Volumes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts a new act into the Jackson site DB or updates an existing one (detects duplicate entries).
        /// </summary>
        /// <param name="newAct">The new act entity that will be inserted into the Jackson site DB, or the existing one will be updated</param>
        /// <returns>A response object that indicates success or failure</returns>
        public Models.Act UpsertAct(Models.Act newAct)
        {
            if (newAct == null) throw new ArgumentNullException("A null act was passed to UpsertAct. Upsert Act requires a non-null value.");

            DataAccess.Act response;

            try
            {
               
                // Does the act already exist? 
                if (newAct.Id == Guid.Empty)
                {
                    // Assign a new guid to the new incoming data. This might seem weird, but 
                    // I don't think that the client should be generating pseudo-guids in javascript 
                    // when I can create them here. 
                    newAct.Id = Guid.NewGuid();
                    response = _myActRepository.InsertAct(AutoMapper.Mapper.Map<DataAccess.Act>(newAct));
                }
                else
                {
                    DataAccess.Act actToUpdate = _myActRepository.GetActById(newAct.Id);
                    response = _myActRepository.UpdateAct(AutoMapper.Mapper.Map<DataAccess.Act>(newAct));
                }

                return AutoMapper.Mapper.Map<Models.Act>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete an act, nothing spectial.
        /// </summary>
        /// <param name="id">Exactly what you think.</param>
        public void DeleteAct(Guid id)
        {
            try
            {
                _myActRepository.DeleteAct(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}