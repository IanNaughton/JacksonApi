using JacksonApi.DataAccess.Interfaces;
using JacksonApi.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace JacksonApi.Adapters
{
    public class VolumeTextAdapter : JacksonApi.Interfaces.IVolumeTextAdapter
    {

        private IVolumeRepository _myVolumeRepository;
        private IVolumeTextRepository _myTextRepository;

        /// <summary>
        /// Set up automapper for this gateway class. In this instance, 
        /// we only need to worry about mapping back to our model poco.
        /// </summary>
        public VolumeTextAdapter(IVolumeRepository volumeRepo, IVolumeTextRepository volumeTextRepo)
        {
            _myVolumeRepository = volumeRepo;
            _myTextRepository = volumeTextRepo;

            AutoMapper.Mapper.CreateMap<DataAccess.VolumeText, Models.VolumeText>();
            AutoMapper.Mapper.CreateMap<Models.VolumeText, DataAccess.VolumeText>();
        }

        /// <summary>
        /// Returns the actual copy for an episode of spaceman jackson based on volumeId
        /// </summary>
        /// <param name="id">The volumne id for the text you would like to retrieve</param>
        /// <returns>A volume text entity that contains the copy for a volume of jackson</returns>
        public Models.VolumeText GetVolumeTextByVolumeId(Guid id)
        {
            Models.VolumeText newVolumeText = null;
            try     
	        {
                DataAccess.VolumeText retrievedVolumeText = _myTextRepository.GetVolumeTextByVolumeId(id);

                if (retrievedVolumeText != null)
                {
                    newVolumeText = AutoMapper.Mapper.Map<Models.VolumeText>(retrievedVolumeText); 
                }
	        }
	        catch (Exception)
	        {
		        throw;
	        }
            return newVolumeText;
        }

        /// <summary>
        /// Returns the actual copy for an episode of spaceman jackson based on act and volume
        /// </summary>
        /// <param name="actNumber">The act number of the episode</param>
        /// <param name="volumeNumber">The volume number of the episode</param>
        /// <returns>A volume text entity that contains the copy for a volume of jackson</returns>
        public Models.VolumeText GetVolumeTextByActAndVolumeNumber(int actNumber, int volumeNumber)
        {
            Models.VolumeText newVolumeText = null;
            try
            {
                DataAccess.Volume retrievedVolume = _myVolumeRepository.RetrieveVolumeByActAndVolume(actNumber, volumeNumber);
                DataAccess.VolumeText retrievedVolumeText = _myTextRepository.GetVolumeTextByActAndVolumeNumber(actNumber, volumeNumber);

                if (retrievedVolumeText != null)
                {
                    newVolumeText = AutoMapper.Mapper.Map<Models.VolumeText>(retrievedVolumeText);
                    newVolumeText.Text = RemoveWhitespaceAndNewLines(newVolumeText.Text);
                    newVolumeText.Name = retrievedVolume.Name;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return newVolumeText;
        }


        public Models.VolumeText UpsertVolumeText(int actNumber, int volumeNumber, Models.VolumeText volumeText)
        {
            Models.VolumeText response = new Models.VolumeText();
            try
            {
                DataAccess.Volume retrievedVolume = _myVolumeRepository.RetrieveVolumeByActAndVolume(actNumber, volumeNumber);
                DataAccess.VolumeText retrievedVolumeText = _myTextRepository.GetVolumeTextByActAndVolumeNumber(actNumber, volumeNumber);
                

                // If the volume we are associating text to exists
                if (retrievedVolume != null)
                {
                    DataAccess.VolumeText newVolumeText = new DataAccess.VolumeText()
                    {
                        VolumeId = retrievedVolume.Id,
                        Text = volumeText.Text
                    };
                    
                    // If volume text exists, use the existing Id, otherwise create a new Id
                    if (retrievedVolumeText != null)
                    {
                        newVolumeText.Id = retrievedVolumeText.Id;
                        _myTextRepository.UpdateVolumeText(newVolumeText);
                    }
                    else
                    {
                        newVolumeText.Id = Guid.NewGuid();
                        _myTextRepository.InsertVolumeText(newVolumeText);
                    }
                }
            }
            catch (Exception)
            {  
                throw;
            }

            return AutoMapper.Mapper.Map<Models.VolumeText>(response);
        }

        /// <summary>
        /// This method deleted volume text by act and volume number. I hate how this method is currently
        /// working. Totally dumb.
        /// </summary>
        /// <param name="actNumber"></param>
        /// <param name="volumeNumber"></param>
        public void DeleteVolumeText(int actNumber, int volumeNumber)
        {
            try
            {
                DataAccess.Volume volumeToDelete = _myVolumeRepository.RetrieveVolumeByActAndVolume(actNumber, volumeNumber);
                _myTextRepository.DeleteVolumeText(volumeToDelete.Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the newline, tab, and carriage return characters from strings
        /// </summary>
        /// <param name="text">The text to remove formatting characters from</param>
        /// <returns>The input text without newline, tab, or carraige return characters</returns>
        private string RemoveWhitespaceAndNewLines(string text)
        {
            return Regex.Replace(text, "(\\n|\\r|\\t)", string.Empty);
        }
    }
}