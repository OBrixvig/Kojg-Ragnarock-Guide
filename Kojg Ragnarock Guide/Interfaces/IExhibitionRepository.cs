using Azure;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.Extensions.Hosting;
using System;

namespace Kojg_Ragnarock_Guide.Interfaces
{
    public interface IExhibitionRepository
    {
        Exhibition? FoundExhibition { get; set; }

        List<Exhibition> GetAllExhibitions();

        List<Exhibition> FilterExhibitions(string floorNr);

        void FindExhibition(int? id);

        void CopyFoundExhibition(ExhibitionDto exhibitionDto);

        void SaveAudioAsFile(ExhibitionDto exhibitionDto);

        void SavePhotoAsFile(ExhibitionDto exhibitionDto);

        void CreateExhibition(ExhibitionDto exhibitionDto);

        void DeleteAudio();

        void DeletePhoto();

        void DeleteExhibition();

        void UpdateAudio(ExhibitionDto exhibitionDto);

        void UpdatePhoto(ExhibitionDto exhibitionDto);

        void UpdateExhibition(ExhibitionDto exhibitionDto);

        void ClearTheForm(ExhibitionDto exhibitionDto);
    }
}
