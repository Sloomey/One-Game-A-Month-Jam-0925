using Unity.AI.Navigation;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;

//@todo Remove Debug.Log 

public class BaseCow : MonoBehaviour
{
    public enum Gender { Male = 0, Female }

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

    // Pathfinding

    [SerializeField] public NavMeshAgent navMeshAgent;
    [SerializeField] public NavMeshSurface navMeshSurface;

    private void Update()
    {
        // 64 - Temporatio
        Move();
    }
    private float moveTimer = 0.0f;
    public bool isWalking = false;
    private void Move()
    {
        // 64 - Cows must wander in the area.
        moveTimer += Time.deltaTime;
        if (moveTimer >= 10.0f | isWalking)
        {
            Debug.Log(NewDestination(navMeshSurface));
            isWalking = true;
            Debug.Log("TICK! TESTING WALKING");
            Debug.Log("isWalking == " + isWalking);
            moveTimer = 0.0f;
        }
    }

    private Vector3 NewDestination(NavMeshSurface surface)
    {
        // Take a point from NavMeshSurface bounds and use it as a base

        // 64 - I hate use `if` in loops to be honest

        if (!surface)
        {
            Debug.LogWarning("surface wasn't allocate for some reason");
            return new Vector3();
        }

        Bounds bounds = surface.navMeshData.sourceBounds;
        Vector3 randomized = new Vector3(Random.Range(bounds.min.x, bounds.max.x), bounds.center.y, Random.Range(bounds.min.z, bounds.max.z));
        return randomized;
    }
}
