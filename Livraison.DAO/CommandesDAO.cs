using Livraison.BO;
using Livraison.DAO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Livraison.DAL
{
   public class CommandesDAO
    {

        private readonly Sql sql;
        public CommandesDAO()
        {
            sql = new Sql("Livraison");
        }

        public void Add(Commandes user)
        {
            sql.Execute
            (
                "Sp_User_Insert",
                GetParameters(user),
                true
            );
        }

        public void Add()
        {
            sql.Execute
            (
                "Sp_User_Default",
                GetParameters(null),
                true
            );
        }
        public void Set(Commandes user)
        {
            sql.Execute
            (
                "Sp_User_Update",
                GetParameters(user),
                true
            );
        }

        public Commandes Get(int id)
        {
            var reader = sql.Read
            (
                "Sp_User_Select",
                GetParameters(new Commandes { Id = id }),
                true
            );

            while (reader.Read())
                return GetObject(reader);
            reader.Close();

            return null;
        }

        public Commandes Login(string username, string password)
        {
            var reader = sql.Read
            (
                "Sp_User_Login",
                GetParameters(new Commandes { Date_Livraison = username, Lieu_Livraison = password }),
                true
            );

            while (reader.Read())
                return GetObject(reader);
            reader.Close();

            return null;
        }

        public IEnumerable<Commandes> Find(Commandes user = null)
        {
            var reader = sql.Read
            (
                "Sp_User_Select",
                GetParameters(user),
                true
            );

            var users = new List<Commandes>();
            while (reader.Read())
                users.Add(GetObject(reader));
            reader.Close();

            return users;
        }

        public void Delete(int id)
        {
            sql.Execute
            (
                "Sp_User_Delete",
                GetParameters(new Commandes { Id = id }),
                true
            );
        }


        private Commandes GetObject(DbDataReader reader)
        {
            return new Commandes
            (
                reader["Reference"] == DBNull.Value ? 0 : int.Parse(reader["Id"].ToString()),
                reader["Date_Livraison"] == DBNull.Value ? null : reader["Username"].ToString(),
                reader["Fullname"] == DBNull.Value ? null : reader["Fullname"].ToString(),
                reader["CreatedAt"] == DBNull.Value ? null : (DateTime?)DateTime.Parse(reader["CreatedAt"].ToString())
            );

        }

        private IEnumerable<Sql.Parameter> GetParameters(Commandes user)
        {
            return new Sql.Parameter[]
            {
                new Sql.Parameter("@Id", DbType.Int32, (user == null || user.Id == 0 ? (object)DBNull.Value : user.Id)),
                new Sql.Parameter("@Username", DbType.String, (user == null || string.IsNullOrEmpty(user.Username) ? (object)DBNull.Value : user.Username)),
                new Sql.Parameter("@Fullname", DbType.String, (user == null || string.IsNullOrEmpty(user.Fullname) ? (object)DBNull.Value : user.Fullname)),
                new Sql.Parameter("@CreatedAt", DbType.DateTime, (user == null || user.CreatedAt == null ? (object)DBNull.Value : user.CreatedAt))
            };
        }
    }
}
