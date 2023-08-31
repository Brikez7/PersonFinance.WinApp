using System;
using System.Threading.Tasks;

namespace PersonFinance.WinApp.Helpers
{
    public static class TaskEx
    {
        public static async Task NoAwait(this Task task)
        {
            await task.ContinueWith(t =>
            {
                if (t.IsFaulted) Console.Error.WriteLine(t.Exception);
            }, TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
