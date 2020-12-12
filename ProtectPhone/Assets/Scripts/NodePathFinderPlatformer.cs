using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePathFinderPlatformer : MonoBehaviour
{
    // 0 = left, 1 = right
    public Vector2 pos;
    public NodePathFinderPlatformer left;
    public NodePathFinderPlatformer right;
    public NodePathFinderPlatformer fallLeft;
    public NodePathFinderPlatformer fallRight;
    public NodePathFinderPlatformer jumpLeft;
    public NodePathFinderPlatformer jumpRight;

    public List<NodePathFinderPlatformer> getAdjNodes () {
        List<NodePathFinderPlatformer> ret = new List<NodePathFinderPlatformer>();
        if (jumpLeft != null) ret.Add(jumpLeft);
        if (jumpRight != null) ret.Add(jumpRight);
        if (left != null) ret.Add(left);
        if (right != null) ret.Add(right);
        if (fallLeft != null) ret.Add(fallLeft);
        if (fallRight != null) ret.Add(fallRight);
        return ret;
    }
}
