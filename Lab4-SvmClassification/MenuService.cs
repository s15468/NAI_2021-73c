namespace Lab4_SvmClassification
{
    /// <summary>
    /// Method to show user main menu of application.
    /// </summary>
    public class MenuService
    {
        private readonly RenderService _renderService;

        /// <summary>
        /// Default constructor of class with initialize fields.
        /// </summary>
        public MenuService()
        {
            _renderService = new RenderService();
        }

        /// <summary>
        /// Method to get from use selected Svm Classification type.
        /// </summary>
        /// <param name="smvList">List of Svm Classificaiton types.</param>
        /// <returns></returns>
        public char SelectSvmType(IEnumerable<ISvmClassification> smvList)
        {
            char input;

            do
            {
                _renderService.RenderSelectSvmMenu(smvList);
                input = Console.ReadKey().KeyChar;

                switch (input)
                {
                    case '0':
                    case '1':
                    case '4':
                        return input;

                    default:
                        _renderService.WrongUserInputMessage();
                        break;
                }
            }
            while (true);
        }
    }
}
