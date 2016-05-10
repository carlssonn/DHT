using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace DHT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string textBoxText;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= textBox_GotFocus;
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            textBoxText = WashText(textBox.Text);

            if (IsAllNumeric(textBoxText))
            {
                string[] numericalList = StringToList(textBoxText);

                Validate(numericalList);
                HandleNumericalList(numericalList);

                Console.WriteLine("all numerical" + textBoxText);
            }
            else if (IsAllAlphabetical(textBoxText))
            {
                string[] alphabeticalList = StringToList(textBoxText);

                Validate(alphabeticalList);
                HandleAlphabeticalList(alphabeticalList);

                Console.WriteLine("all alphabetical" + textBoxText);
            }
            else
            {
                Console.WriteLine("Eror: can only be all alphabetical or all numerical.");
            }
        }

        #region Handlers

        private void HandleNumericalList(string[] numericalStringList)
        {
            int[] numericalIntList = StringListToIntList(numericalStringList);

            string string1 = string.Empty;
            string string2 = string.Empty;
            string string3 = string.Empty;
            string string4 = string.Empty;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox4.Items.Clear();

            foreach (int num in numericalIntList)
            {
                string1 += (char)num;//Numerical To Ascii Alphabetical

                string2 += NumericalToIterativAlphabetical(num);
            }


            ListBoxItem itemString1 = new ListBoxItem();
            ListBoxItem itemString2 = new ListBoxItem();
            itemString1.Content = string1;
            itemString2.Content = string2;

            listBox1.Items.Add(itemString1);
            listBox2.Items.Add(itemString2);
        }

        private string NumericalToIterativAlphabetical(int num)
        {/* http://stackoverflow.com/questions/181596/how-to-convert-a-column-number-eg-127-into-an-excel-column-eg-aa */
            int dividend = num;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private void HandleAlphabeticalList(string[] alphabeticalList)
        {

            foreach (string str in alphabeticalList)
            {

            }
        }

        #endregion //Handlers

        #region Helpmethods

        private void Validate(string[] list)
        {
            foreach(string stringItem in list)
            {
                if (stringItem.Length > 5)
                {
                    System.Windows.MessageBox.Show("Maximum 5 symbols in a row.");
                    textBoxText = string.Empty;
                    textBox.Text = string.Empty;
                }
            }
        }

        private bool IsAllAlphabetical(string text)
        {
            string resultText = text.Replace(" ", "");
            bool isAllAlpha = resultText.All(char.IsLetter);

            return isAllAlpha;
        }

        private string WashText(string text)
        {
            text = Regex.Replace(text, @"\s+", " ");

            return text;
        }

        private bool IsAllNumeric(string text)
        {
            string resultText = text.Replace(" ", "");
            bool isAllDigits = resultText.All(char.IsDigit);

            return isAllDigits;
        }

        private string[] StringToList(string text)
        {
            string[] valueList = text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            return valueList;
        }

        private int[] StringListToIntList(string[] numericalStringList)
        {
            return Array.ConvertAll(numericalStringList, int.Parse);
        }

        #endregion //Helpmethods
    }
}
