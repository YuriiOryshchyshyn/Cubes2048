using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static int staticCubeID;

    [SerializeField] private TMP_Text[] TextMeshProText;
    [SerializeField] private GameObject cubeTrail;

    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public GameObject Trail;
    [HideInInspector] public Rigidbody CubeRigidbody;
    [HideInInspector] public bool IsMainCube;
    [HideInInspector] public int CubeID;

    private MeshRenderer cubeMeshRenderer;

    private void Awake()
    {
        cubeMeshRenderer = GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
        Trail = cubeTrail;
        CubeID = staticCubeID++;
    }

    public void SetColor(Color color)
    {
        CubeColor = color;
        cubeMeshRenderer.material.color = color;
    }

    public void SetNumber(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            TextMeshProText[i].text = number.ToString();
        }
    }

    public void ShowTrail()
    {
        Trail.SetActive(true);
    }

    public void HideTrail()
    {
        Trail.SetActive(false);
    }

    public void SizeEffectCollision()
    {
        StartCoroutine(CoroutineSizeEffect());
    }

    private IEnumerator CoroutineSizeEffect()
    {
        Vector3 normalCubeScale = transform.localScale;

        transform.localScale = new Vector3(
            transform.localScale.x + 0.05f,
            transform.localScale.y + 0.05f,
            transform.localScale.z + 0.05f);

        yield return new WaitForSeconds(0.4f);

        transform.localScale = normalCubeScale;
    }
}
