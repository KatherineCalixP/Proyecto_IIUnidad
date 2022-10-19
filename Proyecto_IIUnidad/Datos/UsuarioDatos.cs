using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class UsuarioDatos
    {
        public async Task<bool> LoginAsync(string codigo, string clave)
        {
            bool valido = false;
            try
            {
                string sql = "SELECT 1 FROM usuario WHERE Codigo=@Codigo AND Clave=@Clave;";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = codigo;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 120).Value = clave;
                        valido = Convert.ToBoolean(await comando.ExecuteScalarAsync());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return valido;
        }

        public async Task<DataTable> DevolverListaAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Usuario;";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        //ejecutar           (CASTEAR       )Es como el convert
                        MySqlDataReader dr = (MySqlDataReader)await comando.ExecuteReaderAsync();
                        dt.Load(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        
        //metodo para asinar nuevo usuario
        public async Task<bool> InsertarAsync(Usuario usuario)
        {
            bool inserto = false;
            try
            {
                string sql = "INSERT INTO usuario VALUES (@Codigo, @Nombre, @Clave, @Correo, @Rol, @EstActivo);";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = usuario.Codigo;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = usuario.Nombre;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 120).Value = usuario.Clave;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = usuario.Correo;
                        comando.Parameters.Add("@Rol", MySqlDbType.VarChar, 20).Value = usuario.Rol;
                        comando.Parameters.Add("@EstActivo", MySqlDbType.Bit).Value = usuario.EstActivo;

                        await comando.ExecuteNonQueryAsync();
                        inserto = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return inserto;

            //
        }

        //Actualizar
        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            bool actualizo= false;
            try
            {
                string sql = "UPDATE usuario SET Nombre=@Nombre, Clave=@Clave, Correo=@Correo, Rol=@Rol, EstActivo=@EstActivo WHERE Codigo=@Codigo;";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = usuario.Codigo;
                        comando.Parameters.Add("@Nombre", MySqlDbType.VarChar, 50).Value = usuario.Nombre;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 120).Value = usuario.Clave;
                        comando.Parameters.Add("@Correo", MySqlDbType.VarChar, 45).Value = usuario.Correo;
                        comando.Parameters.Add("@Rol", MySqlDbType.VarChar, 20).Value = usuario.Rol;
                        comando.Parameters.Add("@EstActivo", MySqlDbType.Bit).Value = usuario.EstActivo;

                        await comando.ExecuteNonQueryAsync();
                        actualizo = true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return actualizo;
        }

        //borrar usuario
        public async Task<bool> EliminarAsync(string codigo)
        {
            bool elimino = false;
            try
            {
                string sql = "DELETE FROM Usuario WHERE Codigo = @Codigo;";

                using (MySqlConnection _conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 20).Value = codigo;
                        await comando.ExecuteNonQueryAsync();
                        elimino = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return elimino;
        }
    }
}
