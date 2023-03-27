using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource switchSound;
    public AudioSource downmusic;
    public Gradient gradient;
    public Gradient lightGradient;
    public GameObject global_lights;
    public GameObject waterCamera;
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

    private ParticlesManager _particleManager;
    private bool flipped = false;
    private Vector3 waterStartingPos;
    private void Start()
    {
        _particleManager = FindObjectOfType<ParticlesManager>();
        var waterMat = Water.GetComponentInChildren<Renderer>().material;
        waterMat.color = gradient.Evaluate(0f);
        waterStartingPos = Water.GetComponentInChildren<Renderer>().transform.position;
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0f && !isRotating) {
            var move = Vector3.right * horizontalInput * moveSpeed * Time.deltaTime * inversion;
            var wtransform = Water.GetComponentInChildren<Renderer>().gameObject.transform;
            if(transform.position.x > minx && horizontalInput * inversion < 0) {
                transform.position += move;
            }
            if(transform.position.x < maxx && horizontalInput * inversion > 0) {
                transform.position += move;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating) {
            originalEulerAngles = transform.eulerAngles;
            originalEulerAnglesWater = Water.transform.eulerAngles;
            originalPosition = transform.position;
            isRotating = true;
            elapsedTime = 0f;
            if (inversion != 1)
            {
                _particleManager.switchToUp();
                downmusic.Pause();
            }
            else
            {
                _particleManager.switchToDown();
                music.Pause();

            }
            switchSound.Play();
        }

        if (!isRotating)
            return;

        var waterMat = Water.GetComponentInChildren<Renderer>().material;
        if (inversion == 1)
        {
            waterMat.SetColor("_Color", gradient.Evaluate(elapsedTime / rotationSpeed));
            //waterMat.SetFloat("_Alpha", (1 - elapsedTime / rotationSpeed) / 2 + 0.4f);

            global_lights.GetComponent<Light2D>().color = lightGradient.Evaluate(elapsedTime / rotationSpeed);
        } else
        {
            waterMat.SetColor("_Color", gradient.Evaluate(1 - elapsedTime / rotationSpeed));
            //waterMat.SetFloat("_Alpha",  (elapsedTime / rotationSpeed) / 2 + 0.4f);

            global_lights.GetComponent<Light2D>().color = lightGradient.Evaluate(1 - elapsedTime / rotationSpeed); 
        }

        elapsedTime += Time.deltaTime;
        var rotationZ = rotationCurveZ.Evaluate(elapsedTime / rotationSpeed) * 180f;
        var positionY = movementCurveY.Evaluate(elapsedTime / rotationSpeed) * -5f * inversion;
        transform.eulerAngles = originalEulerAngles + new Vector3(0, 0, rotationZ);
        waterCamera.transform.eulerAngles = originalEulerAngles + new Vector3(0, 0, rotationZ);
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

        if (inversion == 1)
        {
            Water.GetComponentInChildren<Renderer>().gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            Water.GetComponentInChildren<Renderer>().gameObject.transform.position = waterStartingPos;
            waterMat.SetColor("_Color", gradient.Evaluate(0f));
            global_lights.GetComponent<Light2D>().color = lightGradient.Evaluate(0f);
            music.Play();
        }
        else
        {
            waterMat.SetColor("_Color", gradient.Evaluate(1f));
            global_lights.GetComponent<Light2D>().color = lightGradient.Evaluate(1f);
            downmusic.Play();
        }
    }
}
