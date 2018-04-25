using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VideoGameLauncher.Classes
{
    public abstract class Armor
    {
        #region Properties

        public string Name;
        public Color Colour;
        private SolidColorBrush brushColor;

        public SolidColorBrush BrushColor
        {
            get { return brushColor; }
            set { brushColor = value; }
        }

        #endregion

        #region Methods
        
        public Armor(string name, Color color)
        {
            Name = name;
            Colour = color;
            brushColor = new SolidColorBrush(color);
        }

        public abstract override string ToString();

        #endregion
    }
}
