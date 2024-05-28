using Azure;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.Extensions.Hosting;
using System;

namespace Kojg_Ragnarock_Guide.Interfaces
{
    public interface IAdminActionsRepository
    {
        Exhibition? foundExhibition { get; set; }

        List<Exhibition> GetAllExhibitions();

        List<Exhibition> FilterExhibitions(string floorNr);

        void FindExhibitionInDatabase(int? id);

        void CopyFoundExhibition(ExhibitionDto exhibitionDto);

        void SaveAudioAsFile(ExhibitionDto exhibitionDto);

        void SavePhotoAsFile(ExhibitionDto exhibitionDto);

        void CreateExhibitionInDatabase(ExhibitionDto exhibitionDto);

        void DeleteAudio();

        void DeletePhoto();

        void DeleteExhibition();

        void UpdateAudio(ExhibitionDto exhibitionDto);

        void UpdatePhoto(ExhibitionDto exhibitionDto);

        void UpdateExhibitionInDatabase(ExhibitionDto exhibitionDto);

        void ClearTheForm(ExhibitionDto exhibitionDto);
    }
}
