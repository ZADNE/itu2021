using System;
using System.IO;
using System.Threading.Tasks;

namespace ITU_NET {
    public class UserDiskService : UserInfoServiceBase {
        private const int delay = 1000;
        protected override async Task<string> GetUserAsync(string nickname) {
            //Simulate network delay
            await Task.Delay(delay);

            var dataFilePath = $@"Data/{nickname}.json";
            if (!File.Exists(dataFilePath)) return string.Empty;

            return File.ReadAllText(dataFilePath);
        }
        protected override async Task<bool> AddUserAsync(string nickname, string json) {
            //Simulate network delay
            await Task.Delay(delay);

            var dataFilePath = $@"Data/{nickname}.json";
            if (File.Exists(dataFilePath)) return false;

            var f = File.CreateText(dataFilePath);
            f.Write(json);
            f.Close();

            return true;
        }
        protected override async Task<UserInfoModel> UpdateUserAsync(string nickname, string desc) {
            //Simulate network delay
            await Task.Delay(delay);

            var dataFilePath = $@"Data/{nickname}.json";
            if (!File.Exists(dataFilePath)) throw new ArgumentOutOfRangeException(nameof(nickname));
            Usermap usermap = Usermap.FromJson(File.ReadAllText(dataFilePath));
            usermap.Desc = desc;
            File.WriteAllText(dataFilePath, usermap.ToJson());
            return UserInfoMapper.Map(usermap);
        }
        protected override async Task<UserInfoModel> UpdateUserAsync(string nickname, MarkInfo markInfo) {
            //Simulate network delay
            await Task.Delay(delay);

            var dataFilePath = $@"Data/{nickname}.json";
            if (!File.Exists(dataFilePath)) throw new ArgumentOutOfRangeException(nameof(nickname));
            Usermap usermap = Usermap.FromJson(File.ReadAllText(dataFilePath));
            switch (markInfo.List) {
                case MarkList.Finished:
                    if (!markInfo.Remove) {
                        if (!usermap.Finished.Contains(markInfo.GameName)) {
                            usermap.Finished.Add(markInfo.GameName);
                        }
                    } else {
                        usermap.Finished.Remove(markInfo.GameName);
                    }
                    break;
                case MarkList.Favourite:
                    if (!markInfo.Remove) {
                        if (!usermap.Favourite.Contains(markInfo.GameName)) {
                            usermap.Favourite.Add(markInfo.GameName);
                        }
                    } else {
                        usermap.Favourite.Remove(markInfo.GameName);
                    }
                    break;
            }
            File.WriteAllText(dataFilePath, usermap.ToJson());
            return UserInfoMapper.Map(usermap);
        }
    }
}