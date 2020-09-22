using UnityEngine;

public class PlayerSortingOrder : SortingOrderBase
{
    [ReadOnly] [SerializeField] protected Renderer[] sortingRenderers;
    [ReadOnly] [SerializeField] protected int[] originOrders;

    private void Start() {
        sortingRenderers = transform.GetComponentsInChildren<Renderer>();
        originOrders = new int[sortingRenderers.Length];

        for (var i = 0; i < sortingRenderers.Length; i++) originOrders[i] = sortingRenderers[i].sortingOrder;
    }

    private void Update() {
        for (var i = 0; i < sortingRenderers.Length; i++) SortingOrderFix(sortingRenderers[i], originOrders[i]);
    }
}