using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadCounter
{
    public partial class Form2 : Form
    {
        public string a=" ";
        public Form2()
        {
            
            InitializeComponent();
           
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Worker.isclicked = true;
          
            Close(); Form1.Worker.i = 0;

        }



        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Text = a;
            if (a == "Time Is Up !")
            {
                
            }
            else { Console.Beep(); }
            Form1.Worker.i = 1;
           
        }

     
    }
}
