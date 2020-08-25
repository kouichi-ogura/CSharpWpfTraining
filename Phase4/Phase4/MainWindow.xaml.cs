using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
//using System.Windows.Automation.Peers;
//using System.Windows.Forms;

namespace Phase4
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Thumnail> m_thumnail = new ObservableCollection<Thumnail>();

        //MyListBox.SelectionMode = SelectionMode.Single;
        private string m_ThumNailDirPath = "";
        private string[] m_TargetExts = { ".jpg", ".bmp", ".png", ".tiff", ".gif" };

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
            ListBoxTumnail.SelectionMode = SelectionMode.Single;

            DrawThumnail();
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
            this.m_ThumNailDirPath = Properties.Settings.Default.m_ThumNailDirPath;
            TextFileName.Text = Properties.Settings.Default.TextFileName;
        }

        private void SaveSetting()
        {
            Properties.Settings.Default.ScreenLeft = this.Left;
            Properties.Settings.Default.ScreenTop = this.Top;
            Properties.Settings.Default.ScreenWidth = this.Width;
            Properties.Settings.Default.ScreenHeight = this.Height;
            Properties.Settings.Default.m_ThumNailDirPath = this.m_ThumNailDirPath;
            Properties.Settings.Default.TextFileName = TextFileName.Text;

            Properties.Settings.Default.Save();
        }

        private void ButtonOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            if (!GetDirectoryName())
            {
                MessageBox.Show("フォルダが選択されていません");
                return;
            }

            DrawThumnail();
        }

        private void DrawThumnail()
        {
            string[] files = null;
            if (!GetFileList(ref files))
            {
                MessageBox.Show("サムネイルを表示するフォルダが存在しないか、画像ファイルがありません。" + Environment.NewLine + this.m_ThumNailDirPath);
                return;
            }

            if (ListBoxTumnail.Items.Count != 0)
            {
                ListBoxTumnail.Items.Clear();
            }

            foreach (string file in files)
            {
                m_thumnail.Add(new Thumnail(file));
            }
            ListBoxTumnail.ItemsSource = m_thumnail;
        }

        // サンプルアプリとはふるまい違うけど、こちらの方がパスを貼り付けできるので便利
        private Boolean GetDirectoryName()
        {
            Boolean IsSuccess = false;
            var dlg = new CommonOpenFileDialog("サムネイル表示対象のフォルダ選択")
            {
                IsFolderPicker = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                this.m_ThumNailDirPath = dlg.FileName;
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

        private Boolean GetFileList(ref string[] files)
        {
            Boolean IsSuccess = false;
            if (Directory.Exists(this.m_ThumNailDirPath))
            {
                string[] AllFile = Directory.GetFiles(this.m_ThumNailDirPath);
                files = AllFile.Where(file => m_TargetExts.Any(pattern => file.ToLower().EndsWith(pattern))).ToArray();

                if (files.Length > 0)
                {
                    IsSuccess = true;
                }
            }
            return IsSuccess;
        }
    }
}