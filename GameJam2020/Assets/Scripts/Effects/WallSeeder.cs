using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSeeder : MonoBehaviour
{
    private Object[] objects;
    private GameObject[] gObjects;

    // Start is called before the first frame update
    void Start()
    {
        gObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gObjects.Length; i++)
        {
            if(gObjects[i].layer == 8)
            {
                MeshRenderer renderer = gObjects[i].GetComponent<MeshRenderer>();
                if (renderer)
                renderer.material.SetFloat("_Seed", i + 1);
            }
        }
    }
}
