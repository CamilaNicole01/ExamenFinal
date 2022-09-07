using System;
using System.Collections.Generic;
using System.Text;
using Final.Data;
using Final.Dominio;

namespace Final.Data
{
    public static class PrestamoBL
    {
        public static List<Prestamo> Listar()
        {
            var prestamoData = new ClienteData();
            return prestamoData.Listar()
        }
    }
}
