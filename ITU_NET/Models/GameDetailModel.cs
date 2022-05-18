using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ITU_NET{
    public class GameDetailModel : GameExtractModel{
        public string Reviews { get; set; }
        public string Desc { get; set; }

        public GameDetailModel(string name, DateTime date, string img, string publisher, string developer, string reviews, string desc) : base(
            name, date, new Uri("Images/" + img + ".jpg", UriKind.RelativeOrAbsolute), publisher, developer) {
            Reviews = reviews;
            Desc = desc;
        }
        public GameDetailModel() : base(){
            Reviews = "Loading...";
            Desc = "Loading description...";
        }
    }
}