using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using SistemaFarmaceutico.Controlador;
using SistemaFarmaceutico.Modelo;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;


namespace SistemaFarmaceutico.Vista
{
    public partial class FrmLogin : Form
    {
        
            private FrmInicio form1;
            public int selecRow = 0;
            public List<TabPage> todasLasPestañas;
        List<DetallaVentaModelo> listDV = new List<DetallaVentaModelo>();
        List<DetalleCompraModelo> listDC = new List<DetalleCompraModelo>();
        
        int usuario = 0;
            
            public FrmLogin(FrmInicio form1)
            {
                InitializeComponent();
                this.form1 = form1;
            }
            private void FrmLogin_Load(object sender, EventArgs e)
            {
           
            this.ventasTableAdapter.Fill(this.dataSetVentas.Ventas);
            try
            {
     
                DataSetVentas ds = new DataSetVentas();
                DataSetVentasTableAdapters.VentasTableAdapter adapter = new DataSetVentasTableAdapters.VentasTableAdapter();
                adapter.Fill(ds.Ventas);

               
                this.reportViewer1.LocalReport.DataSources.Clear();

               
                ReportDataSource rds = new ReportDataSource("DataSetVentas", (DataTable)ds.Ventas);
                this.reportViewer1.LocalReport.DataSources.Add(rds);

              
                string rutaReporte = Path.Combine(Application.StartupPath, "Report1.rdlc");
                this.reportViewer1.LocalReport.ReportPath = rutaReporte;

                // 5. Refrescar
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar reporte en esta PC: " + ex.Message);
            }
            GenerarReporte();
          
            this.ventasTableAdapter.Fill(this.dataSetVentas1.Ventas);
           
            this.ventasTableAdapter.Fill(this.dataSetVentas.Ventas);
            dataGridViewListDV.Columns[4].ValueType = typeof(float);
            DateTime f = DateTime.Today;
                form_caja();
                form_producto();
            todasLasPestañas = tabControl1.TabPages.Cast<TabPage>().ToList();
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    if (tab.Name != "InSesion")
                    {
                        tab.Parent = null;
                    }
                }
                tabControl1.SelectedTab = InSesion;
                comboBoxIdCat.DataSource = Metodos.CargaComboBox();
                comboBoxIdCat.ValueMember = "nombre";
                comboBoxIdCat.DisplayMember = "IdCat";
                limp_login();
            this.reportViewer1.RefreshReport();
        }

        public void form_caja()
            {
                textBoxCerrar.Clear();
                groupBoxCerrar.Enabled = false;
                groupBoxAbrir.Enabled = true;
                
                textBoxDepen.Clear();
                textBoxDepen.ReadOnly = true;
                textBoxAbrir.Clear();
                textBoxAbrir.Focus();
                string nombreE = EmpleadoModelo.ObtenerNombreUsuario(usuario);
                textBoxDepen.Text = nombreE;
        }

            public void form_producto()
            {
                groupBoxListaProd.Enabled = false;
                groupBoxDetalleP.Enabled = false;
                groupBoxFotoP.Enabled = false;
                buttonNueProd.Enabled = true;
                buttonRegProd.Enabled = false;
                buttonModProd.Enabled = false;
                buttonEliProd.Enabled = false;
                buttonCanProd.Enabled = false;
                limp_campos_prod();
            }

            public void limp_login()
            {
                textBoxLogUsu.Clear();
                textBoxLogCon.Clear();
                textBoxLogUsu.Focus();
            }

            public void limp_campos_prod()
            {
                textBoxIdProd.Clear();
                textBoxNPro.Clear();
                comboBoxIdCat.SelectedItem = null;

                textBoxPrecio.Clear();

                pictureBoxProd.Image = null;
                textBoxIdProd.ReadOnly = false;
            }

          
            private void textBoxMonto_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (char.IsWhiteSpace(e.KeyChar))
                    e.Handled = true;
                if (char.IsLetter(e.KeyChar))
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

           
            private void textBoxIdProd_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (char.IsWhiteSpace(e.KeyChar))
                    e.Handled = true;
                if (char.IsLetter(e.KeyChar))
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

           private void textBoxNPro_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
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

        private void textBoxIdCat_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (char.IsWhiteSpace(e.KeyChar))
                    e.Handled = true;
                if (char.IsLetter(e.KeyChar))
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
           
