using UnityEngine;

public class PublicReadonlyAttribute : PropertyAttribute
{
    public PublicReadonlyAttribute()
    {
        Debug.Log("Attribute setted");
    }
}
