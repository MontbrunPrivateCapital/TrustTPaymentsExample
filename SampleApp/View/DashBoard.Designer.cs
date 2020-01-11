namespace SampleApp
{
    partial class DashBoard
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPayment = new System.Windows.Forms.Button();
            this.labelFees = new System.Windows.Forms.Label();
            this.btnAddCard = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(12, 24);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(93, 33);
            this.btnPayment.TabIndex = 0;
            this.btnPayment.Text = "Payment";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // labelFees
            // 
            this.labelFees.AutoSize = true;
            this.labelFees.Location = new System.Drawing.Point(12, 167);
            this.labelFees.Name = "labelFees";
            this.labelFees.Size = new System.Drawing.Size(52, 13);
            this.labelFees.TabIndex = 1;
            this.labelFees.Text = "labelFees";
            // 
            // btnAddCard
            // 
            this.btnAddCard.Location = new System.Drawing.Point(135, 24);
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(93, 33);
            this.btnAddCard.TabIndex = 2;
            this.btnAddCard.Text = "Add Card";
            this.btnAddCard.UseVisualStyleBackColor = true;
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // btnMail
            // 
            this.btnMail.Location = new System.Drawing.Point(64, 82);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(93, 37);
            this.btnMail.TabIndex = 3;
            this.btnMail.Text = "Verification Email";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 242);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnAddCard);
            this.Controls.Add(this.labelFees);
            this.Controls.Add(this.btnPayment);
            this.MaximizeBox = false;
            this.Name = "DashBoard";
            this.Text = "Trustt POS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Label labelFees;
        private System.Windows.Forms.Button btnAddCard;
        private System.Windows.Forms.Button btnMail;
    }
}

