using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;



namespace CMS.Areas.Admin.ViewModels
{
    public class ArticlesDetailsViewModel
    {
        [DisplayName("v主題")]
        public string Subject { get; set; }

        [DisplayName("v簡介")]
        public string Summary { get; set; }

        [DisplayName("v內容")]
        [UIHint("Html")]
        public string ContentText { get; set; }

        [DisplayName("v圖片")]
        public string Image { get; set; }

        [DisplayName("v建立者")]
        public string CreateUser { get; set; }
    }
}