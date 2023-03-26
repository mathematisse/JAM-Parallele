using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Gradient gradient;
    public AnimationCurve rotationCurveZ;
    public AnimationCurve movementCurveY;
    public GameObject Water;
    public float rotationSpeed = 1f;
    public float moveSpeed = 50f;

    public float minx = -72;
    public float maxx = 63;
    
    private bool isRotating = false;
    private Vector3 originalEulerAngles;
    private Vector3 originalEulerAnglesWater;
    private Vector3 originalPosition;
    private float elapsedTime = 0f;
    private float inversion = 1f;

    
    private bool flipped = false;

    private void Start()
    {
        var waterMat = Water.GetComponentInChildren<Renderer>().material;
        waterMat.color = gradient.Evaluate(0f);
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0f && !isRotating) {
            var move = Vector3.right * horizontalInput * moveSpeed * Time.deltaTime * inversion;
            var wtransform = Water.GetComponentInChildren<Renderer>().gameObject.transform;
            if(transform.position.x > minx && horizontalInput * inversion < 0) {
                transform.position += move;
                wtransform.position += move;
            }
            if(transform.position.x < maxx && horizontalInput * inversion > 0) {
                transform.position += move;
                wtransform.position += move;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating) {
            originalEulerAngles = transform.eulerAngles;
            originalEulerAnglesWater = Water.transform.eulerAngles;
            originalPosition = transform.position;
            isRotating = true;
            elapsedTime = 0f;
        }

        if (!isRotating)
            return;

        var waterMat = Water.GetComponentInChildren<Renderer>().material;
        if (inversion == 1)
        {
            waterMat.SetColor("_Color", gradient.Evaluate(elapsedTime / rotationSpeed));
            waterMat.SetFloat("_Alpha",  (elapsedTime / rotationSpeed) / 2 + 0.4f);
        }
        else
        {
            waterMat.SetColor("_Color", gradient.Evaluate(1 - elapsedTime / rotationSpeed));
            waterMat.SetFloat("_Alpha", (1 - elapsedTime / rotationSpeed) / 2 + 0.4f);
        }

        elapsedTime += Time.deltaTime;
        var rotationZ = rotationCurveZ.Evaluate(elapsedTime / rotationSpeed) * 180f;
        var positionY = movementCurveY.Evaluate(elapsedTime / rotationSpeed) * -5f * inversion;
        transform.eulerAngles = originalEulerAngles + new Vector3(0, 0, rotationZ);
        transform.position = originalPosition +  new Vector3(0, positionY, 0);
        Water.transform.eulerAngles = originalEulerAnglesWater + new Vector3(rotationZ, 0, 0);

        if (!(elapsedTime >= rotationSpeed / 2) && !flipped)
        {
            Water.GetComponentInChildren<Renderer>().gameObject.transform.eulerAngles += new Vector3(0, 180, 0);
            flipped = !flipped;
        }

        if (!(elapsedTime >= rotationSpeed))
            return;

        isRotating = false;
        inversion *= -1f;
        flipped = false;
    }
}
