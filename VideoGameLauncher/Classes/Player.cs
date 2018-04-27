using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VideoGameLauncher.Classes
{
    public class Player
    {
        #region Properties

        public string Name;

        // Weapons
        public Weapon CurrentWeapon;

        // Armors
        public Armor Chest;
        public Armor Helmet;
        public Armor Shoulders;
        public Armor Utility;
        public Armor Wrist;

        // Armor Colors
        public Color? PrimaryColour;
        public Color? SecondaryColour;
        public Color? LightsColour;
        public Color? VisorColour;
        public Color? HoloColour;

        #endregion

        #region Constructor

        public Player(string name, 
            Color? primaryColor, Color? secondaryColor, 
            Color? lightsColor, Color? visorColor,
            Color? holoColor)
        {
            Name = name;

            PrimaryColour = primaryColor;
            SecondaryColour = secondaryColor;
            LightsColour = lightsColor;
            VisorColour = visorColor;
            HoloColour = holoColor;
        }

        #endregion

        #region Methods



        #endregion
    }
}
