using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPosition : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float vectorPosition;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position = new Vector3(playerTransform.transform.position.x, vectorPosition, playerTransform.transform.position.z);
        gameObject.transform.rotation = Quaternion.identity;
    }
}
