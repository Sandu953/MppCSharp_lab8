using lab8.Domain;

namespace client
{
    partial class RezervareView
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
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            obiectivTuristicDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numeTransportDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            oraPlecareDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            pretDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nrLocuriDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            locuriLibereDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            excursieBindingSource = new BindingSource(components);
            textBoxNume = new TextBox();
            textBoxTelefon = new TextBox();
            textBoxLoc = new TextBox();
            btnRezerva = new Button();
            textBox4 = new TextBox();
            label1 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            btnCauta = new Button();
            dataGridView2 = new DataGridView();
            obiectivTuristicDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            numeTransportDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            oraPlecareDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            pretDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            nrLocuriDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            locuriLibereDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            btnLogout = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)excursieBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeight = 34;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { obiectivTuristicDataGridViewTextBoxColumn, numeTransportDataGridViewTextBoxColumn, oraPlecareDataGridViewTextBoxColumn, pretDataGridViewTextBoxColumn, nrLocuriDataGridViewTextBoxColumn, locuriLibereDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            dataGridView1.DataSource = excursieBindingSource;
            dataGridView1.Location = new Point(29, 60);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            dataGridView1.Size = new Size(586, 537);
            dataGridView1.TabIndex = 0;
            // 
            // obiectivTuristicDataGridViewTextBoxColumn
            // 
            obiectivTuristicDataGridViewTextBoxColumn.DataPropertyName = "ObiectivTuristic";
            obiectivTuristicDataGridViewTextBoxColumn.HeaderText = "ObiectivTuristic";
            obiectivTuristicDataGridViewTextBoxColumn.MinimumWidth = 8;
            obiectivTuristicDataGridViewTextBoxColumn.Name = "obiectivTuristicDataGridViewTextBoxColumn";
            obiectivTuristicDataGridViewTextBoxColumn.Width = 150;
            // 
            // numeTransportDataGridViewTextBoxColumn
            // 
            numeTransportDataGridViewTextBoxColumn.DataPropertyName = "NumeTransport";
            numeTransportDataGridViewTextBoxColumn.HeaderText = "NumeTransport";
            numeTransportDataGridViewTextBoxColumn.MinimumWidth = 8;
            numeTransportDataGridViewTextBoxColumn.Name = "numeTransportDataGridViewTextBoxColumn";
            numeTransportDataGridViewTextBoxColumn.Width = 150;
            // 
            // oraPlecareDataGridViewTextBoxColumn
            // 
            oraPlecareDataGridViewTextBoxColumn.DataPropertyName = "OraPlecare";
            oraPlecareDataGridViewTextBoxColumn.HeaderText = "OraPlecare";
            oraPlecareDataGridViewTextBoxColumn.MinimumWidth = 8;
            oraPlecareDataGridViewTextBoxColumn.Name = "oraPlecareDataGridViewTextBoxColumn";
            oraPlecareDataGridViewTextBoxColumn.Width = 150;
            // 
            // pretDataGridViewTextBoxColumn
            // 
            pretDataGridViewTextBoxColumn.DataPropertyName = "Pret";
            pretDataGridViewTextBoxColumn.HeaderText = "Pret";
            pretDataGridViewTextBoxColumn.MinimumWidth = 8;
            pretDataGridViewTextBoxColumn.Name = "pretDataGridViewTextBoxColumn";
            pretDataGridViewTextBoxColumn.Width = 150;
            // 
            // nrLocuriDataGridViewTextBoxColumn
            // 
            nrLocuriDataGridViewTextBoxColumn.DataPropertyName = "NrLocuri";
            nrLocuriDataGridViewTextBoxColumn.HeaderText = "NrLocuri";
            nrLocuriDataGridViewTextBoxColumn.MinimumWidth = 8;
            nrLocuriDataGridViewTextBoxColumn.Name = "nrLocuriDataGridViewTextBoxColumn";
            nrLocuriDataGridViewTextBoxColumn.Width = 150;
            // 
            // locuriLibereDataGridViewTextBoxColumn
            // 
            locuriLibereDataGridViewTextBoxColumn.DataPropertyName = "LocuriLibere";
            locuriLibereDataGridViewTextBoxColumn.HeaderText = "LocuriLibere";
            locuriLibereDataGridViewTextBoxColumn.MinimumWidth = 8;
            locuriLibereDataGridViewTextBoxColumn.Name = "locuriLibereDataGridViewTextBoxColumn";
            locuriLibereDataGridViewTextBoxColumn.Width = 150;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 8;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.Width = 150;
            // 
            // excursieBindingSource
            // 
            excursieBindingSource.DataSource = typeof(Excursie);
            // 
            // textBoxNume
            // 
            textBoxNume.Location = new Point(665, 107);
            textBoxNume.Name = "textBoxNume";
            textBoxNume.PlaceholderText = "Nume";
            textBoxNume.Size = new Size(186, 31);
            textBoxNume.TabIndex = 1;
            // 
            // textBoxTelefon
            // 
            textBoxTelefon.Location = new Point(665, 161);
            textBoxTelefon.Name = "textBoxTelefon";
            textBoxTelefon.PlaceholderText = "Telefon";
            textBoxTelefon.Size = new Size(186, 31);
            textBoxTelefon.TabIndex = 2;
            // 
            // textBoxLoc
            // 
            textBoxLoc.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxLoc.Location = new Point(665, 216);
            textBoxLoc.Name = "textBoxLoc";
            textBoxLoc.PlaceholderText = "Locuri";
            textBoxLoc.Size = new Size(186, 31);
            textBoxLoc.TabIndex = 3;
            // 
            // btnRezerva
            // 
            btnRezerva.Location = new Point(697, 264);
            btnRezerva.Name = "btnRezerva";
            btnRezerva.Size = new Size(112, 34);
            btnRezerva.TabIndex = 4;
            btnRezerva.Text = "Rezerva";
            btnRezerva.UseVisualStyleBackColor = true;
            btnRezerva.Click += btnRezerva_Click;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(665, 346);
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Obiectiv";
            textBox4.Size = new Size(186, 31);
            textBox4.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(665, 391);
            label1.Name = "label1";
            label1.Size = new Size(111, 25);
            label1.TabIndex = 6;
            label1.Text = "Interval orar:";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(665, 432);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(75, 33);
            comboBox1.TabIndex = 7;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(776, 432);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(75, 33);
            comboBox2.TabIndex = 8;
            // 
            // btnCauta
            // 
            btnCauta.Location = new Point(697, 482);
            btnCauta.Name = "btnCauta";
            btnCauta.Size = new Size(112, 34);
            btnCauta.TabIndex = 9;
            btnCauta.Text = "Cauta";
            btnCauta.UseVisualStyleBackColor = true;
            btnCauta.Click += btnCauta_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { obiectivTuristicDataGridViewTextBoxColumn1, numeTransportDataGridViewTextBoxColumn1, oraPlecareDataGridViewTextBoxColumn1, pretDataGridViewTextBoxColumn1, nrLocuriDataGridViewTextBoxColumn1, locuriLibereDataGridViewTextBoxColumn1, idDataGridViewTextBoxColumn1 });
            dataGridView2.DataSource = excursieBindingSource;
            dataGridView2.Location = new Point(898, 60);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView2.Size = new Size(936, 537);
            dataGridView2.TabIndex = 10;
            // 
            // obiectivTuristicDataGridViewTextBoxColumn1
            // 
            obiectivTuristicDataGridViewTextBoxColumn1.DataPropertyName = "ObiectivTuristic";
            obiectivTuristicDataGridViewTextBoxColumn1.HeaderText = "ObiectivTuristic";
            obiectivTuristicDataGridViewTextBoxColumn1.MinimumWidth = 8;
            obiectivTuristicDataGridViewTextBoxColumn1.Name = "obiectivTuristicDataGridViewTextBoxColumn1";
            obiectivTuristicDataGridViewTextBoxColumn1.Width = 150;
            // 
            // numeTransportDataGridViewTextBoxColumn1
            // 
            numeTransportDataGridViewTextBoxColumn1.DataPropertyName = "NumeTransport";
            numeTransportDataGridViewTextBoxColumn1.HeaderText = "NumeTransport";
            numeTransportDataGridViewTextBoxColumn1.MinimumWidth = 8;
            numeTransportDataGridViewTextBoxColumn1.Name = "numeTransportDataGridViewTextBoxColumn1";
            numeTransportDataGridViewTextBoxColumn1.Width = 150;
            // 
            // oraPlecareDataGridViewTextBoxColumn1
            // 
            oraPlecareDataGridViewTextBoxColumn1.DataPropertyName = "OraPlecare";
            oraPlecareDataGridViewTextBoxColumn1.HeaderText = "OraPlecare";
            oraPlecareDataGridViewTextBoxColumn1.MinimumWidth = 8;
            oraPlecareDataGridViewTextBoxColumn1.Name = "oraPlecareDataGridViewTextBoxColumn1";
            oraPlecareDataGridViewTextBoxColumn1.Width = 150;
            // 
            // pretDataGridViewTextBoxColumn1
            // 
            pretDataGridViewTextBoxColumn1.DataPropertyName = "Pret";
            pretDataGridViewTextBoxColumn1.HeaderText = "Pret";
            pretDataGridViewTextBoxColumn1.MinimumWidth = 8;
            pretDataGridViewTextBoxColumn1.Name = "pretDataGridViewTextBoxColumn1";
            pretDataGridViewTextBoxColumn1.Width = 150;
            // 
            // nrLocuriDataGridViewTextBoxColumn1
            // 
            nrLocuriDataGridViewTextBoxColumn1.DataPropertyName = "NrLocuri";
            nrLocuriDataGridViewTextBoxColumn1.HeaderText = "NrLocuri";
            nrLocuriDataGridViewTextBoxColumn1.MinimumWidth = 8;
            nrLocuriDataGridViewTextBoxColumn1.Name = "nrLocuriDataGridViewTextBoxColumn1";
            nrLocuriDataGridViewTextBoxColumn1.Width = 150;
            // 
            // locuriLibereDataGridViewTextBoxColumn1
            // 
            locuriLibereDataGridViewTextBoxColumn1.DataPropertyName = "LocuriLibere";
            locuriLibereDataGridViewTextBoxColumn1.HeaderText = "LocuriLibere";
            locuriLibereDataGridViewTextBoxColumn1.MinimumWidth = 8;
            locuriLibereDataGridViewTextBoxColumn1.Name = "locuriLibereDataGridViewTextBoxColumn1";
            locuriLibereDataGridViewTextBoxColumn1.Width = 150;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn1.HeaderText = "Id";
            idDataGridViewTextBoxColumn1.MinimumWidth = 8;
            idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            idDataGridViewTextBoxColumn1.Width = 150;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(29, 12);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(112, 34);
            btnLogout.TabIndex = 11;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // RezervareView
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1846, 619);
            Controls.Add(btnLogout);
            Controls.Add(dataGridView2);
            Controls.Add(btnCauta);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(textBox4);
            Controls.Add(btnRezerva);
            Controls.Add(textBoxLoc);
            Controls.Add(textBoxTelefon);
            Controls.Add(textBoxNume);
            Controls.Add(dataGridView1);
            Name = "RezervareView";
            Text = "RezervareView";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)excursieBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private BindingSource excursieBindingSource;
        private DataGridViewTextBoxColumn obiectivTuristicDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numeTransportDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn oraPlecareDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn pretDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nrLocuriDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn locuriLibereDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private TextBox textBoxNume;
        private TextBox textBoxTelefon;
        private TextBox textBoxLoc;
        private Button btnRezerva;
        private TextBox textBox4;
        private Label label1;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private Button btnCauta;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn obiectivTuristicDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn numeTransportDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn oraPlecareDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn pretDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn nrLocuriDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn locuriLibereDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private Button btnLogout;
    }
}