using JacksonApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JacksonApi.DataAccess.Interfaces
{
    public interface IVolumeTextRepository
    {
        VolumeText GetVolumeTextByVolumeId(Guid volumeId);

        VolumeText GetVolumeTextByActAndVolumeNumber(int actNumber, int volumeNumber);

        VolumeText InsertVolumeText(VolumeText volumeToInsert);

        VolumeText UpdateVolumeText(VolumeText volumeToUpdate);

        void DeleteVolumeText(Guid volumeId);
    }
}
