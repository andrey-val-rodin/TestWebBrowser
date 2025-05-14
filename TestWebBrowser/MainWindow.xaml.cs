using System.IO;
using System.Windows;

namespace TestWebBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists("index.html"))
                MessageBox.Show("Файл index.html не найден");

            var path = Path.GetFullPath("index.html").Replace("\\", "/");
            MyWebBrowser.Source = new Uri($"file:///{path}");
            MyWebBrowser.ObjectForScripting = new ScriptManager();
        }
    }
}