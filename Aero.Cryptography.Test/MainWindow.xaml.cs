using Aero.Cryptography.Algorithms.Rsa;
using Aero.Cryptography.Utilities;
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

namespace Aero.Cryptography.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RsaProvider rsa;

        public MainWindow()
        {
            InitializeComponent();
            this.rsa = new RsaProvider(RsaProvider.BitLength.RSA1024);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] data;
                if (rsa.Encoder.PatternConverter == null)
                    data = System.Text.Encoding.Unicode.GetBytes(this.InputTextBox.Text);
                else
                    data = rsa.Encoder.PatternConverter.EncodingProcedure.GetBytes(this.InputTextBox.Text);

                this.OutputTextBox.Text = rsa.Encoder.Encrypt(data).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            Algorithms.Contracts.ISecret secret = new Cypher(this.OutputTextBox.Text);
            byte[] message = this.rsa.Decoder.Decrypt(secret);

            if (rsa.Decoder.PatternConverter == null)
                this.DecryptionResultTextBox.Text = System.Text.Encoding.Unicode.GetString(message);
            else
                this.DecryptionResultTextBox.Text = rsa.Decoder.PatternConverter.EncodingProcedure.GetString(message);            
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            //byte[] bytes = Encoding.UTF8.GetBytes(this.TestTextBox.Text);
            //string text = BitConverter.ToString(bytes);
            //MessageBox.Show(text, this.TestTextBox.Text);

            //Cypher secret = new Cypher(new BigInteger(9716));
            //var cypher = this.rsa.Encoder.Encrypt(secret);
            //MessageBox.Show(this.rsa.Decoder.Decrypt(cypher).ToString());
        }
    }
}
