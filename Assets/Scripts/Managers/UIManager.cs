using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using LD34.Utils;
using LD34.PlayerCode;
using UnityEngine.SceneManagement;

namespace LD34.Manager
{
    public class UIManager : MonoBehaviour 
    {
        //public
        public Slider evolvePointsSlider;
        public Slider healthSlider;
        public Slider energySlider;
        public Slider hungerSlider;

        public AudioClip alertSound;
        public GameObject starvingAlert;
        public GameObject bowelAlert;
        public GameObject evolveAlert;

        public AudioClip evolveSound;
        public float secondsToShowEvolveAlert = 3;
        public Text evolveState;

        public AudioClip gameOverSound;
        public GameObject gameOverAlert;

        public AudioClip winSound;
        public GameObject winAlert;

        //private
        private AudioSource audio;
        private GameManager gameManager;
        private bool bShowedEvolveAlert;

        private bool bShowedBowelAlert;
        private bool bShowedStarvingAlert;
        private bool bShowedGameOverAlert;
        private bool bShowedWinAlert;

        /// <summary>
        /// Start this instance.
        /// </summary>
        void Start()
        {
            AudioSource[] audioSources = GameObject.FindGameObjectWithTag(Strings.audioManager).GetComponents<AudioSource>();
            audio = audioSources[1];
            gameManager = GameObject.FindGameObjectWithTag(Strings.gameManager).GetComponent<GameManager>();
            starvingAlert.SetActive(false);
            bowelAlert.SetActive(false);

            bShowedEvolveAlert = false;
            evolveAlert.SetActive(false);
            gameOverAlert.SetActive(false);
        }

        void Update()
        {
            UpdateSliders();
            UpdateAlerts();
            UpdateEvolveState();
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("Main");
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        #region Private methods

        /// <summary>
        /// Updates the sliders.
        /// </summary>
        private void UpdateSliders()
        {
            evolvePointsSlider.maxValue = gameManager.currentPlayer.NextEvolve;
            evolvePointsSlider.minValue = 0;

            healthSlider.maxValue = gameManager.currentPlayer.MaxHealth;
            healthSlider.minValue = 0;

            energySlider.maxValue = gameManager.currentPlayer.MaxEnergy;
            energySlider.minValue = 0;

            hungerSlider.maxValue = gameManager.currentPlayer.MaxHunger;
            hungerSlider.minValue = 0;

            evolvePointsSlider.value = gameManager.currentPlayer.EvolvePoints;
            healthSlider.value = gameManager.currentPlayer.Health;
            energySlider.value = gameManager.currentPlayer.Energy;
            hungerSlider.value = gameManager.currentPlayer.Hunger;
        }

        /// <summary>
        /// Updates the alerts.
        /// </summary>
        private void UpdateAlerts()
        {
            if (gameManager.currentPlayer.MustShit)
            {
                if (bShowedBowelAlert == false)
                {
                    audio.clip = alertSound;
                    audio.Play();
                    bowelAlert.SetActive(true);
                    bShowedBowelAlert = true;
                }
            }
            else
            {
                bShowedBowelAlert = false;
                bowelAlert.SetActive(false);
            }

            if (gameManager.currentPlayer.Starving)
            {
                if (bShowedStarvingAlert == false)
                {
                    audio.clip = alertSound;
                    audio.Play();
                    starvingAlert.SetActive(true);
                    bShowedStarvingAlert = true;
                }
            }
            else
            {
                bShowedStarvingAlert = false;
                starvingAlert.SetActive(false);
            }

            if (gameManager.currentPlayer.CanEvolve && bShowedEvolveAlert == false)
            {
                audio.clip = evolveSound;
                audio.Play();
                StartCoroutine(ShowEvolveAlert());
            }

            if (gameManager.currentPlayer.CanEvolve == false)
            {
                bShowedEvolveAlert = false;
            }

            if (gameManager.currentPlayer.Dead)
            {
                if (bShowedGameOverAlert == false)
                {
                    audio.clip = gameOverSound;
                    audio.Play();
                    gameOverAlert.SetActive(true);
                    bShowedGameOverAlert = true;
                }
            }

            if (gameManager.currentPlayer.State == EvolveState.HORST)
            {
                if (bShowedWinAlert == false)
                {
                    audio.clip = winSound;
                    audio.Play();
                    winAlert.SetActive(true);
                    bShowedWinAlert = true;
                }
            }
        }

        /// <summary>
        /// Updates the state of the evolve.
        /// </summary>
        private void UpdateEvolveState()
        {
            evolveState.text = Strings.evolveText + Enum.GetName(typeof(EvolveState), gameManager.currentPlayer.State);
        }

        IEnumerator ShowEvolveAlert()
        {
            evolveAlert.SetActive(true);
            yield return new WaitForSeconds(secondsToShowEvolveAlert);
            bShowedEvolveAlert = true;
            evolveAlert.SetActive(false);
        }

        #endregion
    }
}
