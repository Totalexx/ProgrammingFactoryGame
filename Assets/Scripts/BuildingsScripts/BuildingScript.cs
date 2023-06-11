using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public Vector2Int size = new Vector2Int(1, 1);
    public ItemScriptableObject item;

    private SpriteRenderer buildingSprite;

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++) 
            {
                Gizmos.color = new Color(1, 1, 0, 0.5f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 31f, 1));
            }
        }
    }

    private void Awake()
    {
        buildingSprite = transform.GetComponent<SpriteRenderer>();
        buildingSprite.sprite = item.icon;
    }

    
}
