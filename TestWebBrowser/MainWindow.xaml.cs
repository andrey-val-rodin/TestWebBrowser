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
            if (!File.Exists("index2.html"))
                MessageBox.Show("Файл index2.html не найден");

            MyWebBrowser.Source = GetHtmlFileUri();
            MyWebBrowser.ObjectForScripting = new ScriptManager();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await MyWebView2.EnsureCoreWebView2Async();

            var uri = GetHtmlFileUri2();
            MyWebView2.Source = uri;
            MyWebView2.CoreWebView2.WebMessageReceived += (sender, e) =>
            {
                string message = e.TryGetWebMessageAsString();
                Dispatcher.Invoke(() => MessageBox.Show($"Получено из JavaScript: {message}"));
            };
        }

        private Uri GetHtmlFileUri()
        {
            var path = Path.GetFullPath("index.html").Replace("\\", "/");
            return new Uri($"file:///{path}");
        }
        private Uri GetHtmlFileUri2()
        {
            var path = Path.GetFullPath("index2.html").Replace("\\", "/");
            return new Uri($"file:///{path}");
        }

        private void WebBrowserUrl_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    MyWebBrowser.Navigate(WebBrowserUrl.Text);
                }
                catch (UriFormatException)
                {
                    MessageBox.Show("Введите корректный Uri");
                }
            }
        }

        private void WebBrowserReset_Click(object sender, RoutedEventArgs e)
        {
            var uri = GetHtmlFileUri();
            WebBrowserUrl.Text = uri.ToString();
            MyWebBrowser.Navigate(uri);
        }

        private void WebView2Url_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                try
                {
                    var uri = new Uri(WebView2Url.Text);
                    MyWebView2.Source = uri;
                }
                catch (UriFormatException)
                {
                    MessageBox.Show("Веедите корректный Uri");
                }
            }
        }

        private void WebView2Reset_Click(object sender, RoutedEventArgs e)
        {
            var uri = GetHtmlFileUri2();
            WebView2Url.Text = uri.ToString();
            MyWebView2.Source = uri;
        }
    }
}