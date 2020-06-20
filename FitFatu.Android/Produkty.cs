using System;
using System.Collections.Generic;
using System.Text;

namespace FitFatu
{
    class Produkty
    {
        public int IdProduktu;
        public string Nazwa;
        public float Kcal;
        public float Protein;
        public float Fats;
        public float Carbs;
       
        

        

        

        public Produkty(int IdProduktu, string Nazwa, float Kcal, float Protein, float Fats, float Carbs)
        {
            this.IdProduktu = IdProduktu;
            this.Nazwa = Nazwa;
            this.Kcal = Kcal;
            this.Protein = Protein;
            this.Fats = Fats;
            this.Carbs = Carbs;
            
        }
    }
}
