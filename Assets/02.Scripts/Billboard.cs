using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform camTr;

    void Start()
    {
        camTr = Camera.main.transform;    
    }

    void LateUpdate()
    {
        transform.LookAt(camTr.position);
    }
}
