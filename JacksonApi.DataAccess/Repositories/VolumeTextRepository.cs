using JacksonApi.DataAccess;
using JacksonApi.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Gateways
{
    public class VolumeTextRepository : RepositoryBase, IVolumeTextRepository
    {
        /// <summary>
        /// Retrieves a singe volume text by volume Id 
        /// </summary>
        /// <param name="volumeId">The volume Id associated with the text you would like to retrieve</param>
        /// <returns>The copy for a single volume</returns>
        public VolumeText GetVolumeTextByVolumeId(Guid volumeId)
        {
            VolumeText result = new VolumeText();
  
            SetupContext(context =>
            {
                result = (from VolumeText item in context.VolumeTexts where item.VolumeId == volumeId select item).FirstOrDefault<VolumeText>();
            });
          
            return result;
        }

        /// <summary>
        /// Retreive the actual text for a volume of spaceman jackson based on the act and 
        /// volume number
        /// </summary>
        /// <param name="actNumber">The number of the desired act</param>
        /// <param name="volumeNumber">The number of the desired volume</param>
        /// <returns>The actual text content for an episode of spaceman jackson</returns>
        public VolumeText GetVolumeTextByActAndVolumeNumber(int actNumber, int volumeNumber)
        {
            VolumeText result = new VolumeText();
            SetupContext( context =>
            {
                // Use the Volume Id to retrieve the actual volume text... Possibly a bad design decision
                Guid volumeId = (from Volume item in context.Volumes where item.ActNumber == actNumber && item.Number == volumeNumber select item.Id).FirstOrDefault<Guid>();
                    
                if (volumeId != null)
                {
                    // Sidestep any null reference issues and retrieve the actual text for the volume
                    result = (from VolumeText text in context.VolumeTexts where text.VolumeId == volumeId select text).FirstOrDefault<VolumeText>();
                }
            });
       
            return result;
        }
        
        /// <summary>
        /// Inserts a new volume into the volume text repository
        /// </summary>
        /// <param name="volumeToInsert">The volume text you want to insert</param>
        public VolumeText InsertVolumeText(VolumeText volumeToInsert)
        {
            SetupContext(context => {
                context.VolumeTexts.Add(volumeToInsert);
                context.SaveChanges();
            });

            return GetVolumeTextByVolumeId(volumeToInsert.VolumeId);
        }

        /// <summary>
        ///  Updates an existing volume text entry in the volume text repository
        /// </summary>
        /// <param name="volumeToUpdate">The volume text you would like to update</param>
        public VolumeText UpdateVolumeText(VolumeText volumeToUpdate)
        {
            SetupContext(context =>
            {
                context.VolumeTexts.Attach(volumeToUpdate);
                var entry = context.Entry<VolumeText>(volumeToUpdate);
                entry.State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            });

            return GetVolumeTextByVolumeId(volumeToUpdate.VolumeId);
        }

        public void DeleteVolumeText(Guid volumeId)
        {
            SetupContext(context =>
            {
                VolumeText volumeTextToDelete = context.VolumeTexts.First<VolumeText>(text => text.VolumeId == volumeId);
                context.VolumeTexts.Remove(volumeTextToDelete);
                context.SaveChanges();
            });
        }
    }
}
