﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace VideoGameLauncher.Classes.Armors
{
    public class Helmet : Armor
    {
        public Helmet(string name, Color color) : base(name, color)
        {

        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
