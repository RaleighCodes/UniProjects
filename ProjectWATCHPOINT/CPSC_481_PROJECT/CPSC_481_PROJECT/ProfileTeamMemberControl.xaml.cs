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
    /// Interaction logic for ProfileTeamMemberControl.xaml
    /// </summary>
    public partial class ProfileTeamMemberControl : UserControl
    {
        private Profile profile;

        public ProfileTeamMemberControl(Profile memberProfile, int memberCount)
        {
            InitializeComponent();

            //load team member profile detail into user control elements
            profile = memberProfile;
            TeamMemberProfileImage.Source = new BitmapImage(new Uri("pack://application:,,," + profile.ProfileIconSource));
            TeamMemberUsername.Text = "Username: " + profile.getUsernamePassword().Keys.ElementAt(0);
            TeamMemberBattletag.Text = "BattleTag: " + profile.BattleTag;

            //show captain star icon if first member in team list
            if(memberCount == 0)
            {
                TeamCaptainImage.Visibility = Visibility.Visible;
            }
        }

        
    }
}
