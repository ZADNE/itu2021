using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace ITU_NET{
    public struct UserInfoModel{
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string Desc { get; set; }
        public List<string> Finished { get; set; }
        public List<string> Favourite { get; set; }
    }
}