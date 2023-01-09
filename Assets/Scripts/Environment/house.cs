using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Inventory;

public class house : MonoBehaviour
{
    [SerializeField] private MeshCollider coll1;
    [SerializeField] private BoxCollider coll2;
    [SerializeField] private MeshRenderer meshren;
    private bool approach=false;
    
    // Start is called before the first frame update
    void Start()
    {
        meshren = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if(approach)
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (InventoryManager.Instance.SearchItem(1001, 1))
                { 
                    HintLoader.Instance.HintWithSeconds("Unlocked", 1); 
                    coll1.enabled = false;
                    coll2.enabled = false;
                }
                else HintLoader.Instance.HintWithSeconds("No Key", 1);
            }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            //meshren.enabled = false;
            approach = true;
            HintLoader.Instance.HintOn("E:Unlock");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            approach = false;
            HintLoader.Instance.HintOff();
            //meshren.enabled = true;
        }
    }
}
