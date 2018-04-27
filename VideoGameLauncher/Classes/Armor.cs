using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLauncher.Classes
{
    public class Armor
    {
        #region Properties

        public string Name;

        #endregion

        #region Methods
        
        public Armor(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        #endregion
    }
}
