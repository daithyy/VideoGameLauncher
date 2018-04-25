using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLauncher.Classes
{
    public class Weapon
    {
        public int DamageValue;
        public int AmmoCount;
        public string Name;

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
