using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Threading;

namespace SW_PROJEKT
{
    public partial class Form1 : Form
    {
        private Size desired_image_size;

        //Image dla PictureBoxów
        Image<Bgr, byte> image_PB1, image_PB1_Buf, image_PB2, image_PB3, image_PB4, image_buf_wypalone;

        //Image arr dla przeanalizowanych obrazów
        const int IMAGE_ARR_SIZE = 10;   //max liczba obrazów
        Image<Bgr, byte>[] image_arr = new Image<Bgr, byte>[IMAGE_ARR_SIZE];

        //Look up table
        private byte[] LutTable = new byte[256];

        //Arr dla mechaniki przeanalizowanych obrazów
        int[] F_Arr = new int[IMAGE_ARR_SIZE];
        int[] Sx_Arr = new int[IMAGE_ARR_SIZE];
        int[] Sy_Arr = new int[IMAGE_ARR_SIZE];
        int[] Jx0_Arr = new int[IMAGE_ARR_SIZE];
        int[] Jy0_Arr = new int[IMAGE_ARR_SIZE];
        int[] Jxy_Arr = new int[IMAGE_ARR_SIZE];
        Point[] Pc_Arr = new Point[IMAGE_ARR_SIZE];
        int[] dlugosc_Arr = new int[IMAGE_ARR_SIZE];
        int[] alfa_t_deg_Arr = new int[IMAGE_ARR_SIZE];
        int[] wierzcholki_Arr = new int[IMAGE_ARR_SIZE];
        Rectangle [] ROI_Arr = new Rectangle[IMAGE_ARR_SIZE];
        bool[] filled_Arr = new bool[IMAGE_ARR_SIZE];

        //Wypalanie

        //Kolejki. Ułatwiają pracę z danymi przypominającymi listę zadań do wykonia czy punktów do sprawdzenie, gdzie po 
        //pobraniu wartości z kolejki, można od razu usunąć dany wpis z kolejki.
        //Metoda Enqueue służy do dodania nowego obiektu do kolejki
        //Metoda Dequeue zwraca następny obiekt z kolejki i usuwa go z niej
        Queue<Point> pix_tlace = new Queue<Point>();
        Queue<Point> pix_palace = new Queue<Point>();
        Queue<Point> pix_nadpalone = new Queue<Point>();
        Queue<Point> pix_wypalone = new Queue<Point>();

        private MCvScalar aktualnie_klikniety = new MCvScalar(0, 0, 0);
        private MCvScalar cecha_palnosci = new MCvScalar(0x00, 0x00, 0x00);
        private MCvScalar cecha_nadpalenia = new MCvScalar(0, 0, 0);

        private MCvScalar kolor_tlenia = new MCvScalar(51, 153, 255);
        private MCvScalar kolor_palenia = new MCvScalar(0, 0, 204);
        private MCvScalar kolor_nadpalenia = new MCvScalar(51, 204, 51);
        private MCvScalar kolor_wypalenia = new MCvScalar(100, 100, 100);
        private MCvScalar aktualny_kolor_wypalenia = new MCvScalar(100, 100, 100);

        private int nr_pozaru, offset_kolor_wypalenia;
        private bool skos, cecha_dowolna;

        //Camera
        private bool captureInProgress = false;
        VideoCapture camera;

        //Sygnatura radialana
        private int liczba_promieni, kat_poczatkowy;
        private double[] tabela_promieni;
        private double[] tabela_wartosci_srednich;
        MCvScalar kolor_promienia = new MCvScalar(144, 238, 144);
        private double mix_averages_ratio = 0.4;
        private enum RadialSignature_AverageMode
        {
            CONSTANT,
            MOVING,
            MINMAX,
            MIX
        };
        private RadialSignature_AverageMode defaultRadialSignature_AverageMode = RadialSignature_AverageMode.MOVING;

        const bool AUTOLOOP = false;
        int loop_time = 2000;

        public Form1()
        {
            InitializeComponent();
            button_Obrysuj.Enabled = false;
            button_Pokaz_obiekt.Enabled = false;
            button_Analizuj.Enabled = false;
            checkBox_Contrast.Checked = false;
            checkBox_Brightness.Checked = false;
            checkBox_Invert.Checked = false;
            checkBox_Mono_Thresh.Checked = false;
            checkBox_Skos.Checked = false;
            numericUpDown_ThreshUp.Value = 100;
            numericUpDown_ThreshDown.Value = 0;
            numericUpDown_Contrast.Value = 0;
            numericUpDown_Brightness.Value = 0;
            numericUpDown_Ray_count.Value = 720;
            numericUpDown_Moving_Average.Value = 65;
            button_Analizuj.Visible = false;

            skos = false;
            cecha_dowolna = false;
            offset_kolor_wypalenia = 25;
            nr_pozaru = 0;

            liczba_promieni = (int)numericUpDown_Ray_count.Value;
            kat_poczatkowy = 0;

            //Dopasowanie gdy włączone skalowanie ekranu w windowsie
            //dopelnienie szerokosci aby byla podzielna przez 4
            int width = pictureBox1.Width;
            if (width % 4 != 0) width = width - width % 4 + 4;
            pictureBox1.Width = width;
            pictureBox2.Width = width;
            pictureBox3.Width = width;
            pictureBox4.Width = width;

            desired_image_size = pictureBox1.Size;

            image_PB1 = new Image<Bgr, byte>(desired_image_size);
            image_PB1_Buf = new Image<Bgr, byte>(desired_image_size);
            image_PB2 = new Image<Bgr, byte>(desired_image_size);
            image_PB3 = new Image<Bgr, byte>(desired_image_size);
            image_PB4 = new Image<Bgr, byte>(desired_image_size);

            //init img array
            for (int i = 0; i < IMAGE_ARR_SIZE; i++)
            {
                image_arr[i] = new Image<Bgr, byte>(desired_image_size);
                ROI_Arr[i] = new Rectangle(new Point(0, 0), desired_image_size);
            }
        
            image_buf_wypalone = new Image<Bgr, byte>(desired_image_size);

            Wyswietl_dane_pozaru();
            InitLUToperation();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pictureBox1.Image = get_image_bitmap_from_file(@"..\..\..\.png", ref image_PB1);
        }

        #region Read File
        private void button_Browse_Files_PB1_Click(object sender, EventArgs e)
        {
            textBox_Image_Path_PB1.Text = get_image_path();
        }

        private string get_image_path()
        {
            string ret = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Obrazy|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Title = "Wybierz obrazek.";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            //Jeśli wszystko przebiegło ok to pobiera nazwę pliku
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = openFileDialog.FileName;
            }
            return ret;
        }

