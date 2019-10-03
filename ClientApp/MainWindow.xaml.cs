using Encode;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string text;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDlg = new OpenFileDialog();
            bool? result = openFileDlg.ShowDialog();

            if (result == true)
            {
                Filename.Text = openFileDlg.FileName;
                using var reader = new StreamReader(openFileDlg.FileName, Encoding.UTF8);
                text = reader.ReadToEnd();
            }
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEncoder encoder = SetEncoder();
                if (SaveTextToFile(encoder.Encrypt(text)) == true)
                {
                    EncryptInfo.Text = "File succesfully encrypted!";
                }
            }
            catch (System.Exception)
            {
                EncryptInfo.Text = "Something went wrong!!!";
            }
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IEncoder encoder = SetEncoder();
                if (SaveTextToFile(encoder.Decrypt(text)) == true)
                {
                    DecryptInfo.Text = "File succesfully decrypted!";
                }
            }
            catch (System.Exception)
            {
                DecryptInfo.Text = "Something went wrong!!!";
            }

        }
        private bool SaveTextToFile(string text)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files(*.txt)|*.txt"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                using var file = new StreamWriter(saveFileDialog.FileName, false, Encoding.Default); ;
                file.WriteLine(text);
                return true;
            }
            return false;
        }
        private IEncoder SetEncoder() => CipherList.SelectedIndex switch
        {
            0 => new CaesarCipher(int.Parse(a1.Text)),
            1 => new AffineCipher(int.Parse(a2.Text), int.Parse(b2.Text)),
            _ => null
        };

    }
}