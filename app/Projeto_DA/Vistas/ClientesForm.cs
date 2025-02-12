﻿using Projeto_DA.Controladores;
using Projeto_DA.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_DA
{
    public partial class ClientesForm : Form
    {
		public string NomeFuncionario { get; private set; }
		private string nomeFuncionario;
		private Projeto_DA.Modelos.ApplicationContext db;

		public ClientesForm(string nomeFuncionario)
        {
            InitializeComponent();
			this.nomeFuncionario = nomeFuncionario;
			menuToolStripMenuItem.Text = nomeFuncionario;
			db = new Projeto_DA.Modelos.ApplicationContext();
			ClientesRefresh();
		}

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
			MenuForm menuForm = new MenuForm(nomeFuncionario);
			Hide();
			menuForm.ShowDialog();
		}

        private void btAdicionarCliente_Click(object sender, EventArgs e)
        {
            ClienteController.AdicionarCliente(textBoxNome.Text, textBoxMorada.Text, int.Parse(textBoxNif.Text));
            ClientesRefresh();
        }

        private void ClientesRefresh()
        {
            var cliente = ClienteController.GetClientes();
            listBoxClientes.DataSource = null;
            listBoxClientes.DataSource = cliente;
        }

        private void listBoxClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxClientes.SelectedIndex == -1)
            {
                return;
            }

            Cliente cliente = (Cliente)listBoxClientes.SelectedItem;

            textBoxNome.Text = cliente.Nome;
            textBoxMorada.Text = cliente.Morada;
            int NumFiscal = cliente.NumFiscal;
            textBoxNif.Text = NumFiscal.ToString();

			int numBilhetes;
			float valorBilhetes;
			string informacoesBilhetes = cliente.ObterInformacoesBilhetes(out numBilhetes, out valorBilhetes);

			textBoxNBilhetes.Text = numBilhetes.ToString();
			textBoxValorBilhetes.Text = valorBilhetes.ToString();
		}

        private void btAlterarCliente_Click(object sender, EventArgs e)
        {
			if (listBoxClientes.SelectedItem == null)
			{
				MessageBox.Show("Selecione um cliente para alterar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Cliente clienteSelecionado = (Cliente)listBoxClientes.SelectedItem;

			string novoNome = textBoxNome.Text;
			string novaMorada = textBoxMorada.Text;
			int novoNif = int.Parse(textBoxNif.Text);

			ClienteController.AlterarCliente(clienteSelecionado.Id, novoNome, novaMorada,
				novoNif);

			ClientesRefresh();
		}

		private void btRemoverCliente_Click(object sender, EventArgs e)
		{
			if (listBoxClientes.SelectedItem == null)
			{
				MessageBox.Show("Selecione um cliente para remover.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Cliente clienteSelecionado = (Cliente)listBoxClientes.SelectedItem;

			DialogResult result = MessageBox.Show($"Tem certeza que deseja remover o cliente {clienteSelecionado.Nome}?", "Confirmar Remoção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result == DialogResult.Yes)
			{
				ClienteController.RemoverCliente(clienteSelecionado.Id);
				ClientesRefresh();
			}
		}

		private void ClientesForm_Load(object sender, EventArgs e)
		{
			menuToolStripMenuItem.Text = nomeFuncionario;
		}
	}
}
