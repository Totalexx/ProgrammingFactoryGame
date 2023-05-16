using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2Int size = new Vector2Int(1, 1);
    public ItemScriptableObject item;

    private SpriteRenderer buildingSprite;

    public Vector2Int gridSize = new Vector2Int(10, 10);
    public float cellSize;
    public GameObject gridn;

    private BuildingScript[,] grid;
    private BuildingScript flyingBuilding;
    private Camera mainCamera;
    private bool avilable;

    private void Awake()
    {
        mainCamera = Camera.main;
        grid = new BuildingScript[gridSize.x, gridSize.y];
        buildingSprite = transform.GetComponent<SpriteRenderer>();
        buildingSprite.sprite = item.icon;
    }
    public void StartPlacingBuidling(BuildingScript buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (flyingBuilding != null)
        {
            var pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1;

            var x = RoundToCell(pos.x, cellSize);
            var y = RoundToCell(pos.y, cellSize);

            avilable = true;


            var collider = flyingBuilding.GetComponent<Collider2D>();
            var aCollider = gridn.GetComponent<Collider2D>();
            //Debug.Log(collider.ToString() + " " + aCollider.ToString());
            if (Physics2D.IsTouching(collider, aCollider))
            {
                Debug.Log("No");
                avilable = false;
                flyingBuilding.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            flyingBuilding.transform.position = new Vector3((float)x, (float)y, -1);
            //Debug.Log(flyingBuilding.transform.position.ToString() + " " + pos);
            if (Input.GetMouseButtonDown(0))
            {
                flyingBuilding = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("No");
        avilable = false;
        flyingBuilding.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    private double RoundToCell(float axis, float cellSize)
    {
        var result = Math.Floor(axis / cellSize) * cellSize + cellSize / 2;
        return result;
    }

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

}
