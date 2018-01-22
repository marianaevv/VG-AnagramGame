
using MySql.Data.MySqlClient;
using UnityEngine;

public class AdminMYSQL : MonoBehaviour {
    public string servidorBaseDatos;
    public string nombreBaseDatos;
    public string usuarioBaseDatos;
    public string contraseñaBaseDatos;

    private string datosConexion;
    private MySqlConnection conexion;


	// Use this for initialization
	void Start () {

        datosConexion = "Server=" + servidorBaseDatos
                       + ";Database=" + nombreBaseDatos
                       + ";Uid=" + usuarioBaseDatos
                       + ";Pwd=" + contraseñaBaseDatos
                       + ";";
        ConectarConServidorBaseDatos();


    }
private void ConectarConServidorBaseDatos()
    {
        conexion = new MySqlConnection(datosConexion);

        try
        {
            conexion.Open();
            Debug.Log("Conexión con BD éxitosa");
        }
        catch(MySqlException error)
        {
            Debug.LogError("Imposible conectar con Base de Datos: " + error);
        }

    }

    public MySqlDataReader Select(string _select)
    {
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = "SELECT * FROM " + _select;
        MySqlDataReader Resultado = cmd.ExecuteReader();
        return Resultado;
    }
}
