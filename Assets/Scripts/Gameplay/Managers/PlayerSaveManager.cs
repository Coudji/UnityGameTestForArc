using System.Linq;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    private MainMenuController _ui;
    private PlayerSaveData _currentSave;
    private PlayerSaveSystem _playerSaveSystem;

    private void Start()
    {
        _ui = GetComponent<MainMenuController>();

        if (_ui == null)
        {
            Debug.LogError("SaveUIController not found!");
            return;
        }

        _playerSaveSystem = new PlayerSaveSystem();

        LoadLatestSave();
        UpdateButtons();
    }

    public void LoadLatestSave()
    {
        _currentSave = _playerSaveSystem.LoadAllOrdered().FirstOrDefault();
    }

    public void UpdateButtons()
    {
        bool playerHasNoSave = _currentSave == null;
        _ui.UpdateButtons(playerHasNoSave);
    }

    private void OnSaveSelected(PlayerSaveData selectedSave)
    {
        _currentSave = selectedSave;
        _ui.HideLoadGamePanel();
        ContinueGame();
    }

    public void ContinueGame()
    {
        if (_currentSave != null)
        {
            _ui.ShowStartGamePanel();
        }
        else
        {
            Debug.LogWarning("No save data available to continue.");
        }
    }

    public void OpenLoadGameMenu()
    {
        _ui.ShowLoadGamePanel();
        _ui.ClearLoadGameContent();

        foreach (PlayerSaveData playerSaveData in _playerSaveSystem.LoadAllOrdered())
        {
            _ui.CreateLoadGameButton(playerSaveData, OnSaveSelected);
        }
    }

    public void StartNewGame()
    {
        _currentSave = _playerSaveSystem.CreateNewSave();
        UpdateButtons();
        ContinueGame();
    }
}
