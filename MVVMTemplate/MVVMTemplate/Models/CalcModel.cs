using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.Models
{
    public class CalcModel:ObservableObject
    {
        private double _num1;

        public double Num1
        {
            get { return _num1; }
            set { this.Set(ref _num1, value); }
        }

        private double _num2;

        public double Num2
        {
            get { return _num2; }
            set { this.Set(ref _num2, value); }
        }

        private double _result;

        public double Result
        {
            get { return _result; }
            set { this.Set(ref _result,value); }
        }


        public CalcModel()
        {
            Num1 = 0;
            Num2 = 0;
            Result = 0;
        }

        public void CalcSum()
        {
            Result = Num1 + Num2;
        }

        public void CalcSub()
        {
            Result = Num1 - Num2;
        }

        public void CalcMul()
        {
            Result = Num1 * Num2;
        }

        public void CalcDiv()
        {
            Result = Num1 / Num2;
        }
    }
}
