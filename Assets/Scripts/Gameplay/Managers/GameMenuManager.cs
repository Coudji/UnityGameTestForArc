using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Arc.Gameplay.Managers
{
    public class GameMenuManager : BaseMenuManager
    {
        public void Start()
        {
            Button BackToMenu = MenuDocument.rootVisualElement.Q<Button>(UIElementNames.BackToMenu);
    
            if (BackToMenu != null)
            {
                BackToMenu.clicked += OnClick_ReturnToMenu;
            }
        }
    
        public void OnClick_ReturnToMenu()
        {
            LobbyManager.LeaveLobbyFromGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.MainMenu);
        }
    
        public void OnOpenMenu(InputValue value) => OpenMenu();
    
        public void OnCancel(InputValue value) => CloseMenu();
    }
}
