﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagementSystem.Models;
using System.Collections.ObjectModel;

namespace ManagementSystem.ViewModels
{
    public class AddViewModel
    {
        public AddViewModel()
        {
            Jewelry jw = new Jewelry();
        }

        public Jewelry GetNew()
        {
            return Jewelry.GetExample();
        }
    }
}
