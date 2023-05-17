﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SoftwareEngineeringT1.ViewModels
{
    public class SubItem
    {

        public string Name { get; private set; }
        public UserControl Screen { get; private set; }

        public SubItem(string name, UserControl screen = null)
        {
            Name = name;
            Screen = screen;

        }
    }
}
