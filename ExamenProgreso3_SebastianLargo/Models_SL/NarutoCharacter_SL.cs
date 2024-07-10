
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenProgreso3_SebastianLargo.Models_SL
{
    public class NarutoCharacter_SL
    {
        [PrimaryKey, AutoIncrement]
        public string iD { get; set; }
        public string Nombre { get; set; }
        public string Images { get; set; }
    }
}
