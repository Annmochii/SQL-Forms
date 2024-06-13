using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revisao
{
    public partial class Form2 : Form
    {
        Form1 Origem;
        public Form2(Form1 origem)
        {
            InitializeComponent();
            this.Origem = origem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var insertCMD = Origem.conexao.CreateCommand();
            insertCMD.CommandText = "INSERT INTO Contato (Nome, Telefone) VALUES (@nome, @telefone)";

            insertCMD.Parameters.AddWithValue("@nome", textBox1.Text);
            insertCMD.Parameters.AddWithValue("@telefone", textBox2.Text);

            if (Origem.conexao.State == ConnectionState.Closed)
            {
                Origem.conexao.Open();
            }
            insertCMD.ExecuteNonQuery();
            Origem.AtualizarBanco();

            this.Close();
        }
    }
}
