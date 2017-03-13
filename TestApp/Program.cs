using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessTier;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var testPerson = new Person()
            {
                Efternavn = "Soerensen",
                Fornavn = "Jeppe",
                Mellemnavn = "Traberg",
                Persontype = "Boss"
            };

            var dataUtil = new PersonDataUtil();

           // dataUtil.insertPerson(testPerson, 12345678, "Privat");
            dataUtil.DeleteCurrentPerson(testPerson);

            var adresse = new Adresse() {Husnummer = 20, Postnummer = 8920, Vejnavn = "Hampehaven"};

            dataUtil.addAdress(20003, adresse);


        }
    }
}
