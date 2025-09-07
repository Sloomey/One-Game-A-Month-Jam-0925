using UnityEngine;

public class BaseCow : MonoBehaviour
{
    public BaseCowData baseData;

    private Vector3 CalculateSize(float size)
    {
        return new Vector3(size, size, size);
    }

    private void Start()
    {
        gameObject.transform.localScale = CalculateSize(baseData.size);
    }
}
