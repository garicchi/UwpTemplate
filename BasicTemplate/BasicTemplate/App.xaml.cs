using BasicTemplate.Commons;
using BasicTemplate.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BasicTemplate
{
    sealed partial class App : Application
    {
        public static AppStateManager StateManager { get; set; }
        public static event Action<AppState, AppState> OnChangeAppState;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            StateManager = new AppStateManager();
            StateManager.StateList.Add(AppState.Mobile, 0);
            StateManager.StateList.Add(AppState.Normal, 800);
            StateManager.StateList.Add(AppState.Wide, 1600);

            OnChangeAppState += (s, s2) => { };
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
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

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: 以前中断したアプリケーションから状態を読み込みます
                }

                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(340, 400));
                Window.Current.SizeChanged += (s, ex) =>
                {
                    OnWindowSizeChanged(ex.Size);
                };

                rootFrame.Navigate(typeof(MainPage), e.Arguments);

                OnWindowSizeChanged(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));
            }

            Window.Current.Activate();
        }

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

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }


        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: アプリケーションの状態を保存してバックグラウンドの動作があれば停止します
            deferral.Complete();
        }
    }
}
