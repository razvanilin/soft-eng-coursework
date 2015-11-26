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

        private MessageFactory messageFactory;
        private IncidentManager incidentManager;

        public MainWindow()
        {

            messageFactory = MessageFactory.Instance;
            incidentManager = IncidentManager.Instance;

            InitializeComponent();
            addMessageTab.IsSelected = true;
            messageFactory.processAll();
            updateMessagesList();
            MessageBox.Show(messageFactory.getAllMessages().Count.ToString());
    
        }
  
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            // check if the message id field contains something
            if (String.IsNullOrEmpty(idTxt.Text))
            {
                MessageBox.Show("Message ID is empty. Input something like 'E123456789'.");
                return;
            }

            string idRegexString = @"(S|E|T)([0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9])";
            Regex idRegex = new Regex(idRegexString);

            if (!idRegex.IsMatch(idTxt.Text))
            {
                MessageBox.Show("The message ID is not valid. Try something like 'E123456789'.");
                return;
            }

            // check if the factory already has the id
            if (messageFactory.hasId(idTxt.Text))
            {
                MessageBox.Show("The message ID that was entered already exists.");
                return;
            }

            // check to see which type of message must be processed
            switch(idTxt.Text.Substring(0,1))
            {
                case "S":
                    processSms();
                    break;
                case "E":
                    processEmail();
                    break;
                case "T":
                    processTweet();
                    break;
                default:
                    break;
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
            string number;
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

            Message sms = new SMS(idTxt.Text, messageBody, number);
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

            // make sure there are at least 3 elements in the message
            if (elements.Length < 3)
            {
                MessageBox.Show("Some elements are missing from your email message. \nMake sure you have at least a sender, subject and message body.");
                return;
            }

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
            else if (elements.Length == 5)
            {
                sender = elements[0];
                subject = elements[1];
                sortCode = elements[2];
                incidentNature = elements[3];
                messageBody = elements[4];
                isSir = true;
            }
            else
            {
                MessageBox.Show("Your message does not have the right amount of elements.\nStandart email has 3 and SIR emails have 5.");
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

            // validate sort code
            if (isSir && sortCode.ToLower().StartsWith("sort code:")) 
            {
                // get the number from the sort code line
                sortCode = sortCode.Replace(" ", "");
                sortCode = sortCode.Substring(sortCode.IndexOf(':')+1, sortCode.Length-2-sortCode.IndexOf(":"));

                string sortRegexString = @"[0-9][0-9]\-[0-9][0-9]\-[0-9][0-9]";
                Regex sortRegex = new Regex(sortRegexString);
                Match sortMatch = sortRegex.Match(sortCode);
                if (!sortMatch.Success)
                {
                    MessageBox.Show("The sort code number is not valid. Try something like this: 99-99-99");
                    return;
                }
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
                incidentNature = incidentNature.Substring(0, incidentNature.Length - 1);
                // check to see if the nature type is a valid one
                bool isMatch = false;
                string messageTypes = "";
                foreach (string incidentType in incidentManager.getTypes())
                {
                    if (incidentType.ToLower() == incidentNature.ToLower())
                    {
                        isMatch = true;
                        break;
                    }
                    messageTypes += incidentType + ", ";
                }

                if (!isMatch)
                {
                    MessageBox.Show(incidentNature.Substring(incidentNature.Length-1));
                    MessageBox.Show("Incident nature is not valid. Use one of these: " + messageTypes);
                    return;
                }
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
                email = new StandardEmail(idTxt.Text, messageBody, sender, subject);
                MessageBox.Show("The standard email message was created.");
            }
            else if (isSir && matchRegex.Success)
            {
                email = new SIREmail(idTxt.Text, messageBody, sender, subject, sortCode, incidentNature);
                MessageBox.Show("The SIR email message was created.");
            }
            else
            {
                MessageBox.Show("Incident subject not valid. Try something like: 'SIR dd/mm/yy'");
                return;
            }

            messageFactory.addMessage(email);
            messageFactory.recordIncident(email);

            MessageBox.Show(email.getMessageTxt());
            updateMessagesList();
        }

        private void processTweet()
        {
            // get the elements
            string sender = messageTxt.Text.Substring(0, messageTxt.Text.IndexOf(" "));
            string messageBody = messageTxt.Text.Substring(sender.Length + 1);

            string regex = @"^@(\w){1,15}$";
            Regex r = new Regex(regex);
            Match match = r.Match(sender);
            // validate the sender
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid Twitter ID (e.g. @awesomename)");
                return;

            }

            if (String.IsNullOrEmpty(messageBody))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

            if (messageBody.Length > 140)
            {
                MessageBox.Show("The message body must be less than 140 characters long");
                return;
            }

            Message tweet = new Tweet(idTxt.Text, messageBody, sender);
            MessageBox.Show(tweet.getMessageTxt());
            messageFactory.addMessage(tweet);
            updateMessagesList();

            Mention mention = Mention.Instance;
            string hashtagString = "";
            foreach(string m in mention.getMentions().Keys)
            {
                hashtagString += m + " " + mention.getMentions()[m];
            }

            MessageBox.Show(hashtagString);
     
        }

        private void updateMessagesList()
        {
            messagesBox.Items.Clear();
            foreach (Message msg in messageFactory.getAllMessages())
            {
                messagesBox.Items.Add(msg.print());
            }

            // update trends
            trendsBox.Items.Clear();
            foreach(string hashtag in Hashtag.Instance.getHashtags().Keys)
            {
                trendsBox.Items.Add(Hashtag.Instance.getHashtags()[hashtag] + ", " + hashtag);
            }

            trendsBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Descending));

            // update mentions
            mentionsBox.Items.Clear();
            foreach(string mention in Mention.Instance.getMentions().Keys)
            {
                mentionsBox.Items.Add(Mention.Instance.getMentions()[mention] + ", " + mention);
            }
            mentionsBox.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Descending));

            // update incidents
            incidentsBox.Items.Clear();
            foreach(Incident incident in IncidentManager.Instance.getIncidents())
            {
                incidentsBox.Items.Add(incident.getSortCode() + " - " + incident.getNature());
            }

            // update urls
            urlBox.Items.Clear();
            foreach(string url in URLQuarantine.Instance.getList())
            {
                urlBox.Items.Add(url);
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
                    trendsGrid.Visibility = Visibility.Hidden;
                    mentionsGrid.Visibility = Visibility.Hidden;
                    incidentsGrid.Visibility = Visibility.Hidden;
                    urlGrid.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    addMessageGrid.Visibility = Visibility.Hidden;
                    messagesGrid.Visibility = Visibility.Visible;
                    trendsGrid.Visibility = Visibility.Hidden;
                    mentionsGrid.Visibility = Visibility.Hidden;
                    incidentsGrid.Visibility = Visibility.Hidden;
                    urlGrid.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    addMessageGrid.Visibility = Visibility.Hidden;
                    messagesGrid.Visibility = Visibility.Hidden;
                    trendsGrid.Visibility = Visibility.Visible;
                    mentionsGrid.Visibility = Visibility.Hidden;
                    incidentsGrid.Visibility = Visibility.Hidden;
                    urlGrid.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    addMessageGrid.Visibility = Visibility.Hidden;
                    messagesGrid.Visibility = Visibility.Hidden;
                    trendsGrid.Visibility = Visibility.Hidden;
                    mentionsGrid.Visibility = Visibility.Visible;
                    incidentsGrid.Visibility = Visibility.Hidden;
                    urlGrid.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    addMessageGrid.Visibility = Visibility.Hidden;
                    messagesGrid.Visibility = Visibility.Hidden;
                    trendsGrid.Visibility = Visibility.Hidden;
                    mentionsGrid.Visibility = Visibility.Hidden;
                    incidentsGrid.Visibility = Visibility.Visible;
                    urlGrid.Visibility = Visibility.Hidden;
                    break;
                case 5:
                    addMessageGrid.Visibility = Visibility.Hidden;
                    messagesGrid.Visibility = Visibility.Hidden;
                    trendsGrid.Visibility = Visibility.Hidden;
                    mentionsGrid.Visibility = Visibility.Hidden;
                    incidentsGrid.Visibility = Visibility.Hidden;
                    urlGrid.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void serializeBtn_Click(object sender, RoutedEventArgs e)
        {
            serializeAll();
        }

        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            serializeAll();
        }

        private void serializeAll()
        {
            messageFactory.serializeAll();
            URLQuarantine.Instance.serialize();
            Hashtag.Instance.serialize();
            Mention.Instance.serialize();
            IncidentManager.Instance.serialize();
        }
    }
}
