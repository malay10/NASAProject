using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public Canvas playerHud;
    public Canvas pauseMenu;
    public MouseLook cameraY; //Need this to disable it when the pause menu gets pushed

    private CharacterController _charController;
    private PlayerMovement playerMovement;
    private MouseLook playerLook;
    private

    // Start is called before the first frame update
    void Start()
    {
        playerLook = GetComponent<MouseLook>();
        playerMovement = GetComponent<PlayerMovement>();
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

        if (Input.GetButtonDown("Pause"))
        {
            cameraY.enabled = false;
            playerHud.enabled = false;
            pauseMenu.gameObject.SetActive(true);
            playerMovement.speed = 0;
            playerLook.enabled = false;
            GameInfo.GameIsPaused = true;
        }
        else if (Input.GetButtonDown("Submit") && pauseMenu.enabled)
        {
            cameraY.enabled = true;
            playerHud.enabled = true;
            pauseMenu.gameObject.SetActive(false);
            playerMovement.speed = 8;
            playerLook.enabled = true;
            GameInfo.GameIsPaused = false;
        }
        else if(Input.GetButtonDown("Cancel") && pauseMenu.enabled)
        {
            Application.Quit();
        }
    }
}
