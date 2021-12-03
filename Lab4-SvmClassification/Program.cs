using Lab4_SvmClassification.SvmClassification.Banknote;
using Lab4_SvmClassification.SvmClassification.LolGoldStats;

/*
 * Application created by Julian Chodorowski.
 * 
 * How to launch:
 * Copy folder Lab4-SvmClassification\bin\Debug\net6.0-windows
 * and launch Lab4-SvmClassification.exe app.
 * 
 * App contains 2 SVM Datas:
 * 1 - Banknote data https://archive.ics.uci.edu/ml/datasets/banknote+authentication
 * 2 - LolGoldsStats data https://eune.op.gg/summoner/userName=Bot%20Julas
 * 
 * Second data is manual converted information from link to get
 * my Kills/Deaths/Assists, Creep Score Per minute (number of killed minions per minute),
 * Gold diff vs enemy laner in 15 and 30 min.
 * Svm classified data to how good stats is and if player player better over time or start losing.
 * 
 * All datas is available in SvmClassifications sub-folders.
 */

namespace Lab4_SvmClassification
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main app method require to start application.
        /// </summary>
        public static void Main(string[] args)
        {
            MenuService menuService = new MenuService();
            IEnumerable<ISvmClassification> svmList = getSvmClassificationList();

            switch (menuService.SelectSvmType(svmList))
            {
                case char input when input == '0' || input == '1':
                    svmList.ElementAt(int.Parse(input.ToString())).Invoke();
                    break;

                case char input when input == '4':
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Method to get list of implemented Svm Classification classes.
        /// </summary>
        private static IEnumerable<ISvmClassification> getSvmClassificationList()
            => new List<ISvmClassification>()
            {
                new SvmBanknote(),
                new SvmLolGoldStats(),
            };
    }
}