        private void button_From_File_PB1_Click(object sender, EventArgs e)
        {
            get_image_bitmap_from_file(textBox_Image_Path_PB1.Text, ref image_PB1);
            //image_PB1 = image_PB1_Buf.Clone();
            ApplyLUT2Image(image_PB1, image_PB1);
            pictureBox1.Image = image_PB1.Bitmap;
        }

        private Bitmap get_image_bitmap_from_file(string path, ref Image<Bgr, byte> Data)
        {
            if (!String.IsNullOrEmpty(path))
            {
                Mat temp = CvInvoke.Imread(path);
                CvInvoke.Resize(temp, temp, desired_image_size);
                Data = temp.ToImage<Bgr, byte>();
            }
            return Data.Bitmap;
        }
        #endregion

        #region Camera
        private void btnCamera_Click(object sender, EventArgs e)
        {
            Camera_StartStop();
        }

        private void Camera_Init()
        {
            try
            {
                camera = new VideoCapture();
                //camera.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 640);
                //camera.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 480);

                camera.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, desired_image_size.Width);
                camera.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, desired_image_size.Height);

                //camera.FlipHorizontal = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Camera_StartStop()
        {
            if (camera == null)
            {
                Camera_Init();
            }

            if (camera != null)
            {
                if (captureInProgress)
                {
                    btnCamera.Text = "Kamera Start";
                    Application.Idle -= Camera_ProcessFrame;
                    camera.Dispose();
                    camera = null;
                }
                else
                {
                    btnCamera.Text = "Kamera Stop";
                    Application.Idle += Camera_ProcessFrame;
                }
                captureInProgress = !captureInProgress;
            }
        }

