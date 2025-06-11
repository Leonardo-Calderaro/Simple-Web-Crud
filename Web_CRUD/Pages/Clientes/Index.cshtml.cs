using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Web_CRUD.Pages.Clientes
{

    // Aqui é onde se cria o modelo para pegar as informações dos cadastros no servidor.
    // Entenda como um algorítmo que dá o passo a passo para pegar tais informações.
    public class IndexModel : PageModel
    {
        // Cria-se uma instância do tipo InfoClientes.
        public List<InfoCliente> listaClientes = new List<InfoCliente>();

        // OnGet() é o método inicial que será executado ao se buscar as informações de cadastro de clientes no servidor.
        public void OnGet()
        {
            try
            {
                // Guarda em uma variável string a string de conexão, a qual nada mais é do que o conjunto de configurações para se conectar com um banco de dados.
                string stringConexao = "Data Source=DESKTOP-MUVTE8P;Initial Catalog=Web_CRUD;Integrated Security=True;TrustServerCertificate=True";
                
                // Cria a conexão usando a string de conexão.
                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    // Abre a conexão.
                    conexao.Open();

                    // Define a consulta sql a ser executada guardando-a em uma string.
                    String consultaSql = "SELECT * FROM clientes";

                    // Cria um comando sql tendo como parâmetros a consulta sql a ser executada e a conexão na qual executar a consulta.
                    // Importante notar que aqui apenas se passa os parâmetros, mas nada é executado ainda.
                    using (SqlCommand comandoSql = new SqlCommand(consultaSql, conexao))
                    {
                        // Aqui é onde se ativa o método que vai pegar os parâmetros passados e executar de fato o comando sql usando o método ExecuteReader() da classe.
                        using (SqlDataReader leitorDados = comandoSql.ExecuteReader())
                        {
                            // Aqui se define um loop while para ler linha a linha da tabela, definindo como condição o método Read(), o qual irá retornar false quando não houve mais linhas a serem lidas.
                            while (leitorDados.Read())
                            {
                                // Para guardar cada linha, cria-se uma instância da classe InfoCliente.
                                // Define-se então cada propriedade da classe como equivalendo ao correspondente valor lido no método Read().
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

    // Aqui é onde se cria a classe que lista as variáveis (propriedades) que compõe o tipo do cadastro de um cliente.
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
