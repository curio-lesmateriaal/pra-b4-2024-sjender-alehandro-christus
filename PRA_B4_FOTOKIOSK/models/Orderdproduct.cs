using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class Orderdproduct
    {

        public int? Fotonummer { get; set; }
        public string ProductNaam { get; set; }

        public int? Aantal { get; set; }
        public float Totaalprijs { get; set; }

    }
}
