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

namespace Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            smsRadio.IsChecked = true;
        }

        private void smsRadio_Checked(object sender, RoutedEventArgs e)
        {
            subjectLabel.Visibility = Visibility.Hidden;
            subjectTxt.Visibility = Visibility.Hidden;
        }

        private void emailRadio_Checked(object sender, RoutedEventArgs e)
        {
            subjectLabel.Visibility = Visibility.Visible;
            subjectTxt.Visibility = Visibility.Visible;
        }

        private void tweetRadio_Checked(object sender, RoutedEventArgs e)
        {
            subjectLabel.Visibility = Visibility.Hidden;
            subjectTxt.Visibility = Visibility.Hidden;
        }

        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            // check to see which radio button is checked and handle the message differently
            if (smsRadio.IsChecked.Value)
            {
                processSms();
            }
            else if (emailRadio.IsChecked.Value)
            {
                processEmail();
            }
            else if (tweetRadio.IsChecked.Value)
            {
                processTweet();
            }
        }

        private void processSms()
        {
            string regex = @"\+(9[976]\d|8[987530]\d|6[987]\d|5[90]\d|42\d|3[875]\d|
                            2[98654321]\d|9[8543210]|8[6421]|6[6543210]|5[87654321]|
                            4[987654310]|3[9643210]|2[70]|7|1)
                            \W*\d\W*\d\W*\d\W*\d\W*\d\W*\d\W*\d\W*\d\W*(\d{1,2})$";
            Regex r = new Regex(regex);
            Match match = r.Match(senderTxt.Text);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid international number (e.g. +46-234 5678901");
                return;    
            }

            
        }

        private void processEmail()
        {

        }

        private void processTweet()
        {

        }
    }
}
