﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace it_shop.Model
{
    public class Uposlenik : Osoba
    {
        private static int id;
        private DateTime datumZaposlenja;
        private string spol;
        private double plata;
        private double dodatakNaPlatu;
        private int daniGodisnjegOdmora;

        public Uposlenik(string ime, string adresa, string telefon, DateTime datumZasposlenja, string spol, double plata, double dodatak, int godisnji)
            : base(ime, adresa, telefon)
        {
            DatumZaposlenja = datumZasposlenja;
            Spol = spol;
            Plata = plata;
            DodatakNaPlatu = dodatak;
            DaniGodisnjegOdmora = godisnji;

        }


        #region Properties
        public int DaniGodisnjegOdmora
        {
            get { return daniGodisnjegOdmora; }
            set { daniGodisnjegOdmora = value; }
        }
        
        
        public double DodatakNaPlatu
        {
            get { return dodatakNaPlatu; }
            set { dodatakNaPlatu = value; }
        }
        
        public double Plata
        {
            get { return plata; }
            set { plata = value; }
        }
        
        public string Spol
        {
            get { return spol; }
            set { spol = value; }
        }
        
        public DateTime DatumZaposlenja
        {
            get { return datumZaposlenja; }
            set { datumZaposlenja = value; }
        }
        
        public static int ID
        {
            get { return id; }
            set { id = value; }
        }

        #endregion

    }
}
