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
    /// Interaction logic for CreateTeamWindow.xaml
    /// </summary>
    public partial class CreateTeamWindow : Window
    {
        Profile user;
        MainPage userPage;

        /// <summary>
        /// CreateTeamWindow constructor
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="currentPage"></param>
        public CreateTeamWindow(Profile currentUser, MainPage currentPage)
        {
            InitializeComponent();
            TeamGameModeComboBox.ItemsSource = Profile.GameModesList;
            user = currentUser;

            userPage = currentPage;
           
        }

        /// <summary>
        /// Create new team after checking for name input and mode selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateTeamConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            String newTeamName = TeamNameInput.Text;
            String newTeamGameMode = "";
            if (TeamGameModeComboBox.SelectedIndex != -1)
            {
                 newTeamGameMode = (String)TeamGameModeComboBox.SelectedItem;

            }

            //check if team already exists (team names unique)
            bool existingTeam = false;
            foreach (var team in MainWindow.TeamsList)
            {
                if (team.TeamName.Equals(newTeamName))
                {
                    existingTeam = true;
                    break;
                }
            }

            if(!existingTeam && !String.IsNullOrEmpty(newTeamName) && !String.IsNullOrWhiteSpace(newTeamName)
                && newTeamName.Length <= 15 &&!user.hasTeam && TeamGameModeComboBox.SelectedIndex != -1)
            {

                //user.makeNewTeam(newTeamName); 
                //List<Profile> newTeamList = user.getTeamList();

                //will make new team list with current user as first member
                user.makeNewTeam(newTeamName, newTeamGameMode);
                Team newTeam = user.getTeam();
                MainWindow.TeamsList.Add(newTeam);
                String TeamGameMode = (String) TeamGameModeComboBox.SelectedItem;

                //add new team to GroupSearch list
                //userPage.GroupSearchStackPanel.Children.Add(new GroupSearchControl(newTeamName, newTeamList, userPage));
                userPage.GroupSearchStackPanel.Children.Add(new GroupSearchControl(newTeam, userPage));

                userPage.TeamListText.Text = " Team Name: " + newTeamName;

                //update team list on profile tab
                userPage.remakeTeamListPanel();
                userPage.remakeGroupSearchPanel();
                Close();

            }
            else if(existingTeam)
            {
                InvalidCreateTeamPrompt("Team already exists with that name!");
            }
            else if(String.IsNullOrEmpty(newTeamName) || String.IsNullOrWhiteSpace(newTeamName))
            {
                InvalidCreateTeamPrompt("Please enter a name for your new team!");
            }
            else if(newTeamName.Length > 15)
            {
                InvalidCreateTeamPrompt("Maximum length for team name is 15 characters");
            }
            else if(TeamGameModeComboBox.SelectedIndex == -1)
            {
                InvalidCreateTeamPrompt("Please select the preferred Game Mode for your team!");
            }


        }

        /// <summary>
        /// displays text prompt for invalid action during team creation
        /// </summary>
        /// <param name="textPrompt"></param>
        private void InvalidCreateTeamPrompt(String textPrompt)
        {
            InvalidCreateTeamText.Text = textPrompt;
            InvalidCreateTeamText.Visibility = Visibility.Visible;
        }
    }
}
