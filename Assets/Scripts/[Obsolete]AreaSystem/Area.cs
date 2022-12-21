using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VGF.AreaNameSpace
{

    [System.Obsolete]
    [ExecuteAlways]
    public class Area : MonoBehaviour
    {
        // Start is called before the first frame update
        public List<Vector3> positions;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            positions = new List<Vector3>();
            for (int i = 0; i < transform.childCount; i++)
            {
                positions.Add(transform.GetChild(i).position);
            }
        }


        public bool ContainXZ(Vector3 position)
        {
            if(position==null)
                return true;

            switch (positions.Count)
            {
                case 2:
                    if (position.x.Between(positions[0].x, positions[1].x))
                        if (position.z.Between(positions[0].z, positions[1].z))
                            return true;
                    break;
            }
            return false;
        }


    }

}