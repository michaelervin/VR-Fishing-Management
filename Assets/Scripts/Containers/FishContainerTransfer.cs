using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishContainerTransfer : MonoBehaviour
{
    public FishContainer from;
    public FishContainer to;

    public void Transfer()
    {
        while (from.objects.Count > 0)
        {
            Fish f = from.objects[0];
            from.Remove(f);
            f.transform.position = to.transform.position;
        }
    }
}
