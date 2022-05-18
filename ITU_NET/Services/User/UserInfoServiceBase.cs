using System;
using System.Threading.Tasks;

namespace ITU_NET {
    public class Credentials {
        public string Nickname { get; set; }
        public string Password { get; set; }

        public Credentials() {
            Nickname = "d";
            Password = "";
        }
        public Credentials(string nickname, string password) {
            Nickname = "u" + nickname;
            Password = password;
        }
        public Credentials(UserInfoModel user) {
            Nickname = "u" + user.Nickname;
            Password = user.Password;
        }
    }
    public enum MarkList {
        Finished, Favourite
    }

    public class MarkInfo {
        public string GameName;
        public MarkList List;
        public bool Remove;

        public MarkInfo(string gameName, MarkList list, bool remove = false) {
            GameName = gameName;
            List = list;
            Remove = remove;
        }
    }
    public abstract class UserInfoServiceBase {
        public async Task<UserInfoModel> DownloadUserAsync(string nickname) {
            var jsonString = await GetUserAsync(nickname);

            if (string.IsNullOrEmpty(jsonString)) throw new ArgumentOutOfRangeException(nameof(nickname));

            var userFromJson = Usermap.FromJson(jsonString);

            return UserInfoMapper.Map(userFromJson);
        }
        public async Task AddNewUserAsync(Credentials cred) {
            bool success = await AddUserAsync(cred.Nickname, new Usermap(cred).ToJson());
            if (!success) throw new ArgumentOutOfRangeException(nameof(cred.Nickname));
        }
        public async Task<UserInfoModel> UpdateUserAsync(Credentials cred, string desc) {
            return await UpdateUserAsync(cred.Nickname, desc);
        }
        public async Task<UserInfoModel> UpdateUserAsync(Credentials cred, MarkInfo markInfo) {
            return await UpdateUserAsync(cred.Nickname, markInfo);
        }

        protected abstract Task<string> GetUserAsync(string nickname);
        protected abstract Task<bool> AddUserAsync(string nickname, string json);
        protected abstract Task<UserInfoModel> UpdateUserAsync(string nickname, string desc);
        protected abstract Task<UserInfoModel> UpdateUserAsync(string nickname, MarkInfo markInfo);
    }
}