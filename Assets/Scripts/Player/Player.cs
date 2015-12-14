using UnityEngine;
using System.Collections;

namespace LD34.PlayerCode
{
    public class Player : MonoBehaviour 
    {
        //public
        public PlayerController PlayerController { get { return playerController; } }
        public PlayerView PlayerView { get { return playerView; } }

        public EvolveState State { get { return state; } set { state = value; } }
        public int EvolvePoints { get { return evolvePoints; } }
        public int Health { get { return health; } }
        public int Energy { get { return energy; } } 
        public int Hunger { get { return hunger; } }
        public bool Dead { get { return bDead; } }
        public bool Starving { get { return bStarving; } }
        public bool MustShit{ get { return bMustShit; } }
        public bool CanEvolve { get { return bCanEvolve; } }

        public int MaxHealth { get { return maxHealth; } }
        public int MaxEnergy { get { return maxEnergy; } }
        public int MaxHunger { get { return maxHunger; } }
        public int NextEvolve { get { return nextEvolve; } }



        //private
        private EvolveState state;
        [SerializeField]
        private int health;
        [SerializeField]
        private int energy;
        [SerializeField]
        private int hunger;
        [SerializeField]
        private int evolvePoints;

        [SerializeField]
        private int maxHealth = 20;
        [SerializeField]
        private int maxEnergy = 20;
        [SerializeField]
        private int maxHunger = 30;
        [SerializeField]
        private int nextEvolve = 20;

        private bool bCanEvolve;
        private bool bDead;
        private bool bStarving;
        private bool bMustShit;

        private PlayerController playerController;
        private PlayerView playerView;

        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Awake()
        {
            evolvePoints = 0;
            health = maxHealth;
//            energy = maxEnergy;
            hunger = maxHunger;
            bCanEvolve = false;
            bDead = false;
            playerController = GetComponent<PlayerController>();
            playerView = GetComponent<PlayerView>();
        }

        /// <summary>
        /// Evolve the player.
        /// </summary>
        public void Evolve()
        {
            if (bCanEvolve)
            {
                switch (state)
                {
                    case EvolveState.SMALL:
                        state = EvolveState.NORMAL;
                        break;

                    case EvolveState.NORMAL:
                        state = EvolveState.BIG;
                        break;

                    case EvolveState.BIG:
                        state = EvolveState.HORST;
                        break;
                }

                maxHealth *= 2;
                maxEnergy *= 2;
                maxHunger *= 2;
                nextEvolve *= 2;
                bCanEvolve = false;

                health = maxHealth;
                energy = maxEnergy;
                hunger = MaxHunger;

                playerController.Evolve();
                playerView.Evolve();
            }
        }

        /// <summary>
        /// Adds to evolve points.
        /// </summary>
        /// <returns>The to evolve points.</returns>
        /// <param name="value">Value.</param>
        public int AddToEvolvePoints(int value)
        {
            if (bCanEvolve == false)
            {
                evolvePoints += value;
                if (evolvePoints >= nextEvolve)
                {
                    bCanEvolve = true;
                    evolvePoints = 0;
                    return evolvePoints;
                }
            }
            return 0;
        }

        /// <summary>
        /// Adds to the health.
        /// </summary>
        /// <param name="value">Value.</param>
        public void AddHealth(int value)
        {
            health += value;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }

        /// <summary>
        /// Subs from the health.
        /// </summary>
        /// <param name="value">Value.</param>
        public void SubHealth(int value)
        {
            health -= value;
            if (health <= 0)
            {
                health = 0;
                bDead = true;
            }
        }

        /// <summary>
        /// Adds to the energy.
        /// </summary>
        /// <returns>The new value of energy.</returns>
        /// <param name="value">Value to add.</param>
        public void AddEnergy(int value)
        {
            energy += value;
            if (energy >= maxEnergy)
            {
                energy = maxEnergy;
                bMustShit = true;
            }
        }

        /// <summary>
        /// Subs from the energy.
        /// </summary>
        /// <returns>The new value of energy.</returns>
        /// <param name="value">Value to sub.</param>
        public void SubEnergy(int value)
        {
            energy -= value;
            if (energy < 0)
            {
                energy = 0;
            }

            if (energy < maxEnergy)
            {
                bMustShit = false;
            }
        }

        /// <summary>
        /// Adds to the hunger.
        /// </summary>
        /// <returns>The new value of hunger.</returns>
        /// <param name="value">Value to add.</param>
        public void AddHunger(int value)
        {
            hunger += value;
            if (bStarving == true && hunger > 0)
            {
                bStarving = false;    
            }

            if (hunger > maxHunger)
            {
                hunger = maxHunger;
            }
        }

        /// <summary>
        /// Subs from the hunger.
        /// </summary>
        /// <returns>The new value of hunger.</returns>
        /// <param name="value">Value to sub.</param>
        public void SubHunger(int value)
        {
            hunger -= value;
            if (hunger <= 0)
            {
                hunger = 0;
                bStarving = true;
            }
        }
    }
}