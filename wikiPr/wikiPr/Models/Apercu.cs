using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wikiPr.Models {
    public class Apercu {
        public string Content { get; set; }

        public Apercu() { }
        public Apercu(string s) {
            this.Content = s;
        }
    }
}