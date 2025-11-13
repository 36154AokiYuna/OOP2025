using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFileProcessor;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor{
        private int _count = 0;
        private string word = "";

        protected override void Initialize(string fname) {
             _count = 0;
            Console.Write("カウントしたい単語：");
            word = Console.ReadLine();
        }

        protected override void Execute(string line) {
            //自分の回答
            if (line.Contains(word)) {
                _count++;
            }

            //模範解答

        }

        protected override void Terminate() => Console.WriteLine($"{word}の個数：{_count}個");
    }
}
