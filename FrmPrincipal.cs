using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Pantallas
{
    public partial class FrmPrincipal : Form
    {
        clsConsultas consultasPantallas = new clsConsultas();
        static int turno, limiteInferior, lineaInt;
        public static int diferenciaWO = 0, imprimirCaritas=0;
        static string lineaString = "JJ10-1", estatus = "", comida = "", numeroBinario = "";
        static string descripcionAlarma="", codigoAlarma = "";
        public static bool alarmaIniciado = false;




        public FrmPrincipal()
        {
            InitializeComponent();
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                leerNumeroLinea();
                consultasDatosModelos();
                consultasAlarmasTiempoReal();
                //asignarHorarios_Y_Caritas();

                if (Convert.ToInt32(LblActualTurno.Text) >= Convert.ToInt32(LblMetaTurno.Text))
                {
                    this.BackColor = Color.Green;
                }
                else
                {
                    this.BackColor = Color.Red;
                }
            }
            catch (Exception)
            {
            }
            
        }




        private void Form1_Resize(object sender, EventArgs e)
        {
            setFontSize(label1);
            setFontSize(label2);
            setFontSize(label3);
            setFontSize2(LblMetaTurno);
            setFontSize2(LblActualTurno);
            setFontSize2(LblDiferenciaTurno);
            setFontSize(LblLinea);
            setFontSize(LblModelo);
            setFontSize(LblHora);
            setFontSize(LblDescripcionModelo);
            setFontSizegroup(GBEscalerasTurno);
        }




        private void setFontSize(Label label1)
        {
            double tamanoancho = label1.Width;
            double tamanoalto = label1.Height;
            double variable =(Math.Sqrt(Math.Pow(tamanoalto,2) + Math.Pow(tamanoalto, 2)))/2.5F;
            label1.Font = new System.Drawing.Font(label1.Font.Name, (float)variable);
        }


        private void setFontSize2(Label label1)
        {
            double tamanoancho = label1.Width;
            double tamanoalto = label1.Height;
            double variable = (Math.Sqrt(Math.Pow(tamanoalto, 2) + Math.Pow(tamanoalto, 2))) / 2.8F;
            label1.Font = new System.Drawing.Font(label1.Font.Name, (float)variable, FontStyle.Bold);
        }




        private void setFontSizegroup(GroupBox label1)
        {
            double tamanoancho = label1.Width;
            double tamanoalto = label1.Height;
            double variable = (Math.Sqrt(Math.Pow(tamanoalto, 2) + Math.Pow(tamanoalto, 2)))/14F;
            label1.Font = new System.Drawing.Font(label1.Font.Name, (float)variable);
        }
        



        private void Tmr_EscaneoGeneral_Tick(object sender, EventArgs e)
        {
            try
            {
                leerNumeroLinea();
                LblHora.Text = DateTime.Now.ToLongTimeString();
                consultasDatosModelos();

                consultasAlarmasTiempoReal();

                if ((codigoAlarma != "0" || diferenciaWO == 0) & alarmaIniciado == false)
                {
                    alarmaIniciado = true;
                    FrmAlarmas frmAlarmas = new FrmAlarmas();
                    frmAlarmas.ShowDialog();
                }
                else
                {
                    if (comida == "1")
                    {
                        this.BackColor = Color.Blue;
                    }
                    else
                    {
                        if (Convert.ToInt32(LblActualTurno.Text) >= Convert.ToInt32(LblMetaTurno.Text))
                        {
                            this.BackColor = Color.Green;
                        }
                        else
                        {
                            this.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }




        private void consultasDatosModelos()
        {
            try
            {
                DefinirTurno_TiempoReal();
                string[] obtenerResultados = new string[6];
                string ahora = DateTime.Now.ToString("MM/dd/yyyy");
                consultasPantallas.CommandText = "SELECT * FROM dbo.Hist_AV_Modelo WHERE LINEA = '" + lineaString + "' AND TURNO = '" + turno + "' AND FECHA='" + ahora + "' ORDER BY INDICE";
                obtenerResultados = consultasPantallas.Obtener6campos("WORK_ORDER", "DESCRIPCION_WO", "ESTANDAR", "MODELO", "META", "PRODUCCION");

                //GBWorkorder.Text = "Work Order: " + obtenerResultados[0];
                LblDescripcionModelo.Text = "Descripcion: " + obtenerResultados[1];
                //LblRate.Text = obtenerResultados[2] + " pz/H";
                LblModelo.Text = "Modelo: " + obtenerResultados[3];
                //GBModelo.Text = "Modelo: " + obtenerResultados[3];
                ////LblMetaTurno.Text = obtenerResultados[4];
                //LblActualModelo.Text = obtenerResultados[5];
                LblLinea.Text = "Linea: " + lineaString;
                LblDiferenciaTurno.Text = (Convert.ToInt32(LblActualTurno.Text) - Convert.ToInt32(LblMetaTurno.Text)).ToString();
            }
            catch (Exception)
            {
            }
        }

        private void LblModelo_Click(object sender, EventArgs e)
        {

        }

        private void consultasAlarmasTiempoReal()
        {
            try
            {
                string[] obtenerResultados = new string[6];
                string ahora = DateTime.Now.ToString("MM/dd/yyyy");
                int limiteSuperior = DateTime.Now.Hour;
                consultasPantallas.CommandText = "SELECT * FROM dbo.Alarmas_Missing_RivetsVW WHERE Area = 'AV' AND LINEA='" + lineaString + "' AND Work_Order IS NOT NULL AND Andon IS NOT NULL AND Meta_Turno IS NOT NULL";
                obtenerResultados = consultasPantallas.Obtener6campos("Alarma", "Descripcion", "Meta_WO", "Actual_WO", "Andon", "Meta_Turno");
                codigoAlarma = obtenerResultados[0];
                descripcionAlarma = obtenerResultados[1];
                //LblMetaWorkorder.Text = obtenerResultados[2];
                //LblActualWorkorder.Text = obtenerResultados[3];
                estatus = obtenerResultados[4];
                LblMetaTurno.Text = obtenerResultados[5];
                //LblMetaTurno.Text = "423";
                numeroBinario = ConvertirBinario(Convert.ToInt32(estatus.Substring(0, estatus.Length)));
                ////numeroBinario = ConvertirBinario(1073741824);
                comida = numeroBinario.Substring(4, 1);

                try
                {
                    diferenciaWO = Convert.ToInt32(obtenerResultados[2]) - Convert.ToInt32(obtenerResultados[3]);
                    //LblDiferenciaWorkorder.Text = diferenciaWO.ToString();
                }
                catch (Exception)
                {
                    diferenciaWO = 0;
                    //LblDiferenciaWorkorder.Text = "0";
                }

                consultasPantallas.CommandText = "SELECT * FROM Hist_AV WHERE Date_AV='" + ahora + "' AND Shift='" + turno + "'";
                LblActualTurno.Text = consultasPantallas.Obtener("Produccion_L" + lineaInt);
                //LblActualTurno.Text = "123";

                consultasPantallas.CommandText = "SELECT SUM(TMH" + lineaInt + ") AS Tiempo_Muerto FROM Prod_AltoVolumen WHERE Hora >= " + limiteInferior + " and Hora <= " + limiteSuperior;
                //LblTiempoMuerto.Text = "TM: " + consultasPantallas.Obtener("Tiempo_Muerto") + " min";
            }
            catch (Exception)
            {
            }
        }




        private void TmrHorarios_Tick(object sender, EventArgs e)
        {
            //asignarHorarios_Y_Caritas();
        }




        void DefinirTurno_TiempoReal()
        {
            string fechaCortaActual = DateTime.Now.ToString("MM/dd/yyyy");
            DateTime ahorita = DateTime.Now;

            if (Convert.ToDateTime(fechaCortaActual + " 06:00:00") <= ahorita && ahorita <= Convert.ToDateTime(fechaCortaActual + " 15:30:59")) { turno = 1; limiteInferior = 6; }
            if (Convert.ToDateTime(fechaCortaActual + " 15:31:00") <= ahorita && ahorita <= Convert.ToDateTime(fechaCortaActual + " 23:59:59")) { turno = 2; limiteInferior = 15; }
            if (Convert.ToDateTime(fechaCortaActual + " 00:31:00") <= ahorita && ahorita <= Convert.ToDateTime(fechaCortaActual + " 05:59:59")) { turno = 3; limiteInferior = 0; }
            if (ahorita <= Convert.ToDateTime(fechaCortaActual + " 00:30:59")) { turno = 2; limiteInferior = 0; }
        }




        void leerNumeroLinea()
        {
            try
            {
                //int counter = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(@"c:\linea\linea.txt");

                //while ((lineaString = file.ReadLine()) != null)
                //{
                //    //if (counter == 0)
                //    //{
                //    if (lineaString.Length == 7)
                //    {
                //        lineaInt = Convert.ToInt32(lineaString.Substring(lineaString.Length - 2, 2));
                //    }
                //    else
                //    {
                //        lineaInt = Convert.ToInt32(lineaString.Substring(lineaString.Length - 1, 1));
                //    }
                //    //}
                //}

                lineaString = file.ReadToEnd();
                if (lineaString.Length == 7)
                {
                    lineaInt = Convert.ToInt32(lineaString.Substring(lineaString.Length - 2, 2));
                }
                else
                {
                    lineaInt = Convert.ToInt32(lineaString.Substring(lineaString.Length - 1, 1));
                }
                file.Close();
            }
            catch (Exception)
            {
            }
        }




        void asignarHorarios_Y_Caritas()
        {
            try
            {
                int horaActual = DateTime.Now.Hour;
                string[] obtenerResultados = new string[2];

                if (turno == 1)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        Label lbl = this.Controls.Find("lblHora" + i, true).FirstOrDefault() as Label;
                        lbl.Text = (i + 5) + ":00";

                        if ((i + 5) <= horaActual)
                        {
                            consultasPantallas.CommandText = "SELECT PRA" + lineaInt + " AS Produccion_Actual, PRM" + lineaInt + " as Produccion_Meta FROM Prod_AltoVolumen WHERE Hora = " + (i + 5);
                            obtenerResultados = consultasPantallas.Obtener2campos("Produccion_Actual", "Produccion_Meta");
                            //lbl.Text = obtenerResultados[0] + " : " + obtenerResultados[1];
                            PictureBox picture = this.Controls.Find("PictureBox" + i, true).FirstOrDefault() as PictureBox;

                            if (Convert.ToInt32(obtenerResultados[0]) >= Convert.ToInt32(obtenerResultados[1]))
                            {
                                picture.Image = Pantallas.Properties.Resources.verde;
                            }
                            else
                            {
                                picture.Image = Pantallas.Properties.Resources.Rojo;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        Label lbl = this.Controls.Find("lblHora" + i, true).FirstOrDefault() as Label;
                        lbl.Text = (i + 14) + ":00";


                        if ((i + 14) <= horaActual)
                        {
                            consultasPantallas.CommandText = "SELECT PRA" + lineaInt + " AS Produccion_Actual, PRM" + lineaInt + " as Produccion_Meta FROM Prod_AltoVolumen WHERE Hora = " + (i + 14);
                            obtenerResultados = consultasPantallas.Obtener2campos("Produccion_Actual", "Produccion_Meta");
                            PictureBox picture = this.Controls.Find("PictureBox" + i, true).FirstOrDefault() as PictureBox;

                            if (Convert.ToInt32(obtenerResultados[0]) >= Convert.ToInt32(obtenerResultados[1]))
                            {
                                picture.Image = Pantallas.Properties.Resources.verde;
                            }
                            else
                            {
                                picture.Image = Pantallas.Properties.Resources.Rojo;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }




        public string ToBinary(decimal n)
        {
            if (n < 2) return n.ToString();

            var divisor = n / 2;
            var remainder = n % 2;

            return ToBinary(divisor) + remainder;
        }



        public string ConvertirBinario(int numero)
        {
            numeroBinario = "";
            int bin;
            string sal = "";

            while (numero > 0)
            {

                bin = numero % 2;
                numero = numero / 2;
                Convert.ToString(bin);
                sal = bin + sal;

            }
            numeroBinario = sal;
            return numeroBinario;
        }
    }
}
