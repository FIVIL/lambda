using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lambda
{
    public partial class Form1 : Form
    {
        public lambda l;
        int i;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            l = new lambda();
            i = 0;
        }
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            l.set(textBox1.Text);
            
            
            l.analyzer();
            l.s.ToLower();
            for(int i = 0; i < l.s.Length; i++)
            {
                if (l.s[i] == 'l')
                {
                    StringBuilder sb = new StringBuilder(l.s);
                    sb[i] = 'L';
                    l.s = sb.ToString();
                }
            }
            richTextBox1.Text += (i + ". " + l.s + "\n");
            l.reset();
           // l.tokenizer();
           // foreach (token aa in l.token_list)
            //{
            //    System.Diagnostics.Debug.WriteLine(aa.sval);
           // }
            //token t = new token( textBox1.Text,0);
            //l.token_list.Add(t);
            //token p = new token("(ab)",1);
            //token k = new token("(cd)", 2);
            //token[] ta = new token[2];
            //ta[0] = p;
            //ta[1] = k;
            //t.replace(0,ta);
           // t.par();
            //t.replace(0, ta);

            int[] a = new int[10];
            //Console.WriteLine(a[100]);
           // System.Diagnostics.Debug.Write("RESULT:"+l.clear());
        }
    }
}
