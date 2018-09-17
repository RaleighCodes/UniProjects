using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CPSC_481_PROJECT
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {

        public static bool ValidLogin { get; set; }

        private int userListIndex;

        //timer for incorrect login text prompt
        DispatcherTimer invalidLoginTextTimer = new DispatcherTimer();
        


        /// <summary>
        /// Initialize LoginPage UserControl elements
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();
            ValidLogin = false;
            invalidLoginTextTimer.Tick += invalidLoginTextTimer_Tick;
            invalidLoginTextTimer.Interval = new TimeSpan(0, 0, 3); //timer lasts for 3 second intervals
        }

        /// <summary>
        /// After interval time has ticked, hide invalid text prompt and stop timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invalidLoginTextTimer_Tick(object sender, EventArgs e)
        {
            InvalidLoginText.Visibility = Visibility.Hidden;

            invalidLoginTextTimer.Stop();
        }

        /// <summary>
        /// Switch from Login Page to Sign-up Page on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginToSignupButton_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new SignupPage());
        }

        /// <summary>
        /// Switch from Login Page to Main Page after checking for valid login credentials
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginToMainButton_Click(object sender, RoutedEventArgs e)
        {
            bool inputFieldsEmpty = false;


            //check both login input fields non-empty
            if (string.IsNullOrEmpty(UsernameInput.Text)|| string.IsNullOrEmpty(LoginPasswordBox.Password))
            {
                inputFieldsEmpty = true;
            }

            if(!inputFieldsEmpty)
            {
                //username, password case sensitive
                String loginUsername = UsernameInput.Text.ToLower();
                String loginPassword = LoginPasswordBox.Password;


                
                //check if login info matches with any entry in existing user list
                foreach (Profile user in MainWindow.UserList)
                {
                    Dictionary<String, String> userLogins = user.getUsernamePassword();
                    String actualUsername = userLogins.Keys.ElementAt(0);
                    String actualPassword = "";

                    //check if username and password is valid login pair
                    if (actualUsername.ToLower().Equals(loginUsername) && 
                        userLogins.TryGetValue(actualUsername, out actualPassword) &&
                        String.Equals(actualPassword, loginPassword))
                    {
                        userListIndex = MainWindow.UserList.IndexOf(user);
                        ValidLogin = true;
                        break;

                    }
                   
                }
            }
           

            //switch to main page if login is valid
            if (ValidLogin)
            {
                PageSwitcher.Switch(new MainPage(userListIndex));
            }
            //show empty input prompt if missing username or password
            else if(inputFieldsEmpty)
            {
                InvalidLoginPrompt("Please enter input for all fields!");

            }
            //otherwise show incorrect login text on a timer
            else
            {
                InvalidLoginPrompt("Incorrect Username/Password combination!");
            }
            
           
        }

        /// <summary>
        /// Display invalid login prompt with context, and start timer
        /// </summary>
        /// <param name="textPrompt"></param>
        private void InvalidLoginPrompt(String textPrompt)
        {
            InvalidLoginText.Text = textPrompt;
            InvalidLoginText.Visibility = Visibility.Visible;
            //invalidLoginTextTimer.Start();
        }

       
    }
}
