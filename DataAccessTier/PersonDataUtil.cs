using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTier
{
    public class PersonDataUtil
    {
        private SqlConnection conn;
        private Person locPerson;

        public Person currentPerson
        {
            get { return locPerson; }
        }

        public PersonDataUtil()
        {
            conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Handinv2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        
        public void insertPerson(Person person, int tlfNr, string tlfType) // telefon inkluderes, da en person ifølge kravene skal have en telefon. 
        {
            try
            {
                conn.Open();

                string insertString = @"
                    INSERT INTO [Person] (Fornavn, Mellemnavn, Efternavn, PersonType)
                        OUTPUT INSERTED.PersonID
                        VALUES (@Data1, @Data2, @Data3, @Data4)";

                string telefonString = @"
                    INSERT INTO [Telefon] (TelefonType, TelefonNummer, PersonID)
                        OUTPUT INSERTED.TelefonID
                        VALUES (@Data5, @Data6, @Data7)";


                long personid;

                //command til person
                using (SqlCommand cmd = new SqlCommand(insertString, conn))
                {
                    // Person parametre
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters["@Data1"].Value = person.Fornavn; 
                    cmd.Parameters["@Data2"].Value = person.Mellemnavn;
                    cmd.Parameters["@Data3"].Value = person.Efternavn;
                    cmd.Parameters["@Data4"].Value = person.Persontype;

                    personid = (long)cmd.ExecuteScalar();
                    person.PersonID = personid;

                    this.locPerson = person; 

                }

                // command til telefon
                using (SqlCommand cmd = new SqlCommand(telefonString, conn))
                {

                    // Telefon parametre
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data6";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data7";
                    cmd.Parameters["@Data5"].Value = tlfType;
                    cmd.Parameters["@Data6"].Value = tlfNr;
                    cmd.Parameters["@Data7"].Value = personid;

                    cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}


// Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Handinv2;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True