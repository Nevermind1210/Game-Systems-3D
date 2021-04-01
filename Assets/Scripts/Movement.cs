using UnityEngine;
namespace Debugging.Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Speed Vars")]
        public float moveSpeed;
        public float walkSpeed, runSpeed, crouchSpeed, jumpHeight;
        private float _gravity = 20.0f;
        private Vector3 _moveDir;
        CharacterController _charC;
        private Animator characterAnim;
        private void Start()
        {
            _charC = GetComponent<CharacterController>();
            characterAnim = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            Move();
        }
        private void Move()
        {
            Vector2 ctrlVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

            characterAnim.SetBool("moving", ctrlVector.magnitude >= .05f);
            if (_charC.isGrounded)
            {
                if (Input.GetButton("Sprint"))
                {
                    moveSpeed = runSpeed;
                }
                else if (Input.GetButton("Crouch"))
                {
                    moveSpeed = crouchSpeed;
                }
                else
                {
                    moveSpeed = walkSpeed;
                    characterAnim.SetFloat("speed", 1);
                }
                _moveDir = transform.TransformDirection(new Vector3(ctrlVector.x, 0, ctrlVector.y)); 
                if (Input.GetButton("Jump"))
                {
                    _moveDir.y = jumpHeight;
                }
            }
            _moveDir.y -= _gravity * Time.deltaTime;
            _charC.Move(_moveDir * Time.deltaTime);
        }
    }
}
