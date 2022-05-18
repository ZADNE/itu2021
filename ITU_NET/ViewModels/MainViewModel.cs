using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ITU_NET {
    public class MainViewModel : INotifyPropertyChanged {
        private readonly UserInfoServiceBase _userService;
        private readonly GameInfoServiceBase _gameService;
        private string _errorMessage;

        private bool _downloadingUser;
        private bool _uploadingUser;
        private UserInfoModel _user;

        private List<GameExtractModel> _gameExtracts;
        private bool _downloadingGameExtracts;

        private GameDetailModel _gameDetail;
        private bool _downloadingGameDetail;

        public MainViewModel() {
            _userService = new UserDiskService();
            _gameService = new GameDiskService();

            DownloadUserCommand = new AsyncCommand<Credentials>(async cred => await UpdateUserAsync(cred));
            AddNewUserCommand = new AsyncCommand<Credentials>(async cred => await AddNewUserAsync(cred));
            UpdateDescCommand = new AsyncCommand<string>(async desc => await UpdateDescAsync(desc));
            MarkGameCommand = new AsyncCommand<MarkInfo>(async markInfo => await MarkAsync(markInfo));

            DownloadGameExtractsCommand = new AsyncCommand<string>(async filter => await UpdateGameExtractsAsync(filter));
            DownloadGameDetailCommand = new AsyncCommand<string>(async name => await UpdateGameDetailAsync(name));
            DownloadTrendingGamesCommand = new AsyncCommand<int>(async number => await UpdateGameExtractsAsync(number));
        }

        public bool DownloadingUser {
            get => _downloadingUser;
            private set {
                _downloadingUser = value;
                OnPropertyChanged();
            }
        }
        public bool UploadingUser {
            get => _uploadingUser;
            private set {
                _uploadingUser = value;
                OnPropertyChanged();
            }
        }
        public bool DownloadingGameExtracts {
            get => _downloadingGameExtracts;
            private set {
                _downloadingGameExtracts = value;
                OnPropertyChanged();
            }
        }
        public bool DownloadingGameDetail {
            get => _downloadingGameDetail;
            private set {
                _downloadingGameDetail = value;
                OnPropertyChanged();
            }
        }

        public bool IsUserInfoInvalid => !string.IsNullOrEmpty(ErrorMessage);

        public string ErrorMessage {
            get => _errorMessage;
            private set {
                _errorMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsUserInfoInvalid));
            }
        }

        public UserInfoModel User {
            get => _user;
            private set {
                _user = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDetailFavourite));
                OnPropertyChanged(nameof(IsDetailFinished));
            }
        }
        public List<GameExtractModel> GameExtracts {
            get => _gameExtracts;
            private set {
                _gameExtracts = value;
                OnPropertyChanged();
            }
        }
        public GameDetailModel GameDetail {
            get => _gameDetail;
            private set {
                _gameDetail = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDetailFavourite));
                OnPropertyChanged(nameof(IsDetailFinished));
            }
        }
        public bool IsDetailFinished => (User.Finished.Contains(GameDetail.Name));
        public bool IsDetailFavourite => (User.Favourite.Contains(GameDetail.Name));

        public ICommand DownloadUserCommand { get; }
        public ICommand AddNewUserCommand { get; }
        public ICommand UpdateDescCommand { get; }
        public ICommand DownloadGameExtractsCommand { get; }
        public ICommand DownloadGameDetailCommand { get; }
        public ICommand DownloadTrendingGamesCommand { get; }
        public ICommand MarkGameCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task UpdateUserAsync(Credentials cred) {
            DownloadingUser = true;
            ErrorMessage = string.Empty;

            try {
                UserInfoModel temp = await _userService.DownloadUserAsync(cred.Nickname);
                if (temp.Nickname == cred.Nickname[1..] && temp.Password == cred.Password) {
                    User = temp;
                } else {
                    throw new ArgumentException();
                }
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (ArgumentException) {
                HandleCommandFailed("Bad credentials.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                DownloadingUser = false;
            }
        }
        private async Task AddNewUserAsync(Credentials cred) {
            UploadingUser = true;
            ErrorMessage = string.Empty;

            try {
                await _userService.AddNewUserAsync(cred);
                UploadingUser = false;
                DownloadingUser = true;
                UserInfoModel temp = await _userService.DownloadUserAsync(cred.Nickname);
                if (temp.Nickname == cred.Nickname[1..] && temp.Password == cred.Password) {
                    User = temp;
                } else {
                    throw new ArgumentException();
                }
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                DownloadingUser = false;
            }
        }
        private async Task UpdateDescAsync(string desc) {
            UploadingUser = true;
            ErrorMessage = string.Empty;

            try {
                User = await _userService.UpdateUserAsync(new Credentials(_user), desc);
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                UploadingUser = false;
            }
        }
        private async Task MarkAsync(MarkInfo markInfo) {
            UploadingUser = true;
            ErrorMessage = string.Empty;

            try {
                User = await _userService.UpdateUserAsync(new Credentials(_user), markInfo);
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                UploadingUser = false;
            }
        }
        private async Task UpdateGameExtractsAsync(string filter) {
            DownloadingGameExtracts = true;
            ErrorMessage = string.Empty;

            try {
                GameExtracts = await _gameService.DownloadGameExtractsAsync(filter);
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (ArgumentException) {
                HandleCommandFailed("Bad credentials.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                DownloadingGameExtracts = false;
            }
        }
        private async Task UpdateGameDetailAsync(string name) {
            DownloadingGameDetail = true;
            ErrorMessage = string.Empty;

            try {
                GameDetail = new GameDetailModel();
                GameDetail = await _gameService.DownloadGameDetailAsync(name);
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (ArgumentException) {
                HandleCommandFailed("Bad credentials.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                DownloadingGameDetail = false;
            }
        }
        private async Task UpdateGameExtractsAsync(int number) {
            DownloadingGameExtracts = true;
            ErrorMessage = string.Empty;

            try {
                GameExtracts = Enumerable.Repeat(new GameExtractModel(), number).ToList();
                GameExtracts = await _gameService.DownloadTrendingGamesAsync(number);
            } catch (ArgumentOutOfRangeException) {
                HandleCommandFailed("User info for selected nickname is not available.");
            } catch (InvalidOperationException) {
                HandleCommandFailed("User info service is not available.");
            } catch (ArgumentException) {
                HandleCommandFailed("Bad credentials.");
            } catch (Exception) {
                HandleCommandFailed("User info service failed.");
            } finally {
                DownloadingGameExtracts = false;
            }
        }

        private void HandleCommandFailed(string message) {
            ErrorMessage = message;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
