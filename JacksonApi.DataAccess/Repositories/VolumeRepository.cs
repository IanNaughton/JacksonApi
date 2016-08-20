using JacksonApi.DataAccess;
using JacksonApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Gateways
{
    public class VolumeRepository : RepositoryBase, IVolumeRepository
    {
        /// <summary>
        /// Returns a list of all spaceman jackson Volumes
        /// </summary>
        public List<Volume> RetrieveVolumes()
        {
            List<Volume> Volumes = new List<Volume>();
         
                SetupContext( context =>
                    Volumes = (from Volume item in context.Volumes orderby item.Number select item).ToList<Volume>()
                );
          
            return Volumes;
        }

        /// <summary>
        /// Returns an Volume with the specified Id
        /// </summary>
        /// <param name="Id">The id of the Volume to retrieve</param>
        /// <returns>An Volume of spaceman jackson</returns>
        public Volume RetrieveVolumeById(Guid Id)
        {
            Volume currentVolume = null;
            SetupContext( context =>
                    currentVolume = (from Volume item in context.Volumes where item.Id == Id select item).FirstOrDefault<Volume>()
              );
            return currentVolume;
        }

        /// <summary>
        /// Returns a list of all spaceman jackson Volumes in the specified act
        /// </summary>
        /// <param name="Act">The act whose Volumes you intend to retrieve</param>
        /// <returns>All Volumes in the specified act</returns>
        public List<Volume> RetrieveVolumeByAct(int act)
        {
            List<Volume> Volumes = new List<Volume>();
            SetupContext( context =>
                    Volumes = (from Volume item in context.Volumes where item.ActNumber == act select item).ToList<Volume>()
           );
            return Volumes;
        }

        /// <summary>
        /// Returns the specified volume by act and volume 
        /// </summary>
        /// <param name="act">The Volume act number</param>
        /// <param name="volume">The Volume volume number</param>
        /// <returns>An Volume of spaceman jackson</returns>
        public Volume RetrieveVolumeByActAndVolume(int act, int volume)
        {
            Volume currentVolume = null;
           SetupContext( context =>
                    currentVolume = (from Volume item in context.Volumes 
                                where 
                                item.Number == volume && 
                                item.ActNumber == act
                                orderby item.Number 
                                select item).FirstOrDefault<Volume>()
              );
            return currentVolume;
        }

        /// <summary>
        /// Inserts a new act into the Jackson site DB
        /// </summary>
        /// <param name="newAct">An Act entity to insert into the Jackson site DB</param>
        public Volume InsertVolume(Volume newVolume)
        {
            SetupContext(context =>
            {
                context.Volumes.Add(newVolume);
                context.SaveChanges();
            });

            return RetrieveVolumeByActAndVolume(newVolume.ActNumber, newVolume.Number);
        }

        /// <summary>
        /// Updates an existing act in the Jackson site DB
        /// </summary>
        /// <param name="updatedAct">An Act entity to update in the Jackson site DB</param>
        public Volume UpdateVolume(Volume updatedVolume)
        {
            SetupContext(context =>
            {
                context.Volumes.Attach(updatedVolume);
                var entry = context.Entry<Volume>(updatedVolume);
                entry.State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            });

            return RetrieveVolumeByActAndVolume(updatedVolume.ActNumber, updatedVolume.Number);
        }

        /// <summary>
        /// Deletes an act from the DB
        /// </summary>
        /// <param name="id">The id of the act we want to delete</param>
        public void DeleteVolume(int actNumber, int volumeNumber)
        {
            SetupContext(context =>
            {
                // UGHHHHH FUUUUUUUUUUUUU
                Volume volumeToDelete = context.Volumes.First<Volume>(volume => volume.Number == volumeNumber && volume.ActNumber == actNumber);
                context.Volumes.Remove(volumeToDelete);
                context.SaveChanges();
            }
            );
        }

    }
}
