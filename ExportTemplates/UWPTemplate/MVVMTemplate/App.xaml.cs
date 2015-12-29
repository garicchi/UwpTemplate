using Microsoft.Practices.ServiceLocation;
using $safeprojectname$.Commons;
using $safeprojectname$.Models;
using $safeprojectname$.Pages;
using $safeprojectname$.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace $safeprojectname$
{
    sealed partial class App : Application
    {
        //アプリの状態を管理するManager
        public static AppStateManager StateManager { get; set; }
        //アプリの状態が変わったときに変更を通知するイベント
        public static event Action<AppState, AppState> OnChangeAppState;

        //アプリの最小幅を定義する
        private Size _appMinimumSize = new Size(340, 400);

        public App()
        {
            this.InitializeComponent();

            StateManager = new AppStateManager();
            //アプリの状態とその最小幅を定義する
            StateManager.StateList.Add(AppState.Mobile, 0);
            StateManager.StateList.Add(AppState.Normal, 600);
            StateManager.StateList.Add(AppState.Wide, 1200);

            OnChangeAppState += (s, s2) => { };

            //アプリのライフサイクルをフック
            App.Current.Resuming += App_Resuming;
            App.Current.Suspending += App_Suspending;

        }

        //アプリが起動したとき
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                //以前のアプリ状態が中断で終了した場合
                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //以前中断したアプリケーションから状態を読み込む
                    
                }
                

                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                //アプリの最小幅を設定
                ApplicationView.GetForCurrentView().SetPreferredMinSize(_appMinimumSize);
                //ウインドウのサイズ変更がされたとき
                Window.Current.SizeChanged += (s, ex) =>
                {
                    OnWindowSizeChanged(ex.Size);
                };
                //MainPageへNavigate
                rootFrame.Navigate(typeof(MainPage), e.Arguments);

                OnWindowSizeChanged(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));
            }

            Window.Current.Activate();
            await DataLoadAsync();
        }

        //ウインドウサイズが変更されたとき、それに応じてアプリの状態を変える
        private void OnWindowSizeChanged(Size newSize)
        {
            var prevState = App.StateManager.CurrentState;
            bool isChange = StateManager.TryChangeState(newSize.Width);
            if (isChange)
            {
                switch (StateManager.CurrentState)
                {
                    case AppState.Mobile:
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                        break;
                    case AppState.Normal:
                    case AppState.Wide:
                        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                        break;
                }
                OnChangeAppState(StateManager.CurrentState, prevState);
            }
        }

        //アプリが一時停止しようとしたとき
        private async void App_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //ここでアプリのデータや状態を保存するdeferral.Complete()が呼ばれるまではawaitしても待ってくれる

            await DataSaveAsync();
            deferral.Complete();
        }

        //アプリが再開しようとしたとき
        private async void App_Resuming(object sender, object e)
        {
            //ここでアプリのデータや状態を復元する

            await DataLoadAsync();
        }

        //アプリのデータを保存するメソッド
        //App_Suspendingから呼ばれる
        private async Task DataSaveAsync()
        {
            /*ファイルにデータを書き込む場合*/
            var viewModel = ServiceLocator.Current.GetInstance<MainViewModel>();
            string saveData = JsonConvert.SerializeObject(viewModel.MainModel);
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync("SaveFile", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, saveData);
            
        }

        //アプリのデータを復元するメソッド
        //App_ResumingとOnLaunchedから呼ばれる
        //正常復元した場合はtrue、アプリのデータがなかったか、復元失敗した場合はfalseを返す
        private async Task<bool> DataLoadAsync()
        {

            /*ファイルからデータをロードする場合*/
            var folder = ApplicationData.Current.LocalFolder;
            var files = await folder.GetFilesAsync();
            if (files.Any(q => q.Name == "SaveFile"))
            {
                try
                {
                    var file = files.First(q => q.Name == "SaveFile");
                    var saveData = await FileIO.ReadTextAsync(file);
                    var model = (MainModel)JsonConvert.DeserializeObject(saveData,typeof(MainModel));
                    ServiceLocator.Current.GetInstance<MainViewModel>().MainModel = model;
                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("データ復元に失敗しました");
                }
            }
            
            return false;
            

        }
    }
}
