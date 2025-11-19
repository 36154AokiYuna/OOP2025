using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    //P.362 問題15.1
    public class LineToHalfNumberService : ITextFileService {

        private static Dictionary<char, char> _dictionary =
            new Dictionary<char, char>() {
                {'０','0'},{'１','1'},{'２','2'},{'３','3'},{'４','4'},
                {'５','5'},{'６','6'},{'７','7'},{'８','8'},{'９','9'},
            };

        public void Initialize(string fname) {
            
        }

        public void Execute(string line) {
            //自分の回答 途中
            Console.WriteLine(line.Replace("０","0"));

            //模範解答①
            string result = new string(
                line.Select(c => ('０' <= c && c <= '９') ? (char)(c - '０' + '0') : c).ToArray()
            );
            Console.WriteLine(result);

            //模範解答② Dictionaryを使った例
            var s = Regex.Replace(line, "[０-９]", c => _dictionary[c.Value[0]].ToString());
            Console.WriteLine(s);
        }

        public void Terminate() {
            
        }
    }
}
