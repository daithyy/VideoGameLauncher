using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLauncher.Classes
{
    public class Weapon
    {
        #region Properties

        public float DamageValue;
        public int MagazineSize;
        public string Name;

        #endregion

        #region Methods

        public Weapon(string name, float dmg, int magSize)
        {
            Name = name;
            DamageValue = dmg;
            MagazineSize = magSize;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        #endregion
    }
}
