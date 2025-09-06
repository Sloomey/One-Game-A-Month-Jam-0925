using Pathfinding;
using UnityEngine;

/* 64::00 | 2025-09-06 15:57:34
---
Script used in bovine kind for movement, pathfinding, etc.
-
manTa said they had imported A* Pathfinding Project package. I'm not sure how I can use it in the code,
since Pathfinding is a generic name. I was thinking "perhaps Pathfinding may be builtin in unity".

The bovine should generate a new path when it finishes walking, with a break between runs.
*/

public class Bovine : MonoBehaviour
{
    RandomPath randomPath;

    void Start()
    {
        randomPath = RandomPath.Construct(transform.position, 500);
        Debug.Log(randomPath.searchLength);
    }


    void Update()
    {
        transform.position += Walk();
    }

    private Vector3 Walk()
    {
        return new Vector3(1.0f, 0.0f) * Time.deltaTime;
    }
}
