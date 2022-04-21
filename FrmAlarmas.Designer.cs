namespace Pantallas
{
    partial class FrmAlarmas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.tmrAmarillo = new System.Windows.Forms.Timer(this.components);
            this.tmrNegro = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblMensaje
            // 
            this.lblMensaje.AllowDrop = true;
            this.lblMensaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 78F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(0, 0);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(1421, 744);
            this.lblMensaje.TabIndex = 0;
            this.lblMensaje.Text = "Workorder cerrada o cancelada, favor de cargar una nueva";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrAmarillo
            // 
            this.tmrAmarillo.Interval = 2000;
            this.tmrAmarillo.Tick += new System.EventHandler(this.tmrAmarillo_Tick);
            // 
            // tmrNegro
            // 
            this.tmrNegro.Interval = 1000;
            this.tmrNegro.Tick += new System.EventHandler(this.tmrNegro_Tick);
            // 
            // FrmAlarmas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 744);
            this.Controls.Add(this.lblMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmAlarmas";
            this.Text = "Alarmas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Alarmas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Timer tmrAmarillo;
        private System.Windows.Forms.Timer tmrNegro;
    }
}