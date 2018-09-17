using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPSC_481_PROJECT
{
    public class Team
    {
        public String TeamName { get; set; }
        private List<Profile> TeamMembers;
        public String TeamGameMode { get; set; }

        /// <summary>
        /// Team class constructor, adds creator as first member
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="newProfile"></param>
        /// <param name="newGameMode"></param>
        public Team(String newName, Profile newProfile, String newGameMode)
        {
            TeamName = newName;

            //add user profile that created team as first member
            
            TeamMembers = new List<Profile>();
            TeamMembers.Add(newProfile);
            //newProfile.hasTeam = true;

            TeamGameMode = newGameMode;


        }

        /// <summary>
        /// return list of member profiles
        /// </summary>
        /// <returns></returns>
        public List<Profile> getMembersList()
        {
            return TeamMembers;
        }
    }
}
