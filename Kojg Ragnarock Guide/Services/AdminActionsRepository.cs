using Azure;
using Kojg_Ragnarock_Guide.Interfaces;
using Kojg_Ragnarock_Guide.Migrations;
using Kojg_Ragnarock_Guide.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System;
using Exhibition = Kojg_Ragnarock_Guide.Models.Exhibition;

namespace Kojg_Ragnarock_Guide.Services

{
    public class AdminActionsRepository(IWebHostEnvironment environment, ExhibitionDbContext context) : IAdminActionsRepository
    {
        public Exhibition? foundExhibition { get; set; }

        private string newAudioFileName;
        private string audioFullPath;
        private string oldAudioFullPath;

        private string newPhotoFileName;
        private string photoFullPath;
        private string oldPhotoFullPath;

        public List<Exhibition> GetAllExhibitions()
        {
            return context.Exhibitions.OrderByDescending(E => E.ExhibitionNumber).Reverse().ToList();
        }

        public List<Exhibition> FilterExhibitions(string floorNr)
        {
            List<Exhibition> exhibitions = GetAllExhibitions();
            List<Exhibition> filteredList = new List<Exhibition>();

            foreach (Exhibition ex in exhibitions)
            {
                if (ex.Floor.Contains(floorNr))
                {
                    filteredList.Add(ex);
                }
            }
            return filteredList;
        }

        public void FindExhibitionInDatabase(int? id)
        {
            foundExhibition = context.Exhibitions.Find(id.Value);
        }

        public void CopyFoundExhibition(ExhibitionDto exhibitionDto)
        {
            // Return what I want to update
            if (foundExhibition != null)
            {
                exhibitionDto.Title = foundExhibition.Title;
                exhibitionDto.Description = foundExhibition.Description;
                exhibitionDto.Floor = foundExhibition.Floor;
                exhibitionDto.ExhibitionNumber = foundExhibition.ExhibitionNumber;
            }
        }

        public void SaveAudioAsFile(ExhibitionDto exhibitionDto)
        {
            // save Audio as a file
            newAudioFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newAudioFileName += Path.GetExtension(exhibitionDto.AudioFile!.FileName);

            audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + newAudioFileName;
            using (FileStream? stream = System.IO.File.Create(audioFullPath))
            {
                exhibitionDto.AudioFile.CopyTo(stream);
            }
        }

        public void SavePhotoAsFile(ExhibitionDto exhibitionDto)
        {
            // save Image as a file
            newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newPhotoFileName += Path.GetExtension(exhibitionDto.PhotoFile!.FileName);

            photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
            using (FileStream? stream = System.IO.File.Create(photoFullPath))
            {
                exhibitionDto.PhotoFile.CopyTo(stream);
            }
        }

        public void CreateExhibitionInDatabase(ExhibitionDto exhibitionDto)
        {
            //save the new product in the database
            Exhibition exhibition = new Exhibition()
            {
                Title = exhibitionDto.Title,
                ExhibitionNumber = exhibitionDto.ExhibitionNumber,
                Description = exhibitionDto.Description ?? "",
                Floor = exhibitionDto.Floor,
                PhotoFileName = newPhotoFileName,
                AudioFileName = newAudioFileName,
            };
            context.Exhibitions.Add(exhibition);
            context.SaveChanges();
        }

        public void DeleteAudio()
        {
            // Deletes Audio
            audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + foundExhibition.AudioFileName;
            System.IO.File.Delete(audioFullPath);
        }

        public void DeletePhoto()
        {
            // Deletes Photo
            photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + foundExhibition.PhotoFileName;
            System.IO.File.Delete(photoFullPath);
        }

        public void DeleteExhibition()
        {
            //Deletes the the rest of the object
            context.Exhibitions.Remove(foundExhibition);
            context.SaveChanges();
        }

        public void UpdateAudio(ExhibitionDto exhibitionDto)
        {
            // Update Audio If we have a new one
            newAudioFileName = foundExhibition.AudioFileName;
            if (exhibitionDto.AudioFile != null)
            {
                // Important to get Timestamp or else it wont save the picture properly
                newAudioFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newAudioFileName += Path.GetExtension(exhibitionDto.AudioFile!.FileName);
                // Saves the new audio chosen
                audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + newAudioFileName;
                using (FileStream? stream = System.IO.File.Create(audioFullPath))
                {
                    exhibitionDto.AudioFile.CopyTo(stream);
                }

                // delete old audio
                oldAudioFullPath = environment.WebRootPath + "/exhibitionAudios/" + foundExhibition.AudioFileName;
                System.IO.File.Delete(oldAudioFullPath);
            }
            foundExhibition.AudioFileName = newAudioFileName;
        }

        public void UpdatePhoto(ExhibitionDto exhibitionDto)
        {
            // Update Photo If we have a new one
            newPhotoFileName = foundExhibition.PhotoFileName;
            if (exhibitionDto.PhotoFile != null)
            {
                // Important to get Timestamp or else it wont save the picture properly
                newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newPhotoFileName += Path.GetExtension(exhibitionDto.PhotoFile!.FileName);
                // Saves the new picture chosen
                photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
                using (FileStream? stream = System.IO.File.Create(photoFullPath))
                {
                    exhibitionDto.PhotoFile.CopyTo(stream);
                }
                // delete old photo
                oldPhotoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + foundExhibition.PhotoFileName;
                System.IO.File.Delete(oldPhotoFullPath);
            }
            foundExhibition.PhotoFileName = newPhotoFileName;
        }

        public void UpdateExhibitionInDatabase(ExhibitionDto exhibitionDto)
        {
            // update foundExhibition in database
            foundExhibition.Title = exhibitionDto.Title;
            foundExhibition.ExhibitionNumber = exhibitionDto.ExhibitionNumber;
            foundExhibition.Description = exhibitionDto.Description ?? "";
            foundExhibition.Floor = exhibitionDto.Floor;
            context.SaveChanges();
        }

        public void ClearTheForm(ExhibitionDto exhibitionDto)
        {
            //Clear the form
            exhibitionDto.Title = "";
            exhibitionDto.ExhibitionNumber = 0;
            exhibitionDto.Description = "";
            exhibitionDto.Floor = "";
            exhibitionDto.PhotoFile = null;
            exhibitionDto.AudioFile = null;
        }

    }
}
