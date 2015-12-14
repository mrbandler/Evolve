using UnityEngine;
using System.Collections;
using LD34.PlayerCode;
using LD34.Utils;

namespace LD34.Pickupables
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class PickUp : MonoBehaviour
    {
        //protected
        protected Player currentPlayer;
        protected Transform canvasTransform;
       
        /// <summary>
        /// Start this instance.
        /// </summary>
        public void Start()
        {
            canvasTransform = GameObject.FindGameObjectWithTag(Strings.canvas).transform;
        }

        /// <summary>
        /// Called every frame.
        /// </summary>
        public void Update()
        {
            if (Input.GetButtonUp(Strings.use))
            {
                Use();
            }
        }

        /// <summary>
        /// Raises the trigger enter event.
        /// </summary>
        /// <param name="collider">Collider.</param>
        public void OnTriggerEnter(Collider collider)
        {
            currentPlayer = collider.gameObject.GetComponent<Player>();
            if (currentPlayer != null)
            {
                ShowUserFeedback();
            }
        }

        /// <summary>
        /// Raises the trigger exit event.
        /// </summary>
        /// <param name="collider">Collider.</param>
        public void OnTriggerExit(Collider collider)
        {
            if (currentPlayer != null)
            {
                currentPlayer = null;
                HideUserFeedback();
            }
        }

        /// <summary>
        /// Picks up.
        /// </summary>
        protected abstract void Use();
  
        /// <summary>
        /// Shows the user feedback.
        /// </summary>
        protected abstract void ShowUserFeedback();
        protected abstract void HideUserFeedback();
    }
}