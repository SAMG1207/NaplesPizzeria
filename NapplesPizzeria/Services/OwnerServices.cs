using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NapplesPizzeria.Models;
using System.Data;

namespace NapplesPizzeria.Services
{
    public class OwnerServices
    {
        private readonly NaplesPizzeriaContext _context;
        private readonly string _connectionString;

        public OwnerServices(NaplesPizzeriaContext context, IConfiguration configuration)
        {
            context = _context;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public string validateCredentials(string username, string password)
        {
            
            try
            {
                using SqlConnection conn = new(_connectionString);
                conn.Open();
                using SqlCommand cmd = new("loginPizzeria", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                // Agregar parámetros del procedimiento almacenado
                cmd.Parameters.Add(new SqlParameter("@svUser", username));
                cmd.Parameters.Add(new SqlParameter("@svPassword", password));

                // Parámetro de salida para recibir el mensaje de error
                var outParam = new SqlParameter("@out_error", SqlDbType.NVarChar, 4000)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outParam);

                // Ejecutar el procedimiento almacenado
                cmd.ExecuteNonQuery();

                // Obtener el valor del parámetro de salida
                string result = outParam.Value?.ToString();

                return result;
            }
            catch (SqlException ex)
            {
                return ex.Message;
            }
        }
    }
}
