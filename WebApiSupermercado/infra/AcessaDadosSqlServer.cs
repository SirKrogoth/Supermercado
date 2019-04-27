using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApiSupermercado.Properties;

namespace WebApiSupermercado.infra
{
    public class AcessaDadosSqlServer
    {
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;
        public SqlConnection criarConexaoBanco()
        {
            //Aqui temos todos os parametros de conexão com o banco de dados.
            return new SqlConnection(Settings.Default.stringConexao);
        }        

        //Limpando parametro criado.
        public void limparParametro()
        {
            sqlParameterCollection.Clear();
        }

        public void adicionarParametro(string nome, object valor)
        {
            sqlParameterCollection.Add(new SqlParameter(nome, valor));
        }

        public object executarManipulacao(CommandType commandType, string nomeProcedure)
        {
            try
            {
                //criando conexao
                SqlConnection sqlConnection = criarConexaoBanco();
                //abrindo conexao com o banco
                sqlConnection.Open();
                //conexao está aberta, agora temos de criar o comando que irá movimentar os dados
                //nesta conexao já aberta, e este comando é o SqlCommand
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                //usando commandType e nomeProcedure
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeProcedure;
                sqlCommand.CommandTimeout = 600;//tempo de espera
                //agora vmos informar os valores a serem setados no banco, exemplo
                /* 
                 * @nome = valor
                 * @telefone = valor
                 * @cpf = valor
                 */
                //aqui vai ficar os valores e colocar na store procedure
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Consulta
        public DataTable executarConsulta(CommandType commandType, string nomeProcedureOuTexto)
        {
            try
            {
                //criar conexão
                SqlConnection sqlConnection = criarConexaoBanco();
                //abrir conexao
                sqlConnection.Open();
                //criando comando que irá percorrer a conexao
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeProcedureOuTexto;
                sqlCommand.CommandTimeout = 600;

                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }
                //Criando adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                //Criando um dataTable que é o local onde os dados ficarão
                DataTable dataTable = new DataTable();
                //Mandando o comando até o banco
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}