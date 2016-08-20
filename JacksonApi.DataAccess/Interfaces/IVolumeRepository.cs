using JacksonApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Interfaces
{
    public interface IVolumeRepository
    {
        List<Volume> RetrieveVolumes();

        Volume RetrieveVolumeById(Guid Id);

        List<Volume> RetrieveVolumeByAct(int act);

        Volume RetrieveVolumeByActAndVolume(int act, int volume);

        Volume InsertVolume(Volume newVolume);

        Volume UpdateVolume(Volume updatedVolume);

        void DeleteVolume(int actNumber, int volumeNumber);
    }
}
