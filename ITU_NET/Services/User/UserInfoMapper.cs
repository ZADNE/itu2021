using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ITU_NET {
    public class UserInfoMapper {
        public static UserInfoModel Map(Usermap usermap) {
            return new UserInfoModel {
                Nickname = usermap.Nickname,
                Password = usermap.Password,
                Desc = usermap.Desc,
                Finished = usermap.Finished,
                Favourite = usermap.Favourite
            };
        }

        private static ImageSource GetIcon(string icon)
        {
            if (icon == null) return null;
            return new BitmapImage(new Uri("/Icons/" + icon + ".png", UriKind.RelativeOrAbsolute));
        }
    }
}