using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    //P.362 問題15.3
    public class LineOutputService : ITextFileService{
        private int _count;

        public void Initialize(string fname) {
            _count = 0;
        }

        public void Execute(string line) {
            //模範解答
            if(_count < 20) {
                Console.WriteLine(line);
            }
            _count++;
        }

        public void Terminate() {

        }
    }
}
