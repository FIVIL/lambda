using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace lambda
{
    public class token
    {

        public string sval = string.Empty;
        public int  requirments = 0, number;
        public List<KeyValuePair<char, int>> lambda_names_pos = new List<KeyValuePair<char, int>>();
        public List<KeyValuePair<char, int>> change_need = new List<KeyValuePair<char, int>>();
        public List<char> lambda_names = new List<char>();
        public List<KeyValuePair<char, int>> lambda_operators = new List<KeyValuePair<char, int>>();
        public List<KeyValuePair<char, int>> fv = new List<KeyValuePair<char, int>>();
        public token(string s, int num)
        {
            number = num;
            sval = s;
            int stack = 0;
            for (int i = 0; i < sval.Length; i++)
            {
                if (sval[i] == '(') stack++;
                if (sval[i] == ')') stack--;
                if (sval[i] == 'L')
                {
                    int scoupe = stack;
                    lambda_names_pos.Add(new KeyValuePair<char, int>(sval[i + 1], i + 1));
                    lambda_names.Add(sval[i + 1]);
                    if (stack == 1)
                    {
                        requirments++;
                        change_need.Add(new KeyValuePair<char, int>(sval[i + 1], i + 1));
                        
                    }
                    for (int j = i + 2; j < sval.Length; j++)
                    {
                        if (sval[j] == '(') scoupe++;
                        if (sval[j] == ')') scoupe--;
                        if (sval[j] == sval[i + 1] && scoupe >= stack) lambda_operators.Add(new KeyValuePair<char, int>(sval[i+1], j));
                        if (sval[j] == sval[i + 1] && scoupe < stack) fv.Add(new KeyValuePair<char, int>(sval[j], j));
                    }
                }
                if (sval[i] != 'L' && sval[i] != '(' && sval[i] != ')' && !lambda_names.Contains(sval[i])) fv.Add(new KeyValuePair<char, int>(sval[i], i));

            }
        }
        List<KeyValuePair<char, int>> maplist = new List<KeyValuePair<char, int>>();
        int mapfind(char ch)
        {
            for (int i = 0; i < maplist.Count; i++) if (maplist[i].Key == ch) return maplist[i].Value;
            return -1;
        }

        public void replace(int sended, token[] tok)
        {
            
         
           
            
            string p = sval;
            for(int i=0;i<p.Length;i++)
            {
                if (fv.Contains(new KeyValuePair<char, int>(p[i], i)))
                {
                    StringBuilder sb = new StringBuilder(p);
                    sb[i] = char.ToUpper(p[i]);
                    p = sb.ToString();
                }
            }
            for (int i = 0; i < sended; i++)
            {
                p = p.Remove(change_need[i].Value-1, 2);
                if (i+1 < change_need.Count) change_need[i + 1] = new KeyValuePair<char, int>(change_need[i + 1].Key, change_need[i + 1].Value - 2*(i+1));
                //maplist.Add(new KeyValuePair<char, int>(change_need[i].Key, i));
            }
           for(int i = 0; i < sended; i++)
            {
                if(tok.Length>i)p = p.Replace(change_need[i].Key + "", tok[i].sval);

            }

            sval = p;
            resetvalues(sval);
            //System.Diagnostics.Debug.WriteLine("replace"+p);
        }
        public bool parreq()
        {
            int op = 0;
            int cl = 0;
            bool rv = false;
            for(int i = 0; i < sval.Length; i++)
            {
                if (sval[i] == '(') op++;
                if (sval[i] == ')')
                {
                    cl++;
                    if (op - cl > 0) { rv = true; break; }
                }

            }
            return rv;
        }
       public void par()
        {
            int holds = -1;
            int holde = -1;
            int stack = 0;
            for(int i = 0; i < sval.Length; i++)
            {
                if (sval[i] == '(')
                {
                    if (holds == -1) holds = i;
                    stack++;
                }
                
                if (sval[i] == ')')
                {
                     holde = i;
                }
                
            }
           // System.Diagnostics.Debug.WriteLine(sval+stack+" "+holds+" "+holde);
            if (stack>1)
            {
                sval = sval.Remove(holde, 1);
                sval = sval.Remove(holds, 1);
                stack--;
                resetvalues(sval);

               
            }
            //System.Diagnostics.Debug.WriteLine("par1"+sval);
            if (parreq()) par();
        }
        public void par2()
        {
            int holds = -1, holde=-1, stack = 0;
            for (int i = 0; i < sval.Length; i++)
            {
                if (sval[i] == '(')
                {
                    if (holds == -1) holds = i;
                    stack++;
                }

                if (sval[i] == ')')
                {
                    if (holde == -1) holde = i; 
                }

            }
            // System.Diagnostics.Debug.WriteLine(sval+stack+" "+holds+" "+holde);
            if (stack > 1)
            {
                sval = sval.Remove(holde, 1);
                sval = sval.Remove(holds, 1);
                stack--;
                resetvalues(sval);


            }
            System.Diagnostics.Debug.WriteLine("par2"+sval);

        }
        public void resetvalues(string s)
        {
            sval = s;
            requirments = 0;
            lambda_names_pos.Clear();
            lambda_operators.Clear();
            lambda_names.Clear();
            change_need.Clear();
            fv.Clear();
            int stack = 0;
            for (int i = 0; i < sval.Length; i++)
            {
                if (sval[i] == '(') stack++;
                if (sval[i] == ')') stack--;
                if (sval[i] == 'L')
                {
                    int scoupe = stack;
                    lambda_names_pos.Add(new KeyValuePair<char, int>(sval[i + 1], i + 1));
                    lambda_names.Add(sval[i + 1]);
                    if (stack == 1)
                    {
                        
                        requirments++;
                        change_need.Add(new KeyValuePair<char, int>(sval[i + 1], i + 1));
                    }
                    for (int j = i + 2; j < sval.Length; j++)
                    {
                        if (sval[j] == '(') scoupe++;
                        if (sval[j] == ')') scoupe--;
                        if (sval[j] == sval[i + 1] && scoupe >= stack) lambda_operators.Add(new KeyValuePair<char, int>(sval[i + 1], j));
                        if (sval[j] == sval[i + 1] && scoupe < stack) fv.Add(new KeyValuePair<char, int>(sval[j], j));
                    }
                }
                if (sval[i] != 'L' && sval[i] != '(' && sval[i] != ')' && !lambda_names.Contains(sval[i])) fv.Add(new KeyValuePair<char, int>(sval[i], i));

            }
        }
    }
}
