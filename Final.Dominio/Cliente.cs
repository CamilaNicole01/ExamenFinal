using System;
using System.Collections.Generic;
using System.Text;

namespace Final.Dominio
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public int IdTipoCliente { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public bool Estado { get; set; }
    }
}
