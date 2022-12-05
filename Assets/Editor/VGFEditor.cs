using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
public class VGFEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Tag("NPC");
        Tag("Player");
        Tag("Point");
        Tag("Area");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Tag(string tag){
         //检测是否有TAG "NPC"增强代码复用性
            if (!UnityEditorInternal.InternalEditorUtility.tags.Equals(tag))
                UnityEditorInternal.InternalEditorUtility.AddTag(tag);
            gameObject.tag = tag;

    }
}
