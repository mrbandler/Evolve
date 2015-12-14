using UnityEngine;
using System.Collections;

namespace LD34.PlayerCode.Combat
{
    public class Bomb : MonoBehaviour 
    {
        //public
        [Range(0, 10)]
        public float timeToExplode = 5;
        public float damage;

        /// <summary>
        /// Called after the awake funtion.
        /// </summary>
    	void Start () 
        {
            Invoke("Explode", timeToExplode);
    	}

        /// <summary>
        /// ...
        /// </summary>
        void Explode()
        {
            //TODO: Explode mechanic and visuals here!
            Destroy(this.gameObject);
        }
    }
}
