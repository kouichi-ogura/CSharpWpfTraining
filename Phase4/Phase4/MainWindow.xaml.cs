using System.Windows;

namespace Phase4
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSetting();
        }

        ~MainWindow()
        {
            // ウィンドウが消滅しているので、ここでは取得できない
            //SaveSetting();
        }

        private void ButtonOpenDialog_Click(object sender, RoutedEventArgs e)
        {
        }

        private void LoadSetting()
        {
            // 不要（？）
            //Properties.Settings.Default.Reload();

            this.Left = Properties.Settings.Default.ScreenLeft;
            this.Top = Properties.Settings.Default.ScreenTop;
            this.Width = Properties.Settings.Default.ScreenWidth;
            this.Height = Properties.Settings.Default.ScreenHeight;
            TextFileName.Text = Properties.Settings.Default.TextFileName;
        }

        private void SaveSetting()
        {
            Properties.Settings.Default.ScreenLeft   = this.Left;
            Properties.Settings.Default.ScreenTop    = this.Top;
            Properties.Settings.Default.ScreenWidth  = this.Width;
            Properties.Settings.Default.ScreenHeight = this.Height;
            Properties.Settings.Default.TextFileName = TextFileName.Text;

            Properties.Settings.Default.Save();
        }

        // xamlのClosingで定義したメソッド。ウィンドウ閉じるときに呼ばれる
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSetting();
        }
    }
}
