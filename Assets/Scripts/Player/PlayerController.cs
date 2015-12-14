using UnityEngine;
using System.Collections;
using LD34.Utils;

namespace LD34.PlayerCode
{
    public class PlayerController : MonoBehaviour 
    {
        //public
        [Range(0,10)]
        public float shitCoolDown;
        [Range(0,50)]
        public float speed;
        [Range(0,1)]
        public float upDownSpeed;
        [Range(0,10)]
        public float mouseSensitivity;

        public bool EnableMovement { get { return bEnableMovement; } set { bEnableMovement = value; }}

        //private
        private Player player;
        private PlayerView view;
        private Rigidbody rb;
        private bool bEnableMovement;
        private bool bCanShit;

        /// <summary>
        /// Starts this instance.
        /// </summary>
        void Start()
        {
            bCanShit = true;
            bEnableMovement = true;
            player = GetComponent<Player>();
            view = GetComponent<PlayerView>();
            rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        void Update()
        {
            Movement();
            Shit();
        } 

        /// <summary>
        /// Lets the fly die.
        /// </summary>
        public void Die()
        {
            view.flyAudio.Stop();
            bEnableMovement = false;
            rb.mass = 1000f;
            rb.freezeRotation = false;
            rb.useGravity = true;
        }

        /// <summary>
        /// Evolve this instance.
        /// </summary>
        public void Evolve()
        {
            //TODO: Implement the graphics and movement code for the evolve procedure.
        }

        #region Private methods

        /// <summary>
        /// Lets the fly shit little bombs.
        /// </summary>
        private void Shit()
        {
            
            if (bCanShit && Input.GetButtonUp(Strings.shit))
            {
                float tmpEnergy = player.Energy;
                if ((tmpEnergy - 5) >= 0)
                {
                    player.SubEnergy(2);
                    view.Shit();
                    StartCoroutine(CoolDown());
                }
            }
        }

        /// <summary>
        /// Cooldown for the shit mechanic.
        /// </summary>
        /// <returns>The down.</returns>
        IEnumerator CoolDown()
        {
            bCanShit = false;
            yield return new WaitForSeconds(shitCoolDown);
            bCanShit = true;
        }

        /// <summary>
        /// Player movement.
        /// </summary>
        private void Movement()
        {
            if (bEnableMovement)
            {
                float rotX = Input.GetAxis(Strings.mouseX) * mouseSensitivity;
                float rotY = Input.GetAxis(Strings.mouseY) * mouseSensitivity;

                transform.Rotate(rotY, rotX, 0);

                Vector3 move = Vector3.zero;
                float vertical = Input.GetAxis(Strings.vertical);
                float horizontal = Input.GetAxis(Strings.horizontal); 

                if (vertical > 0 || horizontal < 0 || horizontal > 0)
                {
                    move = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
                    view.PlayMoveSound();
                }
                else
                {
                    view.PlayNormalSound();
                }

                transform.Translate(move);

                //up/down
                if (Input.GetButton(Strings.up) && Input.GetKey(KeyCode.LeftControl))
                {
                    transform.Translate(Vector3.down * upDownSpeed);
                }
                else if (Input.GetButton(Strings.up))
                {
                    transform.Translate(Vector3.up * upDownSpeed);
                }
            } 
        }

        #endregion
    }
}