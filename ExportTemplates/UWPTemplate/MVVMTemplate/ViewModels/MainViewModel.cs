using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using $safeprojectname$.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private MainModel _mainModel;

        public MainModel MainModel
        {
            get { return _mainModel; }
            set { this.Set(ref _mainModel,value); }
        }


        public RelayCommand SumCommand { get; set; }

        public RelayCommand SubCommand { get; set; }

        public RelayCommand MulCommand { get; set; }

        public RelayCommand DivCommand { get; set; }

        public MainViewModel()
        {
            MainModel = new MainModel();

            SumCommand = new RelayCommand(() =>
            {
                MainModel.CalcModel.CalcSum();
                Messenger.Default.Send<string>("足し算完了しました","complete");
            });

            SubCommand = new RelayCommand(()=>
            {
                MainModel.CalcModel.CalcSub();
                Messenger.Default.Send<string>("引き算完了しました", "complete");
            });

            MulCommand = new RelayCommand(()=>
            {
                MainModel.CalcModel.CalcMul();
                Messenger.Default.Send<string>("掛け算完了しました", "complete");
            });

            DivCommand = new RelayCommand(()=>
            {
                MainModel.CalcModel.CalcDiv();
                Messenger.Default.Send<string>("割り算完了しました", "complete");
            });
        }
    }
}
