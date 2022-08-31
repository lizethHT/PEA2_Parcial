using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parcial.Dominio;

namespace Parcial.Data
{
    public class ProductoData
    {

        String cadenaConexion = "server=localhost; Database=Parcial; Integrated Security = true";
        public List<Producto> Listar()
        {
            var listado = new List<Producto>();
            using(var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("Select * From Producto", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Producto producto;
                            while (lector.Read())
                            {
                                producto = new Producto();
                                producto.IdProducto = int.Parse(lector[0].ToString());
                                producto.Nombre = lector[1].ToString();
                                producto.Marca = lector[2].ToString();
                                producto.Precio = decimal.Parse(lector[3].ToString());
                                producto.Stock = int.Parse(lector[4].ToString());

                                listado.Add(producto);
                            }
                        }
                    }
                }
            }
            return listado;
        }

        public Producto BucarPorId(int id)
        {
            Producto Producto = new Producto();
            using(var conexion =new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using(var comando = new SqlCommand("SELECT * FROM Producto WHERE IdProducto = @id", conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    using(var lector = comando.ExecuteReader())
                    {
                        if(lector != null && lector.HasRows)
                        {
                            lector.Read();
                            Producto = new Producto();
                            Producto.IdProducto = int.Parse(lector[0].ToString());
                            Producto.Nombre = lector[1].ToString();
                            Producto.Marca = lector[2].ToString();
                            Producto.Precio = decimal.Parse(lector[3].ToString());
                            Producto.Stock = int.Parse(lector[4].ToString());
                        }
                    }
                }
            }
            return Producto;
        }

        public bool Insertar(Producto producto)
        {
            int filaInsertadas = 0;
            using(var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var sql = "INSERT INTO Producto ( Nombre, Marca, Precio, Stock)" +
                          "VALUES( @Nombre, @Marca, @Precio, @Stock)";
                using(var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Marca", producto.Marca);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    filaInsertadas = comando.ExecuteNonQuery();
                }
            }
            return filaInsertadas > 0;
        }

        public bool Actualizar (Producto producto)
        {
            int filasActualizadas = 0;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var sql = "UPDATE Producto SET Nombre = @Nombre, Marca = @Marca, Precio = @Precio, Stock =@Stock"+
                    " WHERE IdProducto = @IdProducto";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Marca", producto.Marca);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);

                    filasActualizadas = comando.ExecuteNonQuery();
                }
            }
                return filasActualizadas > 0;
        }

        public bool Eliminar (int id)
        {
            int filaEliminadas = 0;
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var Sql = "DELETE FROM Producto WHERE IdProducto = @id";
                using (var comando = new SqlCommand(Sql, conexion))
                {
                    comando.Parameters.AddWithValue("@id", id);
                    filaEliminadas = comando.ExecuteNonQuery();

                }
            }
                return filaEliminadas > 0;
        }
    }
}
