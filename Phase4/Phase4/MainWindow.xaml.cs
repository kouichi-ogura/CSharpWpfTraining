using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.IO;
//using System.Windows.Forms;

namespace Phase4
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Dto> _dtos = new ObservableCollection<Dto>();

        //MyListBox.SelectionMode = SelectionMode.Single;
            private string ThumNailDirPath = "";

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
            this.ThumNailDirPath = Properties.Settings.Default.ThumNailDirPath;
            TextFileName.Text = Properties.Settings.Default.TextFileName;
        }

        private void SaveSetting()
        {
            Properties.Settings.Default.ScreenLeft   = this.Left;
            Properties.Settings.Default.ScreenTop    = this.Top;
            Properties.Settings.Default.ScreenWidth  = this.Width;
            Properties.Settings.Default.ScreenHeight = this.Height;
            Properties.Settings.Default.ThumNailDirPath = this.ThumNailDirPath;
            Properties.Settings.Default.TextFileName = TextFileName.Text;

            Properties.Settings.Default.Save();
        }

        private void ButtonOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            if (! GetDirectoryName() )
            {
                MessageBox.Show("フォルダが選択されていません");
                return;
            }
            DrawThumnail();
        }

        private void DrawThumnail()
        {
            if ( ! Directory.Exists(this.ThumNailDirPath) )
            {
                MessageBox.Show("サムネイルを表示するフォルダが存在しません" + Environment.NewLine + this.ThumNailDirPath);
                return;
            }

            ListBoxTumnail.Items.Clear();
            // TODO:テキストコントロールのファイルをお試しで表示
            _dtos.Add(new Dto(this.ThumNailDirPath, this.ThumNailDirPath));
            _dtos.Add(new Dto(this.ThumNailDirPath, this.ThumNailDirPath));
            ListBoxTumnail.ItemsSource = _dtos;

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
                this.ThumNailDirPath = dlg.FileName;
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

    // TODO：別ファイルへ引っ越し
    public sealed class Dto
    {
        public Dto(string fileName ="", string name = "")
        {
            FileName = fileName;
            Name = name;
        }

        public string FileName { get; set; }
        public string Name { get; set; }
    }

}
