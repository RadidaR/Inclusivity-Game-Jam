using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public GameData gameData;
    public float playerRadius;
    public Camera mainCam;
    public NodeScript currentNode;
    NodeScript highLightedNode;

    PlayerInput playerInput;

    [SerializeField] bool isMoving;

    public GameEvent eMoved;
    public GameEvent eShowPaths;
    public GameEvent eHidePaths;

    public GameObject highLighter;
    public LineRenderer lineRenderer;

    private void Awake()
    {
        playerInput = new PlayerInput();

        playerInput.Game.MouseClick.performed += ctx => Clicked();
        playerInput.Game.Space.performed += ctx => eShowPaths.Raise();
        playerInput.Game.Space.canceled += ctx => eHidePaths.Raise();
    }

    private void Update()
    {
        //Collider2D[] nodes = Physics2D.OverlapCircleAll(transform.position, playerRadius);
        Vector2 mousePosition = mainCam.ScreenToWorldPoint(playerInput.Game.MousePosition.ReadValue<Vector2>());

        Collider2D node = Physics2D.OverlapCircle(transform.position, playerRadius);

        //Debug.Log(node.gameObject.name);

        if (node != null && node.gameObject.GetComponent<NodeScript>() != null)
        {
            //Debug.Log(node.gameObject.name);
            currentNode = node.gameObject.GetComponent<NodeScript>();
            currentNode.isOccupied = true;
            currentNode.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            currentNode.ShowPaths();
        }
        else
        {
            if (currentNode != null)
            {
                currentNode.isOccupied = false;
                currentNode.isActive = false;
                currentNode.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                currentNode.HidePaths();
                currentNode = null;
            }
        }

        Collider2D hoverOver = Physics2D.OverlapCircle(mousePosition, 0.65f);

        if (hoverOver != null)
        {
            NodeScript hoverOverNode = hoverOver.gameObject.GetComponent<NodeScript>();
            if (hoverOverNode != null)
            {
                HighLightNode(hoverOverNode);
            }

        }
        else
        {
            if (highLightedNode != null)
            {
                DehighLightNode(highLightedNode);
            }
        }

        if (isMoving)
        {
            if (highLightedNode != null)
            {
                DehighLightNode(highLightedNode);
            }
        }

    }

    void DehighLightNode(NodeScript node)
    {
        node.HidePaths();
        highLighter.SetActive(false);
        lineRenderer.enabled = false;
        highLightedNode = null;

    }
    void HighLightNode(NodeScript node)
    {
        if (node == currentNode)
        {
            return;
        }
        else
        {
            highLighter.transform.position = node.gameObject.transform.position;

            if (currentNode != null && currentNode.activeNodesInReach.Count != 0)
            {
                for (int i = 0; i < currentNode.activeNodesInReach.Count; i++)
                {
                    if (node == currentNode.activeNodesInReach[i])
                    {
                        lineRenderer.SetPosition(0, transform.position);
                        lineRenderer.SetPosition(1, node.gameObject.transform.position);
                        lineRenderer.startColor = gameData.highLightReachableColor;
                        lineRenderer.endColor = gameData.highLightReachableColor;
                        lineRenderer.enabled = true;
                        highLighter.GetComponent<SpriteRenderer>().color = gameData.highLightReachableColor;
                        node.ShowPaths();
                    }
                }

                if (!lineRenderer.enabled)
                {
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, node.gameObject.transform.position);
                    lineRenderer.startColor = gameData.highLightUnreachableColor;
                    lineRenderer.endColor = gameData.highLightUnreachableColor;
                    highLighter.GetComponent<SpriteRenderer>().color = gameData.highLightUnreachableColor;
                    lineRenderer.enabled = true;
                }
            }

            highLighter.SetActive(true);

            highLightedNode = node;
        }

    }

    void Clicked()
    {
        Vector2 clickedSpot = mainCam.ScreenToWorldPoint(playerInput.Game.MousePosition.ReadValue<Vector2>());

        Collider2D clickedNode = Physics2D.OverlapCircle(clickedSpot, 0.5f);



        if (clickedNode != null)
        {
            NodeScript clickedNodeCode = clickedNode.gameObject.GetComponent<NodeScript>();

            if (clickedNodeCode != currentNode)
            {
                foreach (NodeScript node in currentNode.activeNodesInReach)
                {
                    if (node == clickedNodeCode)
                    {
                        if (!isMoving)
                        {
                            StartCoroutine(Move(clickedNode.gameObject.transform.position));
                        }
                    }
                }
            }

        }
    }

    IEnumerator Move(Vector2 location)
    {
        isMoving = true;
        Vector2 initialPosition = transform.position;
        for (float i = 0; i <= 1; i += 0.02f)
        {
            yield return new WaitForSeconds(0.00025f);
            Vector2 newPosition;
            if (i < 0.98f)
            {
                newPosition = new Vector2(Mathf.Lerp(initialPosition.x, location.x, i), Mathf.Lerp(initialPosition.y, location.y, i));
            }
            else
            {
                newPosition = location;
            }

            transform.position = newPosition;
        }
        eMoved.Raise();
        isMoving = false;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        if (playerRadius > 0)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, playerRadius);
        }
    }
}
