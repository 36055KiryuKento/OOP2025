using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    //P362 問題15.1
    public interface ITextFileService {
        private int _count;
        public void Initialize(string fname) {
            _count = 0;
        }
      public  void Execute(string line) {
            if (_count < 20) {
                Console.WriteLine(line);
                _count++;
            }
            void Terminate() {

            }
        }
    }
