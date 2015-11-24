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
        private string number;
        private string sender;
        private string sortCode;
        private string incidentNature;
        private string messageBody;
        private MessageFactory messageFactory;

        public MainWindow()
        {
            // initialise the string debugging variables
            number = "";
            sender = "";
            sortCode = "";
            incidentNature = "";
            messageBody = "";
            messageFactory = MessageFactory.Instance;

            InitializeComponent();
            smsRadio.IsChecked = true;
            addMessageTab.IsSelected = true;

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

            try {
                number = messageTxt.Text.Substring(0, messageTxt.Text.IndexOf(" "));
            } catch(Exception e)
            {
                MessageBox.Show("Please enter a valid international number (e.g. +462345678901) at the beginning of the message.");
                return;
            }

            string regex = @"(\+|00)(999|998|997|996|995|994|993|992|991|990|979|978|977|976|975|974|973|972|971|970|969|968|967|966|965|964|963|962|961|960|899|898|897|896|895|894|893|892|891|890|889|888|887|886|885|884|883|882|881|880|879|878|877|876|875|874|873|872|871|870|859|858|857|856|855|854|853|852|851|850|839|838|837|836|835|834|833|832|831|830|809|808|807|806|805|804|803|802|801|800|699|698|697|696|695|694|693|692|691|690|689|688|687|686|685|684|683|682|681|680|679|678|677|676|675|674|673|672|671|670|599|598|597|596|595|594|593|592|591|590|509|508|507|506|505|504|503|502|501|500|429|428|427|426|425|424|423|422|421|420|389|388|387|386|385|384|383|382|381|380|379|378|377|376|375|374|373|372|371|370|359|358|357|356|355|354|353|352|351|350|299|298|297|296|295|294|293|292|291|290|289|288|287|286|285|284|283|282|281|280|269|268|267|266|265|264|263|262|261|260|259|258|257|256|255|254|253|252|251|250|249|248|247|246|245|244|243|242|241|240|239|238|237|236|235|234|233|232|231|230|229|228|227|226|225|224|223|222|221|220|219|218|217|216|215|214|213|212|211|210|98|95|94|93|92|91|90|86|84|82|81|66|65|64|63|62|61|60|58|57|56|55|54|53|52|51|49|48|47|46|45|44|43|41|40|39|36|34|33|32|31|30|27|20|7|1)[0-9]{0,14}$";
            Regex r = new Regex(regex);
            Match match = r.Match(number);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid international number (e.g. +462345678901");
                return;
            }

            // get rid of the number inside the message body
            string messageBody = messageTxt.Text.Trim(number.ToCharArray());
            // get rid of the leading space
            messageBody = messageBody.Substring(1);

            if (messageBody.Length > 140)
            {
                MessageBox.Show("The message body must be less than 140 characters long");
                return;
            }

            Message sms = new SMS(messageBody, number);
            MessageBox.Show(sms.getMessageTxt());
            messageFactory.addMessage(sms);

            updateMessagesList();
        }

        private void processEmail()
        {
            string sender = "";
            string subject = "";
            string sortCode = "";
            string incidentNature = "";
            string messageBody = "";
            bool isSir = false;
            // split the string into elements - 0 - sender - 1 - subject - 2 - message body
            string[] elements = messageTxt.Text.Split('\n');
            // clear the empty space at the end of each element
            elements[0] = elements[0].Substring(0, elements[0].Length - 1);
            elements[1] = elements[1].Substring(0, elements[1].Length - 1);

            if (elements.Length < 3 || elements.Length > 5)
            {
                MessageBox.Show("Please enter: email, subject and message body. All separated by a new line.\n Include 'Sort Code:' and 'Incident Nature:' after the subject to record an incident.");
                return;
            }

            // check for standard email
            if (elements.Length == 3)
            {
                sender = elements[0];
                subject = elements[1];
                messageBody = elements[2];
            } 
            // else, it's an incident report
            else
            {
                sender = elements[0];
                subject = elements[1];
                sortCode = elements[2];
                incidentNature = elements[3];
                messageBody = elements[4];
                isSir = true;
            }

            // email regex
            string regex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex r = new Regex(regex);
            Match match = r.Match(sender);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            // validate the subject
            if (String.IsNullOrEmpty(subject))
            {
                MessageBox.Show("Please enter a subject.");
                return;
            }
            if (subject.Length > 20)
            {
                MessageBox.Show("The subject must be less than 20 characters.");
                return;
            }

            // validate the message body
            if (String.IsNullOrEmpty(messageBody))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }
            if (messageBody.Length > 1028)
            {
                MessageBox.Show("The email message body must be less than 1028 characters.");
            }

            // validate incident fields
            if (isSir && sortCode.ToLower().StartsWith("sort code:")) 
            {
                // get the number from the sort code line
                sortCode = sortCode.Replace(" ", "");
                sortCode = sortCode.Substring(sortCode.IndexOf(':')+1);
            }
            else if (isSir)
            {
                MessageBox.Show("The sort code is not valid. Try something like 'Sort Code: 99-99-99'");
                return;
            }

            // validate incident
            if (isSir && incidentNature.ToLower().StartsWith("incident nature:"))
            {
                int offset = 1;
                if (incidentNature.Substring(incidentNature.IndexOf(':') + 1, 1) == " ") offset = 2;
                incidentNature = incidentNature.Substring(incidentNature.IndexOf(':') + offset);
            }
            else if (isSir)
            {
                MessageBox.Show("Incident nature is not valid. Try something like 'Incident Nature: Theft'");
                return;
            }

            // validate the subject format
            string sirRegexString = @"SIR [0-9][0-9]/[0-9][0-9]/[0-9][0-9]";
            Regex sirRegex = new Regex(sirRegexString);
            Match matchRegex = sirRegex.Match(elements[1]);

            Email email;
            if (!matchRegex.Success && !isSir)
            {
                email = new StandardEmail(messageBody, sender, subject);
                MessageBox.Show("The standard email message was created.");
            }
            else if (isSir && matchRegex.Success)
            {
                email = new SIREmail(messageBody, sender, subject, sortCode, incidentNature);
                MessageBox.Show("The SIR email message was created.");
            }
            else
            {
                MessageBox.Show("Incident subject not valid. Try something like: 'SIR dd/mm/yy'");
                return;
            }

            messageFactory.addMessage(email);
            updateMessagesList();
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

        private void updateMessagesList()
        {
            foreach (Message msg in messageFactory.getAllMessages())
            {
                messagesBox.Items.Add(msg.print());
            }
        }

        private void messageTxt_KeyUp(object sender, KeyEventArgs e)
        {
            charCountTxt.Text = "";
            int charCounter = messageTxt.Text.Length;
            charCountTxt.Text = "Characters: " + charCounter;
        }

        private void menuTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(menuTab.SelectedIndex)
            {
                case 0:
                    addMessageGrid.Visibility = Visibility.Visible;
                    messagesGrid.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    addMessageGrid.Visibility = Visibility.Hidden;
                    messagesGrid.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }
}
