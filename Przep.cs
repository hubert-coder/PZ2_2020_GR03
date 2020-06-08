using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfatu
{
    class Przep
    {
        public int IdPrzepisu;
        public string Nazwa;
        public int Porcje;
        public int Czas;
        public int Level;
        public float Kcal;
        public float Protein;
        public float Fats;
        public float Carbs;
        public string Skladniki;
        public string Przygotowanie;

        public Przep(int IdPrzepisu, string Nazwa, int Porcje, int Czas, int Level, float Kcal, float Protein, float Fats, float Carbs, string Skladniki, string Przygotowanie)
        {
            this.IdPrzepisu = IdPrzepisu;
            this.Nazwa = Nazwa;
            this.Porcje = Porcje;
            this.Czas = Czas;
            this.Level = Level;
            this.Kcal = Kcal;
            this.Protein = Protein;
            this.Fats = Fats;
            this.Carbs = Carbs;
            this.Skladniki = Skladniki;
            this.Przygotowanie = Przygotowanie;
        }
    }
}
