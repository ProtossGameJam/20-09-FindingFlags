using UnityEngine;

public class SpriteSortingOrder : SortingOrderBase
{
    [SerializeField] protected Renderer sortingRenderer;

    private void Awake() {
        if (sortingRenderer == null) sortingRenderer = GetComponent<Renderer>();
    }

    private void Start() {
        SortingOrderFix(sortingRenderer);
    }
}