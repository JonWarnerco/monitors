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
    public partial class FrmAlarmas : Form
    {
        clsConsultas consultasAlarmas = new clsConsultas();
        static string lineaString = "", codigoAlarma = "", descripcionAlarma = "";



        public FrmAlarmas()
        {
            InitializeComponent();
        }



        private void Alarmas_Load(object sender, EventArgs e)
        {
            try
            {
                leerNumeroLinea();
                consultaAlarmas();
                this.BackColor = Color.Yellow;
                tmrAmarillo.Start();
            }
            catch (Exception)
            {
            }
            
        }
        
        

        private void tmrAmarillo_Tick(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.Black;
                tmrAmarillo.Stop();
                leerNumeroLinea();
                consultaAlarmas();

                if (FrmPrincipal.diferenciaWO > 0)
                {
                    if (codigoAlarma == "0")
                    {
                        FrmPrincipal.alarmaIniciado = false;
                        this.Close();
                    }
                }

                tmrNegro.Start();
            }
            catch (Exception)
            {
            }
            
        }



        private void tmrNegro_Tick(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.Yellow;
                tmrNegro.Stop();
                leerNumeroLinea();
                consultaAlarmas();

                if (FrmPrincipal.diferenciaWO > 0)
                {
                    if (codigoAlarma == "0")
                    {
                        FrmPrincipal.alarmaIniciado = false;
                        this.Close();
                    }
                }

                tmrAmarillo.Start();
            }
            catch (Exception)
            {
            }

        }

        private void consultaAlarmas()
        {
            try
            {
                if (FrmPrincipal.diferenciaWO > 0)
                {
                    consultasAlarmas.CommandText = "SELECT * FROM dbo.Alarmas_Missing_RivetsVW WHERE Area = 'AV' AND LINEA='" + lineaString + "'";
                    descripcionAlarma = consultasAlarmas.Obtener("Descripcion");
                    codigoAlarma = consultasAlarmas.Obtener("Alarma");
                    lblMensaje.Text = descripcionAlarma;
                }
                else
                {
                    lblMensaje.Text = "Workorder cerrada o cancelada, favor de cargar una nueva";
                }
            }
            catch (Exception)
            {
            }
        }


        void leerNumeroLinea()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(@"c:\linea\linea.txt");
                lineaString = file.ReadToEnd();
                file.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
