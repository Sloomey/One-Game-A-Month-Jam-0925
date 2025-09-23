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
        
        /* 64::00 | 2025-09-23 11:06:50 UTC-3
        ---
        Logic: they start idle, THEN they got destination.
        If the cow has reached this destination, it "sleeps" for 4-10 seconds and finds another destination.
        Furthermore IF the couldn't reach this destination in 15-20 secs, it is forced to "sleep" for more 10~20 secs.
        This is an example of how the script that I'll implement would work. Not sure if this is the best approach.
        Seems broken ngl
        */


        moveTimer += Time.deltaTime;

        if (moveTimer >= 10.0f)
        {
            Debug.Log(NewDestination(navMeshSurface));
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
