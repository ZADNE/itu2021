using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ITU_NET{
    public class GameExtractModel{
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Uri Img { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }

        public GameExtractModel(string name, DateTime date, Uri img, string publisher, string developer) {
            Name = name;
            Date = date;
            Img = img;
            Publisher = publisher;
            Developer = developer;
        }

        public GameExtractModel() {
            Name = "Loading...";
            Date = new DateTime();
            Img = new Uri("Images/Loading.png", UriKind.RelativeOrAbsolute);
            Publisher = "Loading...";
            Developer = "Loading...";
        }
    }
}