using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class FornecedorRepository
    {

        public string Insert(Fornecedor fornecedor)
        {
            string resp = "";
            try
            {
                Connection.getConnection();

                SqlCommand SqlCmd = new SqlCommand
                {
                    Connection = Connection.SqlCon,
                    CommandText = "INSERT INTO fornecedor (nome, email, tipoPessoa, cpf_cnpj, razao_social, rua, numero, bairro, cidade, complemento, cep, telefone, celular) " +
                    "VALUES (@pNome, @pEmail, @pTipoPessoa, @pCpf_cnpj, @pRazao_social, @pRua, @pNumero, @pBairro, @pCidade, @pComplemento, @pCep, @pTelefone, @pCelular) ",
                    CommandType = CommandType.Text
                };
                SqlCmd.Parameters.AddWithValue("@pNome", fornecedor.Nome);
                SqlCmd.Parameters.AddWithValue("@pEmail", fornecedor.Email);
                SqlCmd.Parameters.AddWithValue("@pTipoPessoa", fornecedor.tipoPessoa);
                SqlCmd.Parameters.AddWithValue("@pCpf_cnpj", fornecedor.Cpf_cnpj);
                SqlCmd.Parameters.AddWithValue("@pRazao_social", fornecedor.Razao_social);
                SqlCmd.Parameters.AddWithValue("@pRua", fornecedor.Rua);
                SqlCmd.Parameters.AddWithValue("@pNumero", fornecedor.Numero);
                SqlCmd.Parameters.AddWithValue("@pBairro", fornecedor.Bairro);
                SqlCmd.Parameters.AddWithValue("@pCidade",fornecedor.Cidade);
                SqlCmd.Parameters.AddWithValue("@pComplemento", fornecedor.Completemento);
                SqlCmd.Parameters.AddWithValue("@pCep", fornecedor.Cep);
                SqlCmd.Parameters.AddWithValue("@pTelefone", fornecedor.Telefone);
                SqlCmd.Parameters.AddWithValue("@pCelular", fornecedor.Celular);
               
                //executa o stored procedure
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "SUCESSO" : "FALHA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }

            return resp;
        }

        public string Update(Fornecedor fornecedor)
        {
            string resp = "";
            try
            {
                Connection.getConnection();

                string updateSql = String.Format("UPDATE fornecedor SET " +
                                    "Nome = @pNome, email = @pEmail, cpf_cnpj = @pCpf_cnpj, razao_social = @pRazao_social, rua = @pRua, numero = @pNumero, bairro = @pBairro, cidade = @pCidade, complemento = @pCompletemento, cep = @pCep, telefone = @pTelefone, celular = @pCelular" +
                                    "WHERE id = @pId ");
                SqlCommand SqlCmd = new SqlCommand(updateSql, Connection.SqlCon);
                SqlCmd.Parameters.AddWithValue("@pNome", fornecedor.Nome);
                SqlCmd.Parameters.AddWithValue("@pEmail", fornecedor.Email);
                SqlCmd.Parameters.AddWithValue("@pId", fornecedor.Id);
                SqlCmd.Parameters.AddWithValue("@pTipoPessoa", fornecedor.tipoPessoa);
                SqlCmd.Parameters.AddWithValue("@pCpf_cnpj", fornecedor.Cpf_cnpj);
                SqlCmd.Parameters.AddWithValue("@pRazao_social", fornecedor.Razao_social);
                SqlCmd.Parameters.AddWithValue("@pRua", fornecedor.Rua);
                SqlCmd.Parameters.AddWithValue("@pNumero", fornecedor.Numero);
                SqlCmd.Parameters.AddWithValue("@pBairro", fornecedor.Bairro);
                SqlCmd.Parameters.AddWithValue("@pCidade", fornecedor.Cidade);
                SqlCmd.Parameters.AddWithValue("@pComplemento", fornecedor.Completemento);
                SqlCmd.Parameters.AddWithValue("@pCep", fornecedor.Cep);
                SqlCmd.Parameters.AddWithValue("@pTelefone", fornecedor.Telefone);
                SqlCmd.Parameters.AddWithValue("@pCelular", fornecedor.Celular);

                //executa o stored procedure
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "SUCESSO" : "FALHA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }
            return resp;
        }

        public string Remove(int idFornecedor)
        {
            string resp = "";
            try
            {
                Connection.getConnection();

                string updateSql = String.Format("DELETE FROM fornecedor " +
                                    "WHERE id = @pId ");
                SqlCommand SqlCmd = new SqlCommand(updateSql, Connection.SqlCon);
                SqlCmd.Parameters.AddWithValue("pId", idFornecedor);

                //executa o stored procedure
                resp = SqlCmd.ExecuteNonQuery() == 1 ? "SUCESSO" : "FALHA";
            }
            catch (Exception ex)
            {
                resp = ex.Message;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }
            return resp;
        }

        public DataTable getAll()
        {
            DataTable DtResultado = new DataTable("fornecedor");
            try
            {
                Connection.getConnection();
                String sqlSelect = "select * from fornecedor";

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = Connection.SqlCon;
                SqlCmd.CommandText = sqlSelect;
                SqlCmd.CommandType = CommandType.Text;
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);
            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable filterByName(string pNome)
        {
            DataTable DtResultado = new DataTable("fornecedor");
            string selectSql;
            try
            {
                Connection.getConnection();
                if (!string.IsNullOrEmpty(pNome))
                {
                    selectSql = String.Format("SELECT * FROM fornecedor WHERE nome LIKE @pNome");
                    pNome = '%' + pNome + '%';
                }
                else
                {
                    selectSql = String.Format("SELECT * FROM fornecedor");
                }
                SqlCommand SqlCmd = new SqlCommand(selectSql, Connection.SqlCon);
                if (!string.IsNullOrEmpty(pNome))
                    SqlCmd.Parameters.AddWithValue("pNome", pNome);
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);
            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable filterByEmail(string email)
        {
            DataTable DtResultado = new DataTable("fornecedor");
            string selectSql;
            try
            {
                Connection.getConnection();
                selectSql = "SELECT * FROM fornecedor WHERE email = @pEmail";
                SqlCommand SqlCmd = new SqlCommand(selectSql, Connection.SqlCon);
                SqlCmd.Parameters.AddWithValue("@pEmail", email);
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);
            }
            catch (Exception)
            {
                DtResultado = null;
            }
            finally
            {
                if (Connection.SqlCon.State == ConnectionState.Open)
                    Connection.SqlCon.Close();
            }
            return DtResultado;
        }
    }
}
