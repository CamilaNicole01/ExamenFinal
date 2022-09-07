using System;
using System.Collections.Generic;
using System.Text;
using Final.Dominio;
using Final.Data;

namespace Final.Logic
{
    public static class ClienteBL
    {
        public static List<Cliente> Listar()
        {
            var clienteData = new ClienteData();
            return clienteData.Listar();
        }
    }
}
