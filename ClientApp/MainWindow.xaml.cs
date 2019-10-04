using Encode;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string _text;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDlg = new OpenFileDialog();
            var result = openFileDlg.ShowDialog();

            if (result != true) return;
            Filename.Text = openFileDlg.FileName;
            using var reader = new StreamReader(openFileDlg.FileName, Encoding.UTF8);
            _text = reader.ReadToEnd();
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var encoder = SetEncoder();
                if (SaveTextToFile(encoder.Encrypt(_text)))
                {
                    EncryptInfo.Text = "File successfully encrypted!";
                }
            }
            catch (Exception)
            {
                EncryptInfo.Text = "Something went wrong!!!";
            }
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var encoder = SetEncoder();
                if (SaveTextToFile(encoder.Decrypt(_text)))
                {
                    DecryptInfo.Text = "File successfully decrypted!";
                }
            }
            catch (Exception)
            {
                DecryptInfo.Text = "Something went wrong!!!";
            }
        }

        private static bool SaveTextToFile(string text)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files(*.txt)|*.txt"
            };
            if (saveFileDialog.ShowDialog() != true) return false;
            using var file = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default);
            file.Write(text);
            return true;
        }

        private IEncoder SetEncoder() => CipherList.SelectedIndex switch
        {
            0 => new CaesarCipher(int.Parse(a1.Text)),
            1 => new AffineCipher(int.Parse(a2.Text), int.Parse(b2.Text)),
            2 => new XorCipher
            {
                Y1 = int.Parse(a3.Text),
                Y2 = int.Parse(b3.Text),
                Y3 = int.Parse(c3.Text),
            },
            _ => null,
        };
    }
}