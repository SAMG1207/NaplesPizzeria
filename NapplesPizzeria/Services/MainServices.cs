using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using NapplesPizzeria.Models;
using System.Data;

namespace NapplesPizzeria.Services
{
    public class MainServices
    {
        private readonly NaplesPizzeriaContext _context;
        private readonly string _connectionString;

        public MainServices(NaplesPizzeriaContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public bool checkIfTableIsOpen(int tableId)
        {
            try
            {
                using SqlConnection conn = new(_connectionString);
                conn.Open();
                using SqlCommand cmd = new ("checkIfTableIsOpen", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@inMesa", tableId));

                var outParam = new SqlParameter("@boolIsOpen", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);
                cmd.ExecuteNonQuery();
                bool result = (bool)outParam.Value;
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public Dictionary<int , bool> estadoTablas()
        {
            Dictionary<int, bool> estadoTablas = new();
            List<int> pks = [];
            _context.MtabTables.ToList().ForEach(table => pks.Add(table.InMtTabPky)); // Añadimos las Pks a la lista
            try
            {
                foreach(int i in pks)
                {
                    estadoTablas.Add(i, checkIfTableIsOpen(i));
                }
                return estadoTablas;
            }
            catch (Exception ex)
            {
                return estadoTablas;
            }
        }

        public bool postOrder(string direction, int table, int productId, int cuantity)
        {
            try
            {
                int upserts = 0;
                using SqlConnection conn = new(_connectionString);
                conn.Open();
                using SqlTransaction transaction = conn.BeginTransaction();
                using SqlCommand cmd = new("UpsertOrder", conn, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@svCommand", direction));
                cmd.Parameters.Add(new SqlParameter("@inServiceFk" , table));
                cmd.Parameters.Add(new SqlParameter("@inProductoFk", productId));
                var outParam = new SqlParameter("@out_error", SqlDbType.NVarChar,4000)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);
                
                while(upserts < cuantity)
                {
                    cmd.ExecuteNonQuery();
                    string result = outParam.Value?.ToString();
                    if(result != "OK")
                    {
                        transaction.Rollback();
                        return false;
                    }
                    upserts++;
                }
                
                return true;
            }
            catch(Exception ex)
            {
                //Debera loggear
                return false;
            }
        }
    }

}

