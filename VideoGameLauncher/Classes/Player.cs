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
        private Color PrimaryColour;
        private Color SecondaryColour;
        private Color LightsColour;
        private Color VisorColour;
        private Color HoloColour;
        public SolidColorBrush PrimaryColor;
        public SolidColorBrush SecondaryColor;
        public SolidColorBrush LightsColor;
        public SolidColorBrush VisorColor;
        public SolidColorBrush HoloColor;

        #endregion

        #region Constructor

        public Player(string name, 
            Color primaryColor, Color secondaryColor, 
            Color lightsColor, Color visorColor,
            Color holoColor)
        {
            Name = name;

            PrimaryColour = primaryColor;
            SecondaryColour = secondaryColor;
            LightsColour = lightsColor;
            VisorColour = visorColor;
            HoloColour = holoColor;

            PrimaryColor = new SolidColorBrush(primaryColor);
            SecondaryColor = new SolidColorBrush(secondaryColor);
            LightsColor = new SolidColorBrush(lightsColor);
            VisorColor = new SolidColorBrush(visorColor);
            HoloColor = new SolidColorBrush(holoColor);
        }

        #endregion

        #region Methods



        #endregion
    }
}
