using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace WebApplication1.Models
{
    public class Personal_info
    {
        private static readonly string dbConfig = "Data Source=C:/Users/m0ngi/Desktop/db.db";
        public static Person readPersonFromReader(SQLiteDataReader reader)
        {
            Person tmp = new Person(reader.GetInt32(0));
            tmp.firstName = reader.GetString(1);
            tmp.lastName = reader.GetString(2);
            tmp.email = reader.GetString(3);
            tmp.dateBirth = reader.GetString(4);
            tmp.image = reader.GetString(5);
            tmp.country = reader.GetString(6);
            return tmp;
        }

        public static List<Person> GetAllPerson()
        {
            SQLiteConnection conn = new SQLiteConnection(dbConfig);
            conn.Open();

            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info", conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            List<Person> res = new List<Person>();
            while (reader.Read())
            {
                Person tmp = readPersonFromReader(reader);
                res.Add(tmp);
            }

            conn.Close();
            return res;
        }
        public static Person? GetPerson(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(dbConfig))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info where id=@id", conn);
                cmd.Parameters.Add(new SQLiteParameter("@id", id));

                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Person res = readPersonFromReader(reader);

                    return res;
                }
            }
            return null;
        }

        public static Person? searchPerson(Person model)
        {
            using (SQLiteConnection conn = new SQLiteConnection(dbConfig))
            {
                conn.Open();
                model.firstName ??= "";
                model.country ??= "";

                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM personal_info where (first_name LIKE ('%'||@firstname||'%')) and (country LIKE ('%'||@country||'%'))", conn);
                cmd.Parameters.Add(new SQLiteParameter("@firstname", model.firstName));
                cmd.Parameters.Add(new SQLiteParameter("@country", model.country));


                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Person res = readPersonFromReader(reader);

                    return res;
                }
            }
            return null;
        }
    }
}
