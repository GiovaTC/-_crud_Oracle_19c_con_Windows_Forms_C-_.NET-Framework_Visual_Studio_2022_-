using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace crud_oracle_19c
{
    public partial class Form1 : Form
    {
        ConexionOracle conexion = 
            new ConexionOracle();
        public Form1()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                using (OracleConnection cn = 
                    conexion.ObtenerConexion())
                {
                    string sql = 
                        "SELECT * FROM CLIENTES_J ORDER BY ID";

                    OracleDataAdapter da = 
                        new OracleDataAdapter(sql, cn);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dgvClientes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
