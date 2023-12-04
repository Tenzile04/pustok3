﻿using Pustokk.Models;

namespace Pustokk.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<Service> Services { get; set; }
        public List<Book> FeaturedBooks { get; set; }
        public List<Book> NewBooks { get; set; }
        public List<Book> BestSellerBooks { get; set; }


    }
}
