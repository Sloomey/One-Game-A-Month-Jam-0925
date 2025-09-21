using UnityEngine;

public class CowAttributeGiraffe : MonoBehaviour
{
    private BaseCow baseCowReference;

    public float modifierScale = 1.00f;

    void Start()
    {
        baseCowReference = GetComponent<BaseCow>();

        baseCowReference.heightModifier *= modifierScale;
    }
}
