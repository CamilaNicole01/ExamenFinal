using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Final.Dominio;

namespace Final.Data
{
    public class DetallePrestamoData
    {
        string cadenaConexion = "server=localhost; Database=Final; Integrated Security = true";
        public List<DetallePrestamo> Listar()
        {
            var listado = new List<DetallePrestamo>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("Select * From DetallePrestamo", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Prestamo prestamo;
                            while (lector.Read())
                            {
                                prestamo = new Prestamo();
                                prestamo.ID = int.Parse(lector[0].ToString());
                                prestamo.IdPrestamo lector[1].ToString();
                                prestamo.NumeroCuota = int.Parse(lector[2].ToString());
                                prestamo.ImporteCuota= decimal.Parse(lector[3].ToString());
                                prestamo.ImporteInteres = decimal.Parse(lector[4].ToString());

                                listado.Add(prestamo);

                            }
                       }
                    }
                }
            }
            return listado;

        }
    public prestamo BuscarPorId(int id)
    {
        Prestamo prestamo = new Prestamo();
        using (var conexion = new SqlConnection(cadenaConexion))
        {
            conexion.Open();
            using (var comando = new SqlCommand("SELECT * FROM DetallePrestamo WHERE ID = @id", conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                using (var lector = comando.ExecuteReader())
                {
                    if (lector != null && lector.HasRows)
                    {
                        prestamo = new Prestamo();
                        prestamo.ID = int.Parse(lector[0].ToString());
                        prestamo.IdPrestamo lector[1].ToString();
                        prestamo.NumeroCuota = int.Parse(lector[2].ToString());
                        prestamo.ImporteCuota = decimal.Parse(lector[3].ToString());
                        prestamo.ImporteInteres = decimal.Parse(lector[4].ToString());

                        listado.Add(prestamo);
                    }
                }
            }
        }
            return prestamo;
        }
    }
}