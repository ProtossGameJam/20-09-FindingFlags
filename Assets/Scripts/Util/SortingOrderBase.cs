using UnityEngine;

public class SortingOrderBase : MonoBehaviour
{
    [ReadOnly] [SerializeField] protected int orderFix;
    [ReadOnly] [SerializeField] protected int baseOrder;
    [SerializeField] protected int orderOffset;

    protected virtual void Reset()
    {
        orderFix = 1000;
        baseOrder = 1000;
    }

    protected void SortingOrderFix(Renderer sortingRenderer, int currentOrder = 0)
    {
        sortingRenderer.sortingOrder = (int) (baseOrder - transform.position.y * orderFix + currentOrder + orderOffset);
    }
}