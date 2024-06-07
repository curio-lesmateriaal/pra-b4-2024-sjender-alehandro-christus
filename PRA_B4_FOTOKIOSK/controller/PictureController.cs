using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            // Initializeer de lijst met fotos
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;
            var photosByTime = new Dictionary<DateTime, List<KioskPhoto>>();

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                int dayIndex = int.Parse(dir[15].ToString());

                if (day == dayIndex)
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        string[] words = file.Split(@"\")[2].Split("_");
                        DateTime fotoDate = new DateTime(now.Year, now.Month, now.Day, int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]));

                        if (now.Subtract(fotoDate).TotalMinutes < 30 && now.Subtract(fotoDate).TotalMinutes > 2)
                        {
                            if (!photosByTime.ContainsKey(fotoDate))
                            {
                                photosByTime[fotoDate] = new List<KioskPhoto>();
                            }
                            photosByTime[fotoDate].Add(new KioskPhoto() { Id = 0, Source = file });
                        }
                    }
                }
            }

            var sortedTimes = photosByTime.Keys.OrderBy(t => t).ToList();

            for (int i = 0; i < sortedTimes.Count - 1; i++)
            {
                DateTime current = sortedTimes[i];
                DateTime next = sortedTimes[i + 1];

                if ((next - current).TotalSeconds == 60)
                {
                    PicturesToDisplay.AddRange(photosByTime[current]);
                    PicturesToDisplay.AddRange(photosByTime[next]);
                    i++; // Skip the next time as it has been paired
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Refresh logic if needed
        }
    }
}
