using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rescomac_back.repository.Dto
{
    public partial class PropiedadRequest
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int Identificacion { get; set; }
        public int Celular { get; set; }
        public string Correo { get; set; } = null!;
        public string Torre { get; set; } = null!;
        public string Apto { get; set; } = null!;
        public bool EstadoPago { get; set; }
    }
}