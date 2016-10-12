using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PontaCabecaResolver
{
    class Carta
    {

        public string[] data;
        public int RotateValue;

        public Carta(string[] valores)
        {
            data = valores;
        }




        public void Rotate(int cnt)
        {

            string[] nw = new string[4];

            for (int i = 0; i < 4; i++)
            {
                nw[pos(i, cnt)] = data[i];
            }

            data = nw;
            RotateValue = pos(RotateValue, cnt);
        }


        private int pos(int oldPos, int cnt)
        {

            int npos = oldPos + cnt;

            if (npos >= 4)
            {
                npos = npos-4;
            }

            return npos;

        }

        public Carta Clone() {
            Carta c = new Carta(data);
            return c;
        }

        public override string ToString()
        {
            return data[0] + data[1] + data[2] + data[3];
        }
    }
}
