using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputManager
{
    public LaserPointer LaserPointer
    {
        get
        {
            if(_laserPointer == null)
            {
                GameObject go = GameObject.Find("LaserPointer");
                
                if (go == null)
                    return null;

                _laserPointer = go.GetComponent<LaserPointer>();
            }

            return _laserPointer;
        }
    }

    private LaserPointer _laserPointer;
}
