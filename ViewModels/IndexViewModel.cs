﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CMS.Models;

namespace CMS.ViewModels
{
    public class IndexViewModel
    {
        public Article Head { get; set; }

        public int MyProperty { get; set; }
    }

    //private class Article
    //{
    //    public string Title { get; set; }

    //    public string Summary { get; set; }
    //}
}