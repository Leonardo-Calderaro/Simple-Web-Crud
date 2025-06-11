using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

namespace Web_CRUD.Pages.Clientes
{
    public class AdicionarModel : PageModel
    {
        public InfoCliente infoCliente = new InfoCliente();

        public string mensagemErro = "";

        public string mensagemSucesso = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            infoCliente.nome = Request.Form["nome"];
            infoCliente.email = Request.Form["email"];
            infoCliente.contato = Request.Form["contato"];
            infoCliente.endereco = Request.Form["endereco"];

            if (infoCliente.nome.Length == 0 || infoCliente.email.Length == 0 || infoCliente.contato.Length == 0 || infoCliente.endereco.Length == 0)
            {
                mensagemErro = "Todos os campos devem ser preenchidos.";
                return;
            }

            // Salvar o novo cliente no banco de dados.
            try
            {
                string stringConexao = "Data Source=DESKTOP-MUVTE8P;Initial Catalog=Web_CRUD;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();
                    string stringSql = "INSERT INTO clientes " +
                                       "(nome, email, contato, endereco) VALUES " +
                                       "(@nome, @email, @contato, @endereco);";
                    using (SqlCommand comando = new SqlCommand(stringSql, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", infoCliente.nome);
                        comando.Parameters.AddWithValue("@email", infoCliente.email);
                        comando.Parameters.AddWithValue("@contato", infoCliente.contato);
                        comando.Parameters.AddWithValue("@endereco", infoCliente.endereco);

                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception erro)
            {
                mensagemErro = erro.Message;
                return;
            }

            infoCliente.nome = "";
            infoCliente.email = "";
            infoCliente.contato = "";
            infoCliente.endereco = "";

            mensagemSucesso = "Novo cliente adicionado corretamente.";

            Response.Redirect("/Clientes/Index");
        }
    }
}
