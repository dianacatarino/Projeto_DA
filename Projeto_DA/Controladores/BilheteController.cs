﻿using Projeto_DA.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Controladores
{
    internal class BilheteController
    {
        public static void AdicionarBilhete(string lugar, string estado, Cliente cliente,
            Funcionario funcionario, Sessao sessao)
        {
            using (var db = new ApplicationContext())
            {
                var bilhete = new Bilhete { Lugar = lugar, Estado = estado, Cliente = cliente,
                Funcionario = funcionario, Sessao = sessao};
                db.Bilhetes.Add(bilhete);
                db.SaveChanges();
            }
        }

        public static List<Bilhete> GetBilhetes()
        {
            using (var db = new ApplicationContext())
            {
                return db.Bilhetes.ToList();
            }
        }
    }
}
