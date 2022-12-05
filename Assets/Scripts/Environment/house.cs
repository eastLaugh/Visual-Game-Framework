using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour
{
    private MeshRenderer meshren;
    // Start is called before the first frame update
    void Start()
    {
        meshren = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            meshren.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            meshren.enabled = true;
        }
    }
}
