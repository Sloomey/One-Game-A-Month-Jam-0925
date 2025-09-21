using System;
using System.Drawing;
using UnityEngine;

public class BaseCow : MonoBehaviour
{
    public enum Gender { Male, Female }

    [Header("Base Cow Data")]
    public string cowName;
    public Gender cowGender;
    public float widthModifier = 1.00f;
    public float heightModifier = 1.00f;
    public float lengthModifier = 1.00f;
    public float speedModifier = 1.00f;
    [SerializeField][Range(0f, 1.0f)] public float maintenanceLevel = 0.5f;
    public float value = 100.00f;


    private Vector3 CalculateSize(float widthMul, float heightMul, float lengthMul)
    {
        return new Vector3(widthMul, heightMul, lengthMul);
    }

    private void Start()
    {
        gameObject.transform.localScale = CalculateSize(widthModifier, heightModifier, lengthModifier);
    }
}
