using System.Runtime.InteropServices;
using System.Windows;

namespace TestWebBrowser
{
    [ComVisible(true)]
    public class ScriptManager
    {
        public void Notify(string text)
        {
            MessageBox.Show("Получено из JavaScript: " + text);
        }
    }
}
