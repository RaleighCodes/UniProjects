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
using System.Windows.Shapes;

namespace CPSC_481_PROJECT
{
    
    /// <summary>
    /// Interaction logic for ProfilePictureSelectWindow.xaml
    /// </summary>
    public partial class ProfilePictureSelectWindow : Window
    {

        private Profile userProfile;
        


        public ProfilePictureSelectWindow(Profile userProfile)
        {
            InitializeComponent();

            this.userProfile = userProfile;

            //add all profile icon buttons to same event handler
            //(there's probably a way to loop this but I can't figure it out for the life of me...)
            ProfileIcon_1.Click += ProfileIcon_Click;
            ProfileIcon_2.Click += ProfileIcon_Click;
            ProfileIcon_3.Click += ProfileIcon_Click;
            ProfileIcon_4.Click += ProfileIcon_Click;
            ProfileIcon_5.Click += ProfileIcon_Click;
            ProfileIcon_6.Click += ProfileIcon_Click;
            ProfileIcon_7.Click += ProfileIcon_Click;
            ProfileIcon_8.Click += ProfileIcon_Click;
            ProfileIcon_9.Click += ProfileIcon_Click;
            ProfileIcon_10.Click += ProfileIcon_Click;
            ProfileIcon_11.Click += ProfileIcon_Click;
            ProfileIcon_12.Click += ProfileIcon_Click;
       
        }


        /// <summary>
        /// Changes ProfileImage source to match chosen icon, then closes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ProfileIcon_Click (object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "ProfileIcon_1":
                    userProfile.ProfileIconSource = "/Images/ARSON.png";
                    break;
                case "ProfileIcon_2":
                    userProfile.ProfileIconSource = "/Images/BAGUETTE_SNIPER.png";
                    break;
                case "ProfileIcon_3":
                    userProfile.ProfileIconSource = "/Images/BOOPS_IN_SPANISH.png";
                    break;
                case "ProfileIcon_4":
                    userProfile.ProfileIconSource = "/Images/CHEERS_LUV.jpg";
                    break;
                case "ProfileIcon_5":
                    userProfile.ProfileIconSource = "/Images/EDGELORD420.png";
                    break;
                case "ProfileIcon_6":
                    userProfile.ProfileIconSource = "/Images/EXPAND_DONG.png";
                    break;
                case "ProfileIcon_7":
                    userProfile.ProfileIconSource = "/Images/GUN_GRANNY.png";
                    break;
                case "ProfileIcon_8":
                    userProfile.ProfileIconSource = "/Images/JUSTICE.png";
                    break;
                case "ProfileIcon_9":
                    userProfile.ProfileIconSource = "/Images/MLG_KPOP_GREMLIN.png";
                    break;
                case "ProfileIcon_10":
                    userProfile.ProfileIconSource = "/Images/OINK.png";
                    break;
                case "ProfileIcon_11":
                    userProfile.ProfileIconSource = "/Images/PRO_GENJI.png";
                    break;
                case "ProfileIcon_12":
                    userProfile.ProfileIconSource = "/Images/TRENDY_ASIAN_BRO.png";
                    break;
                default:
                    break;
            }

            Close();
        }

        
    }
}
