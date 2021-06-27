using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public GameData gameData;

    public NodeScript[] nearNodes;
    public List<NodeScript> activeNodesInReach;
    public GameObject lineRenderer;

    public List<LineRenderer> renderers;

    public bool isActive = true;
    public bool isOccupied;

    //public Color lineToActiveNode;
    //public Color lineToInactiveNode;
    //public Color lineWithinReach;

    //LineRenderer lineRenderer;

    public float gatherRadius;

    private void OnValidate()
    {
        Collider2D[] nodes = Physics2D.OverlapCircleAll(transform.position, gatherRadius);
        nearNodes = new NodeScript[nodes.Length];

        if (nearNodes != null)
        {
            for (int i = 0; i < nearNodes.Length; i++)
            {
                nearNodes[i] = nodes[i].GetComponent<NodeScript>();
            }
        }

        CheckNearNodes();
    }
    private void Awake()
    {
        Collider2D[] nodes = Physics2D.OverlapCircleAll(transform.position, gatherRadius);
        nearNodes = new NodeScript[nodes.Length];

        if (nearNodes != null)
        {
            for (int i = 0; i < nearNodes.Length; i++)
            {
                nearNodes[i] = nodes[i].GetComponent<NodeScript>();
            }
        }

        CheckNearNodes();
        CreateRenderers();
    }

    private void OnEnable()
    {
        //DrawLines();
    }

    public void ShowPaths()
    {
        if (renderers != null)
        {
            DrawLines();
            foreach (LineRenderer renderer in renderers)
            {
                renderer.enabled = true;
            }
        }
    }

    public void HidePaths()
    {
        if (renderers != null)
        {
            foreach (LineRenderer renderer in renderers)
            {
                renderer.enabled = false;
            }
        }

    }

    public void CheckNearNodes()
    {
        if (nearNodes != null)
        {
            activeNodesInReach.Clear(); 
            foreach (NodeScript node in nearNodes)
            {
                if (node.isActive)
                {
                    if (node != this)
                    {
                        activeNodesInReach.Add(node);
                    }
                }
            }
        }
    }

    void CreateRenderers()
    {
        if (nearNodes != null)
        {
            foreach (NodeScript node in nearNodes)
            {
                renderers.Add(Instantiate(lineRenderer, gameObject.transform, false).GetComponent<LineRenderer>());
            }
        }
    }

    void SetLineColors(LineRenderer renderer, Color color)
    {
        renderer.startColor = color;
        renderer.endColor = color;
    }

    public void DrawLines()
    {
        if (renderers != null)
        {
            if (!isActive)
            {
                foreach (LineRenderer renderer in renderers)
                {
                    SetLineColors(renderer, gameData.inactiveNodeColor);
                }
            }
            else
            {
                if (!isOccupied)
                {
                    for (int i = 0; i < renderers.Count; i++)
                    {
                        if (!nearNodes[i].isActive)
                        {
                            SetLineColors(renderers[i], gameData.inactiveNodeColor);
                        }
                        else
                        {
                            if (!nearNodes[i].isOccupied)
                            {
                                SetLineColors(renderers[i], gameData.activeNodeColor);
                            }
                            else
                            {
                                SetLineColors(renderers[i], gameData.reachableNodeColor);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < renderers.Count; i++)
                    {
                        if (!nearNodes[i].isActive)
                        {
                            SetLineColors(renderers[i], gameData.inactiveNodeColor);
                        }
                        else
                        {
                            SetLineColors(renderers[i], gameData.reachableNodeColor);
                        }
                    }
                }
            }

            for (int i = 0; i < renderers.Count; i++)
            {
                renderers[i].SetPosition(0, transform.position);
                renderers[i].SetPosition(1, nearNodes[i].gameObject.transform.position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (gatherRadius > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, gatherRadius);
        }
    }

    private void OnDrawGizmos()
    {
        if (nearNodes != null)
        {
            Gizmos.color = Color.white;
            foreach (NodeScript node in nearNodes)
            {
                Gizmos.DrawLine(transform.position, node.gameObject.transform.position);
            }
        }
    }
}
