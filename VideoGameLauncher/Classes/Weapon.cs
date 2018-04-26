﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLauncher.Classes
{
    public class Weapon
    {
        #region Properties

        public int DamageValue;
        public int AmmoCount;
        public string Name;

        #endregion

        #region Methods

        public Weapon(string name, int dmg, int ammo)
        {
            Name = name;
            DamageValue = dmg;
            AmmoCount = ammo;
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        #endregion
    }
}
