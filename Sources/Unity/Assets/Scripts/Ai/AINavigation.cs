using UnityEngine;

public class AINavigation : MonoBehaviour
{
    [SerializeField] private GameObject currentNode;

    private void Start()
    {
        if (currentNode == null)
        {
            currentNode = GameObject.FindGameObjectWithTag("Node");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        var nextNode = GetNextNode();
        if (collider.CompareTag("Node") && collider.gameObject.Equals(nextNode))
        {
            SetCurrentNavigation(nextNode);
        }
    }

    public void SetCurrentNavigation(GameObject currentNode)
    {
        this.currentNode = currentNode;
        Debug.Log(currentNode.name);
    }

    public GameObject GetCurrentNode()
    {
        return currentNode;
    }

    public GameObject GetNextNode()
    {
        return currentNode.GetComponent<AINode>().GetRightChild();
    }
}