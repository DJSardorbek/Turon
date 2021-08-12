
namespace JayhunOmbor
{
    partial class frmEditRecieve
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSotishSom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKurs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new FontAwesome.Sharp.IconButton();
            this.btnEdit = new FontAwesome.Sharp.IconButton();
            this.txtSotishDollar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDollar = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 16F);
            this.label1.Location = new System.Drawing.Point(36, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 26);
            this.label1.TabIndex = 10;
            this.label1.Text = "Махсулот :";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtName.Location = new System.Drawing.Point(160, 79);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(529, 32);
            this.txtName.TabIndex = 9;
            // 
            // txtSom
            // 
            this.txtSom.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtSom.Location = new System.Drawing.Point(114, 134);
            this.txtSom.Name = "txtSom";
            this.txtSom.Size = new System.Drawing.Size(575, 32);
            this.txtSom.TabIndex = 1;
            this.txtSom.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 16F);
            this.label2.Location = new System.Drawing.Point(36, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 26);
            this.label2.TabIndex = 11;
            this.label2.Text = "Сўм :";
            // 
            // txtSotishSom
            // 
            this.txtSotishSom.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtSotishSom.Location = new System.Drawing.Point(178, 194);
            this.txtSotishSom.Name = "txtSotishSom";
            this.txtSotishSom.Size = new System.Drawing.Size(511, 32);
            this.txtSotishSom.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 16F);
            this.label3.Location = new System.Drawing.Point(36, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 26);
            this.label3.TabIndex = 12;
            this.label3.Text = "Сотиш_сўм :";
            // 
            // txtKurs
            // 
            this.txtKurs.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtKurs.Location = new System.Drawing.Point(141, 362);
            this.txtKurs.Name = "txtKurs";
            this.txtKurs.Size = new System.Drawing.Size(548, 32);
            this.txtKurs.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 16F);
            this.label4.Location = new System.Drawing.Point(36, 365);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 26);
            this.label4.TabIndex = 15;
            this.label4.Text = "Курс :";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtQuantity.Location = new System.Drawing.Point(160, 419);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(529, 32);
            this.txtQuantity.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 16F);
            this.label5.Location = new System.Drawing.Point(36, 422);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 26);
            this.label5.TabIndex = 16;
            this.label5.Text = "Микдори :";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.IconChar = FontAwesome.Sharp.IconChar.CalendarTimes;
            this.btnCancel.IconColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancel.IconSize = 26;
            this.btnCancel.Location = new System.Drawing.Point(570, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Оркага";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(68)))));
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnEdit.IconChar = FontAwesome.Sharp.IconChar.ObjectGroup;
            this.btnEdit.IconColor = System.Drawing.Color.Gainsboro;
            this.btnEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEdit.IconSize = 26;
            this.btnEdit.Location = new System.Drawing.Point(413, 475);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(151, 36);
            this.btnEdit.TabIndex = 7;
            this.btnEdit.Text = "Ўзгартириш";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtSotishDollar
            // 
            this.txtSotishDollar.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtSotishDollar.Location = new System.Drawing.Point(213, 305);
            this.txtSotishDollar.Name = "txtSotishDollar";
            this.txtSotishDollar.Size = new System.Drawing.Size(476, 32);
            this.txtSotishDollar.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 16F);
            this.label6.Location = new System.Drawing.Point(36, 308);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 26);
            this.label6.TabIndex = 14;
            this.label6.Text = "Сотиш_доллар :";
            // 
            // txtDollar
            // 
            this.txtDollar.Font = new System.Drawing.Font("Consolas", 16F);
            this.txtDollar.Location = new System.Drawing.Point(141, 248);
            this.txtDollar.Name = "txtDollar";
            this.txtDollar.Size = new System.Drawing.Size(548, 32);
            this.txtDollar.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 16F);
            this.label7.Location = new System.Drawing.Point(36, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 26);
            this.label7.TabIndex = 13;
            this.label7.Text = "Доллар :";
            // 
            // frmEditRecieve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 530);
            this.Controls.Add(this.txtSotishDollar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDollar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtKurs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSotishSom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmEditRecieve";
            this.Padding = new System.Windows.Forms.Padding(33, 102, 33, 34);
            this.Text = "Тахрирлаш ойнаси";
            this.Load += new System.EventHandler(this.frmEditRecieve_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSotishSom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKurs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label5;
        private FontAwesome.Sharp.IconButton btnCancel;
        private FontAwesome.Sharp.IconButton btnEdit;
        private System.Windows.Forms.TextBox txtSotishDollar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDollar;
        private System.Windows.Forms.Label label7;
    }
}