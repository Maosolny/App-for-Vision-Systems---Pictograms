namespace SW_PROJEKT
{
    partial class Form1
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_From_File_PB1 = new System.Windows.Forms.Button();
            this.button_Browse_Files_PB1 = new System.Windows.Forms.Button();
            this.textBox_Image_Path_PB1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_ThreshDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_Mono_Thresh = new System.Windows.Forms.CheckBox();
            this.numericUpDown_Contrast = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_Contrast = new System.Windows.Forms.CheckBox();
            this.numericUpDown_ThreshUp = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Brightness = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_Invert = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown_Brightness = new System.Windows.Forms.NumericUpDown();
            this.checkBox_Skos = new System.Windows.Forms.CheckBox();
            this.label_Liczba_obiektow = new System.Windows.Forms.Label();
            this.button_Pokaz_obiekt = new System.Windows.Forms.Button();
            this.numericUpDown_Numer_obiektu = new System.Windows.Forms.NumericUpDown();
            this.button_Czysc = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.button_Analizuj = new System.Windows.Forms.Button();
            this.listView_Dane = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_Obrysuj = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.buttonImgRead = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.listView_Komendy = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDown_Moving_Average = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown_Ray_count = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ThreshDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Contrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ThreshUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Brightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Numer_obiektu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Moving_Average)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ray_count)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.Location = new System.Drawing.Point(496, 58);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 240);
            this.pictureBox2.TabIndex = 50;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(10, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button_From_File_PB1
            // 
            this.button_From_File_PB1.Location = new System.Drawing.Point(10, 28);
            this.button_From_File_PB1.Name = "button_From_File_PB1";
            this.button_From_File_PB1.Size = new System.Drawing.Size(70, 23);
            this.button_From_File_PB1.TabIndex = 51;
            this.button_From_File_PB1.Text = "Z pliku";
            this.button_From_File_PB1.UseVisualStyleBackColor = true;
            this.button_From_File_PB1.Click += new System.EventHandler(this.button_From_File_PB1_Click);
            // 
            // button_Browse_Files_PB1
            // 
            this.button_Browse_Files_PB1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_Browse_Files_PB1.Location = new System.Drawing.Point(302, 3);
            this.button_Browse_Files_PB1.Name = "button_Browse_Files_PB1";
            this.button_Browse_Files_PB1.Size = new System.Drawing.Size(28, 21);
            this.button_Browse_Files_PB1.TabIndex = 54;
            this.button_Browse_Files_PB1.Text = "...";
            this.button_Browse_Files_PB1.UseVisualStyleBackColor = true;
            this.button_Browse_Files_PB1.Click += new System.EventHandler(this.button_Browse_Files_PB1_Click);
            // 
            // textBox_Image_Path_PB1
            // 
            this.textBox_Image_Path_PB1.Location = new System.Drawing.Point(53, 5);
            this.textBox_Image_Path_PB1.Name = "textBox_Image_Path_PB1";
            this.textBox_Image_Path_PB1.ReadOnly = true;
            this.textBox_Image_Path_PB1.Size = new System.Drawing.Size(247, 20);
            this.textBox_Image_Path_PB1.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Ścieżka:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDown_ThreshDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBox_Mono_Thresh);
            this.groupBox1.Controls.Add(this.numericUpDown_Contrast);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox_Contrast);
            this.groupBox1.Controls.Add(this.numericUpDown_ThreshUp);
            this.groupBox1.Controls.Add(this.checkBox_Brightness);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.checkBox_Invert);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDown_Brightness);
            this.groupBox1.Location = new System.Drawing.Point(346, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 149);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ustawienia Obrazu";
            // 
            // numericUpDown_ThreshDown
            // 
            this.numericUpDown_ThreshDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_ThreshDown.Location = new System.Drawing.Point(42, 103);
            this.numericUpDown_ThreshDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_ThreshDown.Name = "numericUpDown_ThreshDown";
            this.numericUpDown_ThreshDown.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_ThreshDown.TabIndex = 105;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(10, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 2);
            this.label3.TabIndex = 104;
            // 
            // checkBox_Mono_Thresh
            // 
            this.checkBox_Mono_Thresh.AutoSize = true;
            this.checkBox_Mono_Thresh.Location = new System.Drawing.Point(9, 124);
            this.checkBox_Mono_Thresh.Name = "checkBox_Mono_Thresh";
            this.checkBox_Mono_Thresh.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_Mono_Thresh.Size = new System.Drawing.Size(53, 17);
            this.checkBox_Mono_Thresh.TabIndex = 92;
            this.checkBox_Mono_Thresh.Text = "Mono";
            this.checkBox_Mono_Thresh.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_Contrast
            // 
            this.numericUpDown_Contrast.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Contrast.Location = new System.Drawing.Point(90, 18);
            this.numericUpDown_Contrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown_Contrast.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Contrast.Name = "numericUpDown_Contrast";
            this.numericUpDown_Contrast.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_Contrast.TabIndex = 103;
            this.numericUpDown_Contrast.ValueChanged += new System.EventHandler(this.numericUpDown_Contrast_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "Próg";
            // 
            // checkBox_Contrast
            // 
            this.checkBox_Contrast.AutoSize = true;
            this.checkBox_Contrast.Location = new System.Drawing.Point(10, 19);
            this.checkBox_Contrast.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_Contrast.Name = "checkBox_Contrast";
            this.checkBox_Contrast.Size = new System.Drawing.Size(65, 17);
            this.checkBox_Contrast.TabIndex = 102;
            this.checkBox_Contrast.Text = "Kontrast";
            this.checkBox_Contrast.UseVisualStyleBackColor = true;
            this.checkBox_Contrast.Click += new System.EventHandler(this.LUT_CheckedChanged);
            // 
            // numericUpDown_ThreshUp
            // 
            this.numericUpDown_ThreshUp.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_ThreshUp.Location = new System.Drawing.Point(88, 103);
            this.numericUpDown_ThreshUp.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_ThreshUp.Name = "numericUpDown_ThreshUp";
            this.numericUpDown_ThreshUp.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_ThreshUp.TabIndex = 92;
            // 
            // checkBox_Brightness
            // 
            this.checkBox_Brightness.AutoSize = true;
            this.checkBox_Brightness.Location = new System.Drawing.Point(10, 41);
            this.checkBox_Brightness.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_Brightness.Name = "checkBox_Brightness";
            this.checkBox_Brightness.Size = new System.Drawing.Size(82, 17);
            this.checkBox_Brightness.TabIndex = 101;
            this.checkBox_Brightness.Text = "Jaśność +/-";
            this.checkBox_Brightness.UseVisualStyleBackColor = true;
            this.checkBox_Brightness.Click += new System.EventHandler(this.LUT_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(118, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 67;
            // 
            // checkBox_Invert
            // 
            this.checkBox_Invert.AutoSize = true;
            this.checkBox_Invert.Location = new System.Drawing.Point(10, 63);
            this.checkBox_Invert.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox_Invert.Name = "checkBox_Invert";
            this.checkBox_Invert.Size = new System.Drawing.Size(68, 17);
            this.checkBox_Invert.TabIndex = 100;
            this.checkBox_Invert.Text = "Negatyw";
            this.checkBox_Invert.UseVisualStyleBackColor = true;
            this.checkBox_Invert.Click += new System.EventHandler(this.LUT_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(118, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 66;
            // 
            // numericUpDown_Brightness
            // 
            this.numericUpDown_Brightness.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_Brightness.Location = new System.Drawing.Point(90, 41);
            this.numericUpDown_Brightness.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.numericUpDown_Brightness.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_Brightness.Name = "numericUpDown_Brightness";
            this.numericUpDown_Brightness.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_Brightness.TabIndex = 92;
            this.numericUpDown_Brightness.ValueChanged += new System.EventHandler(this.numericUpDown_Brightness_ValueChanged);
            // 
            // checkBox_Skos
            // 
            this.checkBox_Skos.AutoSize = true;
            this.checkBox_Skos.Location = new System.Drawing.Point(12, 18);
            this.checkBox_Skos.Name = "checkBox_Skos";
            this.checkBox_Skos.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBox_Skos.Size = new System.Drawing.Size(50, 17);
            this.checkBox_Skos.TabIndex = 69;
            this.checkBox_Skos.Text = "Skos";
            this.checkBox_Skos.UseVisualStyleBackColor = true;
            this.checkBox_Skos.CheckedChanged += new System.EventHandler(this.checkBox_Skos_CheckedChanged);
            // 
            // label_Liczba_obiektow
            // 
            this.label_Liczba_obiektow.AutoSize = true;
            this.label_Liczba_obiektow.Location = new System.Drawing.Point(157, 316);
            this.label_Liczba_obiektow.Name = "label_Liczba_obiektow";
            this.label_Liczba_obiektow.Size = new System.Drawing.Size(87, 13);
            this.label_Liczba_obiektow.TabIndex = 77;
            this.label_Liczba_obiektow.Text = "Liczba obiektów:";
            // 
            // button_Pokaz_obiekt
            // 
            this.button_Pokaz_obiekt.Location = new System.Drawing.Point(10, 311);
            this.button_Pokaz_obiekt.Name = "button_Pokaz_obiekt";
            this.button_Pokaz_obiekt.Size = new System.Drawing.Size(100, 23);
            this.button_Pokaz_obiekt.TabIndex = 79;
            this.button_Pokaz_obiekt.Text = "Pokaż obiekt nr:";
            this.button_Pokaz_obiekt.UseVisualStyleBackColor = true;
            this.button_Pokaz_obiekt.Click += new System.EventHandler(this.button_Pokaz_obiekt_Click);
            // 
            // numericUpDown_Numer_obiektu
            // 
            this.numericUpDown_Numer_obiektu.Location = new System.Drawing.Point(116, 314);
            this.numericUpDown_Numer_obiektu.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Numer_obiektu.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Numer_obiektu.Name = "numericUpDown_Numer_obiektu";
            this.numericUpDown_Numer_obiektu.Size = new System.Drawing.Size(35, 20);
            this.numericUpDown_Numer_obiektu.TabIndex = 80;
            this.numericUpDown_Numer_obiektu.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_Czysc
            // 
            this.button_Czysc.Location = new System.Drawing.Point(370, 264);
            this.button_Czysc.Name = "button_Czysc";
            this.button_Czysc.Size = new System.Drawing.Size(84, 33);
            this.button_Czysc.TabIndex = 81;
            this.button_Czysc.Text = "Czyść";
            this.button_Czysc.UseVisualStyleBackColor = true;
            this.button_Czysc.Click += new System.EventHandler(this.button_Czysc_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Location = new System.Drawing.Point(10, 348);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(320, 240);
            this.pictureBox3.TabIndex = 82;
            this.pictureBox3.TabStop = false;
            // 
            // button_Analizuj
            // 
            this.button_Analizuj.Location = new System.Drawing.Point(370, 475);
            this.button_Analizuj.Name = "button_Analizuj";
            this.button_Analizuj.Size = new System.Drawing.Size(84, 36);
            this.button_Analizuj.TabIndex = 83;
            this.button_Analizuj.Text = "Analizuj";
            this.button_Analizuj.UseVisualStyleBackColor = true;
            this.button_Analizuj.Click += new System.EventHandler(this.button_Analizuj_Click);
            // 
            // listView_Dane
            // 
            this.listView_Dane.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView_Dane.GridLines = true;
            this.listView_Dane.HideSelection = false;
            this.listView_Dane.Location = new System.Drawing.Point(496, 601);
            this.listView_Dane.Name = "listView_Dane";
            this.listView_Dane.Size = new System.Drawing.Size(321, 160);
            this.listView_Dane.TabIndex = 84;
            this.listView_Dane.UseCompatibleStateImageBehavior = false;
            this.listView_Dane.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Wyniki analizy:";
            this.columnHeader1.Width = 409;
            // 
            // button_Obrysuj
            // 
            this.button_Obrysuj.Location = new System.Drawing.Point(370, 552);
            this.button_Obrysuj.Name = "button_Obrysuj";
            this.button_Obrysuj.Size = new System.Drawing.Size(84, 36);
            this.button_Obrysuj.TabIndex = 85;
            this.button_Obrysuj.Text = "Obrysuj";
            this.button_Obrysuj.UseVisualStyleBackColor = true;
            this.button_Obrysuj.Click += new System.EventHandler(this.button_Obrysuj_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Black;
            this.pictureBox4.Location = new System.Drawing.Point(496, 348);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(320, 240);
            this.pictureBox4.TabIndex = 86;
            this.pictureBox4.TabStop = false;
            // 
            // buttonImgRead
            // 
            this.buttonImgRead.Location = new System.Drawing.Point(370, 223);
            this.buttonImgRead.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonImgRead.Name = "buttonImgRead";
            this.buttonImgRead.Size = new System.Drawing.Size(84, 33);
            this.buttonImgRead.TabIndex = 88;
            this.buttonImgRead.Text = "Wczytaj Obraz";
            this.buttonImgRead.UseVisualStyleBackColor = true;
            this.buttonImgRead.Click += new System.EventHandler(this.buttonImgRead_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.Location = new System.Drawing.Point(93, 28);
            this.btnCamera.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(88, 23);
            this.btnCamera.TabIndex = 89;
            this.btnCamera.Text = "Kamera Start";
            this.btnCamera.UseVisualStyleBackColor = true;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // listView_Komendy
            // 
            this.listView_Komendy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_Komendy.GridLines = true;
            this.listView_Komendy.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView_Komendy.HideSelection = false;
            this.listView_Komendy.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView_Komendy.Location = new System.Drawing.Point(10, 601);
            this.listView_Komendy.Name = "listView_Komendy";
            this.listView_Komendy.Size = new System.Drawing.Size(321, 160);
            this.listView_Komendy.TabIndex = 90;
            this.listView_Komendy.UseCompatibleStateImageBehavior = false;
            this.listView_Komendy.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Komendy wysyłane do robota:";
            this.columnHeader2.Width = 417;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numericUpDown_Moving_Average);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericUpDown_Ray_count);
            this.groupBox2.Controls.Add(this.checkBox_Skos);
            this.groupBox2.Location = new System.Drawing.Point(346, 348);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(136, 102);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ustawienia Analizy";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 114;
            this.label7.Text = "śr. ruchoma";
            // 
            // numericUpDown_Moving_Average
            // 
            this.numericUpDown_Moving_Average.Location = new System.Drawing.Point(86, 62);
            this.numericUpDown_Moving_Average.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_Moving_Average.Name = "numericUpDown_Moving_Average";
            this.numericUpDown_Moving_Average.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown_Moving_Average.TabIndex = 112;
            this.numericUpDown_Moving_Average.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 113;
            this.label4.Text = "L. promieni";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 62);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 113;
            this.label8.Text = "Szerokość:";
            // 
            // numericUpDown_Ray_count
            // 
            this.numericUpDown_Ray_count.Location = new System.Drawing.Point(86, 37);
            this.numericUpDown_Ray_count.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDown_Ray_count.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_Ray_count.Name = "numericUpDown_Ray_count";
            this.numericUpDown_Ray_count.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown_Ray_count.TabIndex = 108;
            this.numericUpDown_Ray_count.Value = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.numericUpDown_Ray_count.ValueChanged += new System.EventHandler(this.numericUpDown_Ray_count_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 769);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listView_Komendy);
            this.Controls.Add(this.label_Liczba_obiektow);
            this.Controls.Add(this.btnCamera);
            this.Controls.Add(this.buttonImgRead);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.button_Obrysuj);
            this.Controls.Add(this.listView_Dane);
            this.Controls.Add(this.button_Analizuj);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.button_Czysc);
            this.Controls.Add(this.numericUpDown_Numer_obiektu);
            this.Controls.Add(this.button_Pokaz_obiekt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Browse_Files_PB1);
            this.Controls.Add(this.textBox_Image_Path_PB1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_From_File_PB1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SW PROJEKT PIKTOGRAMY";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ThreshDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Contrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ThreshUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Brightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Numer_obiektu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Moving_Average)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Ray_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button_From_File_PB1;
        private System.Windows.Forms.Button button_Browse_Files_PB1;
        private System.Windows.Forms.TextBox textBox_Image_Path_PB1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_Liczba_obiektow;
        private System.Windows.Forms.Button button_Pokaz_obiekt;
        private System.Windows.Forms.NumericUpDown numericUpDown_Numer_obiektu;
        private System.Windows.Forms.Button button_Czysc;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button_Analizuj;
        private System.Windows.Forms.ListView listView_Dane;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button_Obrysuj;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button buttonImgRead;
        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.ListView listView_Komendy;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckBox checkBox_Skos;
        private System.Windows.Forms.NumericUpDown numericUpDown_ThreshUp;
        private System.Windows.Forms.CheckBox checkBox_Mono_Thresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numericUpDown_Brightness;
        private System.Windows.Forms.CheckBox checkBox_Invert;
        private System.Windows.Forms.CheckBox checkBox_Brightness;
        private System.Windows.Forms.CheckBox checkBox_Contrast;
        private System.Windows.Forms.NumericUpDown numericUpDown_Contrast;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown_ThreshDown;
        private System.Windows.Forms.NumericUpDown numericUpDown_Ray_count;
        private System.Windows.Forms.NumericUpDown numericUpDown_Moving_Average;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}

