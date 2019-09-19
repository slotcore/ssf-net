using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public class Cls_Printer
    {
        public System.Drawing.Printing.PrintPageEventArgs e;
        public PrintDocument printDocument;

        public Font drawFont;
        public short N_NUMEROCOPIAS = 0;
        public void Print_Texto(string c_Texto, int n_PosX, int n_PosY, Color o_Color)
        {
            SolidBrush drawBrush = new SolidBrush(o_Color);
            e.Graphics.DrawString(c_Texto, drawFont, drawBrush, new Point(n_PosX, n_PosY));
        }
        public void Print_TextoCuadro(string c_Texto, int n_PosX, int n_PosY, int n_Ancho, int n_Alto, int n_Alineacion, Color o_ColorTexto, Color o_ColorLinea)
        {
            Rectangle displayRectangle = new Rectangle(new Point(n_PosX, n_PosY), new Size(n_Ancho, n_Alto));
            StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
            Pen ColorLinea = new Pen(o_ColorLinea, 1);
            SolidBrush ColorTexto = new SolidBrush(o_ColorTexto);

            // Set the LineAlignment and Alignment properties for
            // both StringFormat objects to different values.
            format1.LineAlignment = StringAlignment.Near;

            if (n_Alineacion == 1) { format1.Alignment = StringAlignment.Center; }
            if (n_Alineacion == 2) { format1.Alignment = StringAlignment.Near; }
            if (n_Alineacion == 3) { format1.Alignment = StringAlignment.Far; }

            e.Graphics.DrawRectangle(ColorLinea, displayRectangle);
            e.Graphics.DrawString(c_Texto, drawFont, ColorTexto, (RectangleF)displayRectangle, format1);
        }
        public void Print_TextoCuadroWrap(string c_Texto, int n_PosX, ref int n_PosY, int n_Ancho, int n_Alto, int n_Alineacion, Color o_ColorTexto, Color o_ColorLinea)
        {
            Rectangle displayRectangle = new Rectangle(new Point(n_PosX, n_PosY), new Size(n_Ancho, n_Alto));
            StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);
            Pen ColorLinea = new Pen(o_ColorLinea, 1);
            SolidBrush ColorTexto = new SolidBrush(o_ColorTexto);

            // Set the LineAlignment and Alignment properties for
            // both StringFormat objects to different values.
            format1.LineAlignment = StringAlignment.Near;

            if (n_Alineacion == 1) { format1.Alignment = StringAlignment.Center; }
            if (n_Alineacion == 2) { format1.Alignment = StringAlignment.Near; }
            if (n_Alineacion == 3) { format1.Alignment = StringAlignment.Far; }

            using (Font useFont = new Font("Arial", 9, FontStyle.Regular))
            {
                displayRectangle.Location = new Point(n_PosX, n_PosY);

                displayRectangle.Size = new Size(n_Ancho, ((int)e.Graphics.MeasureString(c_Texto, useFont, n_Ancho, StringFormat.GenericTypographic).Height));
                e.Graphics.DrawRectangle(ColorLinea, displayRectangle);
                e.Graphics.DrawString(c_Texto, useFont, Brushes.Black, displayRectangle, format1);
            }
            n_PosY = n_PosY + Convert.ToInt32(displayRectangle.Height);


            //e.Graphics.DrawRectangle(ColorLinea, displayRectangle);
            //e.Graphics.DrawString(c_Texto, drawFont, ColorTexto, (RectangleF)displayRectangle, format1);
        }
        public void Print_Linea(int n_PosX1, int n_PosY1, int n_PosX2, int n_PosY2, int n_TipoLinea, Color o_Color, int n_AnchoLinea)
        {
            //Pen myPen = new Pen(Color.Black, 2);
            Pen myPen = new Pen(o_Color, n_AnchoLinea);
            if (n_TipoLinea == 1) { myPen.DashStyle = DashStyle.Solid; }
            if (n_TipoLinea == 2) { myPen.DashStyle = DashStyle.Dash; }
            if (n_TipoLinea == 3) { myPen.DashStyle = DashStyle.Dot; }
            e.Graphics.DrawLine(myPen, n_PosX1, n_PosY1, n_PosX2, n_PosY2);
        }
        public void Print_Imagen(string c_Archivo, int n_PosX, int n_PosY, int n_Ancho, int n_Alto, int n_TipoLinea, Color o_Color, int n_AnchoLinea)
        {
            Bitmap bitmap = new Bitmap(c_Archivo);
            RectangleF srcRect = new RectangleF(n_PosX, n_PosY, n_Ancho, n_Alto);
            e.Graphics.DrawImage(bitmap, srcRect);
        }
        public void Print_Imprimir()
        {
            PrintDocument printDocument2 = new PrintDocument();
            var printerSettings = new PrinterSettings
            {
                Copies = (short)N_NUMEROCOPIAS
            };
            //string printer = "Microsoft Print To PDF";
            //printDocument2.PrinterSettings.PrinterName = printer;
            //printDocument2.PrinterSettings.Copies = N_NUMEROCOPIAS;
            //printDocument2.PrinterSettings.Copies = N_NUMEROCOPIAS;
            //printDocument2.DefaultPageSettings.PrinterSettings.Copies = N_NUMEROCOPIAS;
            printDocument.PrinterSettings = printerSettings;
            printDocument2 = printDocument;
            printDocument2.Print();
            //if (N_NUMEROCOPIAS == 1) { printDocument2.Print(); }
            //if (N_NUMEROCOPIAS == 2) { printDocument2.Print(); printDocument2.Print(); }
        }
        public void Print_ExportarPDF()
        {
            PrintDocument printDocument2 = new PrintDocument();
            var printerSettings = new PrinterSettings
            {
                // PrinterName = "Microsoft Print To PDF"
                PrinterName = "PDF24 PDF",
                Copies = (short)N_NUMEROCOPIAS
            };
            //string printer = "Microsoft Print To PDF";
            //printDocument2.PrinterSettings.PrinterName = printer;
            //printDocument2.PrinterSettings.Copies = N_NUMEROCOPIAS;
            //printDocument2.PrinterSettings.Copies = N_NUMEROCOPIAS;
            //printDocument2.DefaultPageSettings.PrinterSettings.Copies = N_NUMEROCOPIAS;
            printDocument.PrinterSettings = printerSettings;
            printDocument2 = printDocument;
            printDocument2.Print();
            //if (N_NUMEROCOPIAS == 1) { printDocument2.Print(); }
            //if (N_NUMEROCOPIAS == 2) { printDocument2.Print(); printDocument2.Print(); }
        }
        public void Print_VistaPrevia()
        {
            PrintDocument printDocument2 = new PrintDocument();
            //string printer = "Microsoft Print To PDF";
            //printDocument2.PrinterSettings.PrinterName = printer;
            printDocument2 = printDocument;

            PrintPreviewDialog PrintPreviewDialog1 = new PrintPreviewDialog();
            PrintPreviewDialog1.Document = printDocument;                  // ASIGNADO AL PRINTDOCUMENT1
            PrintPreviewDialog1.WindowState = FormWindowState.Maximized;   // PRESENTARA LA VENTANA MAXIMIZADA
            PrintPreviewDialog1.ShowDialog();                              // MUESTRA EL DIALOGO DE VISTA PREVIA
        }

    }
}
