using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoGameLauncher.Classes.Armors;

namespace VideoGameLauncher.Classes
{
    public class Player
    {
        #region Properties

        public string Name;
        
        // Weapons
        public Weapon CurrentWeapon;

        // Armors
        public Chest CurrentChest;
        public Helmet CurrentHelmet;
        public Shoulders CurrentShoulders;
        public Utility CurrentUtility;
        public Wrist CurrentWrist;

        #endregion
    }
}
