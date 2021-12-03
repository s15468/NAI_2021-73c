namespace Lab4_SvmClassification.SvmClassification.Banknote
{
    /// <summary>
    /// Implementation of SVM for SvmBanknote data.
    /// </summary>
    public class SvmBanknote : ISvmClassification
    {
        private readonly RenderService _renderService;

        /// <summary>
        /// Default constructor with initialize fields.
        /// </summary>
        public SvmBanknote()
        {
            _renderService = new RenderService();
        }

        /// <summary>
        /// Method to invoke SvmBanknote logic of predict.
        /// </summary>
        public void Invoke()
        {
            //Load sample data
            BanknoteData.ModelInput sampleData = loadSampleData();

            //Load model and predict output
            var result = BanknoteData.Predict(sampleData).Prediction;
            Enum.TryParse(result.ToString(), out BanknoteStatus status);
            Console.WriteLine($"Banknote is {status}");
            Console.ReadKey();
        }

        /// <summary>
        /// Method to get from user object BanknoteData with filled properties.
        /// </summary>
        /// <returns>Returning input model of BanknoteData.</returns>
        private BanknoteData.ModelInput loadSampleData()
            => new BanknoteData.ModelInput()
            {
                Variance = getFloatUserInput(BanknoteFieldName.Variance),
                Entropy = getFloatUserInput(BanknoteFieldName.Entropy),
                Kurtosis = getFloatUserInput(BanknoteFieldName.Kurtosis),
                Skewness = getFloatUserInput(BanknoteFieldName.Skewness),
            };

        /// <summary>
        /// Method to get from user float that represent value of expected properties.
        /// </summary>
        /// <param name="fieldName">Enum representing current value to enter.</param>
        /// <returns>Value of current property.</returns>
        private float getFloatUserInput(BanknoteFieldName fieldName)
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
    /// Enum of available predict status.
    /// </summary>
    public enum BanknoteStatus
    {
        Authentic = 0,
        Inauthentic = 1
    }

    /// <summary>
    /// Enum of all fields in input model.
    /// </summary>
    public enum BanknoteFieldName
    {
        Variance,
        Entropy,
        Kurtosis,
        Skewness,
    }
}
