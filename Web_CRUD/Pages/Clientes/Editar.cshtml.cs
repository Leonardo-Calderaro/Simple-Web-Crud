using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Web_CRUD.Pages.Clientes
{
    public class EditarModel : PageModel
    {
        public InfoCliente infoCliente = new InfoCliente();
        public string mensagemErro = "";
        public string mensagemSucesso = "";

        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                string stringConexao = "Data Source=DESKTOP-MUVTE8P;Initial Catalog=Web_CRUD;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();

                    String consultaSql = "SELECT * FROM clientes WHERE id=@id";

                    using (SqlCommand comando = new SqlCommand(consultaSql, conexao))
                    {
                        comando.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader leitorDados = comando.ExecuteReader())
                        {
                            if (leitorDados.Read())
                            {
                                infoCliente.id = "" + leitorDados.GetInt32(0);
                                infoCliente.nome = leitorDados.GetString(1);
                                infoCliente.email = leitorDados.GetString(2);
                                infoCliente.contato = leitorDados.GetString(3);
                                infoCliente.endereco = leitorDados.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                mensagemErro = erro.Message;
                throw;
            }
        }

        public void OnPost()
        {
            infoCliente.id = Request.Form["id"];
            infoCliente.nome = Request.Form["nome"];
            infoCliente.email = Request.Form["email"];
            infoCliente.contato = Request.Form["contato"];
            infoCliente.endereco = Request.Form["endereco"];

            if (infoCliente.id.Length == 0 || infoCliente.nome.Length == 0 || infoCliente.email.Length == 0 || infoCliente.contato.Length == 0 || infoCliente.endereco.Length == 0)
            {
                mensagemErro = "Todos os campos são requeridos.";
                return;
            }

            try
            {
                string stringConexao = "Data Source=DESKTOP-MUVTE8P;Initial Catalog=Web_CRUD;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    string stringSql = "UPDATE clientes " +
                                       "SET nome=@nome, email=@email, contato=@contato, endereco=@endereco " +
                                       "WHERE id=@id";
                    using (SqlCommand comando = new SqlCommand(stringSql, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", infoCliente.nome);
                        comando.Parameters.AddWithValue("@email", infoCliente.email);
                        comando.Parameters.AddWithValue("@contato", infoCliente.contato);
                        comando.Parameters.AddWithValue("@endereco", infoCliente.endereco);
                        comando.Parameters.AddWithValue("@id", infoCliente.id);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception erro)
            {
                mensagemErro = erro.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");
        }
    }
}
