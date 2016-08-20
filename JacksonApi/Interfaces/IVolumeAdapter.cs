using JacksonApi.Models;
using JacksonApi.Responses;
using System;
using System.Collections.Generic;
namespace JacksonApi.Interfaces
{
    public interface IVolumeAdapter
    {
        void DeleteVolume(int actNumber, int volumeNumber);
        List<Models.Volume> GetAllVolumes();
        Models.Volume GetVolumeByActAndVolume(int act, int volume);
        Models.Volume GetVolumeById(Guid Id);
        List<Models.Volume> GetVolumesByAct(int act);
        Models.Volume UpsertVolume(Models.Volume newVolume);
    }
}
