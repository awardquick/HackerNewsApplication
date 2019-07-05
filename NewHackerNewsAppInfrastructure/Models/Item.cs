﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NewHackerNewsAppInfrastructure.Models
{
    public class Item
    {
        public int id { get; set; }
        public bool? deleted { get; set; }

        public string type { get; set; }

        public string by { get; set; }

        public long? time { get; set; }

        public string text { get; set; }

        public bool? dead { get; set; }

        public int? parent { get; set; }

        public int? poll { get; set; }

        public IEnumerable<int> kids { get; set; }

        public string url { get; set; }

        public int? score { get; set; }

        public string title { get; set; }

        public IEnumerable<int> parts { get; set; }

        public int? descendants { get; set; }


    }
}
