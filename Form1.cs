using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Revisao
{
    public partial class Form1 : Form
    {
        public List<Contato> Agenda;
        public SqlConnection conexao;
        public Contato selecionado;

        public Form1()
        {
            InitializeComponent();
            Agenda = new List<Contato>();
            conexao = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDAgenda;Integrated Security=True;Connect Timeout=30;Encrypt=False;ApplicationIntent=ReadWrite;");
            AtualizarBanco();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        public void AtualizarAgenda()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Agenda;
            dataGridView1.Refresh();
        }

        public void AtualizarBanco()
        {
            Agenda.Clear();
            try
            {
                if (conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                var selectCMD = conexao.CreateCommand();
                selectCMD.CommandText = "SELECT * FROM Contato";
                SqlDataReader leitor = selectCMD.ExecuteReader();

                while(leitor.Read())
                {
                    Contato c = new Contato(Convert.ToInt32(leitor["Id"]), Convert.ToString(leitor["Nome"]), Convert.ToString(leitor["Telefone"]));
                    Agenda.Add(c);
                }
                leitor.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERRO");
            }
            finally
            {
                AtualizarAgenda();
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int x = e.RowIndex;
            selecionado = Agenda[x];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int contatoID = selecionado.Id;

            var deleteCMD = conexao.CreateCommand();
            deleteCMD.CommandText = "DELETE FROM Contato WHERE Id = @id";
            deleteCMD.Parameters.AddWithValue("@id", contatoID);

            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }
            deleteCMD.ExecuteNonQuery();
            Agenda.Remove(selecionado);
            AtualizarBanco();
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }

        }
    }
}
