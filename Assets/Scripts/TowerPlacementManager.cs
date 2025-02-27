using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TowerPlacementManager : MonoBehaviour
{
    [Header("Placement Settings")]
    public bool isPlacingTower = false;

    [Tooltip("Real tower prefab (e.g., cylinder)")]
    public GameObject towerPrefab;

    [Tooltip("Ghost/outline tower prefab (semi-transparent or outlined material)")]
    public GameObject towerPreviewPrefab;

    [Tooltip("Tilemap containing grass tiles where towers can be placed")]
    public Tilemap grassTilemap;

    [Tooltip("LayerMask for the ground plane or tilemap collider")]
    public LayerMask groundLayer;

    // Keep track of which cells already have a tower placed
    private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();

    // The currently visible tower preview
    private GameObject currentPreview;

    UI UI;

    void Start()
    {
        UI = UI.Get();
    }

    void Update()
    {
        // Only do placement logic if we are currently in "placing" mode
        if (isPlacingTower)
        {
            // If right clicked, cancel placement
            if (Input.GetMouseButtonUp(1)) {
                Stop();
                return;
            }

            // Ray from camera to mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // If the ray hits the ground plane/tilemap within 1000 units
            if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
            {
                // The world position where the raycast hit
                Vector3 hitPoint = hit.point;

                // Convert that to the tilemap cell position
                Vector3Int cellPos = grassTilemap.WorldToCell(hitPoint);

                // Get the center of the cell in world space
                Vector3 cellCenter = grassTilemap.GetCellCenterWorld(cellPos);

                // If we haven't created a preview object yet, instantiate it
                if (currentPreview == null)
                {
                    currentPreview = Instantiate(towerPreviewPrefab);
                }

                // Move the preview to follow the mouse (snapped to cell center)
                currentPreview.transform.position = cellCenter;

                // Check if there's a valid grass tile at this cell
                TileBase tile = grassTilemap.GetTile(cellPos);
                bool isGrassTile = (tile != null); // or compare tile name, etc.

                // Check if already occupied
                bool isOccupied = occupiedCells.Contains(cellPos);

                // Determine overall validity
                bool validPlacement = isGrassTile && !isOccupied;

                // OPTIONAL: Change the preview color based on validity
                Renderer previewRenderer = currentPreview.GetComponentInChildren<Renderer>();
                if (previewRenderer != null)
                {
                    previewRenderer.material.color = validPlacement ? Color.green : Color.red;
                }

                // If user left-clicks, attempt to place the real tower
                if (Input.GetMouseButtonDown(0))
                {
                    if (validPlacement)
                    {
                        // Instantiate the actual tower
                        Instantiate(towerPrefab, cellCenter, Quaternion.identity);

                        // Mark the cell as occupied
                        occupiedCells.Add(cellPos);

                        // If you only want to place one tower per "mode," exit placing mode
                        Destroy(currentPreview);
                        currentPreview = null;
                        isPlacingTower = false;
                        UI.Castbar.Hide();
                    }
                    else
                    {
                        Debug.Log("Invalid placement: either not grass or cell occupied.");
                    }
                }
            }
            else
            {
                // If raycast hits nothing, either hide the preview or move it offscreen
                if (currentPreview != null)
                {
                    // Move it far away or disable it
                    currentPreview.transform.position = new Vector3(9999f, 9999f, 9999f);
                }
            }
        }
        else
        {
            // If we are NOT placing a tower, make sure no preview object remains
            if (currentPreview != null)
            {
                Destroy(currentPreview);
                currentPreview = null;
            }
        }
    }

    // Call this from a button to toggle placement mode on/off
    public void Place()
    {
        isPlacingTower = true;
    }

    public void Stop() {
        if (!isPlacingTower) return;

        isPlacingTower = false;
        UI.Castbar.Hide();

        // If turning off, destroy any existing preview
        if (!isPlacingTower && currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }
    }
}