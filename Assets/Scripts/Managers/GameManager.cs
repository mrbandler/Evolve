using UnityEngine;
using System.Collections;
using LD34.PlayerCode;
using LD34.Utils;

namespace LD34.Manager
{
    public class GameManager : MonoBehaviour 
    {
        //public
        public int secondsBetweenPlayerStatUpdates = 5;

        //private
        [HideInInspector]
        public Player currentPlayer;

        /// <summary>
        /// Awake is called when this object will be created in memory.
        /// </summary>
        public void Awake()
        {
            currentPlayer = GameObject.FindGameObjectWithTag(Strings.player).GetComponent<Player>();
            Cursor.visible = false;
        }

        /// <summary>
        /// Called after the awake function.
        /// </summary>
        public void Start()
        {
            InvokeRepeating("UpdatePlayerHungerStats", secondsBetweenPlayerStatUpdates, secondsBetweenPlayerStatUpdates);
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void Update()
        {
            DoEvolve();
        }

        #region Private methods

        /// <summary>
        /// Does the evolve.
        /// </summary>
        private void DoEvolve()
        {
            if (Input.GetButtonUp(Strings.evolve))
            {
                currentPlayer.Evolve();
            }
        }

        /// <summary>
        /// Game over.
        /// </summary>
        private void GameOver()
        {
            CancelInvoke();
            currentPlayer.PlayerController.Die();
        }

        /// <summary>
        /// Updates the player stats.
        /// </summary>
        private void UpdatePlayerHungerStats()
        {
            if (currentPlayer)
            {
                int random = Random.Range(1,3);
                currentPlayer.SubHunger(random);
                if (currentPlayer.Starving)
                {
                    InvokeRepeating("UpdatePlayerHealthStats", secondsBetweenPlayerStatUpdates, secondsBetweenPlayerStatUpdates);
                }

                if (currentPlayer.MustShit)
                {
                    InvokeRepeating("UpdatePlayerHealthStats", secondsBetweenPlayerStatUpdates, secondsBetweenPlayerStatUpdates);
                }
            }
        }

        /// <summary>
        /// Updates the player health stats.
        /// </summary>
        private void UpdatePlayerHealthStats()
        {
            if (currentPlayer)
            {
                int random = Random.Range(1,3);
                currentPlayer.SubHealth(random);
                if (currentPlayer.Dead)
                {
                    GameOver();
                }
            }
        }

        #endregion
    }
}
