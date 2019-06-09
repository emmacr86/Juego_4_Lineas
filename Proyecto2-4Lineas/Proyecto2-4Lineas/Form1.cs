using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto2_4Lineas
{
    public partial class Juego : Form
    {
        private static int[,] MatrizJugadas = new int[6, 7];
        List<Fichas> fichasTablero = null;

        private static int jugada = 1;
        private static int cantidadFichas = 42;
        private static int juegosGanadosUsuario = 0;
        private static int juegosGanadosUsuario2 = 0;
        private static int juegosGanadosCompu = 0;
        private static Random objRandom = new Random();
        private static Boolean computadora = false;

        //puntos diagonal derecha
        Point coord1 = new Point(0, 0);
        Point coord2 = new Point(1, 0);
        Point coord3 = new Point(2, 0);
        Point coord4 = new Point(0, 1);
        Point coord5 = new Point(1, 1);
        Point coord6 = new Point(0, 2);
        Point coord7 = new Point(5, 4);
        Point coord8 = new Point(4, 5);
        Point coord9 = new Point(5, 5);
        Point coord10 = new Point(3, 6);
        Point coord11 = new Point(4, 6);
        Point coord12 = new Point(5, 6);

        //puntos diagonal izquierda
        Point coord13 = new Point(3, 0);
        Point coord14 = new Point(4, 0);
        Point coord15 = new Point(5, 0);
        Point coord16 = new Point(4, 1);
        Point coord17 = new Point(5, 1);
        Point coord18 = new Point(5, 2);
        Point coord19 = new Point(0, 4);
        Point coord20 = new Point(0, 5);
        Point coord21 = new Point(1, 5);
        Point coord22 = new Point(0, 6);
        Point coord23 = new Point(1, 6);
        Point coord24 = new Point(2, 6);

        private void InteligenciaComputadora()
        {
            List<Fichas> fichasDisponibles = new List<Fichas>();

            if (computadora == true)
            {
                for (int i = 0; i < fichasTablero.Count; i++)
                {
                    if (fichasTablero[i].Enabled == true)
                    {
                        fichasDisponibles.Add(fichasTablero[i]);
                    }
                }
                int random = objRandom.Next(fichasDisponibles.Count);

                for (int i = 0; i < fichasDisponibles.Count; i++)
                {
                    if (jugada == 2)
                    {
                        if (fichasDisponibles[i].Enabled == true)
                        {
                            if (fichasDisponibles[i].BackColor != Color.Yellow && fichasDisponibles[i].BackColor != Color.Red)
                            {
                                int filaPC = 0;
                                int colPC = 0;
                                int identificador = 0;
                                int identificadorDI = 0;
                                int CheckGanador = 0;
                                string mensajeGanador = "";
                                jugada = 1;
                                cantidadFichas = cantidadFichas - 1;
                                lblCantidadFichas.Text = cantidadFichas.ToString();
                                lblJugadorActivo.Text = "Usuario-1";
                                fichasDisponibles[random].BackColor = Color.Red;
                                String fichas = fichasDisponibles[random].Name;
                                PCLocation objPCLocation = new PCLocation();
                                filaPC = objPCLocation.CoordenadaPCFila(fichas);
                                colPC = objPCLocation.CoordenadaPCCol(fichas);
                                identificador = objPCLocation.CoordenadaIdentDD(fichas);
                                identificadorDI = objPCLocation.CoordenadaIdentDI(fichas);
                                MatrizJugadas[filaPC, colPC] = 3;

                                CheckGanador = VerificarGanador(filaPC, colPC, MatrizJugadas, identificador, identificadorDI);
                                mensajeGanador = ImprimirGanador(CheckGanador);

                                if (mensajeGanador != "")
                                {
                                    MessageBox.Show(mensajeGanador);
                                    ReiniciarJuego(fichasTablero);
                                    CheckGanador = 0;
                                    mensajeGanador = "";
                                }

                                DesbloqueoCampos(fichasDisponibles[i], fichasTablero);
                                Empate();
    }
}
                    }
                }
            }
        }

        //diagonal derecha
        private bool verificarDiagonal(Point CoordenadasMatriz)
{
    if (Point.Equals(CoordenadasMatriz, coord1)
        || Point.Equals(CoordenadasMatriz, coord2)
        || Point.Equals(CoordenadasMatriz, coord3)
        || Point.Equals(CoordenadasMatriz, coord4)
        || Point.Equals(CoordenadasMatriz, coord5)
        || Point.Equals(CoordenadasMatriz, coord6)
        || Point.Equals(CoordenadasMatriz, coord7)
        || Point.Equals(CoordenadasMatriz, coord8)
        || Point.Equals(CoordenadasMatriz, coord9)
        || Point.Equals(CoordenadasMatriz, coord10)
        || Point.Equals(CoordenadasMatriz, coord11)
        || Point.Equals(CoordenadasMatriz, coord12))
    {
        return false;
    }
    else
    {
        return true;
    }
}

//diagonal izquierda
private bool verificarDiagonalI(Point CoordenadasMatriz)
{
    if (Point.Equals(CoordenadasMatriz, coord13)
        || Point.Equals(CoordenadasMatriz, coord14)
        || Point.Equals(CoordenadasMatriz, coord15)
        || Point.Equals(CoordenadasMatriz, coord16)
        || Point.Equals(CoordenadasMatriz, coord17)
        || Point.Equals(CoordenadasMatriz, coord18)
        || Point.Equals(CoordenadasMatriz, coord19)
        || Point.Equals(CoordenadasMatriz, coord20)
        || Point.Equals(CoordenadasMatriz, coord21)
        || Point.Equals(CoordenadasMatriz, coord22)
        || Point.Equals(CoordenadasMatriz, coord23)
        || Point.Equals(CoordenadasMatriz, coord24))
    {
        return false;
    }
    else
    {
        return true;
    }
}

private int VerificarGanador(int fila, int col, int[,] MatrizJugadas, int identificador, int identificadorDI)
{
    //coordenadas donde no ocupa buscar la matriz porque no da la diagonal derecha
    Point CoordenadasMatriz = new Point(fila, col); //creo la coordenada = fila,col
    bool CheckCoordenada = verificarDiagonal(CoordenadasMatriz);
    bool CheckCoordenadaDIz = verificarDiagonalI(CoordenadasMatriz);
    int ganador = 0;

    //contadores horizontal
    int cont1 = 0;
    int cont2 = 0;
    int contCPU = 0;

    //contadores vertical
    int contH1 = 0;
    int contH2 = 0;
    int contHCPU = 0;

    //contadores diagonal derecha
    int contDD1 = 0;
    int contDD2 = 0;
    int contDDCPU = 0;

    //contadores diagonal izquierda
    int contDI1 = 0;
    int contDI2 = 0;
    int contDICPU = 0;

    BasesDiagonal objBases = new BasesDiagonal();
    //debe haber contador vertical, horizontal, diagonal der, diagonal izq
    //verifica si hay ganador en la horizontal
    for (int i = 0; i <= 6; i++)
    {
        if (MatrizJugadas[fila, 0] == 1 && MatrizJugadas[fila, 1] == 1 && MatrizJugadas[fila, 2] == 1 && MatrizJugadas[fila, 3] == 1)
        {
            cont1 = 4;
        }
        else
        {
            if (MatrizJugadas[fila, 1] == 1 && MatrizJugadas[fila, 2] == 1 && MatrizJugadas[fila, 3] == 1 && MatrizJugadas[fila, 4] == 1)
            {
                cont1 = 4;
            }
            else
            {
                if (MatrizJugadas[fila, 2] == 1 && MatrizJugadas[fila, 3] == 1 && MatrizJugadas[fila, 4] == 1 && MatrizJugadas[fila, 5] == 1)
                {
                    cont1 = 4;
                }
                else
                {
                    if (MatrizJugadas[fila, 3] == 1 && MatrizJugadas[fila, 4] == 1 && MatrizJugadas[fila, 5] == 1 && MatrizJugadas[fila, 6] == 1)
                    {
                        cont1 = 4;
                    }
                }
            }
        }

        if (MatrizJugadas[fila, 0] == 2 && MatrizJugadas[fila, 1] == 2 && MatrizJugadas[fila, 2] == 2 && MatrizJugadas[fila, 3] == 2)
        {
            cont2 = 4;
        }
        else
        {
            if (MatrizJugadas[fila, 1] == 2 && MatrizJugadas[fila, 2] == 2 && MatrizJugadas[fila, 3] == 2 && MatrizJugadas[fila, 4] == 2)
            {
                cont2 = 4;
            }
            else
            {
                if (MatrizJugadas[fila, 2] == 2 && MatrizJugadas[fila, 3] == 2 && MatrizJugadas[fila, 4] == 2 && MatrizJugadas[fila, 5] == 2)
                {
                    cont2 = 4;
                }
                else
                {
                    if (MatrizJugadas[fila, 3] == 2 && MatrizJugadas[fila, 4] == 2 && MatrizJugadas[fila, 5] == 1 && MatrizJugadas[fila, 6] == 2)
                    {
                        cont2 = 4;
                    }
                }
            }
        }

        if (MatrizJugadas[fila, 0] == 3 && MatrizJugadas[fila, 1] == 3 && MatrizJugadas[fila, 2] == 3 && MatrizJugadas[fila, 3] == 3)
        {
            contCPU = 4;
        }
        else
        {
            if (MatrizJugadas[fila, 1] == 3 && MatrizJugadas[fila, 2] == 3 && MatrizJugadas[fila, 3] == 3 && MatrizJugadas[fila, 4] == 3)
            {
                contCPU = 4;
            }
            else
            {
                if (MatrizJugadas[fila, 2] == 3 && MatrizJugadas[fila, 3] == 3 && MatrizJugadas[fila, 4] == 3 && MatrizJugadas[fila, 5] == 3)
                {
                    contCPU = 4;
                }
                else
                {
                    if (MatrizJugadas[fila, 3] == 3 && MatrizJugadas[fila, 4] == 3 && MatrizJugadas[fila, 5] == 3 && MatrizJugadas[fila, 6] == 3)
                    {
                        contCPU = 4;
                    }
                }
            }
        }
    }

    //verifica si hay ganador en la vertical
    for (int i = 5; i >= 0; i--)
    {
        if (MatrizJugadas[i, col] == 1)
        {
            contH1++;
            contH2 = 0;
            contHCPU = 0;
        }
        else if (MatrizJugadas[i, col] == 2)
        {
            contH2++;
            contH1 = 0;
            contHCPU = 0;
        }
        else if (MatrizJugadas[i, col] == 3)
        {
            contHCPU++;
            contH1 = 0;
            contH2 = 0;
        }
    }

    //verifica diagonal derecha
    if (CheckCoordenada == true)
    {
        int colD = 0;
        int filD = 0;
        int contt = 0;

        switch (identificador)
        {
            case 4:
                contt = 4;
                colD = objBases.baseCD0;
                filD = objBases.baseFD3;
                break;
            case 5:
                contt = 5;
                colD = objBases.baseCD0;
                filD = objBases.baseFD4;
                break;
            case 6:
                contt = 6;
                colD = objBases.baseCD0;
                filD = objBases.baseFD5;
                break;
            case 61:
                contt = 6;
                colD = objBases.baseCD1;
                filD = objBases.baseFD5;
                break;
            case 51:
                contt = 5;
                colD = objBases.baseCD2;
                filD = objBases.baseFD5;
                break;
            case 41:
                contt = 4;
                colD = objBases.baseCD3;
                filD = objBases.baseFD5;
                break;
        }

        for (int i = 0; i < contt; i++)
        {
            if (MatrizJugadas[filD, colD] == 1)
            {
                contDD1++;
                contDD2 = 0;
                contDDCPU = 0;
            }
            else if (MatrizJugadas[filD, colD] == 2)
            {
                contDD2++;
                contDD1 = 0;
                contDDCPU = 0;
            }
            else if (MatrizJugadas[filD, colD] == 3)
            {
                contDDCPU++;
                contDD1 = 0;
                contDD2 = 0;
            }
            filD--;
            colD++;
        }

    }

    //verifica diagonal izquierda
    if (CheckCoordenadaDIz == true)
    {
        int colD = 0;
        int filD = 0;
        int conttD = 0;

        switch (identificadorDI)
        {
            case 7:
                filD = objBases.baseFD5;
                colD = objBases.baseCD3;
                conttD = 4;
                break;
            case 8:
                filD = objBases.baseFD5;
                colD = objBases.baseCD4;
                conttD = 5;
                break;
            case 9:
                filD = objBases.baseFD5;
                colD = objBases.baseCD5;
                conttD = 6;
                break;
            case 10:
                filD = objBases.baseFD5;
                colD = objBases.baseCD6;
                conttD = 6;
                break;
            case 11:
                filD = objBases.baseFD4;
                colD = objBases.baseCD6;
                conttD = 5;
                break;
            case 12:
                filD = objBases.baseFD3;
                colD = objBases.baseCD6;
                conttD = 4;
                break;
        }

        for (int i = 0; i < conttD; i++)
        {
            if (MatrizJugadas[filD, colD] == 1)
            {
                contDI1++;
                contDI2 = 0;
                contDICPU = 0;
            }
            else if (MatrizJugadas[filD, colD] == 2)
            {
                contDI2++;
                contDI1 = 0;
                contDICPU = 0;
            }
            else if (MatrizJugadas[filD, colD] == 3)
            {
                contDICPU++;
                contDI1 = 0;
                contDI2 = 0;
            }
            filD--;
            colD--;
        }
    }

    if (cont1 == 4 || contH1 == 4 || contDD1 == 4 || contDI1 == 4)
    {
        ganador = 1;
    }
    else if (cont2 == 4 || contH2 == 4 || contDD2 == 4 || contDI2 == 4)
    {
        ganador = 2;
    }
    else if (contCPU == 4 || contHCPU == 4 || contDDCPU == 4 || contDICPU == 4)
    {
        ganador = 3;
    }
    return ganador;
}

private string ImprimirGanador(int ganador)
{
    string winner = "";
    if (ganador == 1)
    {
        winner = "El ganador es Jugador 1";
        juegosGanadosUsuario = juegosGanadosUsuario + 1;
        lblganadosUsuario1.Text = juegosGanadosUsuario.ToString();
        for (int i = 0; i < fichasTablero.Count; i++)
        {
            fichasTablero[i].Enabled = false;
        }
    }
    else if (ganador == 2)
    {
        winner = "El ganador es Jugador 2";
        juegosGanadosUsuario2 = juegosGanadosUsuario2 + 1;
        lblusuario2Ganados.Text = juegosGanadosUsuario2.ToString();
        for (int i = 0; i < fichasTablero.Count; i++)
        {
            fichasTablero[i].Enabled = false;
        }
    }
    else if (ganador == 3)
    {
        winner = "El ganador es Jugador CPU";
        juegosGanadosCompu = juegosGanadosCompu + 1;
        lblcomputadoraGanados.Text = juegosGanadosCompu.ToString();
        for (int i = 0; i < fichasTablero.Count; i++)
        {
            fichasTablero[i].Enabled = false;
        }
    }
    else
    {
        winner = "";
    }
    return winner;
}

private void ReiniciarJuego(List<Fichas> fichasTablero)
{
    string mensaje = "Desea iniciar una partida nueva?";
    DialogResult res;
    res = MessageBox.Show(mensaje, "Juego Nuevo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    if (res == DialogResult.Yes)
    {
        MatrizJugadas = new int[6, 7];
        cantidadFichas = 42;
        jugada = 1;
        lblJugadorActivo.Text = "Usuario-1";
        lblCantidadFichas.Text = cantidadFichas.ToString();

        for (int i = 0; i < fichasTablero.Count; i++)
        {
            fichasTablero[i].BackColor = Color.White;
            if (i <= 34)
            {
                fichasTablero[i].Enabled = false;
            }
            else { fichasTablero[i].Enabled = true; }
        }
    }
}

private void DesbloqueoCampos(Fichas Fichas, List<Fichas> fichasTablero)
{
    for (int i = 7; i < fichasTablero.Count; i++)
    {
        if (fichasTablero[i].BackColor == Color.Red || fichasTablero[i].BackColor == Color.Yellow)
        {
            fichasTablero[i - 7].Enabled = true;
        }
    }
    for (int i = 0; i < fichasTablero.Count; i++)
    {
        if (fichasTablero[i].BackColor == Color.Red || fichasTablero[i].BackColor == Color.Yellow)
        {
            fichasTablero[i].Enabled = false;
        }
    }
}

private void Turno(Fichas Fichas)
{
    if (jugada == 1)
    {
        jugada = 2;
        Color usuario1Color = Color.Yellow;
        cantidadFichas = cantidadFichas - 1;
        lblCantidadFichas.Text = cantidadFichas.ToString();
        Fichas.BackColor = Color.Yellow;
        DesbloqueoCampos(Fichas, fichasTablero);
        Empate();
        if (computadora == true)
        {
            lblJugadorActivo.Text = "Computadora";
        }
        else
        {
            lblJugadorActivo.Text = "Usuario-2";
        }
    }
    else
    {
        if (computadora == false)
        {
            jugada = 1;
            Color usuario1Color = Color.Red;
            lblJugadorActivo.Text = "Usuario-1";
            cantidadFichas = cantidadFichas - 1;
            lblCantidadFichas.Text = cantidadFichas.ToString();
            Fichas.BackColor = Color.Red;
            DesbloqueoCampos(Fichas, fichasTablero);
            Empate();
        }
    }
}

private void Empate()
{
    if (cantidadFichas == 0)
    {
        string mensaje = "Hubo un empate";
        DialogResult res;
        res = MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (res == DialogResult.OK)
        {
            ReiniciarJuego(fichasTablero);
        }
    }
}

public Juego()
{
    InitializeComponent();

    fichasTablero = new List<Fichas>()
                             {fichas1, fichas2, fichas3, fichas4,fichas5, fichas6, fichas7,
                              fichas8, fichas9, fichas10,fichas11,fichas12, fichas13, fichas14,
                              fichas15, fichas16, fichas17,fichas18,fichas19, fichas20, fichas21,
                              fichas22, fichas23, fichas24,fichas25,fichas26, fichas27, fichas28,
                              fichas29, fichas30, fichas31,fichas32,fichas33, fichas34, fichas35,
                              fichas36, fichas37, fichas38,fichas39,fichas40, fichas41, fichas42 };
    timer1.Start();

}

private void salirToolStripMenuItem_Click(object sender, EventArgs e)
{
    string mensaje = "Desea Salir del juego?";
    DialogResult res;
    res = MessageBox.Show(mensaje, "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
    if (res == DialogResult.Yes)
    {
        Application.Exit();
    }
}

private void infoToolStripMenuItem_Click(object sender, EventArgs e)
{
    Info objInfo = new Proyecto2_4Lineas.Info();
    objInfo.Show();
}
private void juegoNuevoToolStripMenuItem_Click(object sender, EventArgs e)
{
    ReiniciarJuego(fichasTablero);
}
private void fichas1_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 9;
    int fila = 0;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas1);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas2_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 10;
    int fila = 0;
    int col = 2;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas2);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas3_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 11;
    int fila = 0;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas3);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas4_Click(object sender, EventArgs e)
{
    int identificador = 4;
    int identificadorDI = 12;
    int fila = 0;
    int col = 3;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas4);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas5_Click(object sender, EventArgs e)
{
    int identificador = 5;
    int identificadorDI = 0;
    int fila = 0;
    int col = 4;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas5);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas6_Click(object sender, EventArgs e)
{
    int identificador = 6;
    int identificadorDI = 0;
    int fila = 0;
    int col = 5;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas6);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas7_Click(object sender, EventArgs e)
{
    int identificador = 61;
    int identificadorDI = 0;
    int fila = 0;
    int col = 6;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas7);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas8_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 8;
    int fila = 1;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas8);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas9_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 9;
    int fila = 1;
    int col = 1;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas9);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas10_Click(object sender, EventArgs e)
{
    int identificador = 4;
    int identificadorDI = 10;
    int fila = 1;
    int col = 2;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas10);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas11_Click(object sender, EventArgs e)
{
    int identificador = 5;
    int identificadorDI = 11;
    int fila = 1;
    int col = 3;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas11);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas12_Click(object sender, EventArgs e)
{
    int identificador = 6;
    int identificadorDI = 12;
    int fila = 1;
    int col = 4;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas12);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas13_Click(object sender, EventArgs e)
{
    int identificador = 61;
    int identificadorDI = 0;
    int fila = 1;
    int col = 5;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas13);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas14_Click(object sender, EventArgs e)
{
    int identificador = 51;
    int identificadorDI = 0;
    int fila = 1;
    int col = 6;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas14);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas15_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 7;
    int fila = 2;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas15);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas16_Click(object sender, EventArgs e)
{
    int identificador = 4;
    int identificadorDI = 8;
    int fila = 2;
    int col = 1;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas16);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas17_Click(object sender, EventArgs e)
{
    int identificador = 5;
    int identificadorDI = 9;
    int fila = 2;
    int col = 2;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas17);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas18_Click(object sender, EventArgs e)
{
    int identificador = 6;
    int identificadorDI = 10;
    int fila = 2;
    int col = 3;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas18);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas19_Click(object sender, EventArgs e)
{
    int identificador = 61;
    int identificadorDI = 11;
    int fila = 2;
    int col = 4;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas19);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas20_Click(object sender, EventArgs e)
{
    int identificador = 51;
    int identificadorDI = 12;
    int fila = 2;
    int col = 5;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas20);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas21_Click(object sender, EventArgs e)
{
    int identificador = 41;
    int identificadorDI = 0;
    int fila = 2;
    int col = 6;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas21);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas22_Click(object sender, EventArgs e)
{
    int identificador = 4;
    int identificadorDI = 0;
    int fila = 3;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas22);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas23_Click(object sender, EventArgs e)
{
    int identificador = 5;
    int identificadorDI = 7;
    int fila = 3;
    int col = 1;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas23);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas24_Click(object sender, EventArgs e)
{
    int identificador = 6;
    int identificadorDI = 8;
    int fila = 3;
    int col = 2;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas24);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas25_Click(object sender, EventArgs e)
{
    int identificador = 61;
    int identificadorDI = 9;
    int fila = 3;
    int col = 3;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas25);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas26_Click(object sender, EventArgs e)
{
    int identificador = 51;
    int identificadorDI = 10;
    int fila = 3;
    int col = 4;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas26);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas27_Click(object sender, EventArgs e)
{
    int identificador = 41;
    int identificadorDI = 11;
    int fila = 3;
    int col = 5;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas27);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas28_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 12;
    int fila = 3;
    int col = 6;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas28);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas29_Click(object sender, EventArgs e)
{
    int identificador = 5;
    int identificadorDI = 0;
    int fila = 4;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas29);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas30_Click(object sender, EventArgs e)
{
    int identificador = 6;
    int identificadorDI = 0;
    int fila = 4;
    int col = 1;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas30);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas31_Click(object sender, EventArgs e)
{
    int identificador = 61;
    int identificadorDI = 7;
    int fila = 4;
    int col = 2;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas31);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas32_Click(object sender, EventArgs e)
{
    int identificador = 51;
    int identificadorDI = 8;
    int fila = 4;
    int col = 3;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas32);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas33_Click(object sender, EventArgs e)
{
    int identificador = 41;
    int identificadorDI = 9;
    int fila = 4;
    int col = 4;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas33);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas34_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 10;
    int fila = 4;
    int col = 5;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas34);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas35_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 11;
    int fila = 4;
    int col = 6;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas35);
    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas36_Click(object sender, EventArgs e)
{
    int identificador = 6;
    int identificadorDI = 0;
    int fila = 5;
    int col = 0;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas36);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas37_Click(object sender, EventArgs e)
{
    int identificador = 61;
    int identificadorDI = 0;
    int fila = 5;
    int col = 1;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas37);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }

}
private void fichas38_Click(object sender, EventArgs e)
{
    int identificador = 51;
    int identificadorDI = 0;
    int fila = 5;
    int col = 2;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas38);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas39_Click(object sender, EventArgs e)
{
    int identificador = 41;
    int identificadorDI = 7;
    int fila = 5;
    int col = 3;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas39);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas40_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 8;
    int fila = 5;
    int col = 4;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas40);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas41_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 9;
    int fila = 5;
    int col = 5;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas41);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}
private void fichas42_Click(object sender, EventArgs e)
{
    int identificador = 0;
    int identificadorDI = 10;
    int fila = 5;
    int col = 6;
    int CheckGanador = 0;
    string mensajeGanador = "";
    MatrizJugadas[fila, col] = jugada;
    Turno(fichas42);

    CheckGanador = VerificarGanador(fila, col, MatrizJugadas, identificador, identificadorDI);
    mensajeGanador = ImprimirGanador(CheckGanador);

    if (mensajeGanador != "")
    {
        MessageBox.Show(mensajeGanador);
        ReiniciarJuego(fichasTablero);
        CheckGanador = 0;
        mensajeGanador = "";
    }
}

private void usuarioVsPCToolStripMenuItem_Click(object sender, EventArgs e)
{
    computadora = true;
}

private void usuario1VsUsuario2ToolStripMenuItem_Click(object sender, EventArgs e)
{
    computadora = false;
}

private void timer1_Tick(object sender, EventArgs e)
{
    if (jugada == 2)
    {
        InteligenciaComputadora();
    }
}
    }
}
