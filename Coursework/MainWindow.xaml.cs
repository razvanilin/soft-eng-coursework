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
            // validate the message body
            if (String.IsNullOrEmpty(messageTxt.Text))
            {
                MessageBox.Show("Please enter something in the message body.");
                return;
            }

            string number = messageTxt.Text.Substring(0, messageTxt.Text.IndexOf(" "));

            string regex = @"\+|00(999|998|997|996|995|994|993|992|991|990|979|978|977|976|975|974|973|972|971|970|969|968|967|966|965|964|963|962|961|960|899|898|897|896|895|894|893|892|891|890|889|888|887|886|885|884|883|882|881|880|879|878|877|876|875|874|873|872|871|870|859|858|857|856|855|854|853|852|851|850|839|838|837|836|835|834|833|832|831|830|809|808|807|806|805|804|803|802|801|800|699|698|697|696|695|694|693|692|691|690|689|688|687|686|685|684|683|682|681|680|679|678|677|676|675|674|673|672|671|670|599|598|597|596|595|594|593|592|591|590|509|508|507|506|505|504|503|502|501|500|429|428|427|426|425|424|423|422|421|420|389|388|387|386|385|384|383|382|381|380|379|378|377|376|375|374|373|372|371|370|359|358|357|356|355|354|353|352|351|350|299|298|297|296|295|294|293|292|291|290|289|288|287|286|285|284|283|282|281|280|269|268|267|266|265|264|263|262|261|260|259|258|257|256|255|254|253|252|251|250|249|248|247|246|245|244|243|242|241|240|239|238|237|236|235|234|233|232|231|230|229|228|227|226|225|224|223|222|221|220|219|218|217|216|215|214|213|212|211|210|98|95|94|93|92|91|90|86|84|82|81|66|65|64|63|62|61|60|58|57|56|55|54|53|52|51|49|48|47|46|45|44|43|41|40|39|36|34|33|32|31|30|27|20|7|1)[0-9]{0,14}$";
            Regex r = new Regex(regex);
            Match match = r.Match(number);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid international number (e.g. +46-234 5678901");
                return;
            }

            if (messageTxt.Text.Length > 140)
            {
                MessageBox.Show("The message body must be less than 140 characters long");
                return;
            }

            
        }

        private void processEmail()
        {
            // email regex
            string regex = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b";
            Regex r = new Regex(regex);
            Match match = r.Match(senderTxt.Text);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // validate the subject
            if (String.IsNullOrEmpty(subjectTxt.Text))
            {
                MessageBox.Show("Please enter a subject.");
                return;
            }
            if (subjectTxt.Text.Length > 20)
            {
                MessageBox.Show("The subject must be less than 20 characters.");
                return;
            }

            // validate the message body
            if (String.IsNullOrEmpty(messageTxt.Text))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }
            if (messageTxt.Text.Length > 1028)
            {
                MessageBox.Show("The email message body must be less than 1028 characters.");
            }
        }

        private void processTweet()
        {
            string regex = @"\@+[A-Za-z0-9]{1,15}";
            Regex r = new Regex(regex);
            Match match = r.Match(senderTxt.Text);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid Twitter ID (e.g. @awesomename)");
                senderTxt.TextWrapping = TextWrapping.Wrap;
                return;

            }
        }

        private void messageTxt_KeyUp(object sender, KeyEventArgs e)
        {
            charCountTxt.Text = "";
            int charCounter = messageTxt.Text.Length;
            charCountTxt.Text = "Characters: " + charCounter;
        }
    }
}
