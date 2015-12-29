using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Models
{
    public class SettingsModel:ObservableObject
    {
        private string _settingContent1;

        public string SettingContent1
        {
            get { return _settingContent1; }
            set { this.Set(ref _settingContent1,value); }
        }

        private bool _settingContent2;

        public bool SettingContent2
        {
            get { return _settingContent2; }
            set { this.Set(ref _settingContent2,value); }
        }


        public SettingsModel()
        {
            SettingContent1 = "";
            SettingContent2 = false;
        }
    }
}
