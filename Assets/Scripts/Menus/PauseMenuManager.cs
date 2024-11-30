using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Pause Menu Objects")]
    [SerializeField] private GameObject _pauseMenuCanvasGO;
    [SerializeField] private GameObject _settingsMenuCanvasGO;
    [SerializeField] private GameObject _keyboardControlsMenuCanvasGO;
    [SerializeField] private GameObject _soundSettingsMenuCanvasGO;

    [Header("Player Scripts to Deactivate on Pause")]
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAttack _playerAttack;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _pauseMenuFirst;
    [SerializeField] private GameObject _settingsMenuFirst;
    [SerializeField] private GameObject _keyboardControlsMenuFirst;
    [SerializeField] private GameObject _soundSettingsMenuFirst;

    private bool isPaused;

    private void Start()
    {
        _pauseMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.MenuOpenCloseInput)
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    #region Pause/Unpause Functions

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        _playerMovement.enabled = false;
        _playerAttack.enabled = false;

        OpenPauseMenu();
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _playerMovement.enabled = true;
        _playerAttack.enabled = true;

        CloseAllMenus();
    }

    #endregion

    #region Canvas Activations/Deactivations

    private void OpenPauseMenu()
    {
        _pauseMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }

    private void OpenSettingsMenuHandle()
    {
        _settingsMenuCanvasGO.SetActive(true);
        _pauseMenuCanvasGO.SetActive(false);
        _keyboardControlsMenuCanvasGO.SetActive(false);
        _soundSettingsMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_settingsMenuFirst);
    }

    private void OpenKeyboardControlsHandle()
    {
        _keyboardControlsMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_keyboardControlsMenuFirst);
    }

    private void OpenSoundSettingsHandle()
    {
        _soundSettingsMenuCanvasGO.SetActive(true);
        _settingsMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(_soundSettingsMenuFirst);
    }

    private void CloseAllMenus()
    {
        _pauseMenuCanvasGO.SetActive(false);
        _settingsMenuCanvasGO.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    #endregion

    #region Pause Menu Button Actions

    public void OnSettingsPress()
    {
        OpenSettingsMenuHandle();
    }

    public void OnResumePress()
    {
        Unpause();
    }

    #endregion

    #region Settings Menu Button Actions

    public void OnSettingsBackPress()
    {
        OpenPauseMenu();
    }

    public void OnKeyboardControlsPress()
    {
        OpenKeyboardControlsHandle();
    }

    public void OnSoundSettingsPress()
    {
        OpenSoundSettingsHandle();
    }

    #endregion
}
