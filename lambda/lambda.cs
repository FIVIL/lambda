using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lambda
{
    public class lambda
    {
        public string s;
        public List<token> token_list = new List<token>();
        public int count;
        public lambda()
        {
            s = "";
            count = 0;
        }
        public void set(string v)
        {
            s = v;
        }
        public string get()
        {
            return s;
        }
        public string clear()
        {
            int hold = 0, i;
            string v = "";
            for(i=0;i<s.Length;i++)
            {
                if (s[i] == ' ')
                {
                    v += s.Substring(hold, i-hold);
                    hold = i + 1;
                }
            }
            v += s.Substring(hold, i - hold);
            return v;
        }
        public void tokenizer()
        {
            //System.Diagnostics.Debug.WriteLine("tok");
            int stack = 0;
            int h = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') stack++;
                if (s[i] == ')') stack--;
                if (stack == 0 && i > 0) 
                {
                    token t = new token(s.Substring(h,i+1-h), count);
                    count++;
                    h = i + 1;
                    token_list.Add(t);
                }

                
            }
        }
        public void analyzer()
        {
            clear();
            tokenizer();
            while (token_list.Count > 1)
            {
                bool isbreak = false;
                for(int i = 0; i < token_list.Count; i++)
                {
                    if (token_list[i].requirments > 0)
                    {
                        isbreak = true;
                        token h = token_list[i];
                        token[] re = new token[h.requirments];
                        for(int j = i + 1; j < token_list[i].requirments + i + 1; j++)
                        {
                            if (j > token_list.Count - 1) break;
                            re[j - (i + 1)] = token_list[j];

                        }
                        h.replace(re.Length, re);
                        //int pr = h.parreq();
                        //for (int j = 0; j < pr; j++) h.par();
                        h.par();
                      
                        s = "";
                        for (int j = 0; j < i; j++) s += token_list[j].sval;
                        s += h.sval;
                        for (int j = i + 1 + re.Length; j < token_list.Count; j++) s += token_list[j].sval;
                        s += "";
                        token_list.Clear();
                        tokenizer();
                        break;
                    }
                }
                //for (int i = 0; i < token_list.Count; i++) if (token_list.Count - (i + 1) < token_list[i].requirments) break;
                if (!isbreak) break;
            }
            /*
            for(int j = 0; j < token_list.Count;)
            {
                if (token_list[j].requirments == 0) j++;
                else
                {
                    token hold = token_list[j];
                    int n = hold.requirments;
                    token[] re = new token[n];
                    for (int i = 0; i < token_list.Count; i++)
                    {
                        System.Diagnostics.Debug.WriteLine(i+token_list[i].sval);
                    }
                    for (int i = j+1; i <= n+j; i++)
                    {
                        if (token_list.Count > i) re[i - (j)-1] = token_list[i];
                        //
                       //MessageBox.Show(i + token_list[i].sval+token_list.Count+" "+n+" "+j);

                    }
                    hold.replace(n, re);
                   // System.Diagnostics.Debug.WriteLine(re[100]);
                    hold.par();
                    //hold.par2();
                    // System.Diagnostics.Debug.WriteLine("s1" + s);
                    s = "";
                    for (int i = 0; i < j; i++) s += token_list[i].sval;
                    s += hold.sval;
                    //System.Diagnostics.Debug.WriteLine("s1" + s);
                    for (int i = n + j; i < token_list.Count; i++)
                    {
                        s += token_list[i].sval;
                        //System.Diagnostics.Debug.WriteLine(i + token_list[i].sval);
                    }
                    //foreach (token tok in token_list) System.Diagnostics.Debug.WriteLine(tok.sval);
                    //System.Diagnostics.Debug.WriteLine("s2" + s);
                    token_list.Clear();
                    tokenizer();
                    break;
                }
            }
            */

        }
        public void start()
        {
            clear();
            analyzer();
            foreach (var item in token_list)
            {
                item.resetvalues(item.sval);
            }
            //token_list[0].resetvalues(token_list[0].sval);
            System.Diagnostics.Debug.WriteLine("s" + s );
            for(int i=0;i<token_list.Count;i++)
            {
                if (token_list[i].requirments > 0 && token_list.Count > i + 1) start();
            }
            //if (token_list[0].requirments>0&&token_list.Count>1) start();
            
        }
        public void reset()
        {
            s = "";
            count = 0;
            token_list.Clear();
        }

    }
}
