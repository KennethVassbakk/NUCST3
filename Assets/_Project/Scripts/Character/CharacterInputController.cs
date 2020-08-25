using UnityEngine;

public class CharacterInputController : MonoBehaviour
{
    [SerializeField] public Vector3 inputVector;

    [SerializeField] public Vector2 mousePosition;

    private void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");

        mousePosition = Input.mousePosition;
    }
}