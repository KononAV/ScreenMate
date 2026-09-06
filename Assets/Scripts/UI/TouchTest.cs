using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //GetComponent<Collider2D>();
    }

    void OnMouseEnter()
    {
        Debug.Log("ENTERED");
    }

    void OnMouseDown()
    {
        Debug.Log("CLICK");
    }
}
