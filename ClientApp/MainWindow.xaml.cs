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
        public List<string> Items { get; set; } = new List<string> {
            "Шифр Цезаря",
            "Афінний шифр Цезаря",
        };
        private IEncoder encoder;

        private string text;
        private string directory;

        public MainWindow()
        {
            InitializeComponent();
            List.ItemsSource = Items;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            bool? result = openFileDlg.ShowDialog();

            if (result == true)
            {
                Filename.Text = openFileDlg.FileName;
                directory = Path.GetDirectoryName(openFileDlg.FileName);
                text = File.ReadAllText(openFileDlg.FileName, Encoding.Default);
            }
        }

        private void EncodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(A.Text, out int a) && int.TryParse(B.Text, out int b))
            {
                using (var file = new StreamWriter($"{directory}/encoded.txt", false, Encoding.Default))
                {
                    encoder = List.SelectedItem.ToString() switch
                    {
                        "Шифр Цезаря" => new CaesarCipher(a),
                        "Aфінний Шифр Цезаря" => new AffineCipher(a, b),
                    };
                    var codedText = encoder.Encrypt(text);
                    file.WriteLine(codedText);
                }
                EncryptInfo.Text = "file succesfully encrypted!";
            }
            else
            {
                EncryptInfo.Text = "a and b are not valid";
            }
        }

        private void DecodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(A.Text, out int a) && int.TryParse(B.Text, out int b))
            {
                using (var file = new StreamWriter($"{directory}/decoded.txt", false, Encoding.Default))
                {
                    IEncoder code = new AffineCipher(a, b);
                    var codedText = code.Decrypt(text);
                    file.WriteLine(codedText);
                }
                DecryptInfo.Text = "file succesfully decrypted!";
            }
            else
            {
                DecryptInfo.Text = "a and b are not valid";
            }
        }

        private void List_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}