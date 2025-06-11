using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Web_CRUD.Pages.Clientes
{

    // Aqui � onde se cria o modelo para pegar as informa��es dos cadastros no servidor.
    // Entenda como um algor�tmo que d� o passo a passo para pegar tais informa��es.
    public class IndexModel : PageModel
    {
        // Cria-se uma inst�ncia do tipo InfoClientes.
        public List<InfoCliente> listaClientes = new List<InfoCliente>();

        // OnGet() � o m�todo inicial que ser� executado ao se buscar as informa��es de cadastro de clientes no servidor.
        public void OnGet()
        {
            try
            {
                // Guarda em uma vari�vel string a string de conex�o, a qual nada mais � do que o conjunto de configura��es para se conectar com um banco de dados.
                string stringConexao = "Data Source=DESKTOP-MUVTE8P;Initial Catalog=Web_CRUD;Integrated Security=True;TrustServerCertificate=True";
                
                // Cria a conex�o usando a string de conex�o.
                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    // Abre a conex�o.
                    conexao.Open();

                    // Define a consulta sql a ser executada guardando-a em uma string.
                    String consultaSql = "SELECT * FROM clientes";

                    // Cria um comando sql tendo como par�metros a consulta sql a ser executada e a conex�o na qual executar a consulta.
                    // Importante notar que aqui apenas se passa os par�metros, mas nada � executado ainda.
                    using (SqlCommand comandoSql = new SqlCommand(consultaSql, conexao))
                    {
                        // Aqui � onde se ativa o m�todo que vai pegar os par�metros passados e executar de fato o comando sql usando o m�todo ExecuteReader() da classe.
                        using (SqlDataReader leitorDados = comandoSql.ExecuteReader())
                        {
                            // Aqui se define um loop while para ler linha a linha da tabela, definindo como condi��o o m�todo Read(), o qual ir� retornar false quando n�o houve mais linhas a serem lidas.
                            while (leitorDados.Read())
                            {
                                // Para guardar cada linha, cria-se uma inst�ncia da classe InfoCliente.
                                // Define-se ent�o cada propriedade da classe como equivalendo ao correspondente valor lido no m�todo Read().
                                InfoCliente infocliente = new InfoCliente();
                                infocliente.id = "" + leitorDados.GetInt32(0);
                                infocliente.nome = leitorDados.GetString(1);
                                infocliente.email = leitorDados.GetString(2);
                                infocliente.contato = leitorDados.GetString(3);
                                infocliente.endereco = leitorDados.GetString(4);
                                infocliente.data_cadastro = leitorDados.GetDateTime(5).ToString();

                                listaClientes.Add(infocliente);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.ToString());
                throw;
            }
        }
    }

    // Aqui � onde se cria a classe que lista as vari�veis (propriedades) que comp�e o tipo do cadastro de um cliente.
    public class InfoCliente
    {
        public string id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string contato { get; set; }
        public string endereco { get; set; }
        public string data_cadastro { get; set; }
    }
}
