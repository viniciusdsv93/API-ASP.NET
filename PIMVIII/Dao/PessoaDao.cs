﻿using PIMVIII.Models;
using System.Data.SqlClient;

namespace PIMVIII.Dao
{
    public class PessoaDao
    {
        string conexao = "Data Source=DESKTOP-EBOL8CS;Initial Catalog=pessoas;Integrated Security=True";

        public List<Pessoa> ObterPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM tb_pessoas", conn))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var pessoa = new Pessoa();
                                pessoa.Id = (int?)reader["Id"];
                                pessoa.Nome = reader["Nome"].ToString();
                                pessoa.Cpf = (long?)reader["Cpf"];
                                pessoa.EnderecoId = (int?)reader["EnderecoId"];
                                pessoas.Add(pessoa);
                            }
                        }
                    }
                }
            }

            return pessoas;
        }

        public void InserirPessoa(PessoaEnderecoTelefone pessoa)
        {
            int tipoTelefoneId = -1;
            int pessoaId = -1;
            int telefoneId = -1;
            int enderecoId = -1;
            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                SqlCommand command = conn.CreateCommand();
                SqlTransaction transaction;

                transaction = conn.BeginTransaction();

                command.Connection = conn;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "SELECT * FROM tb_tipos_telefone WHERE Tipo = @TIPOTELEFONE";
                    command.Parameters.AddWithValue("TIPOTELEFONE", pessoa.TipoTelefone);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            tipoTelefoneId = (int)reader["Id"];
                        }
                        reader.Close();
                        reader.Dispose();
                    }

                    if (tipoTelefoneId == -1)
                    {
                        command.CommandText = "INSERT INTO tb_tipos_telefone (Tipo) output INSERTED.ID VALUES (@TIPOTELEFONEINSERT)";
                        command.Parameters.AddWithValue("TIPOTELEFONEINSERT", pessoa.TipoTelefone);
                        tipoTelefoneId = (int)command.ExecuteScalar();
                    }

                    command.CommandText = "INSERT INTO tb_telefones (Numero, Ddd, TipoTelefoneId) output INSERTED.ID VALUES (@NUMEROTELEFONE, @DDD, @TIPOTELEFONEID)";
                    command.Parameters.AddWithValue("NUMEROTELEFONE", pessoa.NumeroTelefone);
                    command.Parameters.AddWithValue("DDD", pessoa.Ddd);
                    command.Parameters.AddWithValue("TIPOTELEFONEID", tipoTelefoneId);
                    telefoneId = (int)command.ExecuteScalar();

                    command.CommandText = "INSERT INTO tb_enderecos (Logradouro, Numero, Cep, Bairro, Cidade, Estado) output INSERTED.ID VALUES (@LOGRADOURO, @NUMERO, @CEP, @BAIRRO, @CIDADE, @ESTADO)";
                    command.Parameters.AddWithValue("LOGRADOURO", pessoa.Logradouro);
                    command.Parameters.AddWithValue("NUMERO", pessoa.Numero);
                    command.Parameters.AddWithValue("CEP", pessoa.Cep);
                    command.Parameters.AddWithValue("BAIRRO", pessoa.Bairro);
                    command.Parameters.AddWithValue("CIDADE", pessoa.Cidade);
                    command.Parameters.AddWithValue("ESTADO", pessoa.Estado);
                    enderecoId = (int)command.ExecuteScalar();

                    command.CommandText = "INSERT INTO tb_pessoas (Nome, Cpf, EnderecoId) output INSERTED.ID VALUES (@NOME, @CPF, @ENDERECOID)";
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("NOME", pessoa.Nome);
                    command.Parameters.AddWithValue("CPF", pessoa.Cpf);
                    command.Parameters.AddWithValue("ENDERECOID", enderecoId);
                    pessoaId = (int)command.ExecuteScalar();

                    command.CommandText = "INSERT INTO tb_pessoa_telefone (PessoaId, TelefoneId) VALUES (@PESSOAID, @TELEFONEID)";
                    command.Parameters.AddWithValue("PESSOAID", pessoaId);
                    command.Parameters.AddWithValue("TELEFONEID", telefoneId);
                    command.ExecuteScalar();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                }


                //using (SqlCommand cmd = new SqlCommand("SELECT * FROM tb_tipos_telefone WHERE Tipo = @TIPOTELEFONE ", conn))
                //{
                //    cmd.CommandType = System.Data.CommandType.Text;
                //    cmd.Parameters.AddWithValue("TIPOTELEFONE", pessoa.TipoTelefone);
                //    SqlDataReader reader = cmd.ExecuteReader();
                    
                //    if (reader != null)
                //    {
                //        while (reader.Read())
                //        {
                //            tipoTelefoneId = (int)reader["Id"];
                //        }
                //        reader.Close();
                //        reader.Dispose();
                //    }

                //    if (tipoTelefoneId == -1)
                //    {
                //        using (SqlCommand tipoTelefoneCmd = new SqlCommand("INSERT INTO tb_tipos_telefone (Tipo) output INSERTED.ID VALUES (@TIPOTELEFONE)", conn))
                //        {
                //            tipoTelefoneCmd.CommandType = System.Data.CommandType.Text;
                //            tipoTelefoneCmd.Parameters.AddWithValue("TIPOTELEFONE", pessoa.TipoTelefone);
                //            tipoTelefoneId = (int)tipoTelefoneCmd.ExecuteScalar();
                //        }
                //    }
                //}

                //using (SqlCommand cmd = new SqlCommand("INSERT INTO tb_telefones (Numero, Ddd, TipoTelefoneId) output INSERTED.ID VALUES (@NUMEROTELEFONE, @DDD, @TIPOTELEFONEID)", conn))
                //{
                //    cmd.CommandType = System.Data.CommandType.Text;
                //    cmd.Parameters.AddWithValue("NUMEROTELEFONE", pessoa.NumeroTelefone);
                //    cmd.Parameters.AddWithValue("DDD", pessoa.Ddd);
                //    cmd.Parameters.AddWithValue("TIPOTELEFONEID", tipoTelefoneId);
                //    telefoneId = (int)cmd.ExecuteScalar();
                //}

                //using (SqlCommand cmd = new SqlCommand("INSERT INTO tb_enderecos (Logradouro, Numero, Cep, Bairro, Cidade, Estado) output INSERTED.ID VALUES (@LOGRADOURO, @NUMERO, @CEP, @BAIRRO, @CIDADE, @ESTADO)", conn))
                //{
                //    cmd.CommandType = System.Data.CommandType.Text;
                //    cmd.Parameters.AddWithValue("LOGRADOURO", pessoa.Logradouro);
                //    cmd.Parameters.AddWithValue("NUMERO", pessoa.Numero);
                //    cmd.Parameters.AddWithValue("CEP", pessoa.Cep);
                //    cmd.Parameters.AddWithValue("BAIRRO", pessoa.Bairro);
                //    cmd.Parameters.AddWithValue("CIDADE", pessoa.Cidade);
                //    cmd.Parameters.AddWithValue("ESTADO", pessoa.Estado);
                //    enderecoId = (int)cmd.ExecuteScalar();
                //}

                //using (SqlCommand cmd = new SqlCommand("INSERT INTO tb_pessoas (Nome, Cpf, EnderecoId) output INSERTED.ID VALUES (@NOME, @CPF, @ENDERECOID)", conn))
                //{
                //    cmd.CommandType = System.Data.CommandType.Text;
                //    cmd.Parameters.AddWithValue("NOME", pessoa.Nome);
                //    cmd.Parameters.AddWithValue("CPF", pessoa.Cpf);
                //    cmd.Parameters.AddWithValue("ENDERECOID", enderecoId);
                //    pessoaId = (int)cmd.ExecuteScalar();
                //}

                //using (SqlCommand cmd = new SqlCommand("INSERT INTO tb_pessoa_telefone (PessoaId, TelefoneId) VALUES (@PESSOAID, @TELEFONEID);", conn))
                //{
                //    cmd.CommandType = System.Data.CommandType.Text;
                //    cmd.Parameters.AddWithValue("PESSOAID", pessoaId);
                //    cmd.Parameters.AddWithValue("TELEFONEID", telefoneId);
                //    cmd.ExecuteScalar();
                //}
            }
        }
    }
}