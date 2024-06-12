using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        public List<KioskPhoto> PicturesToSearch = new List<KioskPhoto>();

        // De window die we laten zien op het scherm
        public static Home Window { get; set; }


        // Start methode die wordt aangeroepen wanneer de zoek pagina opent.
        public void Start()
        {
            SearchManager.Instance = Window;
        }

        // Wordt uitgevoerd wanneer er op de Zoeken knop is geklikt
        public void SearchButtonClick()
        {
            string foundSearch = "";
            var now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            // Zet de dir op goede plek
            string dir = "";
            if (day == 0) dir = "../../../fotos/0_Zondag";
            else if (day == 1) dir = "../../../fotos/1_Maandag";
            else if (day == 2) dir = "../../../fotos/2_Dinsdag";
            else if (day == 3) dir = "../../../fotos/3_Woensdag";
            else if (day == 4) dir = "../../../fotos/4_Donderdag";
            else if (day == 5) dir = "../../../fotos/5_Vrijdag";
            else if (day == 6) dir = "../../../fotos/6_Zaterdag";

            // haalt photo op
            foreach (string file in Directory.GetFiles(dir))
            {
                string[] search = SearchManager.GetSearchInput().Split(":");
                int hour = int.Parse(search[0]);
                int minute = int.Parse(search[1]);
                int second = int.Parse(search[2]);

                string timeDateString = string.Format("{0:D2}_{1:D2}_{2:D2}_", hour, minute, second);
                if (file.Contains(timeDateString))
                {
                    foundSearch = file;
                    SearchManager.SetPicture(foundSearch);
                    string[] fileInfo = Path.GetFileNameWithoutExtension(file).Split('_');
                    string imageInfo = $"Tijd: {fileInfo[0]}:{fileInfo[1]}:{fileInfo[2]}\nId: {fileInfo[3]}\nDatum: {DateTime.Now.DayOfWeek}";
                    SearchManager.SetSearchImageInfo(imageInfo);
                }
            }
        }
    }
}