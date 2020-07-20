using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpacecraft : MonoBehaviour
{
    [Tooltip("Assign Spacecraft Prefab")]
    public GameObject spacecraft;

    [Tooltip("Crowd of Spacecraft")]
    public int numSpacecraft;

    // Start is called before the first frame update
    void Start()=>
        SpacecraftCreation();

    private void SpacecraftCreation()
    {
        for (int i = 0; i < numSpacecraft; i++)
            Instantiate(spacecraft, SpacecraftPosition(), Quaternion.identity, gameObject.transform);
    }    
    
    private Vector3 SpacecraftPosition() {
        Vector3 position = Vector3.zero;
        switch (Random.Range(1,7))
        {
            case 1: position = Vector3.right;
                break;
            case 2: position = Vector3.up;
                break;
            case 3: position = Vector3.forward;
                break;
            case 4: position = Vector3.left;
                break;
            case 5: position = Vector3.down;
                break;
            case 6: position = Vector3.back;
                break;
        }
        position *= Random.Range(5f, 7f);
        return position;
    }
}
