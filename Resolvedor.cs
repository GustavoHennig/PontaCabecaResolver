using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PontaCabecaResolver
{
    class Resolvedor
    {

        private static string[][] cartasIniciais = {
                                                    new string[]{"BLU2", "BLA2", "BLU1", "YEL1"},
                                                    new string[]{"BLU1", "BLA1", "YEL2", "GRE2"},
                                                    new string[]{"YEL1", "BLU2", "BLA2", "GRE1"},
                                                    new string[]{"BLU1", "YEL1", "GRE2", "BLA2"},
                                                    new string[]{"BLU2", "YEL2", "GRE1", "BLA1"},
                                                    new string[]{"BLU1", "GRE1", "BLA2", "YEL2"},
                                                    new string[]{"BLU1", "YEL2", "BLA2", "GRE1"},
                                                    new string[]{"BLU1", "YEL1", "GRE2", "BLA2"},
                                                    new string[]{"YEL2", "GRE1", "BLA1", "GRE2"}};

        public static List<Carta> BuildCartas(string[][] initData)
        {
            List<Carta> ret = new List<Carta>();

            Dictionary<int, bool> used = new Dictionary<int, bool>();
            int idx = 0;
            for (int i = 0; i < 9; i++)
            {
                //ret.Add(new Carta(initData[i]));

                do
                {
                    idx = Program.Rnd.Next(9);
                } while (used.ContainsKey(idx));

                ret.Add(new Carta(initData[idx]));
                used.Add(idx, true);

            }



            return ret;

        }


        private Dictionary<string, bool> ReadedSolution = new Dictionary<string, bool>();

        public Combinacao CriarCombinacaoInicial()
        {

            Combinacao c = new Combinacao(BuildCartas(cartasIniciais));
            return c;
        }



        public void Inicia()
        {

            const int sizePop = 1000;
            //Combinacao cAtual = null;
            int lastTick = Environment.TickCount;

            List<Combinacao> Populacao1 = CriarPopulacaoInicial(sizePop);


            do
            {

                List<Combinacao> Populacao3 = ReproduzirPopulacoes(Populacao1);

                Populacao1.AddRange(Populacao3);



                Populacao1 = GetMelhores(Populacao1, 250);

                if (Populacao1.Count == 0)
                {
                    Populacao1 = CriarPopulacaoInicial(sizePop);
                }

                if (Populacao1[0].Pontuacao() >= 11)
                {
                    int ms = Environment.TickCount - lastTick;
                }

            }
            while (Populacao1[0].Pontuacao() < 12);

        }


        public void IniciaForcaBruta()
        {
            Combinacao c;

            List<Carta> cartas = BuildCartas(cartasIniciais);

            System.Threading.Tasks.Parallel.For(100287836, 999999999, i =>

            //for (long l = 100000000; l < 999999999; l++)
            {
                c = new Combinacao(cartas, i);

                RotaRecursivamente(c, 0);

                //for (long l2 = 65536; l2 < 262143; l2++)
                //{

                if (c.isSolucionado)
                {
                    MessageBox.Show("Aeeee!!!");
                }

                //    c.ChangeCardRotationSeed(l2);
                //}
            });


        }

        public void RotaRecursivamente(Combinacao c, int nivel)
        {

            if (nivel > 8) return;

            for (int i = 0; i < 4; i++)
            {
                c.cartas[nivel].Rotate(i);
                if (c.isSolucionado)
                {
                    MessageBox.Show("Aeeee!!!");
                }
                RotaRecursivamente(c, nivel + 1);
            }
        }
        public List<Combinacao> GetMelhores(List<Combinacao> pop, int nro)
        {



            //RemoveItensExistente(pop);

            List<Combinacao> ret = new List<Combinacao>();

            pop.Sort();
            pop.Reverse();

            int cnt = nro;
            foreach (Combinacao c in pop)
            {
                if (cnt <= 0) break;
                ret.Add(c);
                cnt--;
            }


            return ret;
        }

        public List<Combinacao> CriarPopulacaoInicial(int nro)
        {


            Combinacao c1 = CriarCombinacaoInicial();
            Combinacao c2 = CriarCombinacaoInicial();
            List<Combinacao> ret = new List<Combinacao>();

            c1.ProvocaMutacao();
            c1.ProvocaMutacao();
            c1.ProvocaMutacao();
            c1.ProvocaMutacao();
            c1.ProvocaMutacao();
            c1.ProvocaMutacao();
            c2.ProvocaMutacao();
            c2.ProvocaMutacao();
            c2.ProvocaMutacao();
            c2.ProvocaMutacao();
            c2.ProvocaMutacao();
            c2.ProvocaMutacao();



            for (int i = 0; i < nro; i++)
            {
                c2 = c1.CriaFilho(c2);
                ret.Add(c2);
            }

            return ret;

        }

        public List<Combinacao> ReproduzirPopulacoes(List<Combinacao> pop1)
        {

            List<Combinacao> ret = new List<Combinacao>();
            int c = pop1.Count;

            int tot = pop1.Count - 1;
             
            do
            {

                if (Program.Rnd.Next(2) == 1 || tot == 0)
                {
                    for (int i = 0; i < pop1.Count; i++)
                    {
                        ret.Add(pop1[i].CriaFilho(pop1[tot - i]));
                    }
                }
                else
                {
                    for (int i = 0; i < pop1.Count - 1; i += 2)
                    {
                        ret.Add(pop1[i].CriaFilho(pop1[i + 1]));
                    }
                }


            } while (c > ret.Count);


            if (Program.Rnd.Next(5) == 1)
            {
                //Eventualmente insere uma combinação zerada, para reduzir vícios da solução
                Combinacao comb =  new Combinacao( Resolvedor.BuildCartas(Resolvedor.cartasIniciais));
                comb.ProvocaMutacao();
                comb.ProvocaMutacao(); 
                comb.ProvocaMutacao(); 
                comb.ProvocaMutacao();
                ret[Program.Rnd.Next(ret.Count)] = comb;
            }
            // 

            return ret;
        }

        private void RemoveItensExistente(List<Combinacao> lista)
        {
            for (int i = lista.Count - 1; i >= 0; i--)
            {

                if (ReadedSolution.ContainsKey(lista[i].ToString()))
                {
                    if (lista[i].Pontuacao() < 10)
                    {
                        lista.RemoveAt(i);
                    }
                }
                else
                {
                    ReadedSolution.Add(lista[i].ToString(), false);
                }
            }
        }

        public void Solucionado()
        {
            MessageBox.Show("feitoo");
        }
    }
}
