using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Final.Dominio;

namespace Final.Data
{
    public class PrestamoData
    {
        string cadenaConexion = "server=localhost; database=Final; integrated security=true";
        public List<Prestamo> Listar()
        {
            var listado = new List<Prestamo>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("Select * From Prestamo", conexion))
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
                                prestamo.Numero = lector[1].ToString();
                                prestamo.IdCliente = int.Parse(lector[2].ToString());
                                prestamo.Importe = decimal.Parse(lector[3].ToString());
                                prestamo.Tasa = decimal.Parse(lector[4].ToString());
                                prestamo.Plazo = int.Parse(lector[5].ToString());

                                listado.Add(prestamo);

                            }
                        }

                    }
                }
            }
            return listado;
        }
        public Prestamo BuscarId(int id)
        {
            Prestamo prestamo = new Prestamo();
            return prestamo;
        }

       public bool Insertar(Prestamo prestamo, List<DetallePrestamo> detalles)
        {
            using (var transaccion = new TransactionScope())
            {
                var lisatdo = new List<Prestamo>();
                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    var numeroPrestamo = 0;
                    int ultimoId = 0;
                    // OBTENER EL SIGUIENTE NÚMERO DE PRESTAMO
                    var sql = "SELECT ISNULL(MAX(Numero),0) FROM Prestamo";
                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        numeroPrestamo = int.Parse(comando.ExecuteScalar().ToString());
                        numeroPrestamo++;
                        prestamo.Numero = numeroPrestamo.ToString().PadLeft(20, char.Parse("0"));
                    }

                    // INSERTAR PRESTAMO Y OBTENER EL ÚLTIMO ID
                    sql = "INSERT INTO Prestamo (Numero, Fecha, IdCliente, Importe, " +
                                "Tasa, Plazo, FechaDeposito, Estado) " +
                            "VALUES (@Numero, @Fecha, @IdCliente, @Importe, " +
                                "@Tasa, @Plazo, @FechaDeposito, @Estado);" +
                            "SELECT ISNULL(@@IDENTITY,0);";
                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        // ASIGNACIÓN DE PARÁMETROS
                        comando.Parameters.AddWithValue("@Numero", prestamo.Numero);
                        comando.Parameters.AddWithValue("@Fecha", prestamo.Fecha);
                        comando.Parameters.AddWithValue("@IdCliente", prestamo.IdCliente);
                        comando.Parameters.AddWithValue("@Importe", prestamo.Importe);
                        comando.Parameters.AddWithValue("@Tasa", prestamo.Tasa);
                        comando.Parameters.AddWithValue("@Plazo", prestamo.Plazo);
                        comando.Parameters.AddWithValue("@FechaDeposito", prestamo.FechaDeposito);
                        comando.Parameters.AddWithValue("@Estado", 1);

                        ultimoId = int.Parse(comando.ExecuteScalar().ToString());
                        prestamo.ID = ultimoId;
                    }

                    // INSERCIÓN DEL DETALLE DE PRESTAMO
                    sql = "INSERT INTO DetallePrestamo (IdPrestamo, NumeroCuota, " +
                            "ImporteCuota, FechaVencimiento, Estado) " +
                          "VALUES(@IdPrestamo, @NumeroCuota, @ImporteCuota, " +
                            "@FechaVencimiento, @Estado)";
                    foreach (var detalle in detalles)
                    {
                        detalle.IdPrestamo = prestamo.ID;
                        using (var comando = new SqlCommand(sql, conexion))
                        {
                            // ASIGNACIÓN DE PARÁMETROS
                            comando.Parameters.AddWithValue("@IdPrestamo", detalle.IdPrestamo);
                            comando.Parameters.AddWithValue("@NumeroCuota", detalle.NumeroCuota);
                            comando.Parameters.AddWithValue("@ImporteCuota", detalle.ImporteCuota);
                            comando.Parameters.AddWithValue("@FechaVencimiento", detalle.FechaVencimiento);
                            comando.Parameters.AddWithValue("@Estado", 1);
                            comando.ExecuteNonQuery();
                        }
                    }
                }
                transaccion.Complete();
            }
            return true;
        }
    }

}
