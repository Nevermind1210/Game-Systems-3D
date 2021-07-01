using System;
using UnityEngine;
using Dialouge;
using KeyBindings;
using Saving;
using TMPro;
using UnityEngine.UI;

namespace Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    public class Movement : MonoBehaviour
    {
        public static Movement _movement;

        [Header("Character")] public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        public int baseSpeed;
       // [SerializeField] private Transform cam;

        [Header("Speed Variables")] public int staminaMax;
        public float stamina;
        [SerializeField] private Slider staminaSlider;
        [SerializeField] private TextMeshProUGUI stamText;

        public Rigidbody rb;
        private Animator playerAnimator;

        private Collider _Collider;
        private bool isOnGround;

        private void Awake()
        {
            _Collider = GetComponent<Collider>();
            if (_movement == null)
            {
                _movement = this;
            }
            else if (_movement != null)
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            playerAnimator = GetComponentInChildren<Animator>();

            SetStamValues();
        }

        public void SetStamValues()
        {
            staminaMax = CustomisationGet.stamina;
            staminaSlider.maxValue = staminaMax;
            stamina = staminaMax;
            baseSpeed = CustomisationGet.speed / 2;
        }

        void FixedUpdate()
        {
            isOnGround = GroundChecker();
        }

        private void Update()
        {
            GameMovement();
            LevelUp();
            
            #region Stamina Bar update and regen
            // Updates stamina slider and value text
            staminaSlider.value = stamina;
            stamText.text = "Stamina: " + Mathf.RoundToInt(stamina) + "/" + staminaMax;

            if (stamina < staminaMax && !Input.GetButton("Sprint"))
            {
                // Regenerate stamina if not using it
                stamina += Time.deltaTime;
            }

            #endregion
        }

        private bool GroundChecker()
        {
            float DisstanceToTheGround = _Collider.bounds.extents.y;
            Debug.DrawRay(transform.position, Vector3.down * (DisstanceToTheGround + 0.2f));
            return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.2f);
        }

        private void GameMovement()
        {
            playerAnimator.SetBool("moving", false);

            Vector3 gravity = Physics.gravity * 4f;
            if (isOnGround)
            {
                gravity = Vector3.down * 2f;
            }
            
            if (BindingManager.BindingHeld("Forward"))
            {
                rb.AddForce(transform.forward * moveSpeed + gravity);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                playerAnimator.SetBool("moving", true);
            }

            if (BindingManager.BindingHeld("Right"))
            {
                rb.AddForce(transform.right * moveSpeed + gravity);
                transform.position += transform.right * moveSpeed * Time.deltaTime;
                playerAnimator.SetBool("moving", true);
            }


            if (BindingManager.BindingHeld("Backward"))
            {
                rb.AddForce(-transform.forward * moveSpeed + gravity);
                transform.position -= transform.forward * moveSpeed * Time.deltaTime;
                playerAnimator.SetBool("moving", true);

            }

            if (BindingManager.BindingHeld("Left"))
            {
                rb.AddForce(-transform.right * moveSpeed + gravity);
                transform.position -= transform.right * moveSpeed * Time.deltaTime;
                playerAnimator.SetBool("moving", true);
            }


            // Controls speeds and animations for Sprint/Crouch/Base and Jump.
            if (isOnGround)
            {
                if (BindingManager.BindingHeld("Run") && stamina > 0)
                {
                    moveSpeed = runSpeed * baseSpeed;
                    stamina -= Time.deltaTime;
                    playerAnimator.SetFloat("speed", 2);
                }
                else if (BindingManager.BindingHeld("Crouch"))
                {
                    moveSpeed = crouchSpeed * baseSpeed;
                    playerAnimator.SetFloat("speed", 0.5f);
                }
                else
                {
                    moveSpeed = walkSpeed * baseSpeed;
                    playerAnimator.SetFloat("speed", 1);
                }

                // Still not working correctly
                if (BindingManager.BindingsPressed("Jump"))
                {
                    rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Acceleration);
                }
            }
            else
            {
                moveSpeed = walkSpeed * baseSpeed;
                playerAnimator.SetFloat("speed", 1);
                rb.AddForce(gravity, ForceMode.Acceleration);
            }
        }

        public void LevelUp()
        {
            if (Input.GetButtonDown("LevelUp"))
            {
                staminaMax += Mathf.RoundToInt(staminaMax * 0.3f);

                staminaSlider.maxValue = staminaMax;
            }
        }
    }
}