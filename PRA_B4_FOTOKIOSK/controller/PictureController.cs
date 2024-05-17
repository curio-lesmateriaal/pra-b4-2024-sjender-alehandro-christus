using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt

            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;


            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                /**
                 * dir string is de map waar de fotos in staan. Bijvoorbeeld:
                 * \fotos\0_Zondag
                 */

                int dayIndex = int.Parse(dir[15].ToString());
                
                if (day == dayIndex)
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        /**
                         * file string is de file van de foto. Bijvoorbeeld:
                         * \fotos\0_Zondag\10_05_30_id8824.jpg
                         */
                        string[] words = file.Split(@"\")[2].Split("_");
                        DateTime fotoDate = new DateTime(now.Year, now.Month, now.Day, int.Parse(words[0]), int.Parse(words[1]), int.Parse(words[2]));
                        Debug.WriteLine(now.Subtract(fotoDate).TotalMinutes);


                        if(now.Subtract(fotoDate).TotalMinutes < 30 && now.Subtract(fotoDate).TotalMinutes > 2)
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });

                        }

                    }
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
             
        }

    }
}
