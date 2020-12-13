using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderPlatformer : MonoBehaviour {
    float height;
    float width;

    public Transform bottomLeft;
    public Transform topRight;

    public float moveSpeed;
    public float jumpInitVerSpeed;

    public float gravity;

    private float minObstacleSize = 0.25f;
    public LayerMask groundLayer;
    public float tileSize = 1.0f;
    public float EPS = (float) 1e-6;

    public NodePathFinderPlatformer pfNode;
    [SerializeField]
    public List<List<NodePathFinderPlatformer> > nodes = new List<List<NodePathFinderPlatformer>> ();

    void Start() {

    }

    void Update() {

    }

    public void Init(float width, float height, float moveSpeed, float jumpInitVerSpeed, float gravity) {
        this.width = width;
        this.height = height;
        this.moveSpeed = moveSpeed;
        this.jumpInitVerSpeed = jumpInitVerSpeed;
        this.gravity = gravity;

        GenerateGraph();
    }

    public void GetOptimalPath(Enemy enemy, Transform target) {
        int idx = xToCol(transform.position.x);
        NodePathFinderPlatformer dst = findClosest(target.position);
        NodePathFinderPlatformer src = findClosest(enemy.transform.position);
        if (dst == null || src == null) {
            enemy.BeIdle();
            return;
        }
        // Debug.Log(dst.transform.position);
        // Debug.Log(src.transform.position);

        // find path
        if (isOnSamePlatform(src, dst)) {
            // Debug.Log("is on same platform");
            if (target.position.x - enemy.transform.position.x > 0) {
                enemy.MoveRight();
            } else {
                enemy.MoveLeft();
            }
            return;
        }
        // NodePathFinderPlatformer jumpTarget = getEdge(dst);
        // Vector2 jumpTerm = new Vector2(jumpTarget.transform.position.x, jumpTarget.transform.position.y);
        // enemy.StartJumpThrough(jumpTerm + Vector2.up * 0.8f);

        enemy.StartJumpThrough(target.position + Vector3.up * 1.0f + Vector3.right * (-0.5f + Random.value) * 3f);

        // NodePathFinderPlatformer next = bfs(src, dst);
        // // Debug.Log("next");
        // // Debug.Log(next.transform.position);
        // if (next == null) {
        //     enemy.BeIdle();
        // } else if (next == src.jumpLeft) {
        //     enemy.Jump();
        //     enemy.MoveLeft();
        // } else if (next == src.jumpRight) {
        //     enemy.Jump();
        //     enemy.MoveRight();
        // } else if (next == src.left || next == src.fallLeft) {
        //     enemy.MoveLeft();
        // } else if (next == src.right || next == src.fallRight) {
        //     enemy.MoveRight();
        // }
    }

    public NodePathFinderPlatformer getEdge(NodePathFinderPlatformer node) {
        float r = Random.value;
        if (r < 0.3f) {
            // right
            while (node.right != null) node = node.right;
            return node;
        } else if (r < 0.3f) {
            // left
            while (node.left != null) node = node.left;
            return node;
        } else {
            if (node.left != null) return node.left;
            else if (node.right != null) return node.right;
            return node;
        }
    }

    public NodePathFinderPlatformer findClosest(Vector3 pos) {
        NodePathFinderPlatformer closest = null;
        float minDiff = 99999999f;
        float targetY = pos.y;
        int idx = xToCol(pos.x);
        // Debug.Log($"find closest {idx}");
        if (idx < 0 || idx > nodes.Count) return null;
        foreach (NodePathFinderPlatformer node in nodes[idx]) {
            // Debug.Log(node.transform.position.y);
            // if (node.transform.position.y <= pos.y) return node;
            float nodey = node.transform.position.y;
            if (nodey > pos.y) continue;
            if (Mathf.Abs(nodey - targetY) < minDiff) {
                closest = node;
                minDiff = Mathf.Abs(nodey - targetY);
            }
        }
        if (closest != null) {
            return closest;
        }
        return closest;
    }

    public bool isOnSamePlatform(NodePathFinderPlatformer a, NodePathFinderPlatformer b) {
        Queue<NodePathFinderPlatformer> q = new Queue<NodePathFinderPlatformer>();
        HashSet<NodePathFinderPlatformer> vis = new HashSet<NodePathFinderPlatformer>();
        q.Enqueue(a);
        while (q.Count > 0) {
            NodePathFinderPlatformer u = q.Peek();
            q.Dequeue();
            vis.Add(u);
            if (u == b) return true;
            if (u.left != null && !vis.Contains(u.left)) q.Enqueue(u.left);
            if (u.right != null && !vis.Contains(u.right)) q.Enqueue(u.right);
        }
        return false;
    }

    public NodePathFinderPlatformer bfs(NodePathFinderPlatformer node, NodePathFinderPlatformer target) {
        // return last node before reaching target
        if (node == target) {
            return target;
        }
        List<NodePathFinderPlatformer> level = new List<NodePathFinderPlatformer>();
        HashSet<NodePathFinderPlatformer> vis = new HashSet<NodePathFinderPlatformer>();
        Queue<NodePathFinderPlatformer> q = new Queue<NodePathFinderPlatformer>();
        q.Enqueue(node);
        vis.Add(node);
        while (q.Count > 0) {
            NodePathFinderPlatformer u = q.Peek();
            q.Dequeue();
            List<NodePathFinderPlatformer> adj = u.getAdjNodes();
            foreach (NodePathFinderPlatformer v in adj) {
                if (v == target) {
                    return u;
                }
                if (!vis.Contains(v)) {
                    vis.Add(v);
                    q.Enqueue(v);
                }
            }
        }
        return null;
    }

    public Vector2 GetTileAtPos(Vector2 v) {
        return Vector2.zero;
    }

    public bool IsGrounded(Vector2 pos){
        Vector2 direction = Vector2.down;
        float distance = height / 2.0f + 0.02f;
        RaycastHit2D hit = Physics2D.Raycast(pos, direction, distance, groundLayer);
        Debug.Log(pos);
        Debug.Log(hit.collider != null);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    public bool IsPosGround(Vector2 pos) {
        Vector3 dir = new Vector3(0, 0, 1);
        Vector3 ori = new Vector3(pos.x, pos.y, -10);
        
        RaycastHit2D hit = Physics2D.Raycast(ori, dir, 20, groundLayer);
        if (hit.collider != null) {
            return true;
        }
        return false;
    }

    public void GenerateGraph() {
        Debug.Log("generate graph");
        float xlo = bottomLeft.position.x + tileSize / 2f;
        float xhi = topRight.position.x;
        float ylo = bottomLeft.position.y;
        float yhi = topRight.position.y;

        Debug.Log($"{xlo}, {xhi}, {ylo}, {yhi}");

        bool isLastGround = false;
        for (float x = xlo; x < xhi; x += tileSize) {
            List<NodePathFinderPlatformer> curCol = new List<NodePathFinderPlatformer>();
            for (float y = ylo; y < yhi; y += minObstacleSize) {
                Vector2 pos = new Vector2(x, y);
                if (!IsPosGround(pos)) {
                    if (isLastGround) {
                        isLastGround = false;
                        float gndY = GetGroundY(x, y - minObstacleSize, y);
                        NodePathFinderPlatformer newNode = Instantiate(pfNode, transform);
                        newNode.transform.position = new Vector2(x, gndY);

                        if (nodes.Count > 0) {  // if not the first column
                            // check for neighbors on column to the left
                            // Debug.Log("check for neighbors");
                            List<NodePathFinderPlatformer> lastCol = nodes[nodes.Count - 1];
                            foreach (NodePathFinderPlatformer node in lastCol) {
                                float diffy = Mathf.Abs(node.transform.position.y - newNode.transform.position.y);
                                // Debug.Log(diffy);
                                if (diffy < tileSize * 0.8f) {
                                    newNode.left = node;
                                    node.right = newNode;
                                    break;
                                }
                            }
                        }
                        curCol.Add(newNode);
                    }
                } else {
                    isLastGround = true;
                }
            }
            nodes.Add(curCol);
        }

        // add fall edge
        for (int i = 0; i < nodes.Count; ++i) {
            foreach (NodePathFinderPlatformer node in nodes[i]) {
                AddFallNeighbors(node, i);
            }
        }

        // add jump edge
        // AddJumpNeighbors();
    }

    public void AddJumpNeighbors() {
        Debug.Log("add jump neighbors");
        for (int i = 0; i < nodes.Count; ++i) {
            foreach (NodePathFinderPlatformer node in nodes[i]) {
                float dt = tileSize / moveSpeed;
                int cnt = 0;
                float t = dt;
                float x0 = node.transform.position.x;
                float y0 = node.transform.position.y;
                float vy = jumpInitVerSpeed;
                float vx = moveSpeed;
                float y = y0;

                // Debug.Log($"{dt} {x0} {y0} {vy} {vx} {y}");


                while (y > bottomLeft.position.y - 100f) {
                    if (cnt + i >= nodes.Count && cnt - i < 0) break;
                    float lasty = y;
                    y = y0 + vy * t + 0.5f * gravity * t * t;
                    // Debug.Log($"{y - lasty} {y - y0}, t = {t}, {vx * t}, cnt = {cnt}, i = {i}");
                    if (y - lasty < 0) { // falling downwards
                        if (node.jumpRight == null && i + cnt < nodes.Count) {
                            // Debug.Log("jump right");
                            // jump right
                            float maxSmallerY = y;
                            NodePathFinderPlatformer foundRight = null;
                            foreach (NodePathFinderPlatformer leftNode in nodes[i + cnt]) {
                                float lefty = leftNode.transform.position.y;
                                // Debug.Log($"lefty = {lefty}");
                                // Debug.Log($"smallery = {maxSmallerY}, lasty = {lasty}, lefty = {lefty}");
                                if (maxSmallerY < lefty && lefty < lasty) {
                                    maxSmallerY = lefty;
                                    foundRight = leftNode;
                                }
                            }
                            node.jumpRight = foundRight;
                        }
                        // jump left (symmetric)
                        if (node.jumpLeft == null && i - cnt >= 0) {
                            // Debug.Log("jump left");
                            float maxSmallerY = y;
                            foreach (NodePathFinderPlatformer rightNode in nodes[i - cnt]) {
                                float righty = rightNode.transform.position.y;
                                // Debug.Log($"righty = {righty}");
                                // Debug.Log($"smallery = {maxSmallerY}, lasty = {lasty}, righty = {righty}");
                                if (maxSmallerY < righty && righty < lasty) {
                                    maxSmallerY = righty;
                                    node.jumpLeft = rightNode;
                                }
                            }
                        }
                    }
                    t += dt;
                    ++cnt;
                }
            }
        }
    }

    public void AddFallNeighbors(NodePathFinderPlatformer node, int col_idx) {
        // Debug.Log("add fall neighbors");
        if (col_idx > 0) {
            // left
            if (node.left == null) {
                float y = node.transform.position.y;
                float maxSmallerY = -9999999f;
                NodePathFinderPlatformer found = null;
                foreach (NodePathFinderPlatformer left in nodes[col_idx-1]) {
                    if (maxSmallerY < left.pos.y && left.pos.y < y) {
                        maxSmallerY = left.pos.y;
                        found = left;
                    }
                }
                node.fallLeft = found;
            }
        }

        if (col_idx < nodes.Count - 1) {
            // right
            if (node.right == null) {
                float y = node.transform.position.y;
                float maxSmallerY = -9999999f;
                NodePathFinderPlatformer found = null;
                foreach (NodePathFinderPlatformer right in nodes[col_idx+1]) {
                    if (maxSmallerY < right.pos.y && right.pos.y < y) {
                        maxSmallerY = right.pos.y;
                        found = right;
                    }
                }
                node.fallRight = found;
            }
        }
    }

    public float GetGroundY(float x, float ylo, float yhi) {
        // Debug.Log($"get ground y {ylo} {yhi}");
        // // top is air, bottom is ground
        // // binary search
        // while (yhi - ylo < EPS) {
        //     float m = (ylo + yhi) / 2f;
        //     if (IsPosGround(new Vector2(x, m))) {
        //         ylo = m;
        //     } else {
        //         yhi = m;
        //     }
        // }
        // Debug.Log($"res {ylo}");
        // return ylo;
        Vector2 ori = new Vector2(x, yhi);
        float distance = yhi - ylo;
        RaycastHit2D hit = Physics2D.Raycast(ori, Vector2.down, distance, groundLayer);
        return hit.point.y;
    }

    public int xToCol(float x) {
        return (int) Mathf.Floor((x - bottomLeft.position.x) / tileSize);
    }
}