using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITU_NET {
    public class GameDiskService : GameInfoServiceBase {

        private const int _delay = 1000;
        private static int _counter = 0;

        private static int Counter{
            get {
                return _counter++;
            }
            set {
                _counter = value;
            }
        }

        readonly private List<GameDetailModel> database = new List<GameDetailModel>{
            new GameDetailModel("Team Fortress 2",  new DateTime(2007, 10, 10), "TF2", "Valve", "Valve", "Simply the best!", LoremIpsum(8, 15, 4, 6, 1, Counter)),
            new GameDetailModel("Factorio", new DateTime(2020, 8, 14), "Factorio", "Wube Software LTD.", "Wube Software LTD.", "Belts!", LoremIpsum(8, 15, 4, 6, 1, Counter)),
            new GameDetailModel("Terraria", new DateTime(2011, 5, 16), "Terraria", "Re-Logic", "Re-Logic", "2D MC! And pink slimes!", LoremIpsum(8, 15, 4, 6, 1, Counter)),
            new GameDetailModel("Orcs Must Die!", new DateTime(2011, 10, 11), "OMD", "Robot Entertainment", "Robot Entertainment", "Orcy orcy orcy!", LoremIpsum(8, 15, 4, 6, 1, Counter)),
            new GameDetailModel("Orcs Must Die! 2", new DateTime(2012, 7, 30), "OMD2", "Robot Entertainment", "Robot Entertainment", "Never gets old!", LoremIpsum(8, 15, 4, 6, 1, Counter)),
            new GameDetailModel("Orcs Must Die! Unchained", new DateTime(2017, 4, 19), "OMDU", "Robot Entertainment", "Robot Entertainment", "It's dead :-(", LoremIpsum(8, 15, 4, 6, 1, Counter)),
            new GameDetailModel("Borderlands 2", new DateTime(2012, 7, 21), "Borderlands2", "2K", "Gearbox Software", "Welcome to Pandora kiddos!", LoremIpsum(8, 15, 4, 6, 1, Counter)),
        };

        protected override async Task<List<GameExtractModel>> GetGameExtracts(string filter) {
            //Simulate network delay
            await Task.Delay(_delay);

            List<GameExtractModel> filtered = new List<GameExtractModel>();
            if (String.IsNullOrEmpty(filter)) {
                filtered.AddRange(database);
            } else {
                foreach (var extract in database) {
                    if (extract.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)) {
                        filtered.Add(extract);
                    }
                }
            }
            
            return filtered;
        }
        protected override async Task<List<GameExtractModel>> GetGameExtracts(int number) {
            //Simulate network delay
            await Task.Delay(_delay);

            List<GameExtractModel> filtered = new List<GameExtractModel>(database);
            if (number >= database.Count) {
                return filtered;
            }
            var rand = new Random();
            return filtered.OrderBy(x => rand.Next()).Take(number).ToList();
        }

        protected override async Task<GameDetailModel> GetGameDetail(string name) {
            //Simulate network delay
            await Task.Delay(_delay);

            return database.Find(x => x.Name.Equals(name));
        }

        //Stolen from StackOverflow  \|/
        static string LoremIpsum(int minWords, int maxWords, int minSentences, int maxSentences, int numParagraphs, int seed) {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random(seed);
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++) {
                result.Append("     ");
                for (int s = 0; s < numSentences; s++) {
                    for (int w = 0; w < numWords; w++) {
                        if (w > 0) {
                            result.Append(' ');
                            result.Append(words[rand.Next(words.Length)]);
                        } else {
                            string str = words[rand.Next(words.Length)];
                            result.Append(str[0].ToString().ToUpper() + str[1..]);
                        }
                    }
                    result.Append(". ");
                }
                result.Append('\n');
            }

            return result.ToString();
        }
    }
}