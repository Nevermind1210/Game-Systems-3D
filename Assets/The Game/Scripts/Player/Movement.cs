using UnityEngine;
using Dialouge;
namespace Player
{
    [AddComponentMenu("RPG/Player/Movement")]
    [RequireComponent(typeof(CharacterController))]
    public class Movement : MonoBehaviour
    {
        [Header("Character")] [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cam;

        [Header("Variables")] 
        [SerializeField] private float gravity = -140f;
        [SerializeField] private float yVelocity;
        [SerializeField] private float turnSmoothTime = 0.1f; //how fast we turn
        [SerializeField] private float speed = 6f;
        [SerializeField] private float jumpSpeed = 40f;

        float turnSmoothVelocity; //the speed we are turning at curretntly

        void Update()
        {
            controller.Move((Movements() +
                             Jump()) * Time.deltaTime);

            Interact();
        }

        void Interact()
        {
            if (Input.GetKeyDown(KeyCode.E)) //keybinding code here
            {
                Ray ray;
                RaycastHit hitInfo;
                ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

                if (Physics.Raycast(ray, out hitInfo, 20f))
                {
                    Dialougue dialogue = hitInfo.collider.GetComponent<Dialougue>();
                    if (dialogue != null)
                    {
                        DialougeManager.theManager.LoadDialouge(dialogue);
                    }
                }
            }
        }


        private Vector3 Movements()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f) //dead zone 
            {
                float targetAngle =
                    Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +
                    cam.eulerAngles
                        .y; //This two blocks make character rotate to target angle-atan2 get acute angle , rad2deg converts to degrees then cam.eulerAngles.y attaches to camera
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime); //rotate smoothly(current angle, target to face, keep track of turnign velocity, 
                transform.rotation = Quaternion.Euler(0f, angle, 0f); //rotating character to directrion we want to move


                Vector3 moveDirection =
                    Quaternion.Euler(0f, targetAngle, 0f) *
                    Vector3.forward; //respect the angle of camera- respect to the camera

                return moveDirection * speed;
            }

            return Vector3.zero;
        }

        private Vector3 Jump()
        {
            if (controller.isGrounded)
            {
                yVelocity = -2;
            }

            if (Input.GetButtonDown("Jump") && controller.isGrounded)
            {
                yVelocity += jumpSpeed;
                //Mathf.Sqrt(jumpSpeed* -3.0f * gravity);
            }

            yVelocity += gravity * Time.deltaTime;

            if (yVelocity < -20)
            {
                yVelocity = -20;
            }


            return new Vector3(0f, yVelocity, 0f);

        }

    }
}