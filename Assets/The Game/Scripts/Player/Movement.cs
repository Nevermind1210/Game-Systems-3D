using System;
using System.Security.Cryptography;
using UnityEngine;
using Dialouge;
using Saving;
using TMPro;
using UnityEngine.UI;

namespace Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    public class Movement : MonoBehaviour
    {
        public static Movement _movement;

        [Header("Character")] 
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpSpeed;
        public int baseSpeed;
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cam;

        [Header("Speed Variables")]
        public int staminaMax;
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
            playerAnimator = GetComponent<Animator>();

            SetStamValues();
        }

        public void SetStamValues()
        {
            staminaMax = CostominsationGet.stamina;
            staminaSlider.maxValue = staminaMax;
            stamina = staminaMax;
            
        }

        void FixedUpdate()
        {
            isOnGround = GroundChecker();
        }

        private bool GroundChecker()
        {
            float DisstanceToTheGround = _Collider.bounds.extents.y;
            Debug.DrawRay(transform.position, Vector3.down * (DisstanceToTheGround + 0.2f));
            return Physics.Raycast(transform.position, Vector3.down, DisstanceToTheGround + 0.2f);

        }

        private void GameMovement()
        {
            
        }
    }
}