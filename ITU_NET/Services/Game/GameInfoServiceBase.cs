using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITU_NET {
    public abstract class GameInfoServiceBase {

        public async Task<List<GameExtractModel>> DownloadGameExtractsAsync(string filter) {
            return await GetGameExtracts(filter);
        }

        public async Task<GameDetailModel> DownloadGameDetailAsync(string name) {
            return await GetGameDetail(name);
        }

        public async Task<List<GameExtractModel>> DownloadTrendingGamesAsync(int number) {
            return await GetGameExtracts(number);
        }

        protected abstract Task<List<GameExtractModel>> GetGameExtracts(int number);
        protected abstract Task<List<GameExtractModel>> GetGameExtracts(string filter);
        protected abstract Task<GameDetailModel> GetGameDetail(string name);
    }
}