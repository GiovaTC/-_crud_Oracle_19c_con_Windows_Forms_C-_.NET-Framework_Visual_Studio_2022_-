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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection cn =
                    conexion.ObtenerConexion())
                {
                    string sql =
                        @"INSERT INTO CLIENTES_J 
                        (NOMBRE, EMAIL, TELEFONO)
                        VALUES
                        (:NOMBRE, :EMAIL, :TELEFONO)";

                    OracleCommand cmd =
                        new OracleCommand(sql, cn);

                    cmd.Parameters.Add(":NOMBRE",
                        txtNombre.Text);

                    cmd.Parameters.Add(":EMAIL",
                        txtEmail.Text);

                    cmd.Parameters.Add(":TELEFONO",
                        txtTelefono.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("registro guardado! ");

                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection cn =
                    conexion.ObtenerConexion())
                {
                    string sql =
                        "SELECT * FROM CLIENTES_J WHERE ID = :ID";

                    OracleCommand cmd =
                        new OracleCommand(sql, cn);

                    cmd.Parameters.Add(":ID",
                        txtId.Text);

                    OracleDataReader dr =
                        cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        txtNombre.Text =
                            dr["NOMBRE"].ToString();

                        txtEmail.Text =
                            dr["EMAIL"].ToString();

                        txtTelefono.Text =
                            dr["TELEFONO"].ToString();
                    }
                    else
                    {
                        MessageBox.Show(
                            "registro no encontrado! ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(
            object sender, 
            EventArgs e)
        {
            try
            {
                using (OracleConnection cn =
                    conexion.ObtenerConexion())
                {
                    string sql =
                        @"UPDATE CLIENTES_J 
                        SET NOMBRE = :NOMBRE,
                            EMAIL = :EMAIL,
                            TELEFONO = :TELEFONO
                        WHERE ID = :ID";

                    OracleCommand cmd =
                        new OracleCommand(sql, cn);
                    
                    cmd.Parameters.Add(":NOMBRE",
                        txtNombre.Text);
                    
                    cmd.Parameters.Add(":EMAIL",
                        txtEmail.Text);
                    
                    cmd.Parameters.Add(":TELEFONO",
                        txtTelefono.Text);
                    
                    cmd.Parameters.Add(":ID",
                        txtId.Text);
                    
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show(
                        "registro actualizado! ");
                    
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

            
    }
}
