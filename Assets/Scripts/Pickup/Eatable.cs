using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using LD34.PlayerCode;
using LD34.Utils;

namespace LD34.Pickupables
{
    public class Eatable : PickUp
    {   
        //public
        public string eatableName;
        public GameObject PickUpAlert;
        public GameObject EatingSlider;
        public int timeToDestroy = 5;
        public int health;
        public int energy;
        public int hunger;
        public int evolvePoints;

        //private
        private Text pickUpAlertText;
        private GameObject tmpPickUpAlert;
        private GameObject tmpEatingSlider;
        private Slider tmpSlider;
        private bool bEating;

        /// <summary>
        /// Override of the Use function from PickUp
        /// </summary>
        protected override void Use()
        {
            if (currentPlayer != null)
            {
                currentPlayer.PlayerView.PlayEatSound();
                bEating = true;
                HideUserFeedback();
                ShowEatingSlider();
                StartCoroutine(Eat());
            }
        }

        /// <summary>
        /// Shows the user feedback.
        /// </summary>
        protected override void ShowUserFeedback()
        {
            tmpPickUpAlert = Instantiate(PickUpAlert) as GameObject;
            tmpPickUpAlert.transform.SetParent(canvasTransform, false);
            pickUpAlertText = tmpPickUpAlert.GetComponentInChildren<Text>();
            pickUpAlertText.text = Strings.eatText + eatableName;
        }

        /// <summary>
        /// Hides the user feedback.
        /// </summary>
        protected override void HideUserFeedback()
        {
            Destroy(tmpPickUpAlert);
        }

        IEnumerator Eat()
        {
            currentPlayer.PlayerController.EnableMovement = false;
            yield return new WaitForSeconds(timeToDestroy);
            currentPlayer.PlayerController.EnableMovement = true;
            currentPlayer.AddHealth(health);
            currentPlayer.AddEnergy(energy);
            currentPlayer.AddHunger(hunger);
            currentPlayer.AddToEvolvePoints(evolvePoints);
            Destroy(tmpEatingSlider);
            Destroy(this.gameObject);
        }

        #region Private methods

        private void ShowEatingSlider()
        {
            tmpEatingSlider = Instantiate(EatingSlider) as GameObject;
            tmpEatingSlider.transform.SetParent(canvasTransform, false);
            tmpSlider = tmpEatingSlider.GetComponent<Slider>();
            tmpSlider.minValue = 0;
            tmpSlider.maxValue = timeToDestroy;
            tmpSlider.wholeNumbers = false;
        }

        void FixedUpdate()
        {
            UpdateEatingSlider();
        }

        void UpdateEatingSlider()
        {
            if (bEating)
            {
                float timer = 0.0f;
                float timerMax = (float)timeToDestroy;

                timer += Time.deltaTime;
                tmpSlider.value += Time.deltaTime;

                if (timer >= timerMax)
                {
                    bEating = false;
                }
            }
        }

        #endregion
    }
}