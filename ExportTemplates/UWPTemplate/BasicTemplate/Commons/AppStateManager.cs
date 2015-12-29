using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Commons
{
    //アプリの状態を変更するマネージャ
    public class AppStateManager
    {
        public Dictionary<AppState, double> StateList { get; set; }

        public AppState CurrentState { get; set; }
        public AppStateManager()
        {
            StateList = new Dictionary<AppState, double>();
            CurrentState = AppState.Init;
        }

        public bool TryChangeState(double windowWidth)
        {
            var next = StateList.LastOrDefault(q => q.Value < windowWidth);
            if (next.Key == CurrentState)
            {
                return false;
            }
            else
            {
                CurrentState = next.Key;
                return true;
            }

        }
    }
}
