using BasicTemplate.Commons;
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

namespace BasicTemplate.Pages
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        //button1がクリックされたとき
        private async void button_1_Click(object sender, RoutedEventArgs e)
        {
            //このようなコードでテキストボックスの値を取得
            string text1 = textBox_1.Text;

            //ダイアログで表示
            var dialog = new MessageDialog(text1);
            await dialog.ShowAsync();
        }

        private void button_notify_Click(object sender, RoutedEventArgs e)
        {
            NotificationManager.SendBasicToast("テスト通知");
        }
    }
}
