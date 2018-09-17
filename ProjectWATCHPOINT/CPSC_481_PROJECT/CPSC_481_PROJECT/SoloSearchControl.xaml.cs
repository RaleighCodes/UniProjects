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
    /// Interaction logic for SoloSearchControl.xaml
    /// </summary>
    public partial class SoloSearchControl : UserControl
    {
        private Profile currentUser, loginUser;
        MainPage currentPage;
        


        /// <summary>
        /// Constructor for Solo Search UserControl
        /// </summary>
        /// <param name="currentProfile"></param>
        /// <param name="currentPage"></param>
        public SoloSearchControl(Profile currentProfile, MainPage currentPage)
        {
            InitializeComponent();

            //Profile of current user
            currentUser = currentProfile;

            this.currentPage = currentPage;

            //should not be able to add self as friend
            loginUser = currentPage.getCurrentProfile();
            if (currentUser.Equals(loginUser))
            {
                SoloSearchAddFriendButton.Visibility = Visibility.Hidden;
                FriendAddedText.Visibility = Visibility.Hidden;
            }

            //should not be able to add if already friends
            foreach (Profile friend in loginUser.getFriendsList())
            {
                if (friend.Equals(currentUser))
                {
                    SoloSearchAddFriendButton.Visibility = Visibility.Hidden;
                    FriendAddedText.Visibility = Visibility.Visible;
                }
                    

 
            }
          
            //load profile picture and username
            SoloSearchProfileImage.Source = new BitmapImage(new Uri("pack://application:,,," + currentUser.ProfileIconSource));
            SoloSearchUsername.Text = currentUser.getUsernamePassword().Keys.ElementAt(0);

            //load role icon based on profile settings
            changeSoloSearchRole(currentUser);


            //load hero icon based on profile settings
            changeSoloSearchHero(currentUser);



        }

        /// <summary>
        /// returns profile associated with UserControl
        /// </summary>
        /// <returns></returns>
        public Profile getProfile()
        {
            return currentUser;
        }

        /// <summary>
        /// Change Role Icon on user's SoloSearchControl
        /// </summary>
        /// <param name="newRole"></param>
        public void changeSoloSearchRole(Profile user)
        {
            String roleIconSource;
            switch (user.Role)
            {
                case "Offense":
                   roleIconSource = "/Images/OffenseIcon.png";                   
                   break;
                case "Defense":
                    roleIconSource = "/Images/DefenseIcon.png";                   
                    break;
                case "Tank":
                    roleIconSource = "/Images/TankIcon.png";                    
                    break;
                case "Support":
                default:
                    roleIconSource = "/Images/SupportIcon.png";
                    break;
            }
            BitmapImage roleImage = new BitmapImage(new Uri("pack://application:,,," + roleIconSource));
            SoloSearchRoleIcon.Source = roleImage;
            SoloSearchRoleIconText.Text = user.Role;


        }

        /// <summary>
        /// Add friend to Friends List & Panel on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchAddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            loginUser.addFriend(currentUser);
            currentPage.FriendsListPanel.Children.Add(new ProfileFriendControl(loginUser, currentUser, currentPage));
            SoloSearchAddFriendButton.Visibility = Visibility.Hidden;
            FriendAddedText.Visibility = Visibility.Visible;
  
        }

        /// <summary>
        /// Change Hero Icon/Name on user's SoloSearchControl
        /// </summary>
        /// <param name="newHero"></param>
        public void changeSoloSearchHero(Profile user)
        {
            String HeroIconSource;
            switch (user.Hero)
            {
                case "Ana":
                    HeroIconSource = "ana.png";
                    break;
                case "Bastion":

                    HeroIconSource = "bastion.png";
                    break;
                case "D.Va":

                    HeroIconSource = "dva.png";
                    break;
                case "Genji":
                    HeroIconSource = "genji.png";
                    break;
                case "Hanzo":
                    HeroIconSource = "hanzo.png";
                    break;
                case "Junkrat":
                    HeroIconSource = "junkrat.png";
                    break;
                case "Lucio":
                    HeroIconSource = "lucio.png";
                    break;
                case "McCree":
                    HeroIconSource = "mccree.png";
                    break;
                case "Mei":
                    HeroIconSource = "mei.png";
                    break;
                case "Mercy":
                    HeroIconSource = "mercy.png";
                    break;
                case "Orisa":
                    HeroIconSource = "orisa.png";
                    break;
                case "Pharah":
                    HeroIconSource = "pharah.png";
                    break;
                case "Reaper":
                    HeroIconSource = "reaper.png";
                    break;
                case "Reinhardt":
                    HeroIconSource = "reinhardt.png";
                    break;
                case "Roadhog":
                    HeroIconSource = "roadhog.png";
                    break;
                case "Soldier: 76":
                    HeroIconSource = "soldier76.png";
                    break;
                case "Sombra":
                    HeroIconSource = "sombra.png";
                    break;
                case "Symmetra":
                    HeroIconSource = "symmetra.png";
                    break;
                case "Torbjorn":
                    HeroIconSource = "torbjorn.png";
                    break;
                case "Tracer":
                    HeroIconSource = "tracer.png";
                    break;
                case "Widowmaker":
                    HeroIconSource = "widowmaker.png";
                    break;
                case "Winston":
                    HeroIconSource = "winston.png";
                    break;
                case "Zarya":
                    HeroIconSource = "zarya.png";
                    break;
                case "Zenyatta":
                default:
                    HeroIconSource = "zenyatta.png";
                    break;
            }
            BitmapImage heroImage = new BitmapImage(new Uri("pack://application:,,,/HeroIcons/" + HeroIconSource));
            SoloSearchHeroImage.Source = heroImage;
            SoloSearchHeroName.Text = user.Hero;
        }



    }
}
