using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;
//using System.Windows.Automation.Peers;
//using System.Windows.Forms;

namespace Phase4
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        //MyListBox.SelectionMode = SelectionMode.Single;
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
        private void WindowLoaded(object sender, RoutedEventArgs e)
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
            // 不要（再読み込み時に使用）
            //Properties.Settings.Default.Reload();

            this.Left = Properties.Settings.Default.ScreenLeft;
            this.Top = Properties.Settings.Default.ScreenTop;
            this.Width = Properties.Settings.Default.ScreenWidth;
            this.Height = Properties.Settings.Default.ScreenHeight;
            TextDirName.Text = Properties.Settings.Default.ThumnailDirPath;
            TextFileName.Text = Properties.Settings.Default.TextFileName;
        }

        private void SaveSetting()
        {
            Properties.Settings.Default.ScreenLeft = this.Left;
            Properties.Settings.Default.ScreenTop = this.Top;
            Properties.Settings.Default.ScreenWidth = this.Width;
            Properties.Settings.Default.ScreenHeight = this.Height;
            Properties.Settings.Default.ThumnailDirPath = TextDirName.Text;
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
                MessageBox.Show("サムネイルを表示するフォルダが存在しないか、画像ファイルがありません。" + Environment.NewLine + this.TextDirName);
                return;
            }

            // クラスをローカル変数にしたので、クリア削除は不要
            //if (ListBoxTumnail.Items.Count != 0)
            //{
            //    Exception
            //    ListBoxTumnail.Items.Clear();

            //    Never displayed
            //    ListBoxTumnail.ClearValue(ListBox.ItemsSourceProperty);
            //}

            var thumnail = new List<Thumnail>();
            foreach (string file in files)
            {
                thumnail.Add(new Thumnail(file));
            }

            DataContext = thumnail;
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
                TextDirName.Text = dlg.FileName;
                IsSuccess = true;
            }
            return IsSuccess;
        }

        // サンプルアプリと同じふるまい
        private Boolean GetDirectoryNameForForm()
        {
            Boolean IsSuccess = false;
            var dlg = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = "サムネイル表示対象のフォルダ選択"
            };

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextDirName.Text = dlg.SelectedPath;
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
            if (Directory.Exists(TextDirName.Text))
            {
                string[] AllFile = Directory.GetFiles(TextDirName.Text);
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