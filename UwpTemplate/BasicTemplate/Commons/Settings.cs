using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicTemplate.Commons
{
    //アプリの設定
    //すでにあるのはサンプル
    public class Settings
    {
        public Settings()
        {
            SettingContent1 = "";
            SettingContent2 = false;
        }
        public string SettingContent1 { get; set; }
        public bool SettingContent2 { get; set; }
    }
}
