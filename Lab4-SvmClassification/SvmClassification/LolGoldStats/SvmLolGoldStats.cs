namespace Lab4_SvmClassification.SvmClassification.LolGoldStats
{
    /// <summary>
    /// Implementation of SVM for LolGoldStats data.
    /// </summary>
    public class SvmLolGoldStats : ISvmClassification
    {
        private readonly RenderService _renderService;

        /// <summary>
        /// Default constructor with initialize fields.
        /// </summary>
        public SvmLolGoldStats()
        {
            _renderService = new RenderService();
        }

        /// <summary>
        /// Method to invoke SvmLolGoldStats logic of predict.
        /// </summary>
        public void Invoke()
        {
            //Load sample data
            var sampleData = loadSampleData();

            //Load model and predict output
            var result = LolGoldStatsData.Predict(sampleData).Prediction;

            Console.WriteLine($"Given stats is predicted as: {result}");
            Console.ReadKey();
        }

        /// <summary>
        /// Method to get from user object LolGoldStatsData with filled properties.
        /// </summary>
        /// <returns>Returning input model of LolGoldStatsData.</returns>
        private LolGoldStatsData.ModelInput loadSampleData()
           => new LolGoldStatsData.ModelInput()
           {
               KDA = getFloatUserInput(LolGoldStatsFieldName.KDA),
               CSPERMIN = getFloatUserInput(LolGoldStatsFieldName.CSPERMIN),
               GOLDINMIN15 = getFloatUserInput(LolGoldStatsFieldName.GOLDINMIN15),
               GOLDIN30MIN = getFloatUserInput(LolGoldStatsFieldName.GOLDINMIN30),
           };

        /// <summary>
        /// Method to get from user float that represent value of expected properties.
        /// </summary>
        /// <param name="fieldName">Enum representing current value to enter.</param>
        /// <returns>Value of current property.</returns>
        private float getFloatUserInput(LolGoldStatsFieldName fieldName)
        {
            float input;

            do
            {
                Console.WriteLine($"Enter {fieldName} value as float-pointing number: ");
                string userInput = Console.ReadLine();

                if (float.TryParse(userInput, out input))
                {
                    return input;
                }

                _renderService.WrongUserInputMessage();
            }
            while (true);
        }
    }

    /// <summary>
    /// Enum of all fields in input model.
    /// </summary>
    public enum LolGoldStatsFieldName
    {
        KDA,
        CSPERMIN,
        GOLDINMIN15,
        GOLDINMIN30,
    }
}
