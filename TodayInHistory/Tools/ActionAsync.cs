using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayInHistory.Tools
{
    public class ActionAsync
    {
        public void Do(Action doWork)
        {
            Action action = new Action(doWork);
            action.BeginInvoke(new AsyncCallback(EndAction), action);
        }

        void EndAction(IAsyncResult result)
        {
            Action action = result.AsyncState as Action;
            action.EndInvoke(result);
        }
    }
}
