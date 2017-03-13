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

            //Use case: Add new Person to database
            dataUtil.insertPerson(testPerson, 22345678, "Privat");

            //Use case: Add new adress to existing (and current) Person
            var adresse = new Adresse() { Husnummer = 20, Postnummer = 8920, Vejnavn = "Hampehaven" };
            dataUtil.addAdress(dataUtil.currentPerson.PersonID, adresse);

            //Use case: Get Person using phone number
            dataUtil.setCurrentPerson(22345678);
            Console.WriteLine(dataUtil.currentPerson.Fornavn);

            //Use case: Change phone owner
            dataUtil.changePhoneOwner(1, 2);

            //Use case: Delete Person from DB
            var personToDelete = new Person()
            {
                Efternavn = "Jørgensen",
                Fornavn = "Niklas3",
                Mellemnavn = "Søby",
                Persontype = "Rising Star"
            };
            dataUtil.insertPerson(personToDelete, 55334411, "Arbejde");
            dataUtil.DeleteCurrentPerson(personToDelete);

        }
    }
}
