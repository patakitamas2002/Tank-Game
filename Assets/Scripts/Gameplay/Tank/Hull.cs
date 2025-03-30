using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour
{
    public HullStats stats;

    public Track[] tracks;

    public bool isGrounded
    {
        get
        {
            foreach (Track track in tracks)
            {
                if (track.isGrounded) return true;
            }
            return false;
        }
    }
}
