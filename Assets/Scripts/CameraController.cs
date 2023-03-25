using UnityEngine;

public class CameraController : MonoBehaviour
{
    public AnimationCurve rotationCurveZ;
    public AnimationCurve movementCurveY;
    public float rotationSpeed = 1f;
    public float moveSpeed = 50f;

    private bool isRotating = false;
    private Vector3 originalEulerAngles;
    private Vector3 originalPosition;
    private float elapsedTime = 0f;
    private float inversion = 1f;

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0f && !isRotating) {
            transform.position += Vector3.right * horizontalInput * moveSpeed * Time.deltaTime * inversion;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating) {
            originalEulerAngles = transform.eulerAngles;
            originalPosition = transform.position;
            isRotating = true;
            elapsedTime = 0f;
        }

        if (!isRotating) 
            return;

        elapsedTime += Time.deltaTime;
        var rotationZ = rotationCurveZ.Evaluate(elapsedTime / rotationSpeed) * 180f;
        var positionY = movementCurveY.Evaluate(elapsedTime / rotationSpeed) * -5f * inversion;
        transform.eulerAngles = originalEulerAngles + new Vector3(0, 0, rotationZ);
        transform.position = originalPosition +  new Vector3(0, positionY, 0);

        if (!(elapsedTime >= rotationSpeed))
            return;

        isRotating = false;
        inversion *= -1f;
    }
}
