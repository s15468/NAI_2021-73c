namespace Lab4_SvmClassification
{
    /// <summary>
    /// Class to render message in console for user.
    /// </summary>
    public class RenderService
    {
        /// <summary>
        /// Method to render menu with option to select.
        /// </summary>
        /// <param name="svmList">List of implemented Svm classes.</param>
        public void RenderSelectSvmMenu(IEnumerable<ISvmClassification> svmList)
        {
            int counter = 0;

            Console.WriteLine("Select SVM type to classificate data:");

            foreach (ISvmClassification svmType in svmList)
            {
                Console.WriteLine($"{counter} {svmType.GetType().Name}");
                counter++;
            }

            Console.WriteLine("4 Exit application");
        }

        /// <summary>
        /// Method to render warning message when user input incorrect value.
        /// </summary>
        public void WrongUserInputMessage()
        {
            Console.WriteLine("Input is incorrect. Click any button to try again.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
