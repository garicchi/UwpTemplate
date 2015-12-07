using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.Models
{
    public class MainModel:ObservableObject
    {
        private string _searchQuery;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set { this.Set(ref _searchQuery, value); }
        }

        private CalcModel _calcModel;

        public CalcModel CalcModel
        {
            get { return _calcModel; }
            set { this.Set(ref _calcModel, value); }
        }

        public MainModel()
        {
            CalcModel = new CalcModel();
        }
    }
}
