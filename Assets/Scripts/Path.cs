using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] mPoints;

    public Transform Begin => mPoints[0];

    // returns a position to walk toward, given the current position
    public Transform getTargetPoint(Transform aCurrentPosition)
    {
        return null;
    }


}
