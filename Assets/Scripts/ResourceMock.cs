using UnityEngine;
using System.Collections;

public static class ResourceMock
{
    public static Sprite GetSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}
