# -_crud_Oracle_19c_con_Windows_Forms_C-_.NET-Framework_Visual_Studio_2022_- :.
CRUD Oracle 19c con Windows Forms C# (.NET Framework) - Visual Studio 2022:

<img width="1536" height="1024" alt="image" src="https://github.com/user-attachments/assets/1920300d-a764-4932-8365-a91edbaa46a3" />  

```

Proyecto completo para Visual Studio 2022 (Windows Forms C# .NET Framework) que implementa un CRUD con Oracle 19c, utilizando:
Windows Forms (Interfaz gráfica)
DataGridView (equivalente al JTable de Java)
Oracle Database 19c
Oracle Managed Data Access (Oracle.ManagedDataAccess)

4 Botones:
Guardar
Buscar
Actualizar
Eliminar

DataGridView para visualizar registros
1. Base de Datos Oracle 19c
Crear Tabla
CREATE TABLE CLIENTES_J
(
    ID NUMBER GENERATED ALWAYS AS IDENTITY,
    NOMBRE VARCHAR2(100),
    EMAIL VARCHAR2(100),
    TELEFONO VARCHAR2(20),

    CONSTRAINT PK_CLIENTES_J
    PRIMARY KEY(ID)
);

Datos de Prueba
INSERT INTO CLIENTES_J(NOMBRE,EMAIL,TELEFONO)
VALUES('Juan Perez','juan@gmail.com','3001111111');

INSERT INTO CLIENTES_J(NOMBRE,EMAIL,TELEFONO)
VALUES('Maria Gomez','maria@gmail.com','3002222222');

COMMIT;

2. Instalar Oracle Driver
Desde la consola de NuGet:
Install-Package Oracle.ManagedDataAccess
o
Install-Package Oracle.ManagedDataAccess.Core

3. Diseño del Formulario
Controles
Labels
ID
Nombre
Email
Telefono

TextBox
txtId
txtNombre
txtEmail
txtTelefono

Buttons
btnGuardar
btnBuscar
btnActualizar
btnEliminar

DataGridView
dgvClientes

4. Estructura del Proyecto
CrudOracleWinForms/
│
├── ConexionOracle.cs
├── Form1.cs
├── Form1.Designer.cs
└── Program.cs

5. ConexionOracle.cs
using Oracle.ManagedDataAccess.Client;

namespace CrudOracleWinForms
{
    public class ConexionOracle
    {
        private string cadena =
            "User Id=SYSTEM;" +
            "Password=123456;" +
            "Data Source=localhost:1521/XEPDB1;";

        public OracleConnection ObtenerConexion()
        {
            OracleConnection cn =
                new OracleConnection(cadena);

            cn.Open();

            return cn;
        }
    }
}

6. Form1.cs
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace CrudOracleWinForms
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection cn =
                    conexion.ObtenerConexion())
                {
                    string sql =
                        @"INSERT INTO CLIENTES_J
                        (NOMBRE,EMAIL,TELEFONO)
                        VALUES
                        (:NOMBRE,:EMAIL,:TELEFONO)";

                    OracleCommand cmd =
                        new OracleCommand(sql, cn);

                    cmd.Parameters.Add(":NOMBRE",
                        txtNombre.Text);

                    cmd.Parameters.Add(":EMAIL",
                        txtEmail.Text);

                    cmd.Parameters.Add(":TELEFONO",
                        txtTelefono.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Registro guardado");

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
                        "SELECT * FROM CLIENTES_J WHERE ID=:ID";

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
                            "Registro no encontrado");
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
                          SET NOMBRE=:NOMBRE,
                              EMAIL=:EMAIL,
                              TELEFONO=:TELEFONO
                          WHERE ID=:ID";

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
                        "Registro actualizado");

                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                using (OracleConnection cn =
                    conexion.ObtenerConexion())
                {
                    string sql =
                        "DELETE FROM CLIENTES_J WHERE ID=:ID";

                    OracleCommand cmd =
                        new OracleCommand(sql, cn);

                    cmd.Parameters.Add(":ID",
                        txtId.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Registro eliminado");

                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvClientes_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila =
                    dgvClientes.Rows[e.RowIndex];

                txtId.Text =
                    fila.Cells["ID"].Value.ToString();

                txtNombre.Text =
                    fila.Cells["NOMBRE"].Value.ToString();

                txtEmail.Text =
                    fila.Cells["EMAIL"].Value.ToString();

                txtTelefono.Text =
                    fila.Cells["TELEFONO"].Value.ToString();
            }
        }
    }
}

7. Eventos del Formulario
Asignar los siguientes eventos:

btnGuardar.Click += btnGuardar_Click;
btnBuscar.Click += btnBuscar_Click;
btnActualizar.Click += btnActualizar_Click;
btnEliminar.Click += btnEliminar_Click;

dgvClientes.CellClick += dgvClientes_CellClick;

8. Program.cs
using System;
using System.Windows.Forms;

namespace CrudOracleWinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
}

Resultado Final
La aplicación permitirá:
Función	Descripción
Guardar	Inserta registros en Oracle 19c
Buscar	Busca por ID
Actualizar	Modifica registros existentes
Eliminar	Elimina registros existentes
DataGridView	Muestra todos los registros
CellClick	Carga los datos al seleccionar una fila

Interfaz Esperada
------------------------------------------------

ID:       [      ]

Nombre:   [___________________]

Email:    [___________________]

Telefono: [___________________]

[Guardar] [Buscar]

[Actualizar] [Eliminar]

------------------------------------------------

| ID | NOMBRE | EMAIL | TELEFONO |
|----|--------|-------|----------|
| 1  | Juan   | ...   | ...      |
| 2  | Maria  | ...   | ...      |

------------------------------------------------
Tecnologías Utilizadas
Visual Studio 2022
C# (.NET Framework)
Windows Forms
Oracle Database 19c
Oracle Managed Data Access
DataGridView
CRUD (Create, Read, Update, Delete)

Compatibilidad
✔ Visual Studio 2022
✔ .NET Framework
✔ Oracle Database 19c
✔ Oracle XE 21c (ajustando el Data Source)
✔ Oracle.ManagedDataAccess
✔ Windows 10 / Windows 11

Este proyecto es totalmente compatible con Visual Studio 2022 + Oracle Database 19c + Oracle Managed Data Access + Windows Forms (.NET Framework).
:. . / .  
