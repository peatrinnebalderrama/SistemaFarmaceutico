using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaFarmaceutico.Vista
{
    public partial class FrmInicio : Form
    {
            public int selecRow = 0;
            List<TabPage> todasLasPestañas;
            public List<Producto> lproductosV = new List<Producto>();
            List<Image> imagenesPublicidad = new List<Image>();
            List<Image> imagenesPublicidad2 = new List<Image>();
            int indiceActual = 0;

            int slideX = 0;
            Image imagenAnterior;
            Image imagenNueva;

            Timer timerSlide = new Timer();

            int indiceActual2 = 0;

            int slideX2 = 0;
            Image imagenAnterior2;
            Image imagenNueva2;

            Timer timerSlide2 = new Timer();
            public FrmInicio()
            {

                InitializeComponent();
                timerSlide.Interval = 15;
                timerSlide.Tick += TimerSlide_Tick;

                timerSlide2.Interval = 15;
                timerSlide2.Tick += TimerSlide2_Tick;
            }

            public void agregarProducto(Producto producto)
            {
                lproductosV.Add(producto);
            }



            private void FrmInicio_Load(object sender, EventArgs e)

            {

                string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PublicidadFarma");

                if (Directory.Exists(ruta))
                {
                    string[] archivos = Directory.GetFiles(ruta)
                                                 .Where(f => f.EndsWith(".jpg") ||
                                                             f.EndsWith(".jpeg") ||
                                                             f.EndsWith(".png") ||
                                                             f.EndsWith(".bmp"))
                                                 .ToArray();

                    foreach (string archivo in archivos)
                    {
                        imagenesPublicidad.Add(Image.FromFile(archivo));
                    }
                }
                pictureBoxPublicidad.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxPublicidad.Image = imagenesPublicidad[0];

                timerCambio.Interval = 3000; // cambia cada 3 segundos
                timerCambio.Start();

                string ruta2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "PublicidadFarma");

                if (Directory.Exists(ruta2))
                {
                    string[] archivos2 = Directory.GetFiles(ruta2)
                                                 .Where(g => g.EndsWith(".jpg") ||
                                                             g.EndsWith(".jpeg") ||
                                                             g.EndsWith(".png") ||
                                                             g.EndsWith(".bmp"))
                                                 .ToArray();

                    foreach (string archivo2 in archivos2)
                    {
                        imagenesPublicidad2.Add(Image.FromFile(archivo2));
                    }
                }

                pictureBoxPublicidad2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxPublicidad2.Image = imagenesPublicidad2[0];

                timerCambio2.Interval = 3000; // cambia cada 3 segundos
                timerCambio2.Start();

                todasLasPestañas = tabControl1.TabPages.Cast<TabPage>().ToList();


                foreach (TabPage tab in tabControl1.TabPages.Cast<TabPage>().ToList())
                {
                    if ((tab.Name == "medicamentos") || (tab.Name == "maternidad") || (tab.Name == "cuidadoPers") || (tab.Name == "belleza") || (tab.Name == "carrito"))
                    {
                        tab.Parent = null;
                    }
                }
                form_catalogo();

            }
            private void iniciarSlide(Image nueva)
            {
                imagenAnterior = pictureBoxPublicidad.Image;
                imagenNueva = nueva;

                slideX = 0;  // posición inicial (0 = imagen actual empieza normal)

                timerSlide.Start();
            }

            private void iniciarSlide2(Image nueva)
            {
                imagenAnterior2 = pictureBoxPublicidad2.Image;
                imagenNueva2 = nueva;

                slideX2 = 0;  // posición inicial (0 = imagen actual empieza normal)

                timerSlide2.Start();
            }

            private void TimerSlide_Tick(object sender, EventArgs e)
            {
                slideX -= 20;  // velocidad del desplazamiento (más negativo = más rápido)

                if (Math.Abs(slideX) >= pictureBoxPublicidad.Width)
                {
                    // deslizamiento completado
                    pictureBoxPublicidad.Image = imagenNueva;
                    timerSlide.Stop();
                    return;
                }

                Bitmap bmp = new Bitmap(pictureBoxPublicidad.Width, pictureBoxPublicidad.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Imagen anterior (se va moviendo a la izquierda)
                    g.DrawImage(imagenAnterior,
                        new Rectangle(slideX, 0, bmp.Width, bmp.Height),
                        new Rectangle(0, 0, imagenAnterior.Width, imagenAnterior.Height),
                        GraphicsUnit.Pixel);

                    // Imagen nueva (entra desde la derecha)
                    g.DrawImage(imagenNueva,
                        new Rectangle(slideX + bmp.Width, 0, bmp.Width, bmp.Height),
                        new Rectangle(0, 0, imagenNueva.Width, imagenNueva.Height),
                        GraphicsUnit.Pixel);
                }

                pictureBoxPublicidad.Image = bmp;
            }

            private void TimerSlide2_Tick(object sender, EventArgs e)
            {

        }

        private void timerCambio_Tick_1(object sender, EventArgs e)
            {
                indiceActual++;

                if (indiceActual >= imagenesPublicidad.Count)
                    indiceActual = 0;

                iniciarSlide(imagenesPublicidad[indiceActual]);
            }

            private void timerCambio2_Tick_1(object sender, EventArgs e)
            {
                indiceActual2++;

                if (indiceActual2 >= imagenesPublicidad2.Count)
                    indiceActual2 = 0;

                iniciarSlide2(imagenesPublicidad2[indiceActual2]);
            }



            private void MostrarSoloCategoria(String categoria)
            {
                // Oculta todas las pestañas
                foreach (TabPage tab in todasLasPestañas)
                {
                    tab.Parent = null;
                }

                // Muestra solo la pestaña  de la categoria especificada
                var pestañaCat = todasLasPestañas.FirstOrDefault(t => t.Name == categoria);
                if (pestañaCat != null)
                {
                    pestañaCat.Parent = tabControl1;
                    tabControl1.SelectedTab = pestañaCat;
                }
            }

            private void ReInicio()
            {
                
                foreach (TabPage tab in todasLasPestañas)
                {
                    if ((tab.Name == "medicamentos") || (tab.Name == "maternidad") || (tab.Name == "cuidadoPers") || (tab.Name == "belleza") || (tab.Name == "carrito"))
                        tab.Parent = null;
                    else
                        tabControl1.TabPages.Add(tab);
                }

                tabControl1.SelectedTab = inicio;
            }

            private void form_catalogo()
            {
                txtBusProd.Clear();
                txtBusProd.Focus();
                groupBoxDetallP.Enabled = false;
                buttonCarrito.Enabled = false;
            }

            private void textBoxBusTodo_KeyPress(object sender, KeyPressEventArgs e)
            {

                if (char.IsNumber(e.KeyChar))
                    e.Handled = true;
                if (char.IsPunctuation(e.KeyChar))
                    e.Handled = true;
                if (char.IsSymbol(e.KeyChar))
                    e.Handled = true;
                if (e.KeyChar == (char)(Keys.Enter))
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }

    


        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmLogin f = new FrmLogin(this);
            this.Hide();
            f.FormClosed += (s, args) => this.Show();
            f.ShowDialog();
        }

        private void Regresar_Click_1(object sender, EventArgs e)
        {
            ReInicio();
        }

        private void buttonBusProd_Click(object sender, EventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Nombre de Producto debe de ser nombre valido{0}", ex.Message);
                txtBusProd.Focus();
            }


        }

        private void buttonAgreC1_Click_1(object sender, EventArgs e)
        {
            txtNombreProdBus.Clear();
            txtPrecioProdBus.Clear();
            txtStockProdBus.Clear();
            pictureProdBus.Image = null;
            groupBoxDetallP.Enabled = false;
            buttonCarrito.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReInicio();
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            ReInicio();
        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            ReInicio();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            ReInicio();
        }

        private void buttonCMed_Click(object sender, EventArgs e)
        {
            MostrarSoloCategoria("carrito");
        }

        private void buttonMedi_Click(object sender, EventArgs e)
        {
            MostrarSoloCategoria("medicamentos");
        }

        private void buttonMaternidad_Click(object sender, EventArgs e)
        {
            MostrarSoloCategoria("maternidad");
        }

        private void buttonCuidado_Click_1(object sender, EventArgs e)
        {
            MostrarSoloCategoria("cuidadoPers");
        }

        private void buttonBelleza_Click_1(object sender, EventArgs e)
        {
            MostrarSoloCategoria("belleza");
        }

        private void buttonCarrito_Click(object sender, EventArgs e)
        {
            MostrarSoloCategoria("carrito");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url1 = "https://whatsapp.com/77980436";
            System.Diagnostics.Process.Start(url1);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url2 = "https://www.facebook.com/ProsaludBolivia/?locale=es_LA";
            System.Diagnostics.Process.Start(url2);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url3 = "https://www.instagram.com/p/DGPP4rHOv7S/?hl=es";
            System.Diagnostics.Process.Start(url3);
        }
    }
  }



