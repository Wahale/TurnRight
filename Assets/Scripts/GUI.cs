using System;
using UnityEngine;

public class GUI : MonoBehaviour
{
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject startGameButton;
        private SpriteRenderer _playerSpriteRenderer;

        private bool isSettingsOpened;
        public static bool StartGame;

        private void Start()
        {
                _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
                CheckForLose();
        }

        public void StartGameMethod()
        {
                if (!StartGame)
                {
                        StartGame = true;
                        startGameButton.SetActive(false);
                }
        }

        private void CheckForLose()
        {
                if (!StartGame)
                {
                        startGameButton.SetActive(true);
                }
        }

        public void OpenShopPanel()
        {
                _playerSpriteRenderer.sortingOrder = 0;
                shopPanel.SetActive(true);
        }

        public void CloseShopPanel()
        {
                _playerSpriteRenderer.sortingOrder = 1;
                shopPanel.SetActive(false);
        }

        public void ToggleSettingsPanel() {
        if (!isSettingsOpened) OpenSettingsPanel();
        else CloseSettingsPanel();
        }

        private void OpenSettingsPanel()
        {
            _playerSpriteRenderer.sortingOrder = 0;
            settingsPanel.SetActive(true);
            isSettingsOpened = true;
        }

        private void CloseSettingsPanel()
        {
            _playerSpriteRenderer.sortingOrder = 1;
            settingsPanel.SetActive(false);
            isSettingsOpened = false;
        }
}