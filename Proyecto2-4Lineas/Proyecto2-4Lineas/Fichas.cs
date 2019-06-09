﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto2_4Lineas
{
    class Fichas : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grafico = new GraphicsPath();
            grafico.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grafico); 
            base.OnPaint(pevent);
        }
    }
}
