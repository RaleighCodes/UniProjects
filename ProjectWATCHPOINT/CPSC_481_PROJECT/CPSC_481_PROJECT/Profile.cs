using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CPSC_481_PROJECT
{
    public class Profile
    {
        //sign-up instance variables
        public String Email { get; set; }
        private Dictionary<String, String> usernamePassword;
        public String BattleTag { get; set; }

        //profile settings variables (with get/set methods)   
        public String Role { get; set; }

        public String Hero { get; set; }

        public String GameMode { get; set; }

        public String Status { get; set; }

        public String ProfileIconSource { get; set; }

        public int Rank { get; }

        public int[] WinsLosses { get; }

        public int[] AvgTotals { get; }


        //roles, heroes, game mode lists
        public static string[] RolesList = new string[] { "Offense", "Defense", "Tank", "Support" };
        public static string[] HeroesList = new string[] {"Ana","Bastion","D.Va","Genji","Hanzo","Junkrat","Lucio","McCree","Mei","Mercy",
            "Orisa","Pharah","Reaper","Reinhardt","Roadhog","Soldier: 76","Sombra", "Symmetra","Torbjorn","Tracer",
            "Widowmaker","Winston","Zarya","Zenyatta"};
        public static string[] GameModesList = new string[] { "Quickplay", "Ranked" };

        private List<Profile> friendsList;

        public bool hasTeam { get; set; }
        //public String teamName;
        //private List<Profile> teamList;

        //won't be instantiated unless create/join team
        private Team userTeam;


        /// <summary>
        /// Profile constructor
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="battletag"></param>
        public Profile(String email, String username, String password, String battletag)
        {
            Email = email;

            usernamePassword = new Dictionary<String, String>(1);
            usernamePassword.Add(username, password);

            BattleTag = battletag;
            Status = "";

            Rank = RNGesus(8);
            int wins = RNGesus(999);
            int losses = RNGesus(wins);
            WinsLosses = new int[] {wins, losses};

            int total_1 = RNGesus(900);
            int avg_1 = RNGesus(total_1/2);
            int total_2 = RNGesus(200);
            int avg_2 = RNGesus(total_2/2);
            int total_3 = RNGesus(500);
            int avg_3 = RNGesus(total_3/2);
            int total_4 = RNGesus(800);
            int avg_4 = RNGesus(total_4/2);
            int total_5 = RNGesus(400);
            int avg_5 = RNGesus(total_5/2);
            int total_6 = RNGesus(300);
            int avg_6 = RNGesus(total_6/2);

            AvgTotals = new int[] {avg_1, avg_2, avg_3, avg_4, avg_5, avg_6,
                total_1, total_2, total_3, total_4, total_5, total_6};

           

            //default profile icon
            ProfileIconSource = "/Images/PRO_GENJI.png";

            friendsList = new List<Profile>();
            hasTeam = false;


        }

        /// <summary>
        /// get method for username-password dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, String> getUsernamePassword()
        {
            return usernamePassword;
        }

        /// <summary>
        /// Add user profile to friends list
        /// </summary>
        /// <param name="newFriendProfile"></param>
        public void addFriend(Profile newFriendProfile)
        {
            friendsList.Add(newFriendProfile);
        }

       

        /// <summary>
        /// debugging method to set default friends list
        /// </summary>
        /// <param name="defaultFriendsList"></param>
        public void setDefaultFriends(List<Profile> defaultFriendsList)
        {
            friendsList = defaultFriendsList;
        }

        /// <summary>
        /// debugging method to set default team
        /// </summary>
        /// <param name="defaultTeam"></param>
        public void setDefaultTeam(Team defaultTeam)
        {
            userTeam = defaultTeam;
        }

        //public void setDefaultTeam(String defaultTeamName,List<Profile> defaultTeamList)
        //{
        //teamName = defaultTeamName;
        //teamList = defaultTeamList;
        //}


        /// <summary>
        /// get friends list
        /// </summary>
        /// <returns></returns>
        public List<Profile> getFriendsList()
        {
            return friendsList;
        }

        /// <summary>
        /// Add new member profile to team list
        /// </summary>
        /// <param name="newMember"></param>
        //public void addTeamMember(Profile newMember)
        //{
        //    if (teamList.Count() <= 5)
        //        teamList.Add(newMember);
        //}

        //public void removeTeamMember(Profile memberToRemove)
        //{
        //    foreach(Profile member in teamList)
        //    {
        //        if (member.Equals(memberToRemove))
        //        {
        //            teamList.Remove(member);
        //            break;
        //        }
        //    }
        //}



        /// <summary>
        /// instantiate new team for user (with self as first member)
        /// Users should only have one team so set boolean
        /// </summary>
        /// <param name="name"></param>
        public void makeNewTeam(String name, String mode)
        {
            //teamName = name;
            //teamList.Add(this);
            //hasTeam = true;

            userTeam = new Team(name, this, mode);
            hasTeam = true;

        }

  
        /// <summary>
        /// return user Team (assuming they have one)
        /// </summary>
        /// <returns></returns>
        public Team getTeam()
        {
            return userTeam;
        }

        //generate a random number given a max range
        public int RNGesus(int maxRange)
        {
            Random rand = new Random();
            int randInt = rand.Next(1, maxRange + 1);
            return randInt;
        }

        public int RNGesus2(int minRange, int maxRange)
        {
            Random rand = new Random();
            int randInt = rand.Next(minRange, maxRange + 1);
            return randInt;
        }


    }
}
