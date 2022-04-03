using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static void SortArrayByObjectNameEnding(GameObject[] objects)
    {
        GameObject[] temp = new GameObject[objects.Length];
        System.Array.Copy(objects, temp, objects.Length);
        for (int i = 0; i < temp.Length; i++)
        {
            string index = temp[i].name.Substring(temp[i].name.IndexOf("_") + 1);
            objects[int.Parse(index)] = temp[i];
        }
    }

    public static GameObject FindGameObjectWithComponentByNameInChildren<T>(GameObject parent, string childrenName)
    {
        Component[] components = parent.GetComponentsInChildren(typeof(T));
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i].gameObject.name.Equals(childrenName))
            {
                return components[i].gameObject;
            }
        }
        return null;
    }
}
