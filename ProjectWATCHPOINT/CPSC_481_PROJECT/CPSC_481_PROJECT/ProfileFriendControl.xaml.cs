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

namespace CPSC_481_PROJECT
{
    /// <summary>
    /// Interaction logic for ProfileFriendControl.xaml
    /// </summary>
    public partial class ProfileFriendControl : UserControl
    {
        private Profile user, friend;
        MainPage userPage;
        StackPanel parentPanel;

        public ProfileFriendControl(Profile userProfile, Profile friendProfile, MainPage currentPage)
        {
            InitializeComponent();

            //load current user
            user = userProfile;

            //load current instance of MainPage
            userPage = currentPage;

            //load friend profile details into user control elements 
            this.friend = friendProfile;
            FriendProfileImage.Source = new BitmapImage(new Uri("pack://application:,,," + friend.ProfileIconSource));
            FriendUsername.Text = friend.getUsernamePassword().Keys.ElementAt(0);
                        
            switch(FriendUsername.Text.Length)
            {
                case 15:
                case 16:
                case 17:
                    FriendUsername.FontSize = 18;
                    break;
                case 18:
                    FriendUsername.FontSize = 16;
                    break;
                case 19:
                    FriendUsername.FontSize = 15;
                    break;
                case 20:
                    FriendUsername.FontSize = 14;
                    break;
                default:
                    break;
            }

            if (!String.IsNullOrEmpty(friend.Status) && !String.IsNullOrWhiteSpace(friend.Status))
                FriendStatusMessage.Text = friend.Status;
            else
                FriendStatusMessage.Text = "Status Message";

            

            parentPanel = userPage.FriendsListPanel;

            
        }

        /// <summary>
        /// Highlights the Friend UserControl background to convey selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FriendControlSelectButton_Click(object sender, RoutedEventArgs e)
        {
            userPage.NoFriendStatsText.Visibility = Visibility.Hidden;

            foreach(ProfileFriendControl friendControl in parentPanel.Children)
            {
                friendControl.FriendControlBackground.Background = Brushes.White;
                

                if (this.Equals(friendControl))
                {
                    this.FriendControlBackground.Background = Brushes.Gold;
                    userPage.loadFriendStats(friend);
                    
                }                
                                   

            }
        }

        /// <summary>
        /// Removes friend from Friends List and Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnfriendButton_Click(object sender, RoutedEventArgs e)
        {
            
            userPage.clearFriendStats();
            userPage.NoFriendStatsText.Visibility = Visibility.Visible;
            


            //remove from friend list Panel
            foreach (ProfileFriendControl friendControl in parentPanel.Children)
            {
                friendControl.FriendControlBackground.Background = Brushes.White;

                if (this.Equals(friendControl))
                {

                    parentPanel.Children.Remove(this);
                    break;
                }


            }

            //remove from Profile friend List
            List<Profile> userFriendList = user.getFriendsList();
            foreach(Profile friendToRemove in userFriendList)
            {
                if(friend.Equals(friendToRemove) && !friend.Equals(user))
                {
                    userFriendList.Remove(friend);
                    //user.removeFriend(friend);
                    break;
                }
            }

            

            userPage.remakeSoloSearchPanel();
        }
    }
}
