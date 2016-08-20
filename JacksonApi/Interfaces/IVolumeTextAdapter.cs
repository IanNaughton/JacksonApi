using JacksonApi.Models;
using JacksonApi.Responses;
using System;
namespace JacksonApi.Interfaces
{
    public interface IVolumeTextAdapter
    {
        void DeleteVolumeText(int actNumber, int volumeNumber);
        Models.VolumeText GetVolumeTextByActAndVolumeNumber(int actNumber, int volumeNumber);
        Models.VolumeText GetVolumeTextByVolumeId(Guid id);
        Models.VolumeText UpsertVolumeText(int actNumber, int volumeNumber, Models.VolumeText volumeText);
    }
}
