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
using System.Windows.Controls.Primitives;


namespace CPSC_481_PROJECT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static List<Profile> UserList = new List<Profile>();
        //public static Dictionary<String, List<Profile>> TeamsList = new Dictionary<String, List<Profile>>();
        public static List<Team> TeamsList = new List<Team>();


        /// <summary>
        /// Initialize WPF MainWindow components on start
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //List<Profile> defaultTeam = new List<Profile>();
            


            //Default User Profile List
            Profile defaultProfile = new Profile("birdnukes@gmail.com", "ItsRainingJustice", "cawcaw", "dronestrikes#2016");
            String[] defaultProfileInfo = new String[] { "Offense", "Pharah", "Ranked", "FIRE ZE MISSILES!", "/Images/JUSTICE.png" };
            setDefaultProfileInfo(defaultProfile, defaultProfileInfo);
            UserList.Add(defaultProfile);

            defaultProfile.makeNewTeam("DefaultTeam", "Quickplay");

            Team defaultTeam = defaultProfile.getTeam();

            //Team defaultTeam = new Team("Default Team", defaultProfile, "Quickplay");
            

            defaultProfile = new Profile("peglegpowderkeg@gmail.com", "FireinDaHome", "cheekynandos", "burningman#1969");
            defaultProfileInfo = new String[] {"Defense", "Junkrat","Quickplay","u wot m8?", "/Images/ARSON.png" };
            setDefaultProfileInfo(defaultProfile, defaultProfileInfo);
            UserList.Add(defaultProfile);

            defaultTeam.getMembersList().Add(defaultProfile);
            
            //defaultTeam.getMembersList().Add(defaultProfile);

            defaultProfile = new Profile("grillsgeneration@op.gg", "xX_GurlGamer_Xx", "geegee", "winkyfayce#2014");
            defaultProfileInfo = new String[] {"Defense", "D.Va", "Quickplay", "Nerf this!", "/Images/MLG_KPOP_GREMLIN.png"};
            setDefaultProfileInfo(defaultProfile, defaultProfileInfo);
           
            UserList.Add(defaultProfile);

            defaultProfile.makeNewTeam("TotallyNotSmurfs", "Ranked");

            Team defaultTeam2 = defaultProfile.getTeam();


            //Team defaultTeam2 = new Team("TotallyNotSmurfs", defaultProfile, "Ranked");


            defaultProfile = new Profile("ih8hanzo@yahoo.com", "GenjiGod", "naruto", "dattebayo#2003");
            defaultProfileInfo = new String[] { "Offense", "Genji", "Ranked", "I Need Healing!", "/Images/PRO_GENJI.png" };
            setDefaultProfileInfo(defaultProfile, defaultProfileInfo);
            UserList.Add(defaultProfile);
            //defaultTeam.Add(defaultProfile);
            defaultTeam2.getMembersList().Add(defaultProfile);




            //default teams in app's "database"
            List<Profile> defaultMembers = defaultTeam.getMembersList();
            foreach(Profile member in defaultMembers)
            {
                member.setDefaultTeam(defaultTeam);
                member.hasTeam = true;
            }
            MainWindow.TeamsList.Add(defaultTeam);


            defaultMembers = defaultTeam2.getMembersList();
            foreach(Profile member in defaultMembers)
            {
                member.setDefaultTeam(defaultTeam2);
                member.hasTeam = true;
            }
            MainWindow.TeamsList.Add(defaultTeam2);



            //initialize MainWindow to Login page by default
            PageSwitcher.pageSwitchWindow = this;
            PageSwitcher.Switch(new LoginPage());

            
        }

        /// <summary>
        /// Navigate to UserControl page and display contents in MainWindow
        /// </summary>
        /// <param name="nextPage"></param>
        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        /// <summary>
        /// set info for default Profiles on startup
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="profileInfo"></param>
        private void setDefaultProfileInfo(Profile profile, String[] profileInfo)
        {
            profile.Role = profileInfo[0];
            profile.Hero = profileInfo[1];
            profile.GameMode = profileInfo[2];
            profile.Status = profileInfo[3];
            profile.ProfileIconSource = profileInfo[4];
        }



        

       
    }
}
