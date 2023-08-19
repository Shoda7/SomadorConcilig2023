using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;

namespace SomadorConcilig2023
{
    class Banco
    {
        private static SQLiteConnection conexao;

        //Verifica sem o banco de dados já foi criado, caso não tenha sido, será criado o banco e a tabela usuario.
        public static void CriarBancoTabela()
        {
            if (!File.Exists(@"usuariosConcilig.sqlite"))
            {
                Banco.CriarBancoSQLite();
                Banco.CriarTabelaSQlite();
            }
        }
        private static SQLiteConnection ConexaoBanco()
        {
            conexao = new SQLiteConnection("Data Source=usuariosConcilig.sqlite");
            conexao.Open();
            return conexao;
        }

        public static void CriarBancoSQLite()
        {
            try
            {
                SQLiteConnection.CreateFile(@"usuariosConcilig.sqlite");
            }
            catch
            {
                throw;
            }
        }
        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    cmd.CommandText = "create table user(id_user INTEGER PRIMARY KEY AUTOINCREMENT, nome varchar(50), cpf varchar(14), email varchar (50), celular varchar (20), senha varchar(100))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Cadastro de usuarios no banco de dados.
        public static void Add(string nome, string cpf, string email, string celular, string senha)
        {
            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO user(nome, cpf, email, celular, senha) values (@nome, @cpf, @email, @celular,@senha)";
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@celular", celular);
                    cmd.Parameters.AddWithValue("@senha", senha);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Procura no banco de dados alguma linha que tenha o cpf igual ao solicitado no form login, ao encontrar verifica se as senhas são iguais.
        public static bool ValidaLogin(string cpf, string senha)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    DataRow row;
                    cmd.CommandText = "SELECT * FROM user Where cpf='" + cpf  + "'";
                    da = new SQLiteDataAdapter(cmd.CommandText, ConexaoBanco());
                    da.Fill(dt);
                    if (dt.Rows.Count != 0)
                    {
                        row = dt.Rows[0];
                        string senhaBanco = row["senha"].ToString();
                        senha = Crypto.sha256encrypt(senha);
                        if (senha == senhaBanco)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        //Busca o CPF que esta sendo cadastrado no banco para ver se o cadastro ja existe.
        public static bool LocalizaCPF(string cpf)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM user Where cpf='" + cpf + "'";
                    da = new SQLiteDataAdapter(cmd.CommandText, ConexaoBanco());
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Busca o email que esta sendo cadastrado no banco para ver se o cadastro ja existe.
        public static bool LocalizaEMAIL(string email)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = ConexaoBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM user Where email='" + email + "'";
                    da = new SQLiteDataAdapter(cmd.CommandText, ConexaoBanco());
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
