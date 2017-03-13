using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTier
{
    public class Adresse
    {
        public long AdresseID { get; set; }
        public string Vejnavn { get; set; }
        public long Husnummer { get; set; }
        public long Postnummer { get; set; }
    }
}
