using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public Vector3 inputDirection = Vector3.zero;
    // Start is called before the first frame update
    public playerMovement movementController;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
        movementController.SetDirection(inputDirection);
    }
}
