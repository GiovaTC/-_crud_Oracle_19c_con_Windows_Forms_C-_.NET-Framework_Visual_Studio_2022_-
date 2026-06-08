using Oracle.ManagedDataAccess.Client;

namespace crud_oracle_19c
{
    public class ConexionOracle
    {
        private string cadena =
            "User Id= SYSTEM;" +
            "Password= Tapiero123;" +
            "Data Source= localhost:1521/orcl;";

        public OracleConnection ObtenerConexion()
        {
            OracleConnection cn =
                new OracleConnection(cadena);

            cn.Open();

            return cn;
        }
    }   
}
