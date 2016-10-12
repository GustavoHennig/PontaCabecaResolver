using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PontaCabecaResolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Resolvedor r = new Resolvedor();
            //r.Inicia();

           // System.Threading.Tasks.Parallel.For(0, 4, i => {
                r.Inicia();
           // });
             
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Resolvedor r = new Resolvedor();
            r.IniciaForcaBruta();
        }
    }
}
