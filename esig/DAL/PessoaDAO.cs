using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using esig.DTO;
using Oracle.ManagedDataAccess.Client;

namespace esig.DAL
{
    public class PessoaDAO : DAO
    {
        public static PessoaDTO GetPessoaPorId(int id)
        {
            PessoaDTO resultado = null;
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("get_pessoa_por_id", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.BindByName = true;

                        // Cursor de retorno. Ja que so retorna uma linha, pode ser subsituido por um ROW_TYPE
                        OracleParameter returnParam = new OracleParameter();
                        returnParam.ParameterName = "RETURN_VALUE";
                        returnParam.OracleDbType = OracleDbType.RefCursor;
                        returnParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnParam);

                        cmd.Parameters.Add("p_id_pessoa", OracleDbType.Int32).Value = id;

                        // TODO: Somente um registro sera retornado. Eliminar o loop vai melhorar a legilibilidade
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id_pessoa = reader.GetInt32(reader.GetOrdinal("id_pessoa"));
                                string nome_pessoa = reader.GetString(reader.GetOrdinal("no_nome"));
                                string email = reader.GetString(reader.GetOrdinal("no_email"));
                                string cep = reader.GetString(reader.GetOrdinal("no_cep"));
                                string telefone = reader.GetString(reader.GetOrdinal("no_telefone"));
                                string cidade = reader.GetString(reader.GetOrdinal("no_cidade"));
                                string usuario = reader.GetString(reader.GetOrdinal("no_usuario"));
                                int id_cargo = reader.GetInt32(reader.GetOrdinal("id_cargo"));
                                DateTime data_nascimento = reader.GetDateTime(reader.GetOrdinal("dt_data_nascimento"));
                                string pais = reader.GetString(reader.GetOrdinal("no_pais"));
                                string endereco = reader.GetString(reader.GetOrdinal("no_endereco"));
                                resultado = new PessoaDTO(id_pessoa, nome_pessoa, email, telefone, data_nascimento,
                                                        endereco, cidade, pais, cep, usuario, id_cargo);
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }
                throw ex;
            }
        }
        public static List<PessoaDTO> GetPessoas(string criterio_ordenacao, Int32 numero_pagina)
        {
            List<PessoaDTO> resultado = new List<PessoaDTO>();
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("get_pessoas_paginado", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Encapsula o Cursor de retorno
                        OracleParameter returnParam = new OracleParameter();
                        returnParam.ParameterName = "ReturnValue";
                        returnParam.OracleDbType = OracleDbType.RefCursor;
                        returnParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnParam);

                        // Entradas
                        cmd.Parameters.Add("p_pag_tamanho", OracleDbType.Int32).Value = TAMANHO_PAGINA_DB;
                        cmd.Parameters.Add("p_pag_numero", OracleDbType.Int32).Value = numero_pagina;
                        cmd.Parameters.Add("p_criterio_ordenacao", OracleDbType.Varchar2).Value = criterio_ordenacao;

                        // Instancia um reader para iterar pelas rows do cursor
                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("id_pessoa"));
                                string nome_pessoa = reader.GetString(reader.GetOrdinal("no_nome"));
                                string email = reader.GetString(reader.GetOrdinal("no_email"));
                                string cep = reader.GetString(reader.GetOrdinal("no_cep"));
                                string telefone = reader.GetString(reader.GetOrdinal("no_telefone"));
                                string cidade = reader.GetString(reader.GetOrdinal("no_cidade"));
                                string usuario = reader.GetString(reader.GetOrdinal("no_usuario"));
                                Int32 id_cargo = reader.GetInt32(reader.GetOrdinal("id_cargo"));
                                DateTime data_nascimento = reader.GetDateTime(reader.GetOrdinal("dt_data_nascimento"));
                                string pais = reader.GetString(reader.GetOrdinal("no_pais"));
                                string endereco = reader.GetString(reader.GetOrdinal("no_endereco"));

                                PessoaDTO novoRegistro = new PessoaDTO(id, nome_pessoa, email, telefone, data_nascimento,
                                                        endereco, cidade, pais, cep, usuario, id_cargo);
                                resultado.Add(novoRegistro);
                            }
                        }
                    }
                }
                return resultado;
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }
                throw ex;
            }
        }
        public static void UpdatePessoa(PessoaDTO novaPessoa)
        {
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("update_pessoa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Parametros de entrada
                        cmd.Parameters.Add("p_id_pessoa", OracleDbType.Int32).Value = novaPessoa.id;
                        cmd.Parameters.Add("p_nome", OracleDbType.Varchar2).Value = novaPessoa.nome;
                        cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = novaPessoa.email;
                        cmd.Parameters.Add("p_cep", OracleDbType.Varchar2).Value = novaPessoa.cep;
                        cmd.Parameters.Add("p_cidade", OracleDbType.Varchar2).Value = novaPessoa.cidade;
                        cmd.Parameters.Add("p_endereco", OracleDbType.Varchar2).Value = novaPessoa.endereco;
                        cmd.Parameters.Add("p_pais", OracleDbType.Varchar2).Value = novaPessoa.pais;
                        cmd.Parameters.Add("p_usuario", OracleDbType.Varchar2).Value = novaPessoa.usuario;
                        cmd.Parameters.Add("p_telefone", OracleDbType.Varchar2).Value = novaPessoa.telefone;
                        cmd.Parameters.Add("p_data_nascimento", OracleDbType.Date).Value = novaPessoa.data_nascimento;
                        cmd.Parameters.Add("p_no_cargo", OracleDbType.Int32).Value = novaPessoa.id_cargo;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }
            }
        }

        public static void CreatePessoa(PessoaDTO pessoaDTO)
        {
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("create_pessoa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Parametros de entrada
                        cmd.Parameters.Add("p_id_pessoa", OracleDbType.Int32).Value = pessoaDTO.id;
                        cmd.Parameters.Add("p_nome", OracleDbType.Varchar2).Value = pessoaDTO.nome;
                        cmd.Parameters.Add("p_email", OracleDbType.Varchar2).Value = pessoaDTO.id;
                        cmd.Parameters.Add("p_cep", OracleDbType.Varchar2).Value = pessoaDTO.nome;
                        cmd.Parameters.Add("p_cidade", OracleDbType.Varchar2).Value = pessoaDTO.id;
                        cmd.Parameters.Add("p_endereco", OracleDbType.Varchar2).Value = pessoaDTO.nome;
                        cmd.Parameters.Add("p_pais", OracleDbType.Varchar2).Value = pessoaDTO.id;
                        cmd.Parameters.Add("p_usuario", OracleDbType.Varchar2).Value = pessoaDTO.nome;
                        cmd.Parameters.Add("p_telefone", OracleDbType.Varchar2).Value = pessoaDTO.id;
                        cmd.Parameters.Add("p_data_nascimento", OracleDbType.Date).Value = pessoaDTO.id;
                        cmd.Parameters.Add("p_id_cargo", OracleDbType.Int32).Value = pessoaDTO.nome;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }
            }
        }

        public static void DeletePessoa(Int32 id)
        {
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("delete_pessoa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_id_pessoa", OracleDbType.Int32).Value = id;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }
            }

        }

        public static string GetNomeCargoPorPessoa(int id)
        {
            string cargo = null;

            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();

                    using (OracleCommand cmd = new OracleCommand("get_no_cargo_pessoa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Encapsulando o retorno como uma string
                        OracleParameter returnParam = new OracleParameter();
                        returnParam.ParameterName = "RETURN_VALUE";
                        returnParam.OracleDbType = OracleDbType.Varchar2;
                        returnParam.Size = 100;
                        returnParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnParam);
                        cmd.Parameters.Add("p_id_pessoa", OracleDbType.Int32).Value = id;
                        // Executa a função
                        cmd.ExecuteNonQuery();
                        cargo = returnParam.Value?.ToString();
                    }
                }

                return cargo;
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }

                throw;
            }
        }

        public static Int32 GetIdCargoPorNome(string nome)
        {
            Int32 cargo = 0;
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("get_id_cargo", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter returnParam = new OracleParameter();
                        returnParam.ParameterName = "RETURN_VALUE";
                        returnParam.OracleDbType = OracleDbType.Int32;
                        returnParam.Direction = ParameterDirection.ReturnValue;
                        cmd.Parameters.Add(returnParam);
                        cmd.Parameters.Add("p_no_cargo", OracleDbType.Varchar2).Value = nome;
                        cmd.ExecuteNonQuery();
                        // TODO: Gambiarra, mudar o return da function do BD
                        cargo = ((Oracle.ManagedDataAccess.Types.OracleDecimal)returnParam.Value).ToInt32();
                    }
                }
                return cargo;
            }
            catch (OracleException ex)
            {
                foreach (OracleError error in ex.Errors)
                {
                    // LOGAR O ERRO
                }
                throw;
            }
        }
    }
}