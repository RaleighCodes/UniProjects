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
    /// Interaction logic for SignupProfileSettingsWindow.xaml
    /// </summary>
    public partial class SignupProfileSettingsWindow : Window
    {
        public String SignupRole { get; set; }
        public String SignupHero { get; set; }
        public String SignupGameMode { get; set; }

        private Profile newProfile;

        public SignupProfileSettingsWindow(Profile newUser)
        {
            InitializeComponent();

            newProfile = newUser;

            //instantiate dropdown lists
            SignupRoleComboBox.ItemsSource = Profile.RolesList;
            SignupHeroComboBox.ItemsSource = Profile.HeroesList;
            SignupGameModeComboBox.ItemsSource = Profile.GameModesList;
        }

        /// <summary>
        /// Role, Hero, and Game Mode must be selected for new Profile before adding to MainWindow UserList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignupProfileSettingsDone_button_Click(object sender, RoutedEventArgs e)
        {
            //check if any of the dropdown menus have a null entry
            if((SignupRoleComboBox.SelectedIndex == -1) || 
                (SignupHeroComboBox.SelectedIndex == -1) || 
                (SignupGameModeComboBox.SelectedIndex == -1))
            {

            }
            else
            {
                //set role, hero, game mode for new profile
                newProfile.Role = (String) SignupRoleComboBox.SelectedItem;
                newProfile.Hero = (String) SignupHeroComboBox.SelectedItem;
                newProfile.GameMode = (String)SignupGameModeComboBox.SelectedItem;

                //add new Profile to list of users
                MainWindow.UserList.Add(newProfile);
                PageSwitcher.Switch(new LoginPage());
                Close();

            }
        }

    }
}
