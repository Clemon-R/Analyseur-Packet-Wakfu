namespace Analyser_Packet_Wakfu
{
    partial class main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.logs = new System.Windows.Forms.ListBox();
            this.len = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.hd = new System.Windows.Forms.GroupBox();
            this.list_pck = new System.Windows.Forms.ListBox();
            this.hd.SuspendLayout();
            this.SuspendLayout();
            // 
            // logs
            // 
            this.logs.FormattingEnabled = true;
            this.logs.Location = new System.Drawing.Point(12, 12);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(514, 303);
            this.logs.TabIndex = 0;
            this.logs.SelectedValueChanged += new System.EventHandler(this.select_packet);
            // 
            // len
            // 
            this.len.AutoSize = true;
            this.len.Location = new System.Drawing.Point(6, 20);
            this.len.Name = "len";
            this.len.Size = new System.Drawing.Size(35, 13);
            this.len.TabIndex = 1;
            this.len.Text = "label1";
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Location = new System.Drawing.Point(6, 38);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(35, 13);
            this.id.TabIndex = 2;
            this.id.Text = "label2";
            // 
            // hd
            // 
            this.hd.Controls.Add(this.list_pck);
            this.hd.Controls.Add(this.len);
            this.hd.Controls.Add(this.id);
            this.hd.Location = new System.Drawing.Point(532, 8);
            this.hd.Name = "hd";
            this.hd.Size = new System.Drawing.Size(221, 307);
            this.hd.TabIndex = 3;
            this.hd.TabStop = false;
            this.hd.Text = "Header";
            this.hd.Visible = false;
            // 
            // list_pck
            // 
            this.list_pck.FormattingEnabled = true;
            this.list_pck.Location = new System.Drawing.Point(13, 63);
            this.list_pck.Name = "list_pck";
            this.list_pck.Size = new System.Drawing.Size(195, 225);
            this.list_pck.TabIndex = 3;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 331);
            this.Controls.Add(this.hd);
            this.Controls.Add(this.logs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analyser Packet";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form_close);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.hd.ResumeLayout(false);
            this.hd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox logs;
        private System.Windows.Forms.Label len;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.GroupBox hd;
        private System.Windows.Forms.ListBox list_pck;
    }
}

