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
using System.Windows.Controls.Primitives;

namespace CPSC_481_PROJECT
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {

        private Profile userProfile;

        DispatcherTimer invalidGroupSearchTextTimer = new DispatcherTimer();
        
    
       /// <summary>
       /// Initialize MainPage UserControl elements based on user login
       /// </summary>
        public MainPage(int userIndex)
        {
            InitializeComponent();
            invalidGroupSearchTextTimer.Tick += invalidGroupSearchTextTimer_Tick;
            invalidGroupSearchTextTimer.Interval = new TimeSpan(0, 0, 3); //timer lasts for 3 second intervals
            InvalidGroupSearchText.Visibility = Visibility.Hidden;
            
            //instantiate dropdown list items
            RoleComboBox.ItemsSource = SoloSearchRoleComboBox.ItemsSource = Profile.RolesList;
            HeroComboBox.ItemsSource = SoloSearchHeroComboBox.ItemsSource = Profile.HeroesList;
            GameModeComboBox.ItemsSource = Profile.GameModesList;
           
           
            //load logged in User's profile from the UserList
            userProfile = MainWindow.UserList.ElementAt(userIndex);

            //username
            ProfileUsername.Text = userProfile.getUsernamePassword().Keys.ElementAt(0);


            //role/hero/mode selections
            RoleComboBox.SelectedItem = userProfile.Role;
            HeroComboBox.SelectedItem = userProfile.Hero;
            GameModeComboBox.SelectedItem = userProfile.GameMode;

            //profile picture           
            ProfileImage.Source = new BitmapImage(new Uri("pack://application:,,," + userProfile.ProfileIconSource));

            //status message
            if (!String.IsNullOrEmpty(userProfile.Status) && !String.IsNullOrWhiteSpace(userProfile.Status))
                ProfileStatusTextBox.Text = userProfile.Status;
            else
                ProfileStatusTextBox.Text = "Status Message";

            //friends list
            List<Profile> ProfileFriendsList = userProfile.getFriendsList();
            if((ProfileFriendsList != null) && ProfileFriendsList.Any())
            {
                foreach(Profile friendProfile in ProfileFriendsList)                
                    FriendsListPanel.Children.Add(new ProfileFriendControl(userProfile, friendProfile, this));                
            }
            else
            {
                FriendStatsWins.Text = "";
                FriendTotalGamesText.Text = "";
                FriendMostPlayedText_1.Text = "";
                FriendMostPlayedText_2.Text = "";
                FriendMostPlayedText_3.Text = "";

                FriendStatsTotal_1.Text = "";
                FriendStatsTotal_2.Text = "";
                FriendStatsTotal_3.Text = "";
                FriendStatsTotal_4.Text = "";
                FriendStatsTotal_5.Text = "";
                FriendStatsTotal_6.Text = "";

            }

            

            //team list
            if(userProfile.hasTeam)
            {
                TeamListText.Text = " Team Name: " + userProfile.getTeam().TeamName;
                remakeTeamListPanel();
            }

            //user rank (randomly generated)
            String[] rankImageToolTipSource = RNGmyRank(userProfile.Rank);
            ProfileRankImage.ToolTip = rankImageToolTipSource[0];
            ProfileRankImage.Source = new BitmapImage(new Uri("pack://application:,,," + rankImageToolTipSource[1]));

            //wins/losses (randomly generated)
            StatsWins.Text = userProfile.WinsLosses[0].ToString();
            StatsLosses.Text = userProfile.WinsLosses[1].ToString();
            totalGamesText.Text = (userProfile.WinsLosses[0] + userProfile.WinsLosses[1]).ToString();

            //general stats (randomly generated)
            int[] AvgTotals = userProfile.AvgTotals;
            StatsAvg_1.Text = AvgTotals[0] + ".0";
            StatsAvg_2.Text = AvgTotals[1] + ".0";
            StatsAvg_3.Text = AvgTotals[2] + ".0";
            StatsAvg_4.Text = AvgTotals[3] + ".0";
            StatsAvg_5.Text = AvgTotals[4] + ".0";
            StatsAvg_6.Text = AvgTotals[5] + ".0";
            StatsTotal_1.Text = AvgTotals[6] + ".0";
            StatsTotal_2.Text = AvgTotals[7] + ".0";
            StatsTotal_3.Text = AvgTotals[8] + ".0";
            StatsTotal_4.Text = AvgTotals[9] + ".0";
            StatsTotal_5.Text = AvgTotals[10] + ".0";
            StatsTotal_6.Text = AvgTotals[11] + ".0";

            //most played heroes (randomly generated)
            int mostPlayed_1 = userProfile.RNGesus2(1,8);
            int mostPlayed_2 = userProfile.RNGesus2(9, 16);
            int mostPlayed_3 = userProfile.RNGesus2(17, 24);

            String[] mostPlayedInfo = RNGmyMostPlayed(mostPlayed_1);
            MostPlayedHero_1.ToolTip = mostPlayedInfo[0];
            MostPlayedHero_1.Source = new BitmapImage(new Uri("pack://application:,,," + mostPlayedInfo[1]));

            mostPlayedInfo = RNGmyMostPlayed(mostPlayed_2);
            MostPlayedHero_2.ToolTip = mostPlayedInfo[0];
            MostPlayedHero_2.Source = new BitmapImage(new Uri("pack://application:,,," + mostPlayedInfo[1]));

            mostPlayedInfo = RNGmyMostPlayed(mostPlayed_3);
            MostPlayedHero_3.ToolTip = mostPlayedInfo[0];
            MostPlayedHero_3.Source = new BitmapImage(new Uri("pack://application:,,," + mostPlayedInfo[1]));

            int percent_1 = userProfile.RNGesus2(21, 35);
            MostPlayedText_1.Text = (String) MostPlayedHero_1.ToolTip + " : " + percent_1 + "%";
            int percent_2 = percent_1 - 10;
            MostPlayedText_2.Text = (String) MostPlayedHero_2.ToolTip + " : " + percent_2 + "%";
            int percent_3 = percent_2 - 10;
            MostPlayedText_3.Text = (String) MostPlayedHero_3.ToolTip + " : " + percent_3 + "%";

            //Solo Search User list
            remakeSoloSearchPanel();

            SoloSearchQuickplayToggle.IsChecked = true;
            GroupSearchQuickplayToggle.IsChecked = true;


            //list of all teams in team search tab
            remakeGroupSearchPanel();
            


        }

        /// <summary>
        /// return profile of currently logged in user
        /// </summary>
        /// <returns></returns>
        public Profile getCurrentProfile()
        {
            return userProfile;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TeamJoinRejectButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Logs out user and switches back to login page
        /// Changed profile settings should still be preserved if log back in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new LoginPage());
        }

        /// <summary>
        /// Changes Role preference of logged in User Profile to selected item
        /// Also updates in Solo Search tab...  by remaking the entire thing lol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userProfile.Role = (String) RoleComboBox.SelectedItem;

            foreach(SoloSearchControl user in SoloSearchStackPanel.Children)
            {
                if (user.Equals(userProfile))
                {
                    user.getProfile().Role = userProfile.Role;           
                    user.changeSoloSearchRole(userProfile);
                    break;
                }
            }
            remakeSoloSearchPanel();

      
        }

        /// <summary>
        /// Changes Hero preference of logged in User Profile to selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeroComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userProfile.Hero = (String) HeroComboBox.SelectedItem;

            foreach(SoloSearchControl user in SoloSearchStackPanel.Children)
            {
                if (user.Equals(userProfile))
                {
 
                    user.getProfile().Hero = userProfile.Hero;
                    user.changeSoloSearchHero(userProfile);
                    break;
                    
                }
            }
            remakeSoloSearchPanel();
        }
           

        /// <summary>
        /// Changes Game Mode preference of logged in User Profile to selected item (requires toggle/filter to refresh)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userProfile.GameMode = (String)GameModeComboBox.SelectedItem;

            foreach(SoloSearchControl user in SoloSearchStackPanel.Children)
            {
                if (user.Equals(userProfile))
                {
                    user.getProfile().GameMode = userProfile.GameMode;
                    break;
                }
            }

            remakeSoloSearchPanel();
            
        }

        /// <summary>
        /// Changes Status Message of logged in User Profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProfileStatusTextBox_Changed(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(ProfileStatusTextBox.Text) && 
                !String.IsNullOrWhiteSpace(ProfileStatusTextBox.Text)
                && !(userProfile == null))
                userProfile.Status = ProfileStatusTextBox.Text;
        }

        /// <summary>
        /// Opens Profile Picture Select Window, then changes image based on icon selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeProfilePicButton_Click(object sender, RoutedEventArgs e)
        {
            new ProfilePictureSelectWindow(userProfile).ShowDialog();
            ProfileImage.Source = new BitmapImage(new Uri("pack://application:,,," + userProfile.ProfileIconSource));

            //update all related MainPage elements involving Profile picture
            remakeSoloSearchPanel();
            remakeGroupSearchPanel();
            if (userProfile.hasTeam)
            {
                remakeTeamListPanel();
            }
        }

        /// <summary>
        /// Opens Create Team Window, but only if logged in user isn't already on a team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTeamButton_Click(object sender, RoutedEventArgs e)
        {
            if (!userProfile.hasTeam)
            {
                InvalidGroupSearchText.Visibility = Visibility.Hidden;
                new CreateTeamWindow(userProfile, this).ShowDialog();
            }
                
            else
            {
                InvalidGroupSearchPrompt("Cannot create team if already member of another team!");
                //invalidGroupSearchTextTimer.Start();
            }

        }


        /// <summary>
        /// Filters users in Solo Search based on Game Mode Toggle (Quickplay)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchQuickplayToggle_Checked(object sender, RoutedEventArgs e)
        {
            remakeSoloSearchPanel();
            //reset role/hero filter menus
            SoloSearchRoleComboBox.SelectedIndex = -1;
            SoloSearchHeroComboBox.SelectedIndex = -1;

            //reset username search
            SoloSearchInput.Text = "Search by Username...";

          
        }

        /// <summary>
        /// Filters users in Solo Search based on Game Mode Toggle (Ranked)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchRankedToggle_Checked(object sender, RoutedEventArgs e)
        {
            remakeSoloSearchPanel();
            //reset role/hero filter menus
            SoloSearchRoleComboBox.SelectedIndex = -1;
            SoloSearchHeroComboBox.SelectedIndex = -1;

            //reset username search
            SoloSearchInput.Text = "Search by Username...";

            
        }

        /// <summary>
        /// Filters users in Solo Search based on Role (filter reset on Game Mode toggle)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchRoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (SoloSearchRoleComboBox.SelectedIndex != -1)
            {
                
                String selectedRole = (String) SoloSearchRoleComboBox.SelectedItem;

                String selectedToggle = "";
                if (SoloSearchQuickplayToggle.IsChecked == true)
                {
                    selectedToggle = "Quickplay";
                }
                    
                else if (SoloSearchRankedToggle.IsChecked == true)
                {
                    selectedToggle = "Ranked";
                }

                String selectedHero = "";
                if(SoloSearchHeroComboBox.SelectedIndex != -1)
                {
                    selectedHero = (String)SoloSearchHeroComboBox.SelectedItem;
                }

                foreach (SoloSearchControl user in SoloSearchStackPanel.Children)
                {
                    Profile profile = user.getProfile();
                    String role = profile.Role;
                    String hero = profile.Hero;
                    String mode = profile.GameMode;

                    if(selectedRole.Equals(role) && selectedToggle.Equals(mode) &&(selectedHero.Equals(hero)||(SoloSearchHeroComboBox.SelectedIndex == -1)))
                    {
                        user.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        user.Visibility = Visibility.Collapsed;
                    }
                    
                }

            }
            

        }

        /// <summary>
        /// Filters users in Solo Search based on Hero (filter reset on Game Mode toggle)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchHeroComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (SoloSearchHeroComboBox.SelectedIndex != -1)
            {
                String selectedHero = (String)SoloSearchHeroComboBox.SelectedItem;

                String selectedToggle = "";
                if(SoloSearchQuickplayToggle.IsChecked == true)
                {
                    selectedToggle = "Quickplay";
                }
                else if (SoloSearchRankedToggle.IsChecked == true)
                {
                    selectedToggle = "Ranked";
                }

                String selectedRole = "";
                if(SoloSearchRoleComboBox.SelectedIndex != -1)
                {
                    selectedRole = (String) SoloSearchRoleComboBox.SelectedItem;
                }
                
                foreach (SoloSearchControl user in SoloSearchStackPanel.Children)
                {
                    Profile profile = user.getProfile();
                    String hero = profile.Hero;
                    String mode = profile.GameMode;
                    String role = profile.Role;

                    if(selectedHero.Equals(hero) && selectedToggle.Equals(mode) && (selectedRole.Equals(role)||(SoloSearchRoleComboBox.SelectedIndex == -1)))
                    {
                        user.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        user.Visibility = Visibility.Collapsed;
                    }
                }

            }
        }

        /// <summary>
        /// Remake Solo Search StackPanel to update userControls 
        /// (Also auto-filters by game mode)
        /// </summary>
        public void remakeSoloSearchPanel()
        {
            SoloSearchStackPanel.Children.Clear();

            //list of all users for solo search tab
            foreach (Profile user in MainWindow.UserList)
                SoloSearchStackPanel.Children.Add(new SoloSearchControl(user, this));


            if(SoloSearchQuickplayToggle.IsChecked == true)
            {
                foreach (SoloSearchControl user in SoloSearchStackPanel.Children)
                {
                    Profile userProfile = user.getProfile();

                    if (userProfile.GameMode.Equals("Quickplay"))
                        user.Visibility = Visibility.Visible;

                    else if (userProfile.GameMode.Equals("Ranked"))
                        user.Visibility = Visibility.Collapsed;

                }
            }
            else if(SoloSearchRankedToggle.IsChecked == true)
            {
                foreach (SoloSearchControl user in SoloSearchStackPanel.Children)
                {
                    Profile profile = user.getProfile();
                    if (profile.GameMode.Equals("Ranked"))
                        user.Visibility = Visibility.Visible;

                    else if (profile.GameMode.Equals("Quickplay"))
                        user.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// remakes list of Team members on Profile Team tab for any changes
        /// </summary>
        public void remakeTeamListPanel()
        {
            TeamListPanel.Children.Clear();

            List<Profile> ProfileTeamList = userProfile.getTeam().getMembersList();
            if ((ProfileTeamList != null) && ProfileTeamList.Any())
            {
                foreach (Profile member in ProfileTeamList)
                {
                    int count = ProfileTeamList.IndexOf(member);
                    TeamListPanel.Children.Add(new ProfileTeamMemberControl(member, count));
                    
                }
            }
        }

        /// <summary>
        /// update Solo Search User list based on Username input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchButton_Click(object sender, RoutedEventArgs e)
        {
            String usernameSearch = SoloSearchInput.Text.ToLower();
            String defaultSearch = "Search by Username...";
            defaultSearch = defaultSearch.ToLower();
            if (!String.IsNullOrEmpty(usernameSearch) && !String.IsNullOrEmpty(usernameSearch) && 
                !usernameSearch.Equals(defaultSearch))
            {
                foreach(SoloSearchControl user in SoloSearchStackPanel.Children)
                {
                    Profile profile = user.getProfile();
                    String username = profile.getUsernamePassword().Keys.ElementAt(0).ToLower();
                    if (username.Equals(usernameSearch) || username.Contains(usernameSearch))
                        user.Visibility = Visibility.Visible;
                    else
                        user.Visibility = Visibility.Collapsed;
                    
                }
            }
        }

        /// <summary>
        /// clear initial username search text on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SoloSearchInput.Text.Equals("Search by Username..."))
                SoloSearchInput.Clear();
        }

        /// <summary>
        /// clear initial team name search text on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupSearchInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (GroupSearchInput.Text.Equals("Search by Team Name..."))
                GroupSearchInput.Clear();
        }

        /// <summary>
        /// Update Group Search team list based on team name input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupSearchButton_Click(object sender, RoutedEventArgs e)
        {
            String teamSearch = GroupSearchInput.Text.ToLower();
            String defaultSearch = "Search by Team Name...";
            defaultSearch = defaultSearch.ToLower();

            if (!String.IsNullOrEmpty(teamSearch) && !String.IsNullOrWhiteSpace(teamSearch)
                && !teamSearch.Equals(defaultSearch))
            {
                foreach(GroupSearchControl team in GroupSearchStackPanel.Children)
                {
                    String teamName = team.TeamSearchName.Text.ToLower();
                    if (teamName.Equals(teamSearch) || teamName.Contains(teamSearch))
                        team.Visibility = Visibility.Visible;
                    else
                        team.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// After interval time has ticked, hide invalid text prompt and stop timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void invalidGroupSearchTextTimer_Tick(object sender, EventArgs e)
        {
            InvalidGroupSearchText.Visibility = Visibility.Hidden;
            invalidGroupSearchTextTimer.Stop();
        }

        private void ProfileTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(TabItem item in ProfileTabControl.Items)
            {
                if (item.IsSelected)
                    item.Background = Brushes.Gold;
                
                else
                    item.Background = Brushes.White;
            }
        }

        /// <summary>
        /// remake Group search panel for any changes to usercontrol
        /// WIP -  sort by game mode like with solo search
        /// </summary>
        public void remakeGroupSearchPanel()
        {
            GroupSearchStackPanel.Children.Clear();

            foreach(var item in MainWindow.TeamsList)
            {
                //String teamName = item.Key;
                //List<Profile> teamMembers = item.Value;
                //GroupSearchStackPanel.Children.Add(new GroupSearchControl(teamName, teamMembers, this));
                GroupSearchStackPanel.Children.Add(new GroupSearchControl(item, this));
            }

            if (GroupSearchQuickplayToggle.IsChecked == true)
            {
                foreach (GroupSearchControl control in GroupSearchStackPanel.Children)
                {
                    Team team = control.getTeam();

                    if(team.TeamGameMode.Equals("Quickplay"))
                        control.Visibility = Visibility.Visible;
                    else if(team.TeamGameMode.Equals("Ranked"))
                        control.Visibility = Visibility.Collapsed;

                }
            }
            else if (GroupSearchRankedToggle.IsChecked == true)
            {
                foreach(GroupSearchControl control in GroupSearchStackPanel.Children)
                {
                    Team team = control.getTeam();

                    if (team.TeamGameMode.Equals("Ranked"))
                        control.Visibility = Visibility.Visible;
                    else if (team.TeamGameMode.Equals("Quickplay"))
                        control.Visibility = Visibility.Collapsed;
                    
                }
            }
            {

            }
        }

        private void MainPageTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //remakeSoloSearchPanel();

        }

        /// <summary>
        /// clears status message text box on click if default message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            String statusMessage = ProfileStatusTextBox.Text;
            if (statusMessage.Equals("Status Message"))
            {
                ProfileStatusTextBox.Clear();
            }
            
        }

        /// <summary>
        /// Reset on loss of focus to default status message if nothing typed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            String statusMessage = ProfileStatusTextBox.Text;
            if (String.IsNullOrEmpty(statusMessage) || String.IsNullOrWhiteSpace(statusMessage))
            {
                ProfileStatusTextBox.Text = "Status Message";
            }
        }

        /// <summary>
        /// Reset on loss of focus to default Group Search message if nothing typed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupSearchInput_LostFocus(object sender, RoutedEventArgs e)
        {
            String teamSearch = GroupSearchInput.Text;
            if(String.IsNullOrEmpty(teamSearch) || String.IsNullOrWhiteSpace(teamSearch))
            {
                GroupSearchInput.Text = "Search by Team Name...";
            }
        }

        /// <summary>
        /// Reset on loss of focus to default Solo Search message if nothing typed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoloSearchInput_LostFocus(object sender, RoutedEventArgs e)
        {
            String soloSearch = SoloSearchInput.Text;
            if (String.IsNullOrEmpty(soloSearch) || String.IsNullOrEmpty(soloSearch))
            {
                SoloSearchInput.Text = "Search by Username...";
            }
        }

        /// <summary>
        /// Removes user from team if they're a member of one, and deletes team from global list if they're a team captain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTeamButton_Click(object sender, RoutedEventArgs e)
        {
            if(userProfile.hasTeam)
            {
                Team teamToDelete = userProfile.getTeam();
                List<Profile> members = teamToDelete.getMembersList();


                //remove team from profile
                userProfile.setDefaultTeam(null);
                userProfile.hasTeam = false;
                TeamListPanel.Children.Clear();
                TeamListText.Text = " Team Name: ";

                //if user is first member (i.e. team captain), delete team from global list
                if (members.IndexOf(userProfile) == 0)
                {
                    //delete team for every non-captain member
                    foreach (Profile member in members)
                    {
                        if(!member.Equals(userProfile))
                        {
                            member.setDefaultTeam(null);
                            member.hasTeam = false;

                        }
                    }


                    //remove from global list and update group search
                    MainWindow.TeamsList.Remove(teamToDelete);
                    remakeGroupSearchPanel();
                }
                //otherwise, just remove user from that team
                else
                {
                    foreach(Team team in MainWindow.TeamsList)
                    {
                        if(team.Equals(teamToDelete))
                        {
                            team.getMembersList().Remove(userProfile);
                            break;
                        }
                    }
                    
                    //update group search
                    remakeGroupSearchPanel();
                }

            }
        }

        /// <summary>
        /// Filters teams in Group Search based on game mode toggle (Quickplay)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupSearchQuickplayToggle_Checked(object sender, RoutedEventArgs e)
        {
            remakeGroupSearchPanel();
            GroupSearchInput.Text = "Search by Team Name...";
        }

        /// <summary>
        /// Filters teams in Group Search based on game mode toggle (Ranked)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupSearchRankedToggle_Checked(object sender, RoutedEventArgs e)
        {
            remakeGroupSearchPanel();
            GroupSearchInput.Text = "Search by Team Name...";
        }

        public void InvalidGroupSearchPrompt(String textPrompt)
        {
            InvalidGroupSearchText.Text = textPrompt;
            InvalidGroupSearchText.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// change rank image source based on profile's randomly generated rank
        /// </summary>
        private String[] RNGmyRank(int rank)
        {
            String rankImageToolTip = "";
            String rankImageSource = "";
            switch(rank)
            {
                case 1:
                    rankImageToolTip = "BRONZE";
                    rankImageSource = "/RankIcons/Rank1.jpg";
                    break;
                case 2:
                    rankImageToolTip = "SILVER";
                    rankImageSource = "/RankIcons/Rank2.jpg";
                    break;
                case 3:
                    rankImageToolTip = "GOLD";
                    rankImageSource = "/RankIcons/Rank3.jpg";
                    break;
                case 4:
                    rankImageToolTip = "PLATINUM";
                    rankImageSource = "/RankIcons/Rank4.jpg";
                    break;
                case 5:
                    rankImageToolTip = "DIAMOND";
                    rankImageSource = "/RankIcons/Rank5.jpg";
                    break;
                case 6:
                    rankImageToolTip = "MASTER";
                    rankImageSource = "/RankIcons/Rank6.jpg";
                    break;
                case 7:
                    rankImageToolTip = "GRANDMASTER";
                    rankImageSource = "/RankIcons/Rank7.jpg";
                    break;
                case 8:
                    rankImageToolTip = "TOP 500";
                    rankImageSource = "/RankIcons/Rank8.jpg";
                    break;
                default:
                    break;
            }

            String[] rankImageToolTipSource = new String[] { rankImageToolTip, rankImageSource };
            return rankImageToolTipSource;


        }

        /// <summary>
        /// change image source for most played heroes
        /// </summary>
        /// <param name="heroNum"></param>
        /// <returns></returns>
        private String[] RNGmyMostPlayed(int heroNum)
        {
            
            String HeroIconSource = "";
            String HeroName = "";
            switch(heroNum)
            {
                case 1:
                    HeroIconSource = "/HeroIcons/ana.png";
                    HeroName = "Ana";
                    break;
                case 2:
                    HeroIconSource = "/HeroIcons/bastion.png";
                    HeroName = "Bastion";
                    break;
                case 3:
                    HeroIconSource = "/HeroIcons/dva.png";
                    HeroName = "D.Va";
                    break;
                case 4:
                    HeroIconSource = "/HeroIcons/genji.png";
                    HeroName = "Genji";
                    break;
                case 5:
                    HeroIconSource = "/HeroIcons/hanzo.png";
                    HeroName = "Hanzo";
                    break;
                case 6:
                    HeroIconSource = "/HeroIcons/junkrat.png";
                    HeroName = "Junkrat";
                    break;
                case 7:
                    HeroIconSource = "/HeroIcons/lucio.png";
                    HeroName = "Lucio";
                    break;
                case 8:
                    HeroIconSource = "/HeroIcons/mccree.png";
                    HeroName = "McCree";
                    break;
                case 9:
                    HeroIconSource = "/HeroIcons/mei.png";
                    HeroName = "Mei";
                    break;
                case 10:
                    HeroIconSource = "/HeroIcons/mercy.png";
                    HeroName = "Mercy";
                    break;
                case 11:
                    HeroIconSource = "/HeroIcons/orisa.png";
                    HeroName = "Orisa";
                    break;
                case 12:
                    HeroIconSource = "/HeroIcons/pharah.png";
                    HeroName = "Pharah";
                    break;
                case 13:
                    HeroIconSource = "/HeroIcons/reaper.png";
                    HeroName = "Reaper";
                    break;
                case 14:
                    HeroIconSource = "/HeroIcons/reinhardt.png";
                    HeroName = "Reinhardt";
                    break;
                case 15:
                    HeroIconSource = "/HeroIcons/roadhog.png";
                    HeroName = "Roadhog";
                    break;
                case 16:
                    HeroIconSource = "/HeroIcons/soldier76.png";
                    HeroName = "Soldier: 76";
                    break;
                case 17:
                    HeroIconSource = "/HeroIcons/sombra.png";
                    HeroName = "Sombra";
                    break;
                case 18:
                    HeroIconSource = "/HeroIcons/tracer.png";
                    HeroName = "Tracer";
                    break;
                case 19:
                    HeroIconSource = "/HeroIcons/symmetra.png";
                    HeroName = "Symmetra";
                    break;
                case 20:
                    HeroIconSource = "/HeroIcons/torbjorn.png";
                    HeroName = "Torbjorn";
                    break;
                case 21:
                    HeroIconSource = "/HeroIcons/widowmaker.png";
                    HeroName = "Widowmaker";
                    break;
                case 22:
                    HeroIconSource = "/HeroIcons/winston.png";
                    HeroName = "Winston";
                    break;
                case 23:
                    HeroIconSource = "/HeroIcons/zarya.png";
                    HeroName = "Zarya";
                    break;
                case 24:
                    HeroIconSource = "/HeroIcons/zenyatta.png";
                    HeroName = "Zenyatta";
                    break;
                default:
                    HeroIconSource = "zenyatta.png";
                    HeroName = "Zenyatta";
                    break;
            }

            String[] mostPlayedNameSource = new String[] { HeroName, HeroIconSource };
            return mostPlayedNameSource;
        }


        /// <summary>
        /// Loads friend profile stats upon selection from friend list
        /// </summary>
        /// <param name="friend"></param>
        public void loadFriendStats(Profile friend)
        {
            String[] friendRankImageToolTipSource = RNGmyRank(friend.Rank);
            FriendRankImage.ToolTip = friendRankImageToolTipSource[0];
            FriendRankImage.Source = new BitmapImage(new Uri("pack://application:,,," + friendRankImageToolTipSource[1]));

            FriendStatsWins.Text = friend.WinsLosses[0].ToString();
            FriendTotalGamesText.Text = (friend.WinsLosses[0] + friend.WinsLosses[1]).ToString();

            int mostPlayed_1 = friend.RNGesus2(1, 10);
            int mostPlayed_2 = friend.RNGesus2(11, 19);
            int mostPlayed_3 = friend.RNGesus2(20, 24);

            String[] mostPlayedFriendInfo = RNGmyMostPlayed(mostPlayed_1);
            FriendMostPlayedHero_1.ToolTip = mostPlayedFriendInfo[0];
            FriendMostPlayedHero_1.Source = new BitmapImage(new Uri("pack://application:,,," + mostPlayedFriendInfo[1]));

            mostPlayedFriendInfo = RNGmyMostPlayed(mostPlayed_2);
            FriendMostPlayedHero_2.ToolTip = mostPlayedFriendInfo[0];
            FriendMostPlayedHero_2.Source = new BitmapImage(new Uri("pack://application:,,," + mostPlayedFriendInfo[1]));

            mostPlayedFriendInfo = RNGmyMostPlayed(mostPlayed_3);
            FriendMostPlayedHero_3.ToolTip = mostPlayedFriendInfo[0];
            FriendMostPlayedHero_3.Source = new BitmapImage(new Uri("pack://application:,,," + mostPlayedFriendInfo[1]));

            int percent_1 = userProfile.RNGesus2(15, 38);
            FriendMostPlayedText_1.Text = (String)FriendMostPlayedHero_1.ToolTip + " : " + percent_1 + "%";
            int percent_2 = percent_1 - 10;
            FriendMostPlayedText_2.Text = (String)FriendMostPlayedHero_2.ToolTip + " : " + percent_2 + "%";
            int percent_3 = percent_2 - 5;
            FriendMostPlayedText_3.Text = (String)FriendMostPlayedHero_3.ToolTip + " : " + percent_3 + "%";


            int[] AvgTotals = friend.AvgTotals;
            FriendStatsTotal_1.Text = AvgTotals[6] + ".0";
            FriendStatsTotal_2.Text = AvgTotals[7] + ".0";
            FriendStatsTotal_3.Text = AvgTotals[8] + ".0";
            FriendStatsTotal_4.Text = AvgTotals[9] + ".0";
            FriendStatsTotal_5.Text = AvgTotals[10] + ".0";
            FriendStatsTotal_6.Text = AvgTotals[11] + ".0";
        }

        /// <summary>
        /// clears friends stats upon removal from friends list and deselects any selected friends
        /// </summary>
        public void clearFriendStats()
        {
            FriendRankImage.ToolTip = "";
            FriendRankImage.Source = null;

            FriendStatsWins.Text = "";
            FriendTotalGamesText.Text = "";

            FriendMostPlayedHero_1.Source = null;
            FriendMostPlayedHero_2.Source = null;
            FriendMostPlayedHero_3.Source = null;

            FriendMostPlayedText_1.Text = "";
            FriendMostPlayedText_2.Text = "";
            FriendMostPlayedText_3.Text = "";

            FriendStatsTotal_1.Text = "";
            FriendStatsTotal_2.Text = "";
            FriendStatsTotal_3.Text = "";
            FriendStatsTotal_4.Text = "";
            FriendStatsTotal_5.Text = "";
            FriendStatsTotal_6.Text = "";

        }

        //private void ChangeProfilePicButton_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    ChangeProfilePicButton.Opacity = 0.75;
        //}

        //private void ChangeProfilePicButton_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    ChangeProfilePicButton.Opacity = 0;
        //}
    }
    
  
}