        private void Camera_ProcessFrame(object sender, EventArgs arg)
        {
            Mat tmp = camera.QueryFrame();

            //Resize z zachowaniem aspect ratio, ale z cropem
            Size size;
            double ratio = (double)camera.Width / camera.Height;
            double ratio2 = (double)desired_image_size.Width / desired_image_size.Height;
            if (ratio < ratio2) size = new Size(desired_image_size.Width, (int)Math.Round((double)desired_image_size.Width / camera.Width * camera.Height));
            else if (ratio > ratio2) size = new Size((int)Math.Round((double)desired_image_size.Height / camera.Height * camera.Width), desired_image_size.Height);
            else size = new Size(desired_image_size.Width, desired_image_size.Height);
            CvInvoke.Resize(tmp, tmp, size);

            //CvInvoke.Resize(tmp, tmp, desired_image_size);
            ApplyLUT2Image(tmp.ToImage<Bgr, byte>(), image_PB1);
            pictureBox1.Image = image_PB1.Bitmap;
        }
        #endregion

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (!pb1_mouseup_flag) CvInvoke.Circle(image_PB1, e.Location, 8, new MCvScalar(255, 255, 255), -1);
                else CvInvoke.Circle(image_PB1, e.Location, 8, new MCvScalar(0, 0, 0), -1);
                pictureBox1.Image = image_PB1.Bitmap;
            }
        }

        private bool pb1_mouseup_flag = false;

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && !pb1_mouseup_flag)
            {
                checkBox_Invert.Checked = true;
                pb1_mouseup_flag = true;
                InitLUToperation();
                pictureBox1.Image = ApplyLUT2Image(image_PB1, image_PB1).Bitmap;
            }
        }

        private void numericUpDown_Brightness_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox_Brightness.Checked) InitLUToperation();
        }

        private void numericUpDown_Contrast_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox_Contrast.Checked) InitLUToperation();
        }

        private void buttonImgRead_Click(object sender, EventArgs e)
        {
            Threshold();
            button_Analizuj.Enabled = true;
            if(!button_Analizuj.Visible) button_Analizuj_Click(sender, e);
        }

        private void Threshold()
        {
            byte[,,] tmp1, tmp2;

            tmp1 = image_PB1.Data;
            tmp2 = image_PB2.Data;
            int threshUp = (int)numericUpDown_ThreshUp.Value;
            int threshDown = (int)numericUpDown_ThreshDown.Value;
            for (int x = 0; x < desired_image_size.Width; x++)
            {
                for (int y = 0; y < desired_image_size.Height; y++)
                {
                    if (!checkBox_Mono_Thresh.Checked)
                    {
                        if ((tmp1[y, x, 0] >= threshDown && tmp1[y, x, 0] <= threshUp) ||
                            (tmp1[y, x, 1] >= threshDown && tmp1[y, x, 1] <= threshUp) ||
                            (tmp1[y, x, 2] >= threshDown && tmp1[y, x, 2] <= threshUp))
                        {
                            tmp2[y, x, 0] = 0;
                            tmp2[y, x, 1] = 0;
                            tmp2[y, x, 2] = 0;
                        }
                        else
                        {
                            tmp2[y, x, 0] = 255;
                            tmp2[y, x, 1] = 255;
                            tmp2[y, x, 2] = 255;
                        }
                    }
                    else
                    {
                        int mono;
                        //mono = (int)(tmp1[y, x, 0] + tmp1[y, x, 1] + tmp1[y, x, 2])/3;
                        mono = (int)(0.3 * tmp1[y, x, 2] + 0.59 * tmp1[y, x, 1] + 0.11 * tmp1[y, x, 0]);
                        if ( mono >= threshDown && mono <= threshUp)
                        {
                            tmp2[y, x, 0] = 0;
                            tmp2[y, x, 1] = 0;
                            tmp2[y, x, 2] = 0;
                        }
                        else
                        {
                            tmp2[y, x, 0] = 255;
                            tmp2[y, x, 1] = 255;
                            tmp2[y, x, 2] = 255;
                        }
                    }
                }
            }
            image_PB2.Data = tmp2;
            pictureBox2.Image = image_PB2.Bitmap;
        }

        private void button_Pokaz_obiekt_Click(object sender, EventArgs e)
        {
            button_Obrysuj.Enabled = true;
            listView_Dane.Items.Clear();

            int nr = (int)numericUpDown_Numer_obiektu.Value;

            //Wczytaj z wypalonego i narysuj wybrany obiekt w PB3
            Wczytaj_wybrany_obiekt(image_buf_wypalone, image_PB3, nr);
            pictureBox3.Image = image_PB3.Bitmap;

            //Narysuj z arr do PB4
            image_PB4 = image_arr[nr-1];
            pictureBox4.Image = image_PB4.Bitmap;

            int index = nr - 1;
            listView_Dane.Items.Add("Powierzchnia: " + F_Arr[index].ToString());
            listView_Dane.Items.Add("Długość: " + dlugosc_Arr[index].ToString());
            //listView_Dane.Items.Add("Moment Sx: " + Sx_Arr[index].ToString());
            //listView_Dane.Items.Add("Moment Sy: " + Sy_Arr[index].ToString());
            listView_Dane.Items.Add("Moment Jx0: " + Jx0_Arr[index].ToString());
            listView_Dane.Items.Add("Moment Jy0: " + Jy0_Arr[index].ToString());
            listView_Dane.Items.Add("Kąt nachylenia (nieb.): " + alfa_t_deg_Arr[index].ToString() + "st");
            listView_Dane.Items.Add("Środek ciężkości: (" + Pc_Arr[index].X.ToString() + ", " + Pc_Arr[index].Y.ToString() + ")");
            listView_Dane.Items.Add("Wierzchołki: " + wierzcholki_Arr[index].ToString());
            listView_Dane.Items.Add("Wypełnienie: " + (filled_Arr[index] ? "Tak" : "Nie"));
            listView_Dane.Items.Add("ROI: " + ROI_Arr[index].ToString());
        }      

        private void Narysuj_wybrany_obiekt(int nr)
        {
            image_PB3.SetZero();
            byte[,,] temp1 = image_buf_wypalone.Data;
            byte[,,] temp2 = image_PB3.Data;

            nr *= offset_kolor_wypalenia;
            MCvScalar kolor = new MCvScalar();
            kolor.V0 = (byte)(kolor_wypalenia.V0 + nr);
            kolor.V1 = (byte)(kolor_wypalenia.V1 + nr);
            kolor.V2 = (byte)(kolor_wypalenia.V2 + nr);

            for (int y = 1; y < desired_image_size.Height - 2; y++)
            {
                for (int x = 1; x < desired_image_size.Width - 2; x++)
                {
                    if (temp1[y, x, 0] == kolor.V0 && temp1[y, x, 1] == kolor.V1 && temp1[y, x, 2] == kolor.V2)
                    {
                        temp2[y, x, 0] = 0xff;
                        temp2[y, x, 1] = 0xff;
                        temp2[y, x, 2] = 0xff;
                    }
                }
            }
            image_PB3.Data = temp2;
            pictureBox3.Image = image_PB3.Bitmap;
        }

        private void Wczytaj_wybrany_obiekt(Image<Bgr, byte> image_input, Image<Bgr, byte> image_output, int nr)
        {
            image_output.SetZero();
            byte[,,] temp1 = image_input.Data;
            byte[,,] temp2 = image_output.Data;

            nr *= offset_kolor_wypalenia;
            MCvScalar kolor = new MCvScalar();
            kolor.V0 = (byte)(kolor_wypalenia.V0 + nr);
            kolor.V1 = (byte)(kolor_wypalenia.V1 + nr);
            kolor.V2 = (byte)(kolor_wypalenia.V2 + nr);

            for (int y = 1; y < desired_image_size.Height - 2; y++)
            {
                for (int x = 1; x < desired_image_size.Width - 2; x++)
                {
                    if (temp1[y, x, 0] == kolor.V0 && temp1[y, x, 1] == kolor.V1 && temp1[y, x, 2] == kolor.V2)
                    {
                        temp2[y, x, 0] = 0xff;
                        temp2[y, x, 1] = 0xff;
                        temp2[y, x, 2] = 0xff;
                    }
                }
            }
            image_output.Data = temp2;
        }

        private void button_Czysc_Click(object sender, EventArgs e)
        {
            button_Obrysuj.Enabled = false;
            button_Pokaz_obiekt.Enabled = false;
            button_Analizuj.Enabled = false;

            pb1_mouseup_flag = false;

            image_PB1.SetZero();
            image_PB2.SetZero();
            image_PB3.SetZero();
            image_PB4.SetZero();
            pictureBox1.Image = image_PB1.Bitmap;
            pictureBox2.Image = image_PB2.Bitmap;
            pictureBox3.Image = image_PB3.Bitmap;
            pictureBox4.Image = image_PB4.Bitmap;

            listView_Komendy.Items.Clear();
            listView_Dane.Items.Clear();
            Wyczysc_dane_pozaru();
            aktualny_kolor_wypalenia = new MCvScalar(100, 100, 100);
        }

        private void checkBox_Skos_CheckedChanged(object sender, EventArgs e)
        {
            skos = checkBox_Skos.Checked;
        }

        async private void button_Analizuj_Click(object sender, EventArgs e)
        {
            //Clear
            image_PB3.SetZero();
            image_PB4.SetZero();
            pictureBox3.Image = image_PB3.Bitmap;
            pictureBox4.Image = image_PB4.Bitmap;
            //listView_Komendy.Items.Clear();
            listView_Dane.Items.Clear();
            numericUpDown_Numer_obiektu.Value = 1;
            numericUpDown_Numer_obiektu.Maximum = 1;

            button_Analizuj.Enabled = false;

            //Pozar calosci
            Wyczysc_dane_pozaru();
            aktualny_kolor_wypalenia = new MCvScalar(100, 100, 100);
            Application.DoEvents();

            Pozar_Calosci();
            Wyswietl_dane_pozaru();

            if (nr_pozaru >= 1)
            {
                numericUpDown_Numer_obiektu.Maximum = nr_pozaru;
                if (nr_pozaru > IMAGE_ARR_SIZE) numericUpDown_Numer_obiektu.Maximum = IMAGE_ARR_SIZE;
            }
            ///////////////
            listView_Komendy.Items.Clear();
            //Mechanika i analiza piktogramow
            for (int nr = 1; nr <= nr_pozaru && nr <= IMAGE_ARR_SIZE; nr++)
            {
                Analizuj_wybrany_obiekt(nr);
            }

            //Wyswietlenie poszczegolnych piktogramów
            button_Pokaz_obiekt_Click(this, new EventArgs());

            //button_Analizuj.Enabled = true;
            button_Pokaz_obiekt.Enabled = true;

            if (AUTOLOOP)
            {
                if (captureInProgress) await Task.Delay(loop_time);
                if (captureInProgress) buttonImgRead_Click(sender, e);
            }
        }

        private void Analizuj_wybrany_obiekt(int nr)
        {
            //Wczytanie obiektu
            image_arr[nr - 1].SetZero();
            Image<Bgr, byte> image_tmp = image_arr[nr - 1];
            Wczytaj_wybrany_obiekt(image_buf_wypalone, image_tmp, nr);

            ///////////////////////MECHANIKA//////////////////////////////////////
            //Reczne liczenie
            double F, Sx, Sy, x0, y0;
            double Jx0, Jy0, Jx0y0, Jx, Jy, Jxy, Je_0, Jt_0;
            double alfa_e, alfa_t, alfa_e_deg, alfa_t_deg;
            F = Sx = Sy = Jx0 = Jy0 = Jx0y0 = Jx = Jy = Jxy = Je_0 = Jt_0 = alfa_e = alfa_t = alfa_e_deg = alfa_t_deg = x0 = y0 = 0;

            //Odciecie ewentualnego stykania sie z krawedzia obrazu
            CvInvoke.Rectangle(image_tmp, new Rectangle(0, 0, desired_image_size.Width-1, desired_image_size.Height-1), new MCvScalar(0, 0, 0), 2);
            Application.DoEvents();

            //Wyliczenie momentow 1 i 2 stopnia
            byte[,,] temp = image_tmp.Data;
            for (int X = 0; X < desired_image_size.Width; X++)
            {
                for (int Y = 0; Y < desired_image_size.Height; Y++)
                {
                    if (temp[Y, X, 0] == 0xFF && temp[Y, X, 1] == 0xFF && temp[Y, X, 2] == 0xFF)
                    {
                        F = F + 1;
                        Sx = Sx + Y;
                        Sy = Sy + X;
                        Jx = Jx + Math.Pow(Y, 2);
                        Jy = Jy + Math.Pow(X, 2);
                        Jxy = Jxy + X * Y;
                    }
                }
            }

            //Obliczenie środka cieżkości
            if (F > 0)
            {
                x0 = Sy / F;
                y0 = Sx / F;
            }
            Point Pc = new Point((int)x0, (int)y0);

            //Obliczenie momentów centralnych
            Jx0 = Jx - F * Math.Pow(y0, 2);
            Jy0 = Jy - F * Math.Pow(x0, 2);
            Jx0y0 = Jxy - F * x0 * y0;

            Je_0 = (Jx0 + Jy0) / 2 + Math.Sqrt(0.25 * Math.Pow(Jy0 - Jx0, 2) + Math.Pow(Jx0y0, 2));
            Jt_0 = (Jx0 + Jy0) / 2 - Math.Sqrt(0.25 * Math.Pow(Jy0 - Jx0, 2) + Math.Pow(Jx0y0, 2));

            if (Jy0 != Je_0)
                alfa_e = Math.Atan(Jx0y0 / (Jy0 - Je_0));
            else
                alfa_e = Math.PI / 2;

            if (Jy0 != Jt_0)
                alfa_t = Math.Atan(Jx0y0 / (Jy0 - Jt_0));
            else
                alfa_t = Math.PI / 2;

            alfa_e_deg = alfa_e * 180 / Math.PI;
            alfa_t_deg = alfa_t * 180 / Math.PI;

            double[] wektor_czerw = new double[2];
            double[] wektor_nieb = new double[2];

            wektor_czerw[0] = Math.Cos(alfa_e);
            wektor_czerw[1] = Math.Sin(alfa_e);

            wektor_nieb[0] = Math.Cos(alfa_t);
            wektor_nieb[1] = Math.Sin(alfa_t);

            //Liczenie wierzchołków
            tabela_promieni = sygnatura_radialna(image_tmp, image_PB2, Pc);
            pictureBox2.Image = image_PB2.Bitmap;
            usrednianie_wykresu();
            int wierzcholki = licz_wierzcholki(tabela_promieni, tabela_wartosci_srednich);

            //Rysowanie punktów przeciecia
            Point P1, P2, P3, P4, P1_wektor, P2_wektor, P3_wektor, P4_wektor;
            P1 = new Point();
            P2 = new Point();
            P3 = new Point();
            P4 = new Point();
            P1_wektor = new Point();
            P2_wektor = new Point();
            P3_wektor = new Point();
            P4_wektor = new Point();
            bool przeciecie;
            int i, zakres_x, zakres_y;
            
            int przeciecie_color = 255 - (int)image_tmp.Data[Pc.Y, Pc.X, 0];
            zakres_x = desired_image_size.Width-1;
            zakres_y = desired_image_size.Height-1;

            //z wektorem czerwonym
            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = (int)(Pc.X + i * wektor_czerw[0]);
                int Y = (int)(Pc.Y + i * wektor_czerw[1]);
                if (Y > zakres_y || X > zakres_x || Y < 0 || X < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P1_wektor.X = X;
                    P1_wektor.Y = Y;
                    przeciecie = true;
                }
                i++;
            }

            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = (int)(Pc.X + i * wektor_czerw[0]);
                int Y = (int)(Pc.Y + i * wektor_czerw[1]);
                if (Y > zakres_y || X > zakres_x || Y < 0 || X < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P2_wektor.X = X;
                    P2_wektor.Y = Y;
                    przeciecie = true;
                }
                i--;
            }

            //z wektorem niebieskim
            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = (int)(Pc.X + i * wektor_nieb[0]);
                int Y = (int)(Pc.Y + i * wektor_nieb[1]);
                if (Y > zakres_y || X > zakres_x || Y < 0 || X < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P3_wektor.X = X;
                    P3_wektor.Y = Y;
                    przeciecie = true;
                }
                i++;
            }

            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = (int)(Pc.X + i * wektor_nieb[0]);
                int Y = (int)(Pc.Y + i * wektor_nieb[1]);
                if (Y > zakres_y || X > zakres_x || Y < 0 || X < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P4_wektor.X = X;
                    P4_wektor.Y = Y;
                    przeciecie = true;
                }
                i--;
            }
            
            //z liniami ukladu wsp.
            //Y
            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = Pc.X;
                int Y = Pc.Y + i;
                if (Y > zakres_y || Y < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P1.X = X;
                    P1.Y = Y;
                    przeciecie = true;
                }
                i--;
            }

            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = Pc.X;
                int Y = Pc.Y + i;
                if (Y > zakres_y || Y < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P2.X = X;
                    P2.Y = Y;
                    przeciecie = true;
                }
                i++;
            }

            //X
            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = Pc.X + i;
                int Y = Pc.Y;
                if (X > zakres_x || X < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P3.X = X;
                    P3.Y = Y;
                    przeciecie = true;
                }
                i++;
            }

            przeciecie = false;
            i = 0;
            while (przeciecie == false)
            {
                int X = Pc.X + i;
                int Y = Pc.Y;
                if (X > zakres_x || X < 0) break;
                if (temp[Y, X, 0] == przeciecie_color)
                {
                    P4.X = X;
                    P4.Y = Y;
                    przeciecie = true;
                }
                i--;
            }

            //Długość
            double d1, d2, dlugosc;
            d1 = d2 = dlugosc = 0;
            d1 = Math.Sqrt(Math.Pow(P3_wektor.X - P4_wektor.X, 2) + Math.Pow(P3_wektor.Y - P4_wektor.Y, 2));
            d2 = Math.Sqrt(Math.Pow(P1_wektor.X - P2_wektor.X, 2) + Math.Pow(P1_wektor.Y - P2_wektor.Y, 2));
            if (d1 >= d2)
                dlugosc = d1;
            else
                dlugosc = d2;

            //Wypełnienie
            bool filled = (image_tmp.Data[Pc.Y, Pc.X, 0] == 255) ? true : false;
            
            //Rysowanie punktu centralnego
            CvInvoke.Circle(image_tmp, Pc, 6, new MCvScalar(255, 255, 0), 2);

            //Rysowanie przeciec z osiami wsp.
            Point P0 = new Point(0, 0);
            if (P1 != P0) CvInvoke.Circle(image_tmp, P1, 6, new MCvScalar(0, 255, 0), 2);
            if (P2 != P0) CvInvoke.Circle(image_tmp, P2, 6, new MCvScalar(0, 255, 0), 2);
            if (P3 != P0) CvInvoke.Circle(image_tmp, P3, 6, new MCvScalar(0, 255, 0), 2);
            if (P4 != P0) CvInvoke.Circle(image_tmp, P4, 6, new MCvScalar(0, 255, 0), 2);

            //Rysowanie przeciec z wektorami czerownym i niebieskim
            if (P1_wektor != P0) CvInvoke.Circle(image_tmp, P1_wektor, 6, new MCvScalar(0, 0, 255), 2);
            if (P2_wektor != P0) CvInvoke.Circle(image_tmp, P2_wektor, 6, new MCvScalar(0, 0, 255), 2);
            if (P3_wektor != P0) CvInvoke.Circle(image_tmp, P3_wektor, 6, new MCvScalar(255, 0, 0), 2);
            if (P4_wektor != P0) CvInvoke.Circle(image_tmp, P4_wektor, 6, new MCvScalar(255, 0, 0), 2);

            //Rysowanie wektorów
            CvInvoke.Line(image_tmp, Pc, new Point((int)(Pc.X + 120), (int)(Pc.Y)), new MCvScalar(0, 255, 0), 2);
            CvInvoke.Line(image_tmp, Pc, new Point((int)(Pc.X + 100 * wektor_czerw[0]), (int)(Pc.Y + 100 * wektor_czerw[1])), new MCvScalar(0, 0, 255), 2);
            CvInvoke.Line(image_tmp, Pc, new Point((int)(Pc.X + 100 * wektor_nieb[0]), (int)(Pc.Y + 100 * wektor_nieb[1])), new MCvScalar(255, 0, 0), 2);

            //////////////ZAPIS DANYCH/////////////////////////////////////
            i = nr - 1;
            F_Arr[i] = (int)F;
            Sx_Arr[i] = (int)Sx;
            Sy_Arr[i] = (int)Sy;
            Jx0_Arr[i] = (int)Jx0;
            Jy0_Arr[i] = (int)Jy0;
            Jxy_Arr[i] = (int)Jxy;
            Pc_Arr[i] = Pc;
            dlugosc_Arr[i] = (int)dlugosc;
            alfa_t_deg_Arr[i] = (int)Math.Round(alfa_t_deg);
            wierzcholki_Arr[i] = wierzcholki;
            filled_Arr[i] = filled;

            ////////////////ANALIZA//////////////////////////////////////
            int l, p, g, d, kat;
            l = P4.X;
            p = P3.X;
            g = P1.Y;
            d = P2.Y;
            string komenda = "---";

            if (F > 100)
            {
                kat = (int)Math.Round(alfa_t_deg);
                double a = Math.Sqrt(Math.Pow(P3_wektor.X - Pc.X, 2) + Math.Pow(P3_wektor.Y - Pc.Y, 2));
                double b = Math.Sqrt(Math.Pow(P4_wektor.X - Pc.X, 2) + Math.Pow(P4_wektor.Y - Pc.Y, 2));
                double J = Jy0 / Jx0;

                if (J > 5 && a < b && kat > -45 && kat < 45 && filled)
                    komenda = "Przesuń w prawo";
                else if (J > 5 && a > b && kat > -45 && kat < 45 && filled)
                    komenda = "Przesuń w lewo";
                else if (J < 1/5.0 && a < b && kat > -135 && kat < -45 && filled)
                    komenda = "Przesuń do przodu";
                else if (J < 1/5.0 && a > b && kat > -135 && kat < -45 && filled)
                    komenda = "Przesuń do tyłu";

                else if (d == 0 && p == 0 && g != 0 && l != 0 && !filled)
                    komenda = "Obróć o 90 stopni";

                else if (d == 0 && l == 0 && g != 0 && p != 0 && !filled)
                    komenda = "Obróć o 180 stopni";

                else if (g == 0 && l == 0 && d != 0 && p != 0 && !filled)
                    komenda = "Obróć o 270 stopni";

                else if (g == 0 && p == 0 && d != 0 && l != 0 && !filled)
                    komenda = "Obróć o 360 stopni";

                else if (J < 1.3 && J > 0.7 && wierzcholki > 8 && filled)
                    komenda = "Zapamiętaj bieżącą pozycję";

                else if (J < 1.3 && J > 0.7 && !filled)
                    komenda = "Zamknij chwytak";

                else if (J > 2 && g == 0 && l != 0 && p != 0 && kat > -15 && kat < 15 && !filled)
                    komenda = "Otwórz chwytak";

                else if (wierzcholki == 8 && filled)
                    komenda = "STOP";

                else if (wierzcholki == 3 || wierzcholki == 4 && a < b && J > 1 && J < 5 && filled)
                    komenda = "Wróć do poprzedniej pozycji";
            }
            listView_Komendy.Items.Add(nr.ToString() + ". " + komenda);
        }

        #region Pozar
        private void Wyczysc_dane_pozaru()
        {
            nr_pozaru = 0;
            pix_nadpalone.Clear();
            pix_palace.Clear();
            pix_tlace.Clear();
            pix_wypalone.Clear();
            Wyswietl_dane_pozaru();
            image_buf_wypalone.SetZero();
        }

        private void Pozar_Calosci()
        {
            byte[,,] temp = image_PB2.Data;
            for (int Y = 1; Y < desired_image_size.Height - 2; Y++)
            {
                for (int X = 1; X < desired_image_size.Width - 2; X++)
                {
                    if (Sprawdz_czy_cecha_palnosci(temp[Y, X, 0], temp[Y, X, 1], temp[Y, X, 2]))
                    {
                        pix_tlace.Enqueue(new Point(X, Y));
                        temp[Y, X, 0] = (byte)kolor_tlenia.V0;
                        temp[Y, X, 1] = (byte)kolor_tlenia.V1;
                        temp[Y, X, 2] = (byte)kolor_tlenia.V2;

                        nr_pozaru += 1;
                        aktualny_kolor_wypalenia.V0 = (byte)(kolor_wypalenia.V0 + nr_pozaru * offset_kolor_wypalenia);
                        aktualny_kolor_wypalenia.V1 = (byte)(kolor_wypalenia.V1 + nr_pozaru * offset_kolor_wypalenia);
                        aktualny_kolor_wypalenia.V2 = (byte)(kolor_wypalenia.V2 + nr_pozaru * offset_kolor_wypalenia);

                        Cykl_Pozaru();
                        temp = image_PB2.Data;
                    }
                }
            }
            Wyswietl_dane_pozaru();
            image_PB2.Data = temp;
            pictureBox2.Image = image_PB2.Bitmap;
        }

        private void Cykl_Pozaru()
        {
            do
            {
                Krok_Pozaru();
            } while (pix_tlace.Count > 0);
        }

        private void Krok_Pozaru()
        {
            //W języku C# wszystkie tablice są tzw typami referencyjnymi. Oznacza to, że w tym przypadku
            //do metody zostanie przekazana referencja, a nie skopiowana wartość czyli zmiany dokonane w metodzie
            //będą widoczne poza nią, a wydajność nie zostanie pogorszona nadmiarowymi operacjami kopiowania.
            byte[,,] temp = image_PB2.Data;

            Tlace_do_palacych(temp);

            foreach (Point pix in pix_palace)
            {
                Tlenie_od_palacego(temp, pix);
            }

            foreach (Point pix in pix_palace)
            {
                Nadpalenie_palacego(temp, pix);
            }

            Wypalenie_palacego(temp);

            image_PB2.Data = temp;
            pictureBox2.Image = image_PB2.Bitmap;
            Wyswietl_dane_pozaru();
            //Dokańcza kolejkę oczekujących zdarzeń interfejsu graficznego. Dodatkowy opis w "button_Krok_pozaru_Click"
            Application.DoEvents();
        }

        private void Tlace_do_palacych(byte[,,] temp)
        {
            while (pix_tlace.Count > 0)
            {
                Point p = pix_tlace.Dequeue();
                pix_palace.Enqueue(p);
                temp[p.Y, p.X, 0] = (byte)kolor_palenia.V0;
                temp[p.Y, p.X, 1] = (byte)kolor_palenia.V1;
                temp[p.Y, p.X, 2] = (byte)kolor_palenia.V2;
            }
        }

        private void Tlenie_od_palacego(byte[,,] temp, Point pix_in)
        {
            if (Czy_piksel_w_zakresie(pix_in))
            {
                Point[] sasiedzi = Wylicz_wspolrzedne_sasiednich_pikseli(pix_in);
                foreach (Point p in sasiedzi)
                {
                    if (Sprawdz_czy_cecha_palnosci(temp[p.Y, p.X, 0], temp[p.Y, p.X, 1], temp[p.Y, p.X, 2]))
                    {
                        pix_tlace.Enqueue(new Point(p.X, p.Y));
                        temp[p.Y, p.X, 0] = (byte)kolor_tlenia.V0;
                        temp[p.Y, p.X, 1] = (byte)kolor_tlenia.V1;
                        temp[p.Y, p.X, 2] = (byte)kolor_tlenia.V2;
                    }
                }
            }
        }

        private void Nadpalenie_palacego(byte[,,] temp, Point pix_in)
        {
            //Należy zobaczyć co się stanie z rysunkiem innym niż *.bmp i/lub takim na którym została wywołana metoda
            //resize zarówno dla cechy dowolnej (jakiejkolwiek) jak i konkretnej
            //Należy zwrócic uwagę na nieoczekiwane zmiany kolorów na modyfikowanych lub kompresowanych obrazach
            if (Czy_piksel_w_zakresie(pix_in))
            {
                Point[] sasiedzi = Wylicz_wspolrzedne_sasiednich_pikseli(pix_in);
                bool nalezy_nadpalic = false;
                foreach (Point p in sasiedzi)
                {
                    if (cecha_dowolna)
                        nalezy_nadpalic = Sprawdz_czy_jakiekolwiek_nadpalenie(temp[p.Y, p.X, 0], temp[p.Y, p.X, 1], temp[p.Y, p.X, 2]);
                    else
                        nalezy_nadpalic = Sprawdz_czy_cecha_nadpalenia(temp[p.Y, p.X, 0], temp[p.Y, p.X, 1], temp[p.Y, p.X, 2]);
                    if (nalezy_nadpalic)
                    {
                        pix_nadpalone.Enqueue(new Point(p.X, p.Y));
                        temp[p.Y, p.X, 0] = (byte)kolor_nadpalenia.V0;
                        temp[p.Y, p.X, 1] = (byte)kolor_nadpalenia.V1;
                        temp[p.Y, p.X, 2] = (byte)kolor_nadpalenia.V2;
                    }
                }
            }
        }

        private void Wypalenie_palacego(byte[,,] temp)
        {
            while (pix_palace.Count > 0)
            {
                Point p = pix_palace.Dequeue();
                pix_wypalone.Enqueue(p);
                temp[p.Y, p.X, 0] = (byte)(aktualny_kolor_wypalenia.V0);
                temp[p.Y, p.X, 1] = (byte)(aktualny_kolor_wypalenia.V1);
                temp[p.Y, p.X, 2] = (byte)(aktualny_kolor_wypalenia.V2);

                image_buf_wypalone.Data[p.Y, p.X, 0] = temp[p.Y, p.X, 0];
                image_buf_wypalone.Data[p.Y, p.X, 1] = temp[p.Y, p.X, 1];
                image_buf_wypalone.Data[p.Y, p.X, 2] = temp[p.Y, p.X, 2];
            }
        }

        private Point[] Wylicz_wspolrzedne_sasiednich_pikseli(Point pix_in)
        {
            List<Point> sasiedzi = new List<Point>();
            sasiedzi.Add(new Point(pix_in.X - 1, pix_in.Y));
            sasiedzi.Add(new Point(pix_in.X + 1, pix_in.Y));
            sasiedzi.Add(new Point(pix_in.X, pix_in.Y - 1));
            sasiedzi.Add(new Point(pix_in.X, pix_in.Y + 1));

            if (skos)
            {
                sasiedzi.Add(new Point(pix_in.X - 1, pix_in.Y - 1));
                sasiedzi.Add(new Point(pix_in.X + 1, pix_in.Y + 1));
                sasiedzi.Add(new Point(pix_in.X - 1, pix_in.Y + 1));
                sasiedzi.Add(new Point(pix_in.X + 1, pix_in.Y - 1));
            }

            return sasiedzi.ToArray();
        }

        private bool Czy_piksel_w_zakresie(Point pix_in)
        {
            int max_W, max_H;
            max_W = desired_image_size.Width - 1;
            max_H = desired_image_size.Height - 1;
            if (pix_in.X > 0 && pix_in.X < max_W && pix_in.Y > 0 && pix_in.Y < max_H)
                return true;
            else
                return false;
        }

        private bool Sprawdz_czy_cecha_palnosci(byte B, byte G, byte R)
        {
            if (B == cecha_palnosci.V0 && G == cecha_palnosci.V1 && R == cecha_palnosci.V2)
                return true;
            else
                return false;
        }

        private bool Sprawdz_czy_cecha_nadpalenia(byte B, byte G, byte R)
        {
            if (B == cecha_nadpalenia.V0 && G == cecha_nadpalenia.V1 && R == cecha_nadpalenia.V2)
                return true;
            else
                return false;
        }

        private bool Sprawdz_czy_jakiekolwiek_nadpalenie(byte B, byte G, byte R)
        {
            if (B == cecha_palnosci.V0 && G == cecha_palnosci.V1 && R == cecha_palnosci.V2)
                return false;
            else if (B == cecha_nadpalenia.V0 && G == cecha_nadpalenia.V1 && R == cecha_nadpalenia.V2)
                return true;
            else if (B == kolor_tlenia.V0 && G == kolor_tlenia.V1 && R == kolor_tlenia.V2)
                return false;
            else if (B == kolor_nadpalenia.V0 && G == kolor_nadpalenia.V1 && R == kolor_nadpalenia.V2)
                return false;
            else if (B == kolor_palenia.V0 && G == kolor_palenia.V1 && R == kolor_palenia.V2)
                return false;
            else if (B == aktualny_kolor_wypalenia.V0 && G == aktualny_kolor_wypalenia.V1 && R == aktualny_kolor_wypalenia.V2)
                return false;
            else
                return true;
        }

        private void Wyswietl_dane_pozaru()
        {
            //label_Wypalone.Text = "Liczba pikseli wypalonych: " + pix_wypalone.Count();
            label_Liczba_obiektow.Text = "Liczba obiektów: " + nr_pozaru;
        }
        #endregion

        #region Obrys
        private void button_Obrysuj_Click(object sender, EventArgs e)
        {
            int nr = (int)numericUpDown_Numer_obiektu.Value;
            ROI_Arr[nr - 1] = Obrysuj(image_PB3, image_PB4);
            pictureBox4.Image = image_PB4.Bitmap;
            listView_Dane.Items.RemoveAt(listView_Dane.Items.Count - 2);
            listView_Dane.Items.Add("ROI: " + ROI_Arr[nr-1].ToString());
        }

        private Rectangle Obrysuj(Image<Bgr, byte> input_image, Image<Bgr, byte> output_image)
        {
            byte[,,] temp = input_image.Data;
            int xmin, xmax, ymin, ymax;
            Point P1, P2, P3, P4;
            P1 = new Point(desired_image_size);
            P2 = new Point(0, 0);
            P3 = new Point(desired_image_size);
            P4 = new Point(0, 0);

            for (int Y = 0; Y < desired_image_size.Height; Y++)
            {
                for (int X = 0; X < desired_image_size.Width; X++)
                {
                    if (temp[Y, X, 0] != 0)
                    {
                        if (X < P1.X) P1 = new Point(X, Y);
                        if (X > P2.X) P2 = new Point(X, Y);
                        if (Y < P3.Y) P3 = new Point(X, Y);
                        if (Y > P4.Y) P4 = new Point(X, Y);
                    }
                }
            }

            xmin = P1.X;
            xmax = P2.X;
            ymin = P3.Y;
            ymax = P4.Y;

            Rectangle rect = new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);

            //input_image.ROI = rect;
            //output_image.ROI = rect;

            //rectangle obrys
            CvInvoke.Rectangle(output_image, rect, new MCvScalar(0, 0, 255), 2);

            //dots
            CvInvoke.Circle(output_image, P1, 2, new MCvScalar(0, 255, 0, 255), -1);
            CvInvoke.Circle(output_image, P2, 2, new MCvScalar(0, 255, 0, 255), -1);
            CvInvoke.Circle(output_image, P3, 2, new MCvScalar(0, 255, 0, 255), -1);
            CvInvoke.Circle(output_image, P4, 2, new MCvScalar(0, 255, 0, 255), -1);       

            listView_Dane.Items.Add("Obrys: " + "xmin: " + xmin.ToString() + ", xmax: " + xmax.ToString() 
                                                        + ", ymin: " + ymin.ToString() + ", ymax: " + ymax.ToString());
            //listView_Dane.Items.Add("ROI: " + rect.ToString());
            return rect;
        }
        #endregion

        #region LUT
        private void LUT_CheckedChanged(object sender, EventArgs e)
        {
            InitLUToperation();
        }

        private void numericUpDown_Ray_count_ValueChanged(object sender, EventArgs e)
        {
            liczba_promieni = (int)numericUpDown_Ray_count.Value;
        }

        private void InitLUToperation()
        {
            if (!checkBox_Invert.Checked)
            {
                for (int i = 0; i < 256; i++)
                {
                    LutTable[i] = (byte)i;
                }
            }
            else
            {
                for (int i = 0; i < 256; i++)
                {
                    LutTable[i] = (byte)(255 - i);
                }
            }

            if (checkBox_Brightness.Checked)
            {
                for (int i = 0; i < 256; i++)
                {
                    LutTable[i] = (byte)Math.Max(Math.Min(255, LutTable[i] + (int)numericUpDown_Brightness.Value), 0);
                }
            }

            if (checkBox_Contrast.Checked)
            {
                double scale = 255 / (255.0 - 2 * Math.Min(127, (int)numericUpDown_Contrast.Value));
                for (int i = 0; i < 256; i++)
                {
                    if (LutTable[i] < (int)numericUpDown_Contrast.Value)
                        LutTable[i] = (byte)0;
                    else if (LutTable[i] > (255 - (int)numericUpDown_Contrast.Value))
                        LutTable[i] = (byte)255;
                    else
                        LutTable[i] = (byte)(scale * (LutTable[i] - (int)numericUpDown_Contrast.Value));
                }
            }
        }

        private Image<Bgr, byte> ApplyLUT2Image(Image<Bgr, byte> image_input, Image<Bgr, byte> image_output)
        {
            byte[,,] tmp1 = image_input.Data;
            byte[,,] tmp2 = image_output.Data;

            int width, height;
            width = pictureBox1.Width;
            height = pictureBox1.Height;
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    tmp2[h, w, 0] = (byte)(LutTable[tmp1[h, w, 0]]);
                    tmp2[h, w, 1] = (byte)(LutTable[tmp1[h, w, 1]]);
                    tmp2[h, w, 2] = (byte)(LutTable[tmp1[h, w, 2]]);
                }
            }
            image_output.Data = tmp2;
            return image_output;
        }
        #endregion

        #region Sygnatura Radialna
        private void button_Count_vertices_Click(object sender, EventArgs e)
        {
            //Zliczanie ilości wierzchołków figury
            usrednianie_wykresu();
            int wierzcholki = licz_wierzcholki(tabela_promieni, tabela_wartosci_srednich);
            listView_Dane.Items.Add("Wierzchołki: " + wierzcholki.ToString());
        }

        private double[] sygnatura_radialna(Image<Bgr, byte> image_input, Image<Bgr, byte> image_output, Point start)
        {
            double[,] katy_kolejnych_promieni = new double[liczba_promieni, 2];
            double[] promienie = new double[liczba_promieni];
            double krok_katowy, aktualny_kat;

            aktualny_kat = kat_poczatkowy * (Math.PI / 180);

            krok_katowy = (2 * Math.PI / liczba_promieni);

            for (int i = 0; i < liczba_promieni; i++)
            {
                katy_kolejnych_promieni[i, 0] = Math.Cos(aktualny_kat);
                katy_kolejnych_promieni[i, 1] = Math.Sin(aktualny_kat);
                aktualny_kat += krok_katowy;
            }

            //image_output.SetZero();
            byte[,,] temp1 = image_input.Data;
            int zakres = (int)Math.Sqrt(Math.Pow(desired_image_size.Width, 2) + Math.Pow(desired_image_size.Height, 2));
            for (int p = 0; p < liczba_promieni; p++)
            {
                for (int d = 0; d < zakres; d++)
                {
                    Point cp = new Point();
                    int dx, dy;
                    dx = (int)(d * katy_kolejnych_promieni[p, 0]);
                    dy = (int)(d * katy_kolejnych_promieni[p, 1]);
                    if (Math.Abs(dx) < zakres && Math.Abs(dy) < zakres)
                    {
                        cp.X = start.X + dx;
                        cp.Y = start.Y + dy;
                        if (cp.Y < 0 || cp.X < 0 || cp.Y > desired_image_size.Height - 1 || cp.X > desired_image_size.Width - 1) break;
                        if (temp1[cp.Y, cp.X, 0] == 255 - (int)image_input.Data[start.Y, start.X, 0])
                        {
                            CvInvoke.Line(image_output, start, cp, kolor_promienia, 1);
                            promienie[p] = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                            break;
                        }
                    }
                }
            }
            return promienie;
        }

        private void usrednianie_wykresu()
        {
            MCvScalar kolor_nad_srednia = new MCvScalar(0, 255, 255);
            tabela_wartosci_srednich = wylicz_srednia_z_sygnatury(tabela_promieni);
            kolor_nad_srednia = new MCvScalar(0, 100, 255);
        }

        private double[] wylicz_srednia_z_sygnatury(double[] data)
        {
            double[] srednia = new double[data.Length];
            bool mix_averages = defaultRadialSignature_AverageMode == RadialSignature_AverageMode.MIX ? true : false;

            if (defaultRadialSignature_AverageMode == RadialSignature_AverageMode.MINMAX)
            {
                double avg = (data.Max() + data.Min()) / 2.0;
                for (int i = 0; i < data.Length; i++)
                {
                    srednia[i] = avg;
                }
            }
            else if (mix_averages == false)
            {
                if (defaultRadialSignature_AverageMode == RadialSignature_AverageMode.MOVING)
                {
                    int avg_width = (int)numericUpDown_Moving_Average.Value;
                    int maxID = data.Length - 1;
                    for (int i = 0; i < data.Length; i++)
                    {
                        double avg = 0;
                        int nr = 0;
                        int id = 0;
                        for (int f = -avg_width; f <= avg_width; f++)
                        {
                            nr++;
                            id = i + f;
                            avg += data[(Math.Abs(id * maxID) + id) % maxID];
                            
                        }
                        avg /= nr;
                        srednia[i] = avg;
                    }
                }
                else if (defaultRadialSignature_AverageMode == RadialSignature_AverageMode.CONSTANT)
                {
                    double avg = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        avg += data[i];
                    }
                    avg /= (data.Length);
                    for (int i = 0; i < data.Length; i++)
                    {
                        srednia[i] = avg;
                    }
                }
            }
            else if (mix_averages == true)
            {
                double avg_C = 0;
                double ratio = (double)mix_averages_ratio;
                for (int i = 0; i < data.Length; i++)
                {
                    avg_C += data[i];
                }
                avg_C /= (data.Length);

                int avg_width = (int)numericUpDown_Moving_Average.Value;
                int maxID = data.Length - 1;
                for (int i = 0; i < data.Length; i++)
                {
                    double avg_M = 0;
                    int nr = 0;
                    int id = 0;
                    for (int f = -avg_width; f <= avg_width; f++)
                    {
                        nr++;
                        id = i + f;
                        avg_M += data[(Math.Abs(id * maxID) + id) % maxID];
                    }
                    avg_M /= nr;
                    srednia[i] = ((avg_C * ratio) + (avg_M * (1 - ratio)));
                }
            }
            return srednia;
        }

        private int licz_wierzcholki(double[] dane, double[] krzywa)
        {
            int przeskok = (liczba_promieni / 15);
            int wierzcholki = 0;

            for (int i = 0; i < liczba_promieni - 1; i++)
            {
                if (dane[i] < krzywa[i] && dane[i + 1] >= krzywa[i + 1])
                {
                    wierzcholki++;
                    i += przeskok;
                }
            }
            return wierzcholki;
        }
        #endregion
    }
}
