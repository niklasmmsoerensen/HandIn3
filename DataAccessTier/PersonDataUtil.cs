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

        public void insertPerson(Person person)
        {
            try
            {
                conn.Open();

                string insertString = @"
                    INSERT INTO [Person] (Fornavn, Mellemnavn, Efternavn, PersonType)
                        OUTPUT INSERTED.PersonID
                        VALUES (@Data1, @Data2, @Data3, @Data4)";

                using (SqlCommand cmd = new SqlCommand(insertString, conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1"; //Works even whit lower case "d"
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters["@Data1"].Value = person.Fornavn; //.ToString("yyyy-MM-dd HH:mm:ss"); ;
                    cmd.Parameters["@Data2"].Value = person.Mellemnavn;
                    cmd.Parameters["@Data3"].Value = person.Efternavn;
                    cmd.Parameters["@Data4"].Value = person.Persontype;

                    //var id 
                    person.PersonID = (long)cmd.ExecuteScalar(); //Returns the identity of the new tuple/record

                    //hv.HID = (int)cmd.ExecuteNonQuery(); //Does not workReturns row affected and not the identity of the new tuple/record

                    this.locPerson = person; //Make new Håndværker to currentHåndværker 

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


       

        public void DeleteCurrentPerson(Person person)
        {
            try
            {
                // Open the connection
                conn.Open();

                // prepare command string
                string deleteString =
                   @"DELETE FROM [Person]
                        WHERE (Fornavn = @Data1 AND  @Data2 = Mellemnavn AND @Data3 = Efternavn)";
                  using (SqlCommand cmd = new SqlCommand(deleteString, conn))
                {
                    // Get your parameters ready 
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";

                    cmd.Parameters["@Data1"].Value = person.Fornavn;
                    cmd.Parameters["@Data2"].Value = person.Mellemnavn;
                    cmd.Parameters["@Data3"].Value = person.Efternavn;


                    var id = (int)cmd.ExecuteNonQuery(); //Returns thenumber of tuple/record affected
                    locPerson = null;

                }
            }
            finally
            {
                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

    }
}


// Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Handinv2;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True