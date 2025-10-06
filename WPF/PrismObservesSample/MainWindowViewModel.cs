using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismObservesSample{
    class MainWindowViewModel : BindableBase{
        private string _input1 = "";
        public string Input1 {
            get => _input1;
            set => SetProperty( ref _input1, value);
        }

        private string _input2 = "";
        public string Input2 {
            get => _input2;
            set => SetProperty(ref _input2, value);  
        }

        private string _result = "";
        public string Result {
            get => _result;
            set => SetProperty(ref _result, value);
        }
        public DelegateCommand SumCommand { get; }
        //コンストラクタ
        public MainWindowViewModel() {
            SumCommand = new DelegateCommand(ExcuteSum);
        }
       


        //足し算の処理
        private void ExcuteSum() {
    
                Result = (Input1 + Input2).ToString();
           
        }
        //足し算処理を実行できるか否かのチェック
        private bool canExcuteSum() {

        }
    }
}
