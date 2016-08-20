using JacksonApi.DataAccess.Interfaces;
using JacksonApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JacksonApi.Adapters
{
    public class VolumeAdapter : JacksonApi.Interfaces.IVolumeAdapter
    {
        IVolumeRepository _myVolumeRepository;
         
        public VolumeAdapter(IVolumeRepository repository)
        {
            _myVolumeRepository = repository;
            AutoMapper.Mapper.CreateMap<DataAccess.Volume, Models.Volume>();
            AutoMapper.Mapper.CreateMap<Models.Volume, DataAccess.Volume>();
        }

        /// <summary>
        /// Returns a list of all Jackson Volumes stored in the DB
        /// </summary>
        /// <returns>All stored Jackson Volumes</returns>
        public List<Models.Volume> GetAllVolumes()
        {
            
            List<Models.Volume> Volumes = new List<Models.Volume>();
           
            try
            {
                List<DataAccess.Volume> retrievedVolumes = _myVolumeRepository.RetrieveVolumes();

                foreach (DataAccess.Volume item in retrievedVolumes)
                {
                    Volumes.Add(AutoMapper.Mapper.Map<Models.Volume>(item));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Volumes;
        }

        /// <summary>
        /// Returns an Volume of Jackson that corresponds to the Id that was passed
        /// </summary>
        /// <param name="Id">The Id of the Volume to retrieve</param>
        /// <returns>An Volume whose Id corresponds to the Id that was passed in</returns>
        public Models.Volume GetVolumeById(Guid Id)
        {
            Models.Volume currentVolume = null;

            try
            {
                DataAccess.Volume retrievedVolume = _myVolumeRepository.RetrieveVolumeById(Id);

                if (retrievedVolume != null)
                {
                    currentVolume = AutoMapper.Mapper.Map<Models.Volume>(retrievedVolume);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return currentVolume;
        }

       /// <summary>
       /// Returns all Volumes in the specified act
       /// </summary>
       /// <param name="act">The act you want to retrieve</param>
       /// <returns>All Volumes in a act</returns>
        public List<Models.Volume> GetVolumesByAct(int act)
        {
            List<Models.Volume> Volumes = new List<Models.Volume>();

            try
            {
                List<DataAccess.Volume> retrievedVolumes = _myVolumeRepository.RetrieveVolumeByAct(act);

                foreach (DataAccess.Volume item in retrievedVolumes)
                {
                    Volumes.Add(AutoMapper.Mapper.Map<Models.Volume>(item));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Volumes;
        }

        /// <summary>
        /// Returns an Volume by act and volume number. These two values are effectively a composite key.
        /// </summary>
        /// <param name="act">The Volume act number</param>
        /// <param name="volume">The Volume volume number</param>
        /// <returns>An Volume with the specified act and volume number</returns>
        public Models.Volume GetVolumeByActAndVolume(int act, int volume)
        {
            Models.Volume currentVolume = null;

            try
            {
                DataAccess.Volume retrievedVolume = _myVolumeRepository.RetrieveVolumeByActAndVolume(act, volume);

                if (retrievedVolume != null)
                {
                    currentVolume = AutoMapper.Mapper.Map<Models.Volume>(retrievedVolume);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return currentVolume;
        }

        /// <summary>
        /// Inserts a new act into the Jackson site DB or updates an existing one (detects duplicate entries).
        /// </summary>
        /// <param name="newAct">The new act entity that will be inserted into the Jackson site DB, or the existing one will be updated</param>
        /// <returns>A response object that indicates success or failure</returns>
        public Models.Volume UpsertVolume(Models.Volume newVolume)
        {
            if (newVolume == null) throw new ArgumentNullException("A null act was passed to UpsertVolume. Upsert Volume requires a non-null value.");

            DataAccess.Volume response = new DataAccess.Volume();

            try
            {

                // Does the act already exist? 
                if (_myVolumeRepository.RetrieveVolumeByActAndVolume(newVolume.ActNumber, newVolume.Number) == null)
                {
                    // Assign a new guid to the new incoming data. This might seem weird, but 
                    // I don't think that the client should be generating pseudo-guids in javascript 
                    // when I can create them here. 
                    newVolume.Id = Guid.NewGuid();
                    _myVolumeRepository.InsertVolume(AutoMapper.Mapper.Map<DataAccess.Volume>(newVolume));
                }
                else
                {
                    DataAccess.Volume volumeToUpdate = _myVolumeRepository.RetrieveVolumeById(newVolume.Id);
                    _myVolumeRepository.UpdateVolume(AutoMapper.Mapper.Map<DataAccess.Volume>(newVolume));
                }

                return AutoMapper.Mapper.Map<Models.Volume>(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a volume, nothing spectial.
        /// </summary>
        /// <param name="id">Exactly what you think.</param>
        public void DeleteVolume(int actNumber, int volumeNumber)
        {
            try
            {
                _myVolumeRepository.DeleteVolume(actNumber, volumeNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}