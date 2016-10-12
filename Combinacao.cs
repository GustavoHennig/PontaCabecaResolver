using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PontaCabecaResolver
{
    class Combinacao : IComparable
    {


        public List<Carta> cartas = new List<Carta>();

        private string hash;

        public Combinacao(List<Carta> cartas)
        {
            this.cartas = cartas;
            UpdateHash();
        }

        public Combinacao(List<Carta> pCartas, long seed)
        {
            string sseed = Convert.ToString(seed);

            for (int i = 0; i < 9; i++)
            {
                int idx = Convert.ToInt32(sseed.Substring(i, 1));

                if (idx > 8) idx = Program.Rnd.Next(9);

                cartas.Add(pCartas[idx]);
            }
            UpdateHash();
        }

        public void ChangeCardRotationSeed(long seed)
        {

            string sseed = Program.IntToStringFast(seed, new char[] { '0', '1', '2', '3' });

            for (int i = 0; i < 9; i++)
            {

                int idx = Convert.ToInt32(sseed.Substring(i, 1));

                if (idx > 8) idx = Program.Rnd.Next(9);

                cartas[i].Rotate(idx);
            }
            UpdateHash();
        }

        private int pontuacao = -1;
        public int Pontuacao()
        {
            if (pontuacao >= 0)
            {
                return pontuacao;
            }
            int pontos = 0;


            //Compara os dados de cada carta
            pontos += EqualLados(cartas[0], cartas[1]) ? 1 : 0;
            pontos += EqualLados(cartas[1], cartas[2]) ? 1 : 0;

            pontos += EqualLados(cartas[3], cartas[4]) ? 1 : 0;
            pontos += EqualLados(cartas[4], cartas[5]) ? 1 : 0;

            pontos += EqualLados(cartas[6], cartas[7]) ? 1 : 0;
            pontos += EqualLados(cartas[7], cartas[8]) ? 1 : 0;

            //Compara cima e baixo de cada carta
            pontos += EqualCimaBaixo(cartas[0], cartas[3]) ? 1 : 0;
            pontos += EqualCimaBaixo(cartas[3], cartas[6]) ? 1 : 0;

            pontos += EqualCimaBaixo(cartas[1], cartas[4]) ? 1 : 0;
            pontos += EqualCimaBaixo(cartas[4], cartas[7]) ? 1 : 0;

            pontos += EqualCimaBaixo(cartas[2], cartas[5]) ? 1 : 0;
            pontos += EqualCimaBaixo(cartas[5], cartas[8]) ? 1 : 0;


            if (pontos >= 12)
            {
                MessageBox.Show("Feitooooo!!!");
            }
            pontuacao = pontos;
            return pontos;
        }

        public bool isSolucionado
        {
            get
            {
                if (EqualLados(cartas[0], cartas[1]) &&
                    EqualLados(cartas[1], cartas[2]) &&

                    EqualLados(cartas[3], cartas[4]) &&
                    EqualLados(cartas[4], cartas[5]) &&

                    EqualLados(cartas[6], cartas[7]) &&
                    EqualLados(cartas[7], cartas[8]) &&

                    //Compara cima e baixo de cada carta
                    EqualCimaBaixo(cartas[0], cartas[3]) &&
                    EqualCimaBaixo(cartas[3], cartas[6]) &&

                    EqualCimaBaixo(cartas[1], cartas[4]) &&
                    EqualCimaBaixo(cartas[4], cartas[7]) &&

                    EqualCimaBaixo(cartas[2], cartas[5]) &&
                    EqualCimaBaixo(cartas[5], cartas[8]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool EqualLados(Carta esq, Carta dir)
        {
            return Equal(esq.data[1], dir.data[3]);
        }

        private bool EqualCimaBaixo(Carta cima, Carta baixo)
        {
            return Equal(cima.data[2], baixo.data[0]);
        }

        private bool Equal(string vl1, string vl2)
        {


            if (vl1[0] == vl2[0] &&
                vl1[1] == vl2[1] &&
                vl1[2] == vl2[2])
            //if (vl2.StartsWith(vl1.Substring(0, 3)))
            {
                if (vl1[3] != vl2[3])
                {
                    return true;
                }
            }
            return false;
        }

        public Combinacao CriaFilho(Combinacao parceiro)
        {

            //Gera filho com características dos pais
            List<Carta> cartas2 = new List<Carta>(9);
            for (int i = 0; i < 9; i++)
            {
                //foreach (Carta c in cartas){
                Carta c2 = cartas[i].Clone();
                c2.Rotate(parceiro.cartas[i].RotateValue);
                cartas2.Add(c2);
            }

            Combinacao ret = new Combinacao(cartas2);

            //Mutação 
            if (Program.Rnd.Next(9) == 1)
            {
                ret.ProvocaMutacao();
                ret.ProvocaMutacao();
            }
            //Mutação 
            if (Program.Rnd.Next(4) == 1)
            {
                ret.ProvocaMutacao();
            }
            return ret;
        }

        public void ProvocaMutacao()
        {
            //Mutação de carta
            int swap1 = Program.Rnd.Next(9);
            int swap2 = Program.Rnd.Next(9);

            Carta tmp;
            tmp = cartas[swap1];
            cartas[swap1] = cartas[swap2];
            cartas[swap2] = tmp;
             
            //Mutação de posição de carta
            cartas[Program.Rnd.Next(9)].Rotate(Program.Rnd.Next(3));
        }



        public int CompareTo(object obj)
        {
            Combinacao c = obj as Combinacao;

            if (c.Pontuacao() == this.Pontuacao())
            {
                return 0;
            }
            else if (c.Pontuacao() > this.Pontuacao())
            {
                return -1;
            }
            else
            {
                return 1;
            }

        }


        private void UpdateHash()
        {
            StringBuilder sb = new StringBuilder(20);
            for (int i = 0; i < 9; i++)
            {
                sb.Append(cartas[i].ToString());
            }
            hash = sb.ToString();
        }

        public override string ToString()
        {
            return hash;
        }
    }
}
