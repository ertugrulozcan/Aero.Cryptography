using Aero.Cryptography.Contracts;
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
        /// <summary>
        /// Rsa Provider
        /// </summary>
        private RsaProvider Rsa { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Rsa = new RsaProvider(RsaProvider.BitLength.RSA512);
        }

        /// <summary>
        /// Encrypt Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();

            try
            {
                // Text to byte array as selected encoding procedure
                byte[] data;
                if (Rsa.Encoder.PatternConverter == null)
                    data = System.Text.Encoding.Unicode.GetBytes(this.InputTextBox.Text);
                else
                    data = Rsa.Encoder.PatternConverter.EncodingProcedure.GetBytes(this.InputTextBox.Text);
                
                // Encrypt
                var secret = Rsa.Encoder.Encrypt(data);

                // Sign
                if (this.IsSignCheckBox.IsChecked.Value)
                {
                    secret.Sign = this.Rsa.Signer.Sign(data);
                }

                // Print result
                this.OutputTextBox.Text = secret.Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }
        
        /// <summary>
        /// Decrypt Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var secret = new Cypher();
                secret.Deserialize(this.OutputTextBox.Text);

                // Decrypt
                byte[] message = this.Rsa.Decoder.Decrypt(secret);

                Encoding encodingProcedure;
                if (Rsa.Decoder.PatternConverter == null)
                    encodingProcedure = System.Text.Encoding.Unicode;
                else
                    encodingProcedure = Rsa.Decoder.PatternConverter.EncodingProcedure;

                // Print to result
                this.DecryptionResultTextBox.Text = encodingProcedure.GetString(message);

                // Signature verification
                if (secret.Sign == null)
                {
                    this.SignatureVerificationTextBlock.Text = "Data is unsigned.";
                    this.SignatureVerificationTextBlock.Foreground = new SolidColorBrush(Colors.DarkOrange);
                }
                else
                {
                    bool isVerified = this.Rsa.Signer.Verify(message, secret.Sign);
                    if (isVerified)
                    {
                        this.SignatureVerificationTextBlock.Text = "Signature verified.";
                        this.SignatureVerificationTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        this.SignatureVerificationTextBlock.Text = "Signature not verified!";
                        this.SignatureVerificationTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        /// <summary>
        /// Test Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            //byte[] bytes = Encoding.UTF8.GetBytes(this.TestTextBox.Text);
            //string text = BitConverter.ToString(bytes);
            //MessageBox.Show(text, this.TestTextBox.Text);

            //Cypher secret = new Cypher(new BigInteger(9716));
            //var cypher = this.rsa.Encoder.Encrypt(secret);
            //MessageBox.Show(this.rsa.Decoder.Decrypt(cypher).ToString());
        }

        private void Clear()
        {
            this.DecryptionResultTextBox.Text = string.Empty;
            this.SignatureVerificationTextBlock.Text = string.Empty;
            this.SignatureVerificationTextBlock.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
