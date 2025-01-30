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
    }

}

