using $safeprojectname$.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace $safeprojectname$.Pages
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //バックボタンをフックする
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;

            //アプリの状態が変わったとき、VisualStateManagerを変更する
            App.OnChangeAppState += (state, prev) =>
            {
                switch (state)
                {
                    case AppState.Mobile:

                        VisualStateManager.GoToState(this, "MobileState", true);
                        break;
                    case AppState.Normal:

                        VisualStateManager.GoToState(this, "NormalState", true);

                        break;
                    case AppState.Wide:
                        VisualStateManager.GoToState(this, "WideState", true);

                        break;
                }
            };

        }

        //ページが読み込まれた時
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //HomePageにNavigateする
            frameContent.Navigate(typeof(HomePage));
        }

        //検索ボックスで検索しようとしたとき
        private void suggestBoxSearch_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var text = args.QueryText;
        }

        //SplitViewのPaneに作ったナビゲーションボタンをおした時、各Pageに移動
        private void appButtonSetting_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(typeof(SettingPage));
            splitView.IsPaneOpen = false;
        }

        private void appButtonHome_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(typeof(HomePage));
            splitView.IsPaneOpen = false;
        }

        private void appButtonFavorite_Click(object sender, RoutedEventArgs e)
        {
            frameContent.Navigate(typeof(FavoritePage));
            splitView.IsPaneOpen = false;
        }

        

        //バックボタンが押された時
        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (frameContent.CanGoBack)
            {
                //HandledをtrueにすることでWindows10Mobileのバックボタンで終了しなくなる
                e.Handled = true;
                //コンテンツを表示しているFrameをBack
                frameContent.GoBack();
            }
        }
    }
}
