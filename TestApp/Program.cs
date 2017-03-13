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

            dataUtil.insertPerson(testPerson);


        }
    }
}
