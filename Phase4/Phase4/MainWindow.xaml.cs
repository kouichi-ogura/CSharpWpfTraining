using Microsoft.Win32;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
//using System.Windows.Forms;

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
        }

        ~MainWindow()
        {
            // ウィンドウが消滅しているので、ここでは取得できない
            //SaveSetting();
        }

        // xamlのLoadedで定義したメソッド。
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSetting();
        }

        // xamlのClosingで定義したメソッド。
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSetting();
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

        private void ButtonOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            string DirName = "";
            if (! GetDirectoryName(ref DirName) )
            {
                MessageBox.Show("フォルダが選択されていません");
                return;
            }
            MessageBox.Show(DirName);
        }

        // サンプルアプリとはふるまい違うけど、こちらの方がパスを貼り付けできるので便利
        private Boolean GetDirectoryName(ref string DirectoryName)
        {
            Boolean IsSuccess = false;
            var dlg = new CommonOpenFileDialog("サムネイル表示対象のフォルダを選択")
            {
                IsFolderPicker = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                DirectoryName = dlg.FileName;
                IsSuccess = true;
            }
            return IsSuccess;
        }

        // 本アプリでは未使用。参考までに作成
        private Boolean GetFileName(ref string FileName)
        {
            Boolean IsSuccess = false;
            var dlg = new OpenFileDialog()
            {
                Filter = "テキストファイル (*.txt)|*.txt|全てのファイル (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                FileName = dlg.FileName;
                IsSuccess = true;
            }
            return IsSuccess;
        }
    }
}