            private void textBoxPrecio_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (char.IsControl(e.KeyChar))
                    return;
                if (char.IsDigit(e.KeyChar))
                    return;
                if (e.KeyChar == '.' && !textBoxPrecio.Text.Contains("."))
                    return;
                if (char.IsWhiteSpace(e.KeyChar) ||
                    char.IsLetter(e.KeyChar) ||
                    char.IsPunctuation(e.KeyChar) && e.KeyChar != '.' ||
                    char.IsSymbol(e.KeyChar))
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    SendKeys.Send("{TAB}");
                }
            }

            private void textBoxStock_KeyPress(object sender, KeyPressEventArgs e)
            {
                if (char.IsWhiteSpace(e.KeyChar))
                    e.Handled = true;
                if (char.IsLetter(e.KeyChar))
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
            
            public void from_cat()
            {
                
                buttonNuevocatg.Enabled = true;
                buttonGuardarCatg.Enabled = false;
                buttonModCatg.Enabled = false;
                buttonElimCatg.Enabled = false;
                buttonCanCatg.Enabled = false;
                limp_campos_cat();
                groupBoxRegCat.Enabled = false;
                groupBoxListCat.Enabled = false;
            }

            public void limp_campos_cat()
            {
                textBoxidCatg.Clear();
                textBoxnombreCatg.Clear();
                textBoxdescrCatg.Clear();
                textBoxidCatg.ReadOnly = false;
            }

          
            public static byte[] ImageToByteArray(Image img)
            {

                if (img == null)
                {
                    return null;
                }


                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }

            private void refresh()
            {
                
                DataTable productos = Metodos.ObtenerProductos();
                dataGridViewListaProd.DataSource = productos;
                dataGridViewListProd.DataSource = productos;

                if (dataGridViewListaProd.Rows.Count > 0)
                {
                }
            }
        //seccion de login ingreso////////////////////////////////////
        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(textBoxLogUsu.Text) || string.IsNullOrWhiteSpace(textBoxLogCon.Text))
            {
                MessageBox.Show("ERROR: Ambos campos (Usuario y Contraseña) son obligatorios");
                limp_login();
                textBoxLogUsu.Focus();
                return;
            }

            try
            {
                string user = textBoxLogUsu.Text;
                usuario = UsuarioModelo.ObtenerCiUsuario(user);
               
                string pass = textBoxLogCon.Text;
                string cargoDelUsuario = UsuarioModelo.VerificarUsuario(user, pass);
                if (!string.IsNullOrEmpty(cargoDelUsuario))
                {
                    MessageBox.Show($"¡Acceso exitoso! Bienvenido, {cargoDelUsuario}.", "Bienvenido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     switch (cargoDelUsuario.ToLower()) 
                    {
                        case "supervisor":
                            foreach (TabPage tab in todasLasPestañas)
                            {
                                if ((tab.Name != "InSesion" && tab.Parent == null)&& (tab.Name != "facturacion" && tab.Parent == null) && (tab.Name != "requerimiento" && tab.Parent == null))
                                    tabControl1.TabPages.Add(tab);
                            }

                            InSesion.Parent = null;
                            tabControl1.SelectedTab = tabControl1.TabPages[0];
                            form_caja();
                            string nombreE = EmpleadoModelo.ObtenerNombreUsuario(usuario);
                            textBoxDepen.Text = nombreE;
                            dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();
                            dataGridViewListaVenta.DataSource = VentaModelo.ListarVenta();
                            form_Cliente();
                            form_Emp();
                            form_producto();
                            form_prov();
                            form_venta();
                            form_usuario();
                            form_invent();
                            break;

                        case "vendedor":
                            foreach (TabPage tab in todasLasPestañas)
                            {
                                if ((tab.Name != "InSesion" && tab.Parent == null) && (tab.Name != "RegEmp" && tab.Parent == null) 
                                    && (tab.Name != "regisProd" && tab.Parent == null) && (tab.Name != "RegCat" && tab.Parent == null) 
                                    && (tab.Name != "RegUs" && tab.Parent == null) && (tab.Name != "RegProv" && tab.Parent == null)
                                    && (tab.Name != "Inventario" && tab.Parent == null) && (tab.Name != "Compra" && tab.Parent == null)
                                     && (tab.Name != "facturacion" && tab.Parent == null) && (tab.Name != "requerimiento" && tab.Parent == null))
                                    tabControl1.TabPages.Add(tab);
                            }

                            InSesion.Parent = null;
                            tabControl1.SelectedTab = tabControl1.TabPages[0];
                            form_caja();
                            form_venta();
                            dataGridViewListaVenta.DataSource = VentaModelo.ListarVenta();
                            string nombrE = EmpleadoModelo.ObtenerNombreUsuario(usuario);
                            textBoxDepen.Text = nombrE;
                            form_Cliente();
                            break;

                        default:
                            
                            break;
                            
                    }
                    dataGridViewListCM.DataSource = Metodos.listarPC1(1000);
                    dataGridViewListCMa.DataSource = Metodos.listarPC1(2000);
                    dataGridViewListCP.DataSource = Metodos.listarPC1(3000);
                    dataGridViewListCN.DataSource = Metodos.listarPC1(4000);
                    dataGridViewListCM2.DataSource = Metodos.listarPC1(1000);
                    dataGridViewListCMa2.DataSource = Metodos.listarPC1(2000);
                    dataGridViewListCP2.DataSource = Metodos.listarPC1(3000);
                    dataGridViewListCN2.DataSource = Metodos.listarPC1(4000);
                    dataGridViewFC.DataSource = ClienteControlador.listarCliCon();
                    dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();
                    dataGridViewListaVenta.DataSource = VentaModelo.ListarVenta();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    limp_login();
                    textBoxLogUsu.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ", ex.Message);
            }

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBoxLogCon.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxLogCon.UseSystemPasswordChar = true;
            }

        }

        private void buttonRegis_Click(object sender, EventArgs e)
        {
            form_Emp();
            form_usuario();
            foreach (TabPage tab in todasLasPestañas)
            {
                if ((tab.Name != "InSesion" && tab.Parent == null) && (tab.Name != "caja" && tab.Parent == null) && (tab.Name != "Nventa" && tab.Parent == null) && (tab.Name != "regisProd" && tab.Parent == null) && (tab.Name != "RegCli" && tab.Parent == null) && (tab.Name != "RegCat" && tab.Parent == null) && (tab.Name != "RegProv" && tab.Parent == null) && (tab.Name != "Inventario" && tab.Parent == null) && (tab.Name != "Compra" && tab.Parent == null) && (tab.Name != "facturacion" && tab.Parent == null) && (tab.Name != "requerimiento" && tab.Parent == null))
                    tabControl1.TabPages.Add(tab);
            }

           
            InSesion.Parent = null;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }
        
       
        //seccion para retornar a inicio///////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            form1.FormClosed += (s, args) => this.Show();
            this.Hide();
            form1.ShowDialog();
        }
        //seccion de botones para abrir caja/////////////////////////////////////////////
        private void buttonAbrir_Click(object sender, EventArgs e)
        {
            dataGridViewListCM.DataSource = Metodos.listarPC1(1000);
            dataGridViewListCMa.DataSource = Metodos.listarPC1(2000);
            dataGridViewListCP.DataSource = Metodos.listarPC1(3000);
            dataGridViewListCN.DataSource = Metodos.listarPC1(4000);
            dataGridViewListCM2.DataSource = Metodos.listarPC1(1000);
            dataGridViewListCMa2.DataSource = Metodos.listarPC1(2000);
            dataGridViewListCP2.DataSource = Metodos.listarPC1(3000);
            dataGridViewListCN2.DataSource = Metodos.listarPC1(4000);
            dataGridViewFC.DataSource= ClienteControlador.listarCliCon();
            dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();
            dataGridViewListaVenta.DataSource = VentaModelo.ListarVenta();
            textBoxAbrir.Clear();
            textBoxDepen.Clear();
            groupBoxCerrar.Enabled = true;
            groupBoxAbrir.Enabled = false;
            groupBoxInf.Enabled = true;
            groupBoxDetalleV.Enabled = true;
            groupBoxFact.Enabled = true;
            limp_campos_venta();
            textBoxCerrar.Clear();
            textBoxCerrar.Focus();
        }

        private void buttonCerrar_Click_1(object sender, EventArgs e)
        {
            
            float totalVendido = VentaModelo.CalcularVentasDelDia();
            string fechaActual = DateTime.Now.ToShortDateString();
            //string fechaFormateada = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            if (totalVendido > 0)
            {
                MessageBox.Show($"--- RESUMEN DE CIERRE DE CAJA ---\n\n" +
                                $"Fecha: {fechaActual}\n" +
                                $"Total de Ventas: {totalVendido.ToString("C")}\n\n" +
                                $"Operación completada con éxito.",
                                "Cierre de Caja",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"No se encontraron ventas registradas para el día de hoy ({fechaActual}).",
                                "Cierre de Caja",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            form_caja();
        }

        private void buttonRegInSe_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in todasLasPestañas)
            {
                if (tab.Name != "InSesion")
                    tab.Parent = null;
                else if (tab.Parent == null)
                    tabControl1.TabPages.Add(tab);
            }

            tabControl1.SelectedTab = InSesion;
            limp_login();
        }

        private void buttonSubFoto_Click(object sender, EventArgs e)
        {
            openFileDialogFotoP.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialogFotoP.Title = "Seleccione una foto del producto";

            if (openFileDialogFotoP.ShowDialog() == DialogResult.OK)
            {
                pictureBoxProd.Image = Image.FromFile(openFileDialogFotoP.FileName);
                pictureBoxProd.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        //secion de registro producto///////////////////////
        private void buttonNueProd_Click(object sender, EventArgs e)
        {
            groupBoxListaProd.Enabled = true;
            groupBoxDetalleP.Enabled = true;
            groupBoxFotoP.Enabled = true;
            buttonNueProd.Enabled = false;
            buttonRegProd.Enabled = true;
            buttonModProd.Enabled = false;
            buttonEliProd.Enabled = false;
            buttonCanProd.Enabled = true;
            limp_campos_prod();
            refresh();
            textBoxIdProd.Focus();
        }

        private void buttonRegProd_Click(object sender, EventArgs e)
        {
            
            if (textBoxIdProd.Text == "" || string.IsNullOrWhiteSpace(textBoxNPro.Text) ||
                comboBoxIdCat.Text == "" || textBoxPrecio.Text == "" ||
                pictureBoxProd.Image == null)
            {
                MessageBox.Show("ERROR: Todos los campos son obligatorios, incluyendo la imagen.");
                limp_campos_prod();
                textBoxIdProd.Focus();
                return;
            }
            try
            {
                string fechaFormateada = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                Producto prod = new Producto();
                prod.Id = Convert.ToInt32(textBoxIdProd.Text);
                prod.Idcategoria = Convert.ToInt32(comboBoxIdCat.Text);
                prod.nombre = textBoxNPro.Text.Trim();
                prod.precio = float.Parse(textBoxPrecio.Text);
                prod.fechaVenc = Convert.ToDateTime(fechaFormateada);
                prod.foto = pictureBoxProd.Image != null ? (Image)pictureBoxProd.Image.Clone() : null;

                int result = Metodos.AgregarProducto(prod);
                if (result > 0)
                {
                    MessageBox.Show("Exito al guardar");
                    refresh();
                    limp_campos_prod();
                    form_producto();
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                    limp_campos_prod();
                    form_producto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar Producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            form_producto();
        }

        private void buttonModProd_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(textBoxIdProd.Text))
            {
                MessageBox.Show("Por favor, selecciona un producto de la lista primero.");
                return;
            }

            
            if (string.IsNullOrWhiteSpace(textBoxNPro.Text) || comboBoxIdCat.Text == "")
            {
                MessageBox.Show("El nombre y la categoría son obligatorios.");
                return;
            }

            try
            {
                Producto prod = new Producto();
                prod.Id = Convert.ToInt32(textBoxIdProd.Text);
                prod.Idcategoria = Convert.ToInt32(comboBoxIdCat.Text);
                prod.nombre = textBoxNPro.Text.Trim();
                prod.precio = float.Parse(textBoxPrecio.Text);
                prod.fechaVenc = dateTimePicker2.Value;
                if (pictureBoxProd.Image != null)
                {
                    prod.foto = new Bitmap(pictureBoxProd.Image);
                }
                else
                {
                    prod.foto = null;
                }
                int result = Metodos.ModificarProducto(prod);

                if (result > 0)
                {
                    MessageBox.Show("Producto modificado correctamente.");
                    refresh();           
                    form_producto();  
                }
                else
                {
                    MessageBox.Show("No se encontró el producto para modificar. Verifique el ID.");
                    form_producto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            form_producto();
        }

        private void buttonEliProd_Click(object sender, EventArgs e)
        {
            if (dataGridViewListaProd.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un Producto", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            
            DialogResult confirm = MessageBox.Show("¿Esta seguro de eliminar este registro?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
               string fechaFormateada = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                Producto prod = new Producto();
                prod.Id = Convert.ToInt32(textBoxIdProd.Text);
                prod.Idcategoria = Convert.ToInt32(comboBoxIdCat.Text);
                prod.nombre = textBoxNPro.Text.Trim();
                prod.precio = float.Parse(textBoxPrecio.Text);
                prod.fechaVenc = Convert.ToDateTime(fechaFormateada);
                prod.foto = pictureBoxProd.Image != null ? (Image)pictureBoxProd.Image.Clone() : null;

                int result = Metodos.EliminarProducto(prod);
                if (result > 0)
                {
                    MessageBox.Show("Exito al eliminar");
                    //from_cat();
                    refresh();
                    limp_campos_prod();
                    form_producto();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                    limp_campos_prod();
                    form_producto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar Producto {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            form_producto();
        }

        private void buttonCanProd_Click(object sender, EventArgs e)
        {
            limp_campos_prod();
            dataGridViewListaProd.ClearSelection();
            form_producto();
            textBoxIdProd.Focus();
        }

        private void dataGridViewListaProd_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            textBoxIdProd.ReadOnly = true;
            textBoxNPro.Focus();
            try
            {
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListaProd.Rows[selecRow];
                textBoxIdProd.Text = row.Cells[0].Value.ToString();
                comboBoxIdCat.Text = row.Cells[1].Value.ToString();
                textBoxNPro.Text = row.Cells[2].Value.ToString();
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells[4].Value.ToString());
                textBoxPrecio.Text = row.Cells[3].Value.ToString();
                if (pictureBoxProd.Image != null)
                {
                    pictureBoxProd.Image.Dispose();
                    pictureBoxProd.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxProd.Image = img;
                    pictureBoxProd.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                buttonNueProd.Enabled = false;
                buttonRegProd.Enabled = false;
                buttonModProd.Enabled = true;
                buttonEliProd.Enabled = true;
                buttonCanProd.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }
        //seccion para guardar categoria///////////////////////////////////////////
        private void buttonNuevocatg_Click(object sender, EventArgs e)
        {
            groupBoxRegCat.Enabled = true;
            groupBoxListCat.Enabled = true;
            limp_campos_cat();
            buttonNuevocatg.Enabled = false;
            buttonGuardarCatg.Enabled = true;
            buttonModCatg.Enabled = false;
            buttonElimCatg.Enabled = false;
            buttonCanCatg.Enabled = true;
            dataGridViewListCatg.DataSource = CategoriaControlador.Refresh();
        }

        private void buttonGuardarCatg_Click(object sender, EventArgs e)
        {
            if (textBoxidCatg.Text == "" || string.IsNullOrWhiteSpace(textBoxnombreCatg.Text) ||
              string.IsNullOrWhiteSpace(textBoxdescrCatg.Text))
            {
                MessageBox.Show("ERROR: Todos los campos son obligatorios");
                limp_campos_cat();
                textBoxidCatg.Focus();
                return;
            }

            try
            {
                int var1 = Convert.ToInt32(textBoxidCatg.Text);
                string var2 = textBoxnombreCatg.Text;
                string var3 = textBoxdescrCatg.Text;

                int result = CategoriaModelo.AgregarCategoria(var1, var2, var3);
                if (result == 1)
                {
                    MessageBox.Show("Exito al guardar");
                    //from_cat();
                    from_cat();
                    dataGridViewListCatg.DataSource = CategoriaControlador.Refresh();
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                    from_cat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar categoria: {0}", ex.Message);
            }
        }

        private void buttonModCatg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxidCatg.Text))
            {
                MessageBox.Show("Por favor, selecciona una categoria de la lista primero.");
                return;
            }
            if (textBoxidCatg.Text == "" || string.IsNullOrWhiteSpace(textBoxnombreCatg.Text) || string.IsNullOrWhiteSpace(textBoxdescrCatg.Text))
                
            {
                MessageBox.Show("ERROR: todos los campos son obligatorios.");
                textBoxnombreCatg.Focus();
                return;
            }

            try
            {

                CategoriaControlador.modificarCatCon(Convert.ToInt32(textBoxidCatg.Text), textBoxnombreCatg.Text, textBoxdescrCatg.Text);
                dataGridViewListCatg.DataSource = CategoriaControlador.Refresh();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            from_cat();
        }
        private void dataGridViewListCatg_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            textBoxidCatg.ReadOnly = true;
            textBoxnombreCatg.Focus();
            try
            {
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCatg.Rows[selecRow];
                textBoxidCatg.Text = row.Cells[0].Value.ToString();
                textBoxnombreCatg.Text = row.Cells[1].Value.ToString();
                textBoxdescrCatg.Text = row.Cells[2].Value.ToString();


                buttonNuevocatg.Enabled = false;
                buttonGuardarCatg.Enabled = false;
                buttonModCatg.Enabled = true;
                buttonElimCatg.Enabled = true;
                buttonCanCatg.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void buttonElimCatg_Click(object sender, EventArgs e)
        {
            if (dataGridViewListCatg.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione una Categoria", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            DialogResult confirm = MessageBox.Show("¿Esta seguro de eliminar este registro?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                CategoriaControlador.eliminarCatgCon(Convert.ToInt32(textBoxidCatg.Text), textBoxnombreCatg.Text, textBoxdescrCatg.Text);
                dataGridViewListCatg.DataSource = CategoriaControlador.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar Categoria {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            from_cat();
        }

        private void buttonCanCatg_Click(object sender, EventArgs e)
        {
            limp_campos_cat();
            dataGridViewListCatg.ClearSelection();
            from_cat();
            textBoxidCatg.Focus();
        }
        //seccion de registro de empleado/////////////////////////////
        private void buttonReg_Click(object sender, EventArgs e)
        {
            if (textBoxCiE.Text == "" || string.IsNullOrWhiteSpace(textBoxNE.Text) || string.IsNullOrWhiteSpace(textBoxApE.Text) 
                || textBoxTelE.Text == "" || string.IsNullOrWhiteSpace(textBoxDirE.Text))
            {
                MessageBox.Show("ERROR: A exepcion del apellido materno, todos los campos son obligatorios.");
                textBoxNE.Focus();
                return;
            }
            try
            {
                EmpleadoControlador.registrarEmpCon(Convert.ToInt32(textBoxCiE.Text), textBoxNE.Text, textBoxApE.Text, textBoxAmE.Text, Convert.ToInt32(textBoxTelE.Text), textBoxDirE.Text);
                dataGridViewListE.DataSource = EmpleadoControlador.listarEmpCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar Empleado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_Emp();
        }

        
        private void buttonMod_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCiE.Text))
            {
                MessageBox.Show("Por favor, selecciona un empleado de la lista primero.");
                return;
            }
            if (textBoxCiE.Text == "" || string.IsNullOrWhiteSpace(textBoxNE.Text) || string.IsNullOrWhiteSpace(textBoxApE.Text)
                || textBoxTelE.Text == "" || string.IsNullOrWhiteSpace(textBoxDirE.Text))
            {
                MessageBox.Show("ERROR: A exepcion del apellido materno, todos los campos son obligatorios.");
                textBoxNE.Focus();
                return;
            }

            try
            {
                
            EmpleadoControlador.modificarEmpCon(Convert.ToInt32(textBoxCiE.Text), textBoxNE.Text, textBoxApE.Text, textBoxAmE.Text, Convert.ToInt32(textBoxTelE.Text), textBoxDirE.Text);
            dataGridViewListE.DataSource = EmpleadoControlador.listarEmpCon();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            form_Emp();

        }

        private void dataGridViewListE_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            textBoxCiE.ReadOnly = true;
            textBoxNE.Focus();
            try
            {
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListE.Rows[selecRow];
                //carga de datos del DataGridView al fomulario
                textBoxCiE.Text = row.Cells[0].Value.ToString();
                textBoxNE.Text = row.Cells[1].Value.ToString();
                textBoxApE.Text = row.Cells[2].Value.ToString();
                textBoxAmE.Text = row.Cells[3].Value.ToString();
                textBoxTelE.Text = row.Cells[4].Value.ToString();
                textBoxDirE.Text = row.Cells[5].Value.ToString();
               

                buttonNuevo.Enabled = false;
                buttonReg.Enabled = false;
                buttonMod.Enabled = true;
                buttonElim.Enabled = true;
                buttonCancelar.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        public void limp_campos_emp()
        {
            textBoxCiE.Clear();
            textBoxNE.Clear();
            textBoxApE.Clear();
            textBoxAmE.Clear();
            textBoxTelE.Clear();
            textBoxDirE.Clear();
            textBoxCiE.ReadOnly = false;
        }
        public void form_Emp() 
        {
            groupBoxRegE.Enabled = false;
            groupBoxListaE.Enabled = false;
            buttonNuevo.Enabled = true;
            buttonReg.Enabled = false;
            buttonMod.Enabled = false;
            buttonElim.Enabled = false;
            buttonCancelar.Enabled = false;
            limp_campos_emp();
        }

        private void buttonNuevo_Click(object sender, EventArgs e)
        {
            dataGridViewListE.DataSource = EmpleadoControlador.listarEmpCon();
            groupBoxRegE.Enabled = true;
            groupBoxListaE.Enabled = true;
            buttonNuevo.Enabled = false;
            buttonReg.Enabled = true;
            buttonMod.Enabled = false;
            buttonElim.Enabled = false;
            buttonCancelar.Enabled = true;
            limp_campos_emp();
            textBoxCiE.Focus();
        }

        private void buttonElim_Click(object sender, EventArgs e)
        {
            if (dataGridViewListE.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un Empleado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            //confirmar eliminacion
            DialogResult confirm = MessageBox.Show("¿Esta seguro de eliminar este registro?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                EmpleadoControlador.eliminarEmpCon(Convert.ToInt32(textBoxCiE.Text), textBoxNE.Text, textBoxApE.Text, textBoxAmE.Text, Convert.ToInt32(textBoxTelE.Text), textBoxDirE.Text);
                dataGridViewListE.DataSource = EmpleadoControlador.listarEmpCon();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar Producto {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            form_Emp();
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            limp_campos_emp();
            dataGridViewListE.ClearSelection();
            form_Emp();
            textBoxCiE.Focus();
        }

        
        //Seccion de registro de Cliente //////////////////////////////////
        public void form_Cliente() 
        {
            groupBoxRegCli.Enabled = false;
            groupBoxListCli.Enabled = false;
            buttonNewCli.Enabled = true;
            buttonRegCli.Enabled = false;
            buttonModCli.Enabled = false;
            buttonElimCli.Enabled = false;
            buttonCanCli.Enabled = false;
            limp_campos_cliente();
        }

        public void limp_campos_cliente() 
        {
            textBoxciCli.Clear();
            textBoxnomCli.Clear();
            textBoxtelCli.Clear();
            textBoxciCli.ReadOnly = false;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBoxciCli.Text == "" || string.IsNullOrWhiteSpace(textBoxnomCli.Text) || string.IsNullOrWhiteSpace(textBoxtelCli.Text))
            {
                MessageBox.Show("ERROR: todos los campos son obligatorios.");
                textBoxciCli.Focus();
                return;
            }
            try
            {
                ClienteControlador.registrarCliCon(Convert.ToInt32(textBoxciCli.Text), textBoxnomCli.Text, textBoxtelCli.Text);
                dataGridViewListCli.DataSource = ClienteControlador.listarCliCon();
                dataGridViewFC.DataSource = ClienteControlador.listarCliCon();
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar Cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_Cliente();
        }


        private void buttonNewCli_Click(object sender, EventArgs e)
        {
            dataGridViewListCli.DataSource = ClienteControlador.listarCliCon();
            dataGridViewFC.DataSource = ClienteControlador.listarCliCon();
            limp_campos_cliente();
            textBoxciCli.Focus();
            groupBoxRegCli.Enabled = true;
            groupBoxListCli.Enabled = true;
            buttonNewCli.Enabled = false;
            buttonRegCli.Enabled = true;
            buttonModCli.Enabled = false;
            buttonElimCli.Enabled = false;
            buttonCanCli.Enabled = true;
        }

        private void buttonModCli_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxciCli.Text))
            {
                MessageBox.Show("Por favor, selecciona un cliente de la lista primero.");
                return;
            }
            if (textBoxciCli.Text == "" || string.IsNullOrWhiteSpace(textBoxnomCli.Text) || string.IsNullOrWhiteSpace(textBoxtelCli.Text))
            {
                MessageBox.Show("ERROR:  todos los campos son obligatorios.");
                textBoxnomCli.Focus();
                return;
            }

            try
            {
                ClienteControlador.modificarCliCon(Convert.ToInt32(textBoxciCli.Text), textBoxnomCli.Text, textBoxtelCli.Text);
                dataGridViewListCli.DataSource = ClienteControlador.listarCliCon();
                dataGridViewFC.DataSource = ClienteControlador.listarCliCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            form_Cliente();

        }

        private void buttonElimCli_Click(object sender, EventArgs e)
        {
            if (dataGridViewListCli.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un Cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            //confirmar eliminacion
            DialogResult confirm = MessageBox.Show("¿Esta seguro de eliminar este registro?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                ClienteControlador.eliminarCliCon(Convert.ToInt32(textBoxciCli.Text), textBoxnomCli.Text, textBoxtelCli.Text);
                dataGridViewListCli.DataSource = ClienteControlador.listarCliCon();
                dataGridViewFC.DataSource = ClienteControlador.listarCliCon();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar Cliente {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            form_Cliente();
        }

        private void buttonCanCli_Click(object sender, EventArgs e)
        {
            limp_campos_cliente();
            dataGridViewListCli.ClearSelection();
            form_Cliente();
            textBoxciCli.Focus();
        }

        private void dataGridViewListCli_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            textBoxciCli.ReadOnly = true;
            textBoxnomCli.Focus();
            try
            {
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCli.Rows[selecRow];
                textBoxciCli.Text = row.Cells[0].Value.ToString();
                textBoxnomCli.Text = row.Cells[1].Value.ToString();
                textBoxtelCli.Text = row.Cells[2].Value.ToString();
                buttonNewCli.Enabled = false;
                buttonRegCli.Enabled = false;
                buttonModCli.Enabled = true;
                buttonElimCli.Enabled = true;
                buttonCanCli.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }
        //Seccion de registro de Proveedor /////////////////////////////////////////
        public void form_prov() 
        {
            groupBoxRegProv.Enabled = false;
            groupBoxListProv.Enabled = false;
            buttonNueProv.Enabled = true;
            buttonRegProv.Enabled = false;
            buttonModProv.Enabled = false;
            buttonElimProv.Enabled = false;
            buttonCanProv.Enabled = false;
            limp_campos_prov();
        }
        public void limp_campos_prov()
        {
            textBoxIdProv.Clear();
            textBoxNomProv.Clear();
            textBoxCorreoProv.Clear();
            textBoxDirProv.Clear();
            textBoxTelProv.Clear();
            textBoxIdProv.ReadOnly = false;

        }
        private void buttonNueProv_Click(object sender, EventArgs e)
        {
            groupBoxRegProv.Enabled = true;
            groupBoxListProv.Enabled = true;
            buttonNueProv.Enabled = false;
            buttonRegProv.Enabled = true;
            buttonModProv.Enabled = false;
            buttonElimProv.Enabled = false;
            buttonCanProv.Enabled = true;
            dataGridViewListProv.DataSource = ProveedorControlador.listarProvCon();
            dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();
        }

        private void buttonRegProv_Click(object sender, EventArgs e)
        {
            if (textBoxIdProv.Text == "" || string.IsNullOrWhiteSpace(textBoxNomProv.Text) || string.IsNullOrWhiteSpace(textBoxCorreoProv.Text) || string.IsNullOrWhiteSpace(textBoxDirProv.Text)|| textBoxTelProv.Text == "")
            {
                MessageBox.Show("ERROR: todos los campos son obligatorios.");
                textBoxIdProv.Focus();
                return;
            }
            try
            {
                ProveedorControlador.registrarProvCon(Convert.ToInt32(textBoxIdProv.Text), textBoxNomProv.Text, textBoxCorreoProv.Text, textBoxDirProv.Text, Convert.ToInt32(textBoxTelProv.Text));
                dataGridViewListProv.DataSource = ProveedorControlador.listarProvCon();
                dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar Proveedor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_prov();
        }

        private void buttonModProv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxIdProv.Text))
            {
                MessageBox.Show("Por favor, selecciona un Proveedor de la lista primero.");
                return;
            }
            if (textBoxIdProv.Text == "" || string.IsNullOrWhiteSpace(textBoxNomProv.Text) || string.IsNullOrWhiteSpace(textBoxCorreoProv.Text) || string.IsNullOrWhiteSpace(textBoxDirProv.Text) || textBoxTelProv.Text == "")
            {
                MessageBox.Show("ERROR: todos los campos son obligatorios.");
                textBoxIdProv.Focus();
                return;
            }

            try
            {
                ProveedorControlador.modificarProvCon(Convert.ToInt32(textBoxIdProv.Text), textBoxNomProv.Text, textBoxCorreoProv.Text, textBoxDirProv.Text, Convert.ToInt32(textBoxTelProv.Text));
                dataGridViewListProv.DataSource = ProveedorControlador.listarProvCon();
                dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            form_prov();
        }

        private void buttonElimProv_Click(object sender, EventArgs e)
        {
            if (dataGridViewListProv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un Proveedor", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            //confirmar eliminacion
            DialogResult confirm = MessageBox.Show("¿Esta seguro de eliminar este registro?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                ProveedorControlador.eliminarProvCon(Convert.ToInt32(textBoxIdProv.Text), textBoxNomProv.Text, textBoxCorreoProv.Text, textBoxDirProv.Text, Convert.ToInt32(textBoxTelProv.Text));
                dataGridViewListProv.DataSource = ProveedorControlador.listarProvCon();
                dataGridViewListPC.DataSource = ProveedorControlador.listarProvCon();

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar Proveedor {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            form_prov();
        }

        private void buttonCanProv_Click(object sender, EventArgs e)
        {
            limp_campos_prov();
            dataGridViewListProv.ClearSelection();
            form_prov();
            textBoxIdProv.Focus();
        }

        private void dataGridViewListProv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            textBoxIdProv.ReadOnly = true;
            textBoxNomProv.Focus();
            try
            {
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListProv.Rows[selecRow];
                textBoxciCli.Text = row.Cells[0].Value.ToString();
                textBoxnomCli.Text = row.Cells[1].Value.ToString();
                textBoxtelCli.Text = row.Cells[2].Value.ToString();
                buttonNueProv.Enabled = false;
                buttonRegProv.Enabled = false;
                buttonModProv.Enabled = true;
                buttonElimProv.Enabled = true;
                buttonCanProv.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }
        //registro de seccion de usuario//////////////////////////////////////////////////
        public void form_usuario() 
        {
            groupBoxListU.Enabled = false;
            groupBoxListCiE.Enabled = false;
            groupBoxRegU.Enabled = false;
            buttonNU.Enabled =true;
            buttonRegU.Enabled = false;
            buttonModU.Enabled = false;
            buttonElimU.Enabled = false;
            buttonCanProv.Enabled = false;
            limp_campos_usuario();
        }

        public void limp_campos_usuario() 
        {
            textBoxCiU.Clear();
            textBoxnomU.Clear();
            textBoxcontU.Clear();
            textBoxcargoU.Clear();
            textBoxCiU.ReadOnly = true;
        }
        private void buttonNU_Click(object sender, EventArgs e)
        {
            dataGridViewListCiE.DataSource = EmpleadoControlador.listarEmpCon();
            dataGridViewListU.DataSource = UsuarioControlador.listarUCon();
            groupBoxListU.Enabled = true;
            groupBoxListCiE.Enabled = true;
            groupBoxRegU.Enabled = true;
            buttonNU.Enabled = false;
            buttonRegU.Enabled = true;
            buttonModU.Enabled = false;
            buttonElimU.Enabled = false;
            buttonCanProv.Enabled = true;

        }

        private void buttonRegU_Click(object sender, EventArgs e)
        {
            if (textBoxCiU.Text == "" || string.IsNullOrWhiteSpace(textBoxnomU.Text) || string.IsNullOrWhiteSpace(textBoxcontU.Text) || string.IsNullOrWhiteSpace(textBoxcargoU.Text) )
            {
                MessageBox.Show("ERROR: todos los campos son obligatorios.");
                textBoxnomU.Focus();
                return;
            }
            try
            {
                UsuarioControlador.registrarUCon(textBoxnomU.Text, textBoxcontU.Text, textBoxcargoU.Text, Convert.ToInt32(textBoxCiU.Text));
                dataGridViewListU.DataSource = UsuarioControlador.listarUCon();
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_usuario();
        }

        private void buttonModU_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCiU.Text))
            {
                MessageBox.Show("Por favor, selecciona un Usuario de la lista primero.");
                return;
            }
            if (textBoxCiU.Text == "" || string.IsNullOrWhiteSpace(textBoxnomU.Text) || string.IsNullOrWhiteSpace(textBoxcontU.Text) || string.IsNullOrWhiteSpace(textBoxcargoU.Text))
            {
                MessageBox.Show("ERROR: todos los campos son obligatorios.");
                textBoxnomU.Focus();
                return;
            }

            try
            {
                UsuarioControlador.modificarUCon(textBoxnomU.Text, textBoxcontU.Text, textBoxcargoU.Text, Convert.ToInt32(textBoxCiU.Text));
                dataGridViewListU.DataSource = UsuarioControlador.listarUCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message);
            }
            form_usuario();
        }

        private void buttonElimU_Click(object sender, EventArgs e)
        {
            if (dataGridViewListU.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor seleccione un Usuario", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            //confirmar eliminacion
            DialogResult confirm = MessageBox.Show("¿Esta seguro de eliminar este registro?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                UsuarioControlador.eliminarUCon(textBoxnomU.Text, textBoxcontU.Text, textBoxcargoU.Text, Convert.ToInt32(textBoxCiU.Text));
                dataGridViewListU.DataSource = UsuarioControlador.listarUCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error al eliminar Usuario {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            form_usuario();
        }

        private void buttonCanU_Click(object sender, EventArgs e)
        {
            limp_campos_usuario();
            dataGridViewListU.ClearSelection();
            dataGridViewListCiE.ClearSelection();
            form_usuario();
            textBoxnomU.Focus();
        }

        private void dataGridViewListU_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            textBoxnomU.Focus();
            try
            {
                limp_campos_usuario();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListU.Rows[selecRow];
                textBoxCiU.Text = row.Cells[0].Value.ToString();
                textBoxnomU.Text = row.Cells[1].Value.ToString();
                textBoxcontU.Text = row.Cells[2].Value.ToString();
                textBoxcargoU.Text = row.Cells[3].Value.ToString();
                buttonNU.Enabled = false;
                buttonRegU.Enabled = false;
                buttonModU.Enabled = true;
                buttonElimU.Enabled = true;
                buttonCanU.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCiE_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            textBoxnomU.Focus();
            try
            {
                limp_campos_usuario();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCiE.Rows[selecRow];
                textBoxCiU.Text = row.Cells[0].Value.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }
        //Seccion de registro de nueva ventaaa ////////////////////////////////////////////////////////////////
        public void limp_campos_venta()
        {
            textBoxCodV.Clear();
            textBoxNV.Clear();
            textBoxPV.Clear();
            textBoxSV.Clear();
            pictureBoxFV.Image = null;
            textBoxCodV.ReadOnly = true;
            textBoxNV.ReadOnly = true;
            textBoxPV.ReadOnly = true;
            numericUpDownV.ReadOnly = true;
            textBoxSV.ReadOnly = true;

        }
        public void form_venta() 
        {
            groupBoxInf.Enabled = false;
            groupBoxDetalleV.Enabled = false;
            groupBoxFact.Enabled = false;
            limp_campos_venta();
        }
        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            if (textBoxPV.Text == "" )
            {
                MessageBox.Show("ERROR: seleccione un producto para calcular el subtotal.");
                return;
            }
            try
            {
                textBoxSV.Clear();
                float precio = float.Parse(textBoxPV.Text);
                float subttotal = precio * Convert.ToInt32(numericUpDownV.Value);
                textBoxSV.Text = subttotal.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewListDV.Rows.Count <= 1)
            {
                MessageBox.Show("ERROR: No existen productos en la lista para facturar","Aviso", MessageBoxButtons.OK,MessageBoxIcon.Warning );
                return;
            }
           
            foreach (TabPage tab in todasLasPestañas)
            {
                if (tab.Name != "facturacion")
                    tab.Parent = null;
                else if (tab.Parent == null)
                    tabControl1.TabPages.Add(tab);
            }

            tabControl1.SelectedTab = facturacion;
            
            string nombreE = EmpleadoModelo.ObtenerNombreUsuario(usuario);
            limp_campos_fact();
            textBoxENF.Text= nombreE.ToString();
            dataGridViewFDV.DataSource = null;
            dataGridViewFDV.DataSource = listDV;
            float total= 0;
            foreach (DetallaVentaModelo det in listDV) 
            {
                total += det.subtotal;
            }
            textBoxTotal.Text = total.ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿Está seguro de cancelar la venta? El stock se restaurará.",
                                       "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                try
                { 
                    foreach (DetallaVentaModelo det in listDV)
                    {
                        InventarioModelo.RestaurarStock(det.IdProd, det.cant);
                    }
                    dataGridViewListDV.Rows.Clear();
                    listDV.Clear();
                    limp_campos_venta(); 
                    MessageBox.Show("Venta cancelada y stock restaurado.");
                    dataGridViewListI.DataSource = InventarioControlador.listarInCon();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cancelar: " + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxCodV.Text == "" || textBoxPV.Text == "")
            {
                MessageBox.Show("ERROR: seleccione un producto para agregarlo");
                return;
            }
            try
            {
                int idProdActual = Convert.ToInt32(textBoxCodV.Text);
                int cantidadNueva = Convert.ToInt32(numericUpDownV.Value);
                float precio = float.Parse(textBoxPV.Text);
                if (InventarioModelo.DescontarStock(idProdActual, cantidadNueva))
                {
                    var detalleExistente = listDV.FirstOrDefault(d => d.IdProd == idProdActual);

                    if (detalleExistente != null)
                    {
                        detalleExistente.cant += cantidadNueva;
                        detalleExistente.subtotal = detalleExistente.cant * detalleExistente.precio;
                        foreach (DataGridViewRow fila in dataGridViewListDV.Rows)
                        {
                            if (Convert.ToInt32(fila.Cells[0].Value) == idProdActual)
                            {
                                fila.Cells[2].Value = detalleExistente.cant; 
                                fila.Cells[4].Value = detalleExistente.subtotal; 
                                break;
                            }
                        }
                    }
                    else
                    {
                        float subtotal = precio * cantidadNueva;
                        DetallaVentaModelo nuevoDetalle = new DetallaVentaModelo();
                        nuevoDetalle.IdProd = idProdActual;
                        nuevoDetalle.cant = cantidadNueva;
                        nuevoDetalle.precio = precio;
                        nuevoDetalle.subtotal = subtotal;
                        nuevoDetalle.IdVenta = 0;
                        listDV.Add(nuevoDetalle);
                        dataGridViewListDV.Rows.Add(idProdActual, textBoxNV.Text, cantidadNueva, precio, subtotal);
                    }
                    dataGridViewListI.DataSource = InventarioControlador.listarInCon();
                    limp_campos_venta();
                }
                else
                {
                    
                    MessageBox.Show("No hay suficiente stock en inventario para este producto.",
                                    "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void dataGridViewListCM_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCM.Rows[selecRow];
                textBoxCodV.Text = row.Cells[0].Value.ToString();
                textBoxNV.Text = row.Cells[2].Value.ToString();
                textBoxPV.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFV.Image != null)
                {
                    pictureBoxFV.Image.Dispose();
                    pictureBoxFV.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFV.Image = img;
                    pictureBoxFV.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCMa_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCMa.Rows[selecRow];
                textBoxCodV.Text = row.Cells[0].Value.ToString();
                textBoxNV.Text = row.Cells[2].Value.ToString();
                textBoxPV.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFV.Image != null)
                {
                    pictureBoxFV.Image.Dispose();
                    pictureBoxFV.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFV.Image = img;
                    pictureBoxFV.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCP_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCP.Rows[selecRow];
                textBoxCodV.Text = row.Cells[0].Value.ToString();
                textBoxNV.Text = row.Cells[2].Value.ToString();
                textBoxPV.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFV.Image != null)
                {
                    pictureBoxFV.Image.Dispose();
                    pictureBoxFV.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFV.Image = img;
                    pictureBoxFV.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCN_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCN.Rows[selecRow];
                textBoxCodV.Text = row.Cells[0].Value.ToString();
                textBoxNV.Text = row.Cells[2].Value.ToString();
                textBoxPV.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFV.Image != null)
                {
                    pictureBoxFV.Image.Dispose();
                    pictureBoxFV.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFV.Image = img;
                    pictureBoxFV.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }
        //SECCION DE FACTURACION////////////////////////////////////////////////////////////////////
        public void limp_campos_fact() 
        {
            textBoxCiF.Clear();
            textBoxNCF.Clear();
            textBoxENF.Clear();
            textBoxTotal.Clear();
            textBoxCiF.ReadOnly = true;
            textBoxNCF.ReadOnly = true;
            textBoxENF.ReadOnly = true;
            textBoxTotal.ReadOnly = true;
            
        }

        private void dataGridViewFC_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                textBoxCiF.Clear();
                textBoxNCF.Clear();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewFC.Rows[selecRow];
                textBoxCiF.Text = row.Cells[0].Value.ToString();
                textBoxNCF.Text = row.Cells[1].Value.ToString();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (textBoxCiF.Text == "" || textBoxNCF.Text == "")
            {
                MessageBox.Show("ERROR: seleccione un cliente para facturar");
                return;
            }
            try
            {
                string fechaFormateada = dateTimePickerF.Value.ToString("yyyy/MM/dd");
               
                int nuevoIdVenta = VentaControlador.registrarVCon(
                    usuario,
                    Convert.ToInt32(textBoxCiF.Text),
                    Convert.ToDateTime(fechaFormateada),
                    float.Parse(textBoxTotal.Text)
                );

                if (nuevoIdVenta > 0)
                {
                    foreach (DetallaVentaModelo det in listDV)
                    {
                        det.IdVenta = nuevoIdVenta; 
                        DetalleVentaControlador.registrarDVCon(det.IdProd, det.cant, det.precio, det.subtotal, det.IdVenta);
                    }
                    MessageBox.Show("Venta y detalles registrados con éxito.");
                    dataGridViewListDV.Rows.Clear();
                    dataGridViewListaVenta.DataSource = VentaModelo.ListarVenta();
                    listDV.Clear();
                }

                foreach (TabPage tab in todasLasPestañas)
                {
                    if ((tab.Name != "InSesion" && tab.Parent == null) && (tab.Name != "facturacion" && tab.Parent == null))
                        tabControl1.TabPages.Add(tab);
                }

                facturacion.Parent = null;
                tabControl1.SelectedTab = Nventa;
                limp_campos_venta();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al facturar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancelarF_Click(object sender, EventArgs e)
        {
            limp_campos_fact();
            foreach (TabPage tab in todasLasPestañas)
            {
                if ((tab.Name != "InSesion" && tab.Parent == null)&& (tab.Name != "facturacion" && tab.Parent == null))
                    tabControl1.TabPages.Add(tab);
            }
            facturacion.Parent = null;
            tabControl1.SelectedTab = Nventa;
        }

        // SECCION DE INVENTARIO//////////////////////////////////////////////////////////////////



        // Asegúrate de que este método esté en el formulario (FrmLogin.cs o un formulario hijo)
        private void CargarReporteVentas()
    {
        // 1. Configuración de la Conexión y la Consulta
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaFarmaceutico;Integrated Security=True;Trust Server Certificate=True";
        // La consulta debe traer los campos exactos que usaste en el RDLC (Id_nro_Venta, IdEmpleado, IdCliente, fecha, Total)
        string query = "SELECT Id_nro_Venta, IdEmpleado, IdCliente, fecha, Total FROM Venta";

        DataTable dtVentas = new DataTable();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dtVentas); // Llena el DataTable con los datos de SQL Server
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al cargar datos del reporte: " + ex.Message);
            return;
        }

        // --- 2. Asignar los datos al ReportViewer ---

        // 2.1 Limpiar y preparar el ReportViewer (asumiendo que tu control se llama 'reportViewer1')
        reportViewer1.LocalReport.ReportPath = "Report1.rdlc"; // Ruta de tu archivo RDLC
        reportViewer1.LocalReport.DataSources.Clear();

        // 2.2 Crear el ReportDataSource
        // ATENCIÓN: "DataSetVentas" debe coincidir EXACTAMENTE con el nombre de tu DataSet en el diseño RDLC
        ReportDataSource rds = new ReportDataSource("DataSetVentas", dtVentas);

        // 2.3 Asignar y Refrescar
        reportViewer1.LocalReport.DataSources.Add(rds);
        reportViewer1.RefreshReport();
    }
        public void form_invent() 
        {
            groupBoxListPi.Enabled = false;
            groupBoxListIn.Enabled = false;
            groupBoxRegI.Enabled = false;
            buttonNi.Enabled = true;
            buttonRi.Enabled = false;
            buttonMi.Enabled = false;
            buttonEi.Enabled = false;
            buttonCi.Enabled = false;
            limp_campos_invent();
        }
        public void limp_campos_invent() 
        {
            textBoxIdPi.Clear();
            textBoxNPi.Clear();
            textBoxSi.Clear();
            textBoxIdPi.ReadOnly=true;
            textBoxNPi.ReadOnly=true;
        }
        private void buttonNi_Click(object sender, EventArgs e)
        {
            dataGridViewListI.DataSource = InventarioControlador.listarInCon();
            groupBoxListPi.Enabled = true;
            groupBoxListIn.Enabled = true;
            groupBoxRegI.Enabled = true;
            buttonNi.Enabled = false;
            buttonRi.Enabled = true;
            buttonMi.Enabled = false;
            buttonEi.Enabled = false;
            buttonCi.Enabled = true;
            refresh();
        }

        private void buttonRi_Click(object sender, EventArgs e)
        {
            if (textBoxIdPi.Text == "" || textBoxSi.Text == "")
            {
                MessageBox.Show("ERROR: A exepcion del nombre, todos los campos son obligatorios ");
                return;
            }
            try
            {
                int resultado = InventarioControlador.registrarInCon(Convert.ToInt32(textBoxIdPi.Text),Convert.ToInt32(textBoxSi.Text) );

                if (resultado > 0)
                {
                    MessageBox.Show("Producto registrado en inventario exitosamente.");
                    dataGridViewListI.DataSource = InventarioControlador.listarInCon();
                }
                else
                {
                    MessageBox.Show("El producto ya se encuentra registrado en el inventario.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_invent();
        }

        private void buttonMi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxIdPi.Text))
            {
                MessageBox.Show("Por favor, selecciona un Item de la lista primero.");
                return;
            }
            if (textBoxIdPi.Text == "" || textBoxSi.Text == "")
            {
                MessageBox.Show("ERROR: A exepcion del nombre, todos los campos son obligatorios ");
                return;
            }
            try
            {
                InventarioControlador.modificarInCon(Convert.ToInt32(textBoxIdPi.Text),  Convert.ToInt32(textBoxSi.Text));
                dataGridViewListI.DataSource = InventarioControlador.listarInCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_invent();
        }

        private void buttonEi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxIdPi.Text))
            {
                MessageBox.Show("Por favor, selecciona un Item de la lista primero.");
                return;
            }
            try
            {
                InventarioControlador.eliminarInCon(Convert.ToInt32(textBoxIdPi.Text), Convert.ToInt32(textBoxSi.Text));
                dataGridViewListI.DataSource = InventarioControlador.listarInCon();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            form_invent();
        }

        private void buttonCi_Click(object sender, EventArgs e)
        {
            form_invent();
        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxListPi_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewListProd_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                textBoxIdPi.Clear();
                textBoxNPi.Clear();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListProd.Rows[selecRow];
                textBoxIdPi.Text = row.Cells[0].Value.ToString();
                textBoxNPi.Text = row.Cells[2].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListI_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                textBoxIdPi.Clear();
                textBoxSi.Clear();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListI.Rows[selecRow];
                textBoxIdPi.Text = row.Cells[0].Value.ToString();
                textBoxSi.Text = row.Cells[1].Value.ToString();

                buttonNi.Enabled = false;
                buttonRi.Enabled = false;
                buttonMi.Enabled = true;
                buttonEi.Enabled = true;
                buttonCi.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void groupBoxFact2_Enter(object sender, EventArgs e)
        {

        }
        /// seccion de compra//////////////////////////////////////

        public void limp_campos_compra() 
        {
            textBoxCodC.Clear();
            textBoxNC.Clear();
            textBoxPC.Clear();
            textBoxSC.Clear();
            pictureBoxFC.Image = null;
            textBoxCodC.ReadOnly = true;
            textBoxNC.ReadOnly = true;
            textBoxPC.ReadOnly = true;
            numericUpDownC.ReadOnly = true;
            textBoxSC.ReadOnly = true;
        }
        
        private void dataGridViewListCM2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCM2.Rows[selecRow];
                textBoxCodC.Text = row.Cells[0].Value.ToString();
                textBoxNC.Text = row.Cells[2].Value.ToString();
                textBoxPC.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFC.Image != null)
                {
                    pictureBoxFC.Image.Dispose();
                    pictureBoxFC.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFC.Image = img;
                    pictureBoxFC.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCMa2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCMa2.Rows[selecRow];
                textBoxCodC.Text = row.Cells[0].Value.ToString();
                textBoxNC.Text = row.Cells[2].Value.ToString();
                textBoxPC.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFC.Image != null)
                {
                    pictureBoxFC.Image.Dispose();
                    pictureBoxFC.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFC.Image = img;
                    pictureBoxFC.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCP2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCP2.Rows[selecRow];
                textBoxCodC.Text = row.Cells[0].Value.ToString();
                textBoxNC.Text = row.Cells[2].Value.ToString();
                textBoxPC.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFC.Image != null)
                {
                    pictureBoxFC.Image.Dispose();
                    pictureBoxFC.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFC.Image = img;
                    pictureBoxFC.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void dataGridViewListCN2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListCN2.Rows[selecRow];
                textBoxCodC.Text = row.Cells[0].Value.ToString();
                textBoxNC.Text = row.Cells[2].Value.ToString();
                textBoxPC.Text = row.Cells[3].Value.ToString();
                if (pictureBoxFC.Image != null)
                {
                    pictureBoxFC.Image.Dispose();
                    pictureBoxFC.Image = null;
                }
                int id = Convert.ToInt32(row.Cells["IdProd"].Value);
                Image img = Metodos.ObtenerImagenProductoSP(id);
                if (img != null)
                {
                    pictureBoxFC.Image = img;
                    pictureBoxFC.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void buttonCalC_Click(object sender, EventArgs e)
        {
            if (textBoxPC.Text == "")
            {
                MessageBox.Show("ERROR: seleccione un producto para calcular el subtotal.");
                return;
            }
            try
            {
                textBoxSC.Clear();
                float precio = float.Parse(textBoxPC.Text);
                float subttotal = precio * Convert.ToInt32(numericUpDownC.Value);
                textBoxSC.Text = subttotal.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBoxCodC.Text == "" || textBoxPC.Text == "")
            {
                MessageBox.Show("ERROR: seleccione un producto para comprarlo");
                return;
            }
            try
            {
                int idProdActual = Convert.ToInt32(textBoxCodC.Text);
                int cantidadNueva = Convert.ToInt32(numericUpDownC.Value);
                float precio = float.Parse(textBoxPC.Text);
                InventarioModelo.RestaurarStock(idProdActual, cantidadNueva);
                    var detalleExistente = listDC.FirstOrDefault(d => d.IdProducto == idProdActual);

                    if (detalleExistente != null)
                    {
                        detalleExistente.Cantidad += cantidadNueva;
                        detalleExistente.subtotal = detalleExistente.Cantidad * detalleExistente.precio;
                        foreach (DataGridViewRow fila in dataGridViewListDC.Rows)
                        {
                            if (Convert.ToInt32(fila.Cells[0].Value) == idProdActual)
                            {
                                fila.Cells[2].Value = detalleExistente.Cantidad;
                                fila.Cells[4].Value = detalleExistente.subtotal;
                                break;
                            }
                        }
                    }
                    else
                    {
                        float subtotal = precio * cantidadNueva;
                        DetalleCompraModelo nuevoDetalle = new DetalleCompraModelo();
                        nuevoDetalle.Cantidad = cantidadNueva;
                        nuevoDetalle.precio = precio;
                        nuevoDetalle.subtotal = subtotal;
                        nuevoDetalle.IdCompra = 0;
                        nuevoDetalle.IdProducto = idProdActual;
                        listDC.Add(nuevoDetalle);
                        dataGridViewListDC.Rows.Add(idProdActual, textBoxNC.Text, cantidadNueva, precio, subtotal);
                    }
                    dataGridViewListI.DataSource = InventarioControlador.listarInCon();
                    limp_campos_compra();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al calcular: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCan2_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿Está seguro de cancelar la compra? El stock se descontará del inventario.",
                               "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                try
                {
                    string alertasStock = "";

                    foreach (DetalleCompraModelo det in listDC)
                    {
                        bool bajoStock; 
                        bool seDesconto = InventarioModelo.DescontarStock(det.IdProducto, det.Cantidad, out bajoStock);

                        if (seDesconto)
                        {
                            if (bajoStock)
                            {
                                alertasStock += $"- ID Producto: {det.IdProducto} (Quedan 2 o menos)\n";
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No se pudo descontar el producto {det.IdProducto}. Stock insuficiente para la operación.");
                        }
                    }
                    dataGridViewListDC.Rows.Clear();
                    listDC.Clear();
                    dataGridViewListI.DataSource = InventarioControlador.listarInCon();

                    MessageBox.Show("Compra cancelada y stock actualizado.");
                    if (!string.IsNullOrEmpty(alertasStock))
                    {
                        MessageBox.Show("¡ADVERTENCIA DE INVENTARIO!\n\nLos siguientes productos han quedado con existencias mínimas:\n" + alertasStock,
                                        "Poco Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    limp_campos_compra(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cancelar la operación: " + ex.Message);
                }
            }
        }

        private void buttonFac2_Click(object sender, EventArgs e)
        {
            if (dataGridViewListDC.Rows.Count <= 1)
            {
                MessageBox.Show("ERROR: No existen productos en la lista para comprar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (TabPage tab in todasLasPestañas)
            {
                if (tab.Name != "requerimiento")
                    tab.Parent = null;
                else if (tab.Parent == null)
                    tabControl1.TabPages.Add(tab);
            }

            tabControl1.SelectedTab = requerimiento;
           
            string nombreE = EmpleadoModelo.ObtenerNombreUsuario(usuario);
            limp_campos_requerimiento();
            textBoxENR.Text = nombreE.ToString();
            dataGridViewListDCR.DataSource = null;
            dataGridViewListDCR.DataSource = listDC;
            float total = 0;
            foreach (DetalleCompraModelo det in listDC)
            {
                total += det.subtotal;
            }
            textBoxTC.Text = total.ToString();
        }
        // seccion de detalle de compra

        public void limp_campos_requerimiento() 
        {
            textBoxCiC.Clear();
            textBoxNR.Clear();
            textBoxENR.Clear();
            textBoxTC.Clear();

            textBoxCiC.ReadOnly = true;
            textBoxNR.ReadOnly = true;
            textBoxENR.ReadOnly = true;
            textBoxTC.ReadOnly = true;
        }
        private void dataGridViewListPC_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                textBoxCiC.Clear();
                textBoxNR.Clear();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListPC.Rows[selecRow];
                textBoxCiC.Text = row.Cells[0].Value.ToString();
                textBoxNR.Text = row.Cells[1].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }

        private void buttonFacturarC_Click(object sender, EventArgs e)
        {
            if (textBoxCiC.Text == "" || textBoxNR.Text == "")
            {
                MessageBox.Show("ERROR: seleccione un Proveedor para facturar");
                return;
            }
            try
            {
                string fechaFormateada = dateTimePickerC.Value.ToString("yyyy/MM/dd");

                int nuevoIdCompra = CompraControlador.registrarCCon(
                    
                    Convert.ToInt32(textBoxCiC.Text), usuario,
                    Convert.ToDateTime(fechaFormateada),
                    float.Parse(textBoxTC.Text)
                );

                if (nuevoIdCompra > 0)
                {
                    foreach (DetalleCompraModelo det in listDC)
                    {
                        det.IdCompra = nuevoIdCompra;
                        DetalleCompraControlador.registrarCmCon( det.Cantidad, det.precio, det.subtotal, det.IdCompra, det.IdProducto);
                    }
                    MessageBox.Show("Venta y detalles registrados con éxito.");
                    dataGridViewListDC.Rows.Clear();
                    listDC.Clear();
                }

                foreach (TabPage tab in todasLasPestañas)
                {
                    if ((tab.Name != "InSesion" && tab.Parent == null) && (tab.Name != "requerimiento" && tab.Parent == null))
                        tabControl1.TabPages.Add(tab);
                }

                requerimiento.Parent = null;
                tabControl1.SelectedTab = Nventa;
                limp_campos_compra();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al facturar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancelarC_Click(object sender, EventArgs e)
        {
            limp_campos_requerimiento();
            foreach (TabPage tab in todasLasPestañas)
            {
                if ((tab.Name != "InSesion" && tab.Parent == null) && (tab.Name != "facturacion" && tab.Parent == null) && (tab.Name != "requerimiento" && tab.Parent == null))
                    tabControl1.TabPages.Add(tab);
            }
            requerimiento.Parent = null;
            tabControl1.SelectedTab = Nventa;
        }
        public void GenerarReporte()
        {

            string cadenaConexion = "Data Source=PEATRINNE1\\SQLEXPRESS; Initial Catalog=SistemaFarmaceutico; Integrated Security=True";

            string consulta = "SELECT Id_nro_Venta, IdEmpleado, IdCliente, fecha, Total FROM Ventas";

            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta, con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                reportViewer1.LocalReport.DataSources.Clear();


                ReportDataSource rds = new ReportDataSource("DataSetVentas", dt);
                reportViewer1.LocalReport.DataSources.Add(rds);
                //reportViewer1.LocalReport.DataSources.Add(rds);
              


                reportViewer1.RefreshReport();
            }
        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewListaVenta_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                limp_campos_venta();
                selecRow = e.RowIndex;
                DataGridViewRow row = dataGridViewListaVenta.Rows[selecRow];
                textBoxIDV.Text = row.Cells[0].Value.ToString();
                
                string nombreE = EmpleadoModelo.ObtenerNombreUsuario(usuario);
                textBoxCajero.Text = nombreE;
                textBoxNIT.Text = row.Cells[2].Value.ToString();
                string nombreCli = ClienteModelo.ObtenerNombreUsuario(Convert.ToInt32(textBoxNIT.Text));

                textBoxNCliente.Text = nombreCli;
                textBoxFV.Text = row.Cells[3].Value.ToString();
                textBoxTV.Text = row.Cells[4].Value.ToString();
                dataGridViewLDV.DataSource = DetallaVentaModelo.ListarDVenta();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: {0}", ex.Message);
            }
        }
    }
}

