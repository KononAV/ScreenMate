using System.Reflection.Metadata;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    private float startPosX;
    private float endPosX;
    private float added = 2f;

    void Start()
    {
        startPosX = GetComponent<Transform>().position.x;
        endPosX = -GetComponent<Transform>().position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= startPosX)
        {
            added = Mathf.Abs(added);

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else if (transform.position.x >= endPosX)
        {
            added = -Mathf.Abs(added);

            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        transform.position += Vector3.right * added * Time.deltaTime;
    }
}
