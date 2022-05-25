using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPositionX;
    [Space]
    [SerializeField] private TouchSlider touchSlider;

    private Cube mainCube;

    private bool isPointerDown;
    private Vector3 cubePosition;

    private void Start()
    {
        SpawnCube();

        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }

    private void Update()
    {
        if (isPointerDown && mainCube != null)
        {
            mainCube.transform.position = Vector3.Lerp(
                mainCube.transform.position,
                cubePosition,
                moveSpeed);
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
        mainCube?.ShowTrail();
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown && mainCube != null)
        {
            cubePosition = mainCube.transform.position;
            cubePosition.x = xMovement * cubeMaxPositionX;
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown)
            isPointerDown = false;

        mainCube?.HideTrail();

        if (mainCube != null)
        {
            mainCube.CubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);
            mainCube = null;

            Invoke("SpawnNewCube", 0.3f);
        }
    }

    private void SpawnNewCube()
    {
        SpawnCube();
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.IsMainCube = true;
        cubePosition = mainCube.transform.position;
    }

    private void OnDestroy()
    {
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
