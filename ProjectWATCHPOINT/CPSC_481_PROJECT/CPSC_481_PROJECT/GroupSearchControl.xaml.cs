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
    /// Interaction logic for GroupSearchControl.xaml
    /// </summary>
    public partial class GroupSearchControl : UserControl
    {

        MainPage userPage;
        Profile user;
        String teamName;
        List<Profile> teamMembers;
        Team team;
        

        //public GroupSearchControl(String currentTeamName, List<Profile> currentTeamMembers, MainPage currentPage)
        public GroupSearchControl (Team currentTeam, MainPage currentPage)
        {
            InitializeComponent();
            userPage = currentPage;
            user = userPage.getCurrentProfile();
            
            team = currentTeam;
            TeamSearchName.Text = teamName = team.TeamName;
            teamMembers = team.getMembersList();




            //instatiate only as many images/borders as there are members

            foreach(Profile member in teamMembers)
            {
                switch(teamMembers.IndexOf(member) + 1)
                {
                    case 1:
                        MemberBorder1.Visibility = MemberName1.Visibility = Visibility.Visible;
                        MemberName1.Text = member.getUsernamePassword().Keys.ElementAt(0);
                        TeamMemberImage1.Source = new BitmapImage(new Uri("pack://application:,,," + member.ProfileIconSource));
                        resizeUsername(MemberName1);

                        break;
                    case 2:                      
                        MemberBorder2.Visibility = MemberName2.Visibility = Visibility.Visible;
                        MemberName2.Text = member.getUsernamePassword().Keys.ElementAt(0);
                        TeamMemberImage2.Source = new BitmapImage(new Uri("pack://application:,,," + member.ProfileIconSource));
                        resizeUsername(MemberName2);
                        break;
                    case 3:
                        MemberBorder3.Visibility = MemberName3.Visibility = Visibility.Visible;
                        MemberName3.Text = member.getUsernamePassword().Keys.ElementAt(0);
                        TeamMemberImage3.Source = new BitmapImage(new Uri("pack://application:,,," + member.ProfileIconSource));
                        resizeUsername(MemberName3);
                        break;
                    case 4:
                        MemberBorder4.Visibility = MemberName4.Visibility = Visibility.Visible;
                        MemberName4.Text = member.getUsernamePassword().Keys.ElementAt(0);
                        TeamMemberImage4.Source = new BitmapImage(new Uri("pack://application:,,," + member.ProfileIconSource));
                        resizeUsername(MemberName4);
                        break;
                    case 5:                      
                        MemberBorder5.Visibility = MemberName5.Visibility = Visibility.Visible;
                        MemberName5.Text = member.getUsernamePassword().Keys.ElementAt(0);
                        TeamMemberImage5.Source = new BitmapImage(new Uri("pack://application:,,," + member.ProfileIconSource));
                        resizeUsername(MemberName5);
                        break;
                    default:
                        break;
                }
                
            }

            if(user.hasTeam && user.getTeam().TeamName.Equals(teamName))
                JoinTeamButton.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Resize font of long usernames and adjust textblock margin for each member
        /// </summary>
        /// <param name="username"></param>
        private void resizeUsername(TextBlock username)
        {
            switch(username.Name)
            {
                case "MemberName1":
                    if (username.Text.Length >= 15)
                    {
                        username.FontSize = 11;
                        username.Margin = new Thickness(278, 120, 0, 0);
                    }                    
                    break;
                case "MemberName2":
                    if (username.Text.Length >= 15)
                    {
                        username.FontSize = 11;
                        username.Margin = new Thickness(388, 120, 0, 0);
                    }
                    break;
                case "MemberName3":
                    if (username.Text.Length >= 15)
                    {
                        username.FontSize = 11;
                        username.Margin = new Thickness(498, 120, 0, 0);
                    }
                    break;
                case "MemberName4":
                    if (username.Text.Length >= 15)
                    {
                        username.FontSize = 11;
                        username.Margin = new Thickness(608, 120, 0, 0);
                    }
                    break;
                case "MemberName5":
                    if (username.Text.Length >= 15)
                    {
                        username.FontSize = 11;
                        username.Margin = new Thickness(723, 120, 0, 0);
                    }
                    break;                
                default:
                    break;
            }
        }

        /// <summary>
        /// User joins existing team from Group Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoinTeamButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO later: add timer to simulate team accepting join request
            //for now: auto join team
            if(!user.hasTeam && teamMembers.Count < 5)
            {
                userPage.InvalidGroupSearchText.Visibility = Visibility.Hidden;

                teamMembers.Add(user);
                //user.setDefaultTeam(teamName,teamMembers);
                user.setDefaultTeam(team);
                user.hasTeam = true;
                userPage.remakeTeamListPanel();
                userPage.TeamListText.Text = " Team Name: " + teamName;
                userPage.remakeGroupSearchPanel();
            }
            else if(user.hasTeam)
            {
                userPage.InvalidGroupSearchPrompt("Cannot join team if already member of another team!");
            }
            else if(teamMembers.Count == 5)
            {
                userPage.InvalidGroupSearchPrompt("Team already has maximum of 5 members!");
            }
        }

        /// <summary>
        /// return team associated with Group Search Control
        /// </summary>
        /// <returns></returns>
        public Team getTeam()
        {
            return team;
        }
    }
}
