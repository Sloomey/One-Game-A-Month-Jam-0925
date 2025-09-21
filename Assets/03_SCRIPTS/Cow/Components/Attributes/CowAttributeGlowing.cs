using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CowAttributeGlowing : MonoBehaviour
{
    private BaseCow baseCowReference;

    public static float albinoValueModifier = 10.00f;

    void Start()
    {
        Light glowLight = gameObject.AddComponent<Light>();
        glowLight.intensity = 4.0f;

        baseCowReference = GetComponent<BaseCow>();

        baseCowReference.value *= albinoValueModifier;
    }
}
