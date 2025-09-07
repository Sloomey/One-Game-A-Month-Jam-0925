using System.Drawing;
using UnityEngine;

public class BaseCow : MonoBehaviour
{
    [Header("Base Cow Data")]
    [SerializeField] private string cowName;
    [SerializeField] private float size = 1.00f;

    private Vector3 CalculateSize(float size)
    {
        return new Vector3(size, size, size);
    }

    private void Start()
    {
        gameObject.transform.localScale = CalculateSize(size);
    }
}
