﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Modelos
{
    public class Filme
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TimeSpan Duracao { get; set; }
        public Boolean Ativo { get; set; }
		public Categoria Categoria { get; set; }

        public override string ToString()
        {
			string categoriaNome = Categoria != null ? Categoria.Nome : "N/A";
			return $"Filme {Id}: {Nome}, {Duracao}, {categoriaNome}, {Ativo}";
		}
    }
}
