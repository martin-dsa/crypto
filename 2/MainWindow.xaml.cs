using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace _2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string text;
        private string directory;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock

            if (result == true)
            {
                Filename.Text = openFileDlg.FileName;
                directory = System.IO.Path.GetDirectoryName(openFileDlg.FileName);


                text = File.ReadAllText(openFileDlg.FileName, Encoding.Default);
            }
        }

        private void CodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(A.Text, out int a) && int.TryParse(B.Text, out int b))
            {
                using (var file = new StreamWriter($"{directory}/coded.txt"))
                {
                    var code = new Code(a, b);
                    var codedText = code.Encrypt(text);
                    file.WriteLine(codedText);
                }
            }
            else
            {
                isValid.Text = "a and b are not valid";
            }

        }
    }
}
