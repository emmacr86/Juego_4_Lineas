using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto2_4Lineas
{
    class PCLocation
    {
        public int CoordenadaPCFila(string fichas)
        {
            int FilaPC = 0;
            if (fichas == "fichas1"
               || fichas == "fichas2"
               || fichas == "fichas3"
               || fichas == "fichas4"
               || fichas == "fichas5"
               || fichas == "fichas6"
               || fichas == "fichas7")
            {
                FilaPC = 0;
            }
            else if (fichas == "fichas8"
               || fichas == "fichas9"
               || fichas == "fichas10"
               || fichas == "fichas11"
               || fichas == "fichas12"
               || fichas == "fichas13"
               || fichas == "fichas14")
            {
                FilaPC = 1;
            }
            else if (fichas == "fichas15"
               || fichas == "fichas16"
               || fichas == "fichas17"
               || fichas == "fichas18"
               || fichas == "fichas19"
               || fichas == "fichas20"
               || fichas == "fichas21")
            {
                FilaPC = 2;
            }
            else if (fichas == "fichas22"
               || fichas == "fichas23"
               || fichas == "fichas24"
               || fichas == "fichas25"
               || fichas == "fichas26"
               || fichas == "fichas27"
               || fichas == "fichas28")
            {
                FilaPC = 3;
            }
            else if (fichas == "fichas29"
               || fichas == "fichas30"
               || fichas == "fichas31"
               || fichas == "fichas32"
               || fichas == "fichas33"
               || fichas == "fichas34"
               || fichas == "fichas35")
            {
                FilaPC = 4;
            }
            else if (fichas == "fichas36"
               || fichas == "fichas37"
               || fichas == "fichas38"
               || fichas == "fichas39"
               || fichas == "fichas40"
               || fichas == "fichas41"
               || fichas == "fichas42")
            {
                FilaPC = 5;
            }

            return FilaPC;
        }

        public int CoordenadaPCCol(string fichas)
        {
            int ColmPC = 0;
            if (fichas == "fichas1"
               || fichas == "fichas8"
               || fichas == "fichas15"
               || fichas == "fichas22"
               || fichas == "fichas29"
               || fichas == "fichas36")
            {
                ColmPC = 0;
            }
            else if (fichas == "fichas2"
               || fichas == "fichas9"
               || fichas == "fichas16"
               || fichas == "fichas23"
               || fichas == "fichas30"
               || fichas == "fichas37")
            {
                ColmPC = 1;
            }
            else if (fichas == "fichas3"
               || fichas == "fichas10"
               || fichas == "fichas17"
               || fichas == "fichas24"
               || fichas == "fichas31"
               || fichas == "fichas38")
            {
                ColmPC = 2;
            }
            else if (fichas == "fichas4"
               || fichas == "fichas11"
               || fichas == "fichas18"
               || fichas == "fichas25"
               || fichas == "fichas32"
               || fichas == "fichas39")
            {
                ColmPC = 3;
            }
            else if (fichas == "fichas5"
               || fichas == "fichas12"
               || fichas == "fichas19"
               || fichas == "fichas26"
               || fichas == "fichas33"
               || fichas == "fichas40")
            {
                ColmPC = 4;
            }
            else if (fichas == "fichas6"
               || fichas == "fichas13"
               || fichas == "fichas20"
               || fichas == "fichas27"
               || fichas == "fichas34"
               || fichas == "fichas41")
            {
                ColmPC = 5;
            }
            else if (fichas == "fichas7"
                || fichas == "fichas14"
                || fichas == "fichas21"
                || fichas == "fichas28"
                || fichas == "fichas35"
                || fichas == "fichas42")
            {
                ColmPC = 6;
            }

            return ColmPC;
        }

        public int CoordenadaIdentDD(string fichas) {
            int identificador = 0;

            if (fichas == "fichas22"
               || fichas == "fichas16"
               || fichas == "fichas10"
               || fichas == "fichas4")
            {
                identificador = 4;
            }
            else if (fichas == "fichas29"
               || fichas == "fichas23"
               || fichas == "fichas17"
               || fichas == "fichas11"
               || fichas == "fichas5")
            {
                identificador = 5;
            }
            else if (fichas == "fichas36"
               || fichas == "fichas30"
               || fichas == "fichas24"
               || fichas == "fichas18"
               || fichas == "fichas12"
               || fichas == "fichas6")
            {
                identificador = 6;
            }
            else if (fichas == "fichas37"
               || fichas == "fichas31"
               || fichas == "fichas25"
               || fichas == "fichas19"
               || fichas == "fichas13"
               || fichas == "fichas7")
            {
                identificador = 61;
            }
            else if (fichas == "fichas38"
               || fichas == "fichas32"
               || fichas == "fichas26"
               || fichas == "fichas20"
               || fichas == "fichas14")
            {
                identificador = 51;
            }
            else if (fichas == "fichas39"
               || fichas == "fichas33"
               || fichas == "fichas27"
               || fichas == "fichas21")
            {
                identificador = 41;
            }
            return identificador;
        }

        public int CoordenadaIdentDI(string fichas)
        {
            int identificadorDI = 0;

            if (fichas == "fichas15"
               || fichas == "fichas23"
               || fichas == "fichas31"
               || fichas == "fichas39")
            {
                identificadorDI = 7;
            }
            else if (fichas == "fichas8"
               || fichas == "fichas16"
               || fichas == "fichas24"
               || fichas == "fichas32"
               || fichas == "fichas40")
            {
                identificadorDI = 8;
            }
            else if (fichas == "fichas1"
               || fichas == "fichas9"
               || fichas == "fichas17"
               || fichas == "fichas25"
               || fichas == "fichas33"
               || fichas == "fichas41")
            {
                identificadorDI = 9;
            }
            else if (fichas == "fichas2"
               || fichas == "fichas10"
               || fichas == "fichas18"
               || fichas == "fichas26"
               || fichas == "fichas34"
               || fichas == "fichas42")
            {
                identificadorDI = 10;
            }
            else if (fichas == "fichas3"
               || fichas == "fichas11"
               || fichas == "fichas19"
               || fichas == "fichas27"
               || fichas == "fichas35")
            {
                identificadorDI = 11;
            }
            else if (fichas == "fichas4"
               || fichas == "fichas12"
               || fichas == "fichas20"
               || fichas == "fichas28")
            {
                identificadorDI = 12;
            }
            return identificadorDI;
        }
    }
}
