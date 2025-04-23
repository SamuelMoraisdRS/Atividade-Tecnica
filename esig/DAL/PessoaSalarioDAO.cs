using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using esig.DTO;
using Oracle.ManagedDataAccess.Client;

namespace esig.DAL
{
    public class PessoaSalarioDAO : DAO
    {
        // Tamanho das paginas em requisicoes para o banco de dados deve ser maior que o tamanho de pagina no GridView, para diminuir
        // o numero de requisicoes
        private static Int32 TAMANHO_PAGINA_DB = 50;
        public static List<PessoaSalarioDTO> GetPessoasSalario(string criterio_ordenacao, Int32 numero_pagina)
        {
            List<PessoaSalarioDTO> resultado = new List<PessoaSalarioDTO>();
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("get_pessoa_salario_paginado", conexao))
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
                                int id = reader.GetInt32(reader.GetOrdinal("id_pessoa_salario"));
                                string nome_cargo = reader.GetString(reader.GetOrdinal("no_cargo"));
                                string nome_pessoa = reader.GetString(reader.GetOrdinal("no_pessoa"));
                                int id_pessoa = reader.GetInt32(reader.GetOrdinal("id_pessoa"));
                                decimal salario = reader.GetDecimal(reader.GetOrdinal("nu_salario"));

                                PessoaSalarioDTO novoRegistro = new PessoaSalarioDTO(id, nome_cargo, nome_pessoa, salario, id_pessoa);

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
        public static void UpdateSalario(int id, Decimal novoSalario)
        {
            try
            {
                using (OracleConnection conexao = new OracleConnection(STRING_CONEXAO))
                {
                    conexao.Open();
                    using (OracleCommand cmd = new OracleCommand("atualizar_salario_pessoa", conexao))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Parametros de entrada
                        cmd.Parameters.Add("p_id_pessoa_salario", OracleDbType.Int32).Value = id;
                        cmd.Parameters.Add("p_salario", OracleDbType.Decimal).Value = novoSalario;
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

    }
}