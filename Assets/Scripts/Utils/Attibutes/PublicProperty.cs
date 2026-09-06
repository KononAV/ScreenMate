using UnityEngine;

public class PublicPropertyAttribute : PropertyAttribute
{
    public PublicPropertyAttribute()
    {
        Debug.Log("Attribute setted");
    }
}
