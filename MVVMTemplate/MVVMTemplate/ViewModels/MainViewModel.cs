using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTemplate.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private string _searchQuery;

        public string SearchQuery
        {
            get { return _searchQuery; }
            set { this.Set(ref _searchQuery,value); }
        }


        public MainViewModel()
        {

        }
    }
}
