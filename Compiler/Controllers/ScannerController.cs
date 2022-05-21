using Compiler.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Compiler.Controllers
{
    public class ScannerController : Controller
    {
        Dictionary<string, int> convertCharsToInt = new Dictionary<string, int>();
        Dictionary<string, string> KeyWords = new Dictionary<string, string>();
        Dictionary<int, string> tokensType = new Dictionary<int, string>();
        List<KeyWord> keyWordsList;
        public List<string> Tokens { get; set; } = new List<string>();
        int[,] t;
        Dictionary<int, bool> Accept = new Dictionary<int, bool>();
        string newLine = '\n'.ToString();
        string tap = '\t'.ToString();

        public IActionResult Index()
        {
            return View(Tokens);
        }


        public void readKeyWordsFromDB()
        {
            keyWordsList = db.KeyWords.ToList();
            foreach (var keyWord in keyWordsList)
            {
                string key = keyWord.KeyWord1;
                string value = keyWord.ReturnToken;
                KeyWords[key] = value;
            }
        }

        public void ConvertCharsToInt()
        {
            for (char c = 'a'; c <= 'z'; c++) convertCharsToInt[c.ToString()] = 0;
            for (char c = 'A'; c <= 'Z'; c++) convertCharsToInt[c.ToString()] = 0;
            for (char c = '0'; c <= '9'; c++) convertCharsToInt[c.ToString()] = 1;
            List<CharsId> chars = db.CharsIds.ToList();
            int id = 2;
            foreach (CharsId charr in chars)
            {
                string ch = charr.Char;
                id = charr.CharId;
                convertCharsToInt[ch] = id;
            }
            convertCharsToInt[newLine] = id + 1;
            convertCharsToInt[tap.ToString()] = id + 2;

        }


        public void IntializedCompile()
        {

            readKeyWordsFromDB();
            ConvertCharsToInt();
            createTransitionTable();
            createAcepptArray();
        }

        public void createTransitionTable()
        {
            int numberOfChars = convertCharsToInt.Count() + 20;
            int dfaStates = 179;
            t = new int[dfaStates, numberOfChars];
            for (int i = 0; i < dfaStates; i++)
                for (int j = 0; j < numberOfChars; j++)
                    t[i, j] = 181;
            doTransitionTableForD0();
            doTransitionTableForD145();
            doTransitionTableForD150();
            doTransitionTableForD152();
            doTransitionTableForD157();
            doTransitionTableForD158();
            doTransitionTableForD159();
            doTransitionTableForD162();
            doTransitionTableForDError181Error();
        }


        public void doTransitionTableForDError181Error()
        {
            for (char c = 'a'; c <= 'z'; c++) t[181, convertCharsToInt[c.ToString()]] = 181;
            for (char c = 'A'; c <= 'Z'; c++) t[181, convertCharsToInt[c.ToString()]] = 181;
            for (char c = '0'; c <= '9'; c++) t[181, convertCharsToInt[c.ToString()]] = 181;

            t[181, convertCharsToInt["+"]] = 181;
            t[181, convertCharsToInt["-"]] = 181;
            t[181, convertCharsToInt["*"]] = 181;
            t[181, convertCharsToInt["/"]] = 181;
            t[181, convertCharsToInt["&"]] = 181;
            t[181, convertCharsToInt["|"]] = 181;
            t[181, convertCharsToInt["~"]] = 181;
            t[181, convertCharsToInt["="]] = 181;
            t[181, convertCharsToInt["."]] = 181;
            t[181, convertCharsToInt["<"]] = 181;
            t[181, convertCharsToInt[">"]] = 181;
            t[181, convertCharsToInt["!"]] = 181;
            t[181, convertCharsToInt["\""]] = 181;
            t[181, convertCharsToInt["'"]] = 181;
            t[181, convertCharsToInt[newLine]] = 180;
            t[181, convertCharsToInt[tap]] = 180;
            t[181, convertCharsToInt[" "]] = 180;
        }
        public void doTransitionTableForD0()
        {
            for (char c = 'a'; c <= 'z'; c++) t[0, convertCharsToInt[c.ToString()]] = 157;
            for (char c = 'A'; c <= 'Z'; c++) t[0, convertCharsToInt[c.ToString()]] = 157;
            for (char c = '0'; c <= '9'; c++) t[0, convertCharsToInt[c.ToString()]] = 171;

            t[0, convertCharsToInt["+"]] = 144;
            t[0, convertCharsToInt["-"]] = 145;
            t[0, convertCharsToInt["*"]] = 147;
            t[0, convertCharsToInt["/"]] = 148;
            t[0, convertCharsToInt["&"]] = 150;
            t[0, convertCharsToInt["|"]] = 152;
            t[0, convertCharsToInt["~"]] = 154;
            t[0, convertCharsToInt["="]] = 167;
            t[0, convertCharsToInt["."]] = 155;
            t[0, convertCharsToInt["<"]] = 162;
            t[0, convertCharsToInt[">"]] = 169;
            t[0, convertCharsToInt["!"]] = 169;
            t[0, convertCharsToInt["\""]] = 169;
            t[0, convertCharsToInt["'"]] = 177;
            t[0, convertCharsToInt[newLine]] = 180;
            t[0, convertCharsToInt[tap]] = 180;
            t[0, convertCharsToInt[" "]] = 180;


        }
        public void doTransitionTableForD145()
        {
            t[145, convertCharsToInt["-"]] = 146;
            t[1, convertCharsToInt[tap]] = 180;
            t[1, convertCharsToInt[newLine]] = 180;
        }
        public void doTransitionTableForD150()
        {

            t[150, convertCharsToInt["&"]] = 151;
        }
        public void doTransitionTableForD152()
        {

            t[152, convertCharsToInt["|"]] = 153;
        }
        public void doTransitionTableForD157()
        {

            for (char c = 'a'; c <= 'z'; c++) t[157, convertCharsToInt[c.ToString()]] = 158;
            for (char c = 'A'; c <= 'Z'; c++) t[157, convertCharsToInt[c.ToString()]] = 158;
            for (char c = '0'; c <= '9'; c++) t[157, convertCharsToInt[c.ToString()]] = 159;

            t[157, convertCharsToInt[newLine]] = 180;
            t[157, convertCharsToInt[tap]] = 180;
            t[157, convertCharsToInt[" "]] = 180;
        }
        public void doTransitionTableForD158()
        {

            for (char c = 'a'; c <= 'z'; c++) t[158, convertCharsToInt[c.ToString()]] = 158;
            for (char c = 'A'; c <= 'Z'; c++) t[158, convertCharsToInt[c.ToString()]] = 158;
            for (char c = '0'; c <= '9'; c++) t[158, convertCharsToInt[c.ToString()]] = 159;

            t[158, convertCharsToInt[newLine]] = 180;
            t[158, convertCharsToInt[tap]] = 180;
            t[158, convertCharsToInt[" "]] = 180;
        }
        public void doTransitionTableForD159()
        {

            for (char c = 'a'; c <= 'z'; c++) t[159, convertCharsToInt[c.ToString()]] = 158;
            for (char c = 'A'; c <= 'Z'; c++) t[159, convertCharsToInt[c.ToString()]] = 158;
            for (char c = '0'; c <= '9'; c++) t[159, convertCharsToInt[c.ToString()]] = 159;

            t[159, convertCharsToInt[newLine]] = 180;
            t[159, convertCharsToInt[tap]] = 180;
            t[159, convertCharsToInt[" "]] = 180;
        }
        public void doTransitionTableForD162()
        {

            t[10, convertCharsToInt["*"]] = 164;
            t[10, convertCharsToInt["="]] = 163;
            t[10, convertCharsToInt[newLine]] = 180;
            t[10, convertCharsToInt[tap]] = 180;
            t[10, convertCharsToInt[" "]] = 180;
        }

        public void createAcepptArray()
        {
            Accept[180] = true; //Accept
            Accept[181] = false; // Error
        }

        public ActionResult readTokens()
        {
            string code = "werr @ efgr fdg 87 * /n er ";

            IntializedCompile();

            List<int> states = new List<int>();

            Tokens.Add(code);
            int lineNumber = 1;
            int state = 0;
            int newState;
            states.Add(state);
            states.Add(convertCharsToInt[newLine.ToString()]);
            for (int i = 0; code != null && i < code.Length;)
            {
                state = 0;
                string token = "";
                while (i < code.Length && !Accept[state])
                {
                    if (code[i] == '\n')
                    {
                        lineNumber++;
                    }
                    newState = t[state, convertCharsToInt[code[i].ToString()]];
                    state = newState;
                    token += code[i];
                    states.Add(newState);
                    i++;
                }
                token = token.Remove(token.Length - 1);
                if (state == -1)
                {
                    string tokenMessage = token + " in line " + lineNumber + " is " + tokensType[state];
                    if (!token.Equals(""))
                        Tokens.Add(tokenMessage);
                }
                else if (Accept[state])
                {
                    string tokenMessage = "";
                    if (KeyWords.ContainsKey(token))
                        tokenMessage = token + " in line " + lineNumber + " is " + KeyWords[token];
                    else tokenMessage = token + " in line " + lineNumber + " is " + tokensType[state];
                    if (!token.Equals(""))
                        Tokens.Add(tokenMessage);
                }
            }

            return View(Tokens);
        }




    }
}
