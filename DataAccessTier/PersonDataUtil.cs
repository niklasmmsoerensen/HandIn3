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

                    personid = (long) cmd.ExecuteScalar();
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
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }


        //Use case: Find Person ud fra telefonID
        public void setCurrentPerson(long telefonID) // getCurrentPerson()
        {
            SqlDataReader rdr = null;

            long personID = 0;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Telefon WHERE (Telefonnummer ='" + telefonID + "')", conn);
                
                rdr = cmd.ExecuteReader();

                // transfer data from result set to local model
                if (rdr.HasRows)
                {
                    if (rdr.Read())
                    {
                        personID = (long) rdr["PersonID"];
                    }
                }
                //  break; //Only the first comming Håndværker with name specified but here superflous   
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Person WHERE (PersonID ='" + personID + "')", conn);
                
                rdr = cmd.ExecuteReader();

                // transfer data from result set to local model
                if (rdr.HasRows)
                {
                    if (rdr.Read())
                    {
                            locPerson = new Person
                            {
                                PersonID = (long) rdr["PersonID"],
                                Fornavn = (string) rdr["Fornavn"],
                                Mellemnavn = (string) rdr["Mellemnavn"],
                                Efternavn = (string) rdr["Efternavn"],
                                Persontype = (string) rdr["PersonType"]
                            };
                        }
                    }
                //  break; //Only the first comming Håndværker with name specified but here superflous
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }



        }

        public void addAdress(long personId, Adresse adresse) // tilføj en adresse til en person
        {
            try
            {
                conn.Open();

                string adressString = @"
                    INSERT INTO [Adresse] (Vejnavn, Husnummer, Postnummer)
                        OUTPUT INSERTED.AdresseID
                        VALUES (@Data1, @Data2, @Data3)";

                string PersonAdresseString = @"
                    INSERT INTO [PersonAdresse] (PersonID, AdresseID)
                        VALUES (@Data4, @Data5)";


                long adresseId;

                //command til adresse
                using (SqlCommand cmd = new SqlCommand(adressString, conn))
                {
                    // Person parametre
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters["@Data1"].Value = adresse.Vejnavn;
                    cmd.Parameters["@Data2"].Value = adresse.Husnummer;
                    cmd.Parameters["@Data3"].Value = adresse.Postnummer;

                    adresseId = (long) cmd.ExecuteScalar();

                }

                // command til PersonAdresse entitet
                using (SqlCommand cmd = new SqlCommand(PersonAdresseString, conn))
                {

                    // Telefon parametre
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";
                    cmd.Parameters["@Data4"].Value = personId;
                    cmd.Parameters["@Data5"].Value = adresseId;

                    cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public void changePhoneOwner(long PhoneID, long newOwnerID)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("UPDATE Telefon SET PersonID='"+ newOwnerID +"' WHERE (TelefonID='" + PhoneID + "')", conn);

                cmd.ExecuteScalar();

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