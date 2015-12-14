using UnityEngine;
using System.Collections;

namespace LD34.PlayerCode
{
    public class PlayerView : MonoBehaviour
    {
        //public
        public AudioClip buzzSound;
        public GameObject shitPrefab;
        public Transform shitPosition;
        [HideInInspector]
        public AudioSource flyAudio;

        //private
        private AudioSource[] audioSources;

        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Start()
        {
            audioSources = GetComponents<AudioSource>();
            flyAudio = audioSources[0];
            audioSources[0].clip = buzzSound;
            audioSources[0].Play();
        }

        /// <summary>
        /// Lets the fly evolve graphically.
        /// </summary>
        public void Evolve()
        {
            //TODO: Implement the graphics and movement code for the evolve procedure.
            transform.localScale = new Vector3(transform.localScale.x + 1, transform.localScale.y + 1, transform.localScale.z + 1); 
        }

        /// <summary>
        /// Lets the fly shit graphically.
        /// </summary>
        public void Shit()
        {
            Instantiate(shitPrefab, shitPosition.position, Quaternion.identity);
            audioSources[2].Play();
        }

        /// <summary>
        /// Plays the sound.
        /// </summary>
        public void PlayNormalSound()
        {
            audioSources[0].volume = 0.5f;
            audioSources[0].pitch = 1f;
        }

        /// <summary>
        /// Plays the move sound.
        /// </summary>
        public void PlayMoveSound()
        {
            audioSources[0].volume = 0.7f;
            audioSources[0].pitch = 1.2f;
        }

        /// <summary>
        /// Plays the eat sound.
        /// </summary>
        public void PlayEatSound()
        {
            audioSources[1].Play();
        }
    }
}