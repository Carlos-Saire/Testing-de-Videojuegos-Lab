using UnityEngine;

public class NodeControl : MonoBehaviour
{
    private DoublyLinkedCircularList<NodeControl> listAdjacentNodes = new DoublyLinkedCircularList<NodeControl>();
    public void AddAdjacentNode(NodeControl adjacentNode)
    {
        listAdjacentNodes.InsertAtEnd(adjacentNode);
    }
    public NodeControl GetAdjacentNode()
    {
        return listAdjacentNodes.GetAtPosition(Random.Range(0, listAdjacentNodes.GetCount()));
    }
}
