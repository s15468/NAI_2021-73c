using Lab6_OpenCV.Services;
using System.Windows.Automation.Peers;

namespace Lab6_OpenCV.Service
{
    /// <summary>
    /// Service to control spotify application
    /// </summary>
    public class SpotifyService
    {
        private readonly UserInterfaceAutomationService _uIAutomationService;

        /// <summary>
        /// Default constructor with field initialize.
        /// </summary>
        public SpotifyService()
        {
            _uIAutomationService = new UserInterfaceAutomationService();
        }

        /// <summary>
        /// Method to attach to Spotify window
        /// </summary>
        /// <param name="isPremiumAccount">If application is logged in to premium account then window changes name</param>
        public void AttachToApplication(bool isPremiumAccount = true)
        {
            _uIAutomationService.AttachToControl($"Spotify{(isPremiumAccount ? " Premium" : string.Empty)}", AutomationControlType.Pane);
        }

        /// <summary>
        /// Method to invoke play button at the bottom of application.
        /// Button after click change to pause button.
        /// </summary>
        public void InvokePlayButton()
        {
            UserInterfaceControl playButton = _uIAutomationService.FindSingle("Play", null, AutomationControlType.Button);
            _uIAutomationService.Invoke(playButton);
        }

        /// <summary>
        /// Method to invoke pause button at the bottom of application.
        /// Button after click change to play button.
        /// </summary>
        public void InvokePauseButton()
        {
            UserInterfaceControl pauseButton = _uIAutomationService.FindSingle("Pause", null, AutomationControlType.Button);
            _uIAutomationService.Invoke(pauseButton);
        }

        /// <summary>
        /// Mehtod to invoke next button at the bottom of application. On the right from play/pause button.
        /// Button move to next track on list.
        /// </summary>
        public void InvokeNextButton()
        {
            UserInterfaceControl nextButton = _uIAutomationService.FindSingle("Next", null, AutomationControlType.Button);
            _uIAutomationService.Invoke(nextButton);
        }

        /// <summary>
        /// Mehtod to invoke previous button at the bottom of application. On the left from play/pause button.
        /// Button move to previous track on list.
        /// </summary>
        public void InvokePreviousButton()
        {
            UserInterfaceControl previousButton = _uIAutomationService.FindSingle("Previous", null, AutomationControlType.Button);
            _uIAutomationService.Invoke(previousButton);
        }

        /// <summary>
        /// Method to invoke steps that open in spotify liked songs and invoke button to play music on this playlist.
        /// </summary>
        public void InvokeStartPlayLikedSongs()
        {
            UserInterfaceControl likedSongsButton = _uIAutomationService.FindSingle("Liked Songs", null, AutomationControlType.Hyperlink);
            _uIAutomationService.Invoke(likedSongsButton);

            UserInterfaceControl playLikedSongsButton = _uIAutomationService.FindSingle("Play Liked Songs", null, AutomationControlType.Button);
            _uIAutomationService.Invoke(playLikedSongsButton);
        }
    }
}
