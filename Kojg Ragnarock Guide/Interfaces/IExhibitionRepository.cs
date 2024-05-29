using Azure;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.Extensions.Hosting;
using System;

namespace Kojg_Ragnarock_Guide.Interfaces
{
    public interface IExhibitionRepository
    {
        List<Exhibition> GetAllExhibitions();

        List<Exhibition> FindExhibition(int? id);

        List<Exhibition> FilterExhibitions(string floorNr);

        void CopyFoundExhibition(ExhibitionDto exhibitionDto, int? id);

        void SaveAudioAsFile(ExhibitionDto exhibitionDto);

        void SavePhotoAsFile(ExhibitionDto exhibitionDto);

        void CreateExhibition(ExhibitionDto exhibitionDto);

        void DeleteAudio(int? id);

        void DeletePhoto(int? id);

        void DeleteExhibition(int? id);

        void UpdateAudio(ExhibitionDto exhibitionDto, int? id);

        void UpdatePhoto(ExhibitionDto exhibitionDto, int? id);

        void UpdateExhibition(ExhibitionDto exhibitionDto, int? id);

        void ClearTheForm(ExhibitionDto exhibitionDto);
    }
}
