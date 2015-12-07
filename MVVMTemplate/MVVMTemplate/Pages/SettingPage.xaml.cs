using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace MVVMTemplate.Pages
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        //設定ページに遷移してきたとき
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //現在の設定をUIに反映する
            textBoxSetting1.Text = App.Setting.SettingContent1;
            toggleSwitchSetting2.IsOn = App.Setting.SettingContent2;
        }

        //設定1のテキストボックスの内容が変わったとき
        private void textBoxSetting1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //設定に反映
            App.Setting.SettingContent1 = textBoxSetting1.Text;
        }

        //設定2のトグルスイッチの内容が変わったとき
        private void toggleSwitchSetting2_Toggled(object sender, RoutedEventArgs e)
        {
            //設定に反映
            App.Setting.SettingContent2 = toggleSwitchSetting2.IsOn;
        }
    }
}
