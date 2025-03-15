using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerManager : MonoBehaviour
{
    

    [SerializeField] private GameObject cannonTower;
    [SerializeField] private GameObject gattlingTower;
    [SerializeField] private GameObject rocketTower;
    [SerializeField] private GameObject laserTower;
    [SerializeField] private LayerMask towerLayer;

    
    private GameObject placingTower;
    private GameObject selectedTower;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            AttemptSelectTower(cannonTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)){
            AttemptSelectTower(gattlingTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            AttemptSelectTower(rocketTower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)){
            AttemptSelectTower(laserTower);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)){
            ClearSelected();
        }

        if (placingTower){
            if (!placingTower.GetComponent<TowerPlacement>().isPlacing){
                placingTower = null;
            }
        }

        if (placingTower == null && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, towerLayer);
            
            // If nothing is hit, unselect the tower if one is selected.
            if (hit.collider == null)
            {
                if (selectedTower != null)
                {
                    GameObject range = selectedTower.transform.GetChild(1).gameObject;
                    range.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log("Unselected tower: " + selectedTower.name);
                    selectedTower = null;
                }
            }
            else
            {
                GameObject clickedTower = hit.collider.gameObject;
                
                // If the clicked tower is the one already selected, unselect it.
                if (selectedTower == clickedTower)
                {
                    GameObject range = selectedTower.transform.GetChild(1).gameObject;
                    range.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log("Unselected tower: " + selectedTower.name);
                    selectedTower = null;
                }
                else
                {
                    if (selectedTower != null)
                    {
                        GameObject range = selectedTower.transform.GetChild(1).gameObject;
                        range.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    selectedTower = clickedTower;
                    GameObject newRange = selectedTower.transform.GetChild(1).gameObject;
                    newRange.GetComponent<SpriteRenderer>().enabled = true;
                    Debug.Log("Selected tower: " + selectedTower.name);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.U) && selectedTower)
        {
            selectedTower.GetComponent<TowerUpgrades>().Upgrade();
        }
    }

    private void ClearSelected(){
        if (placingTower){
            Destroy(placingTower);
            placingTower = null;
        }
    }

    private void SelectTower(GameObject tower){
        ClearSelected();
        placingTower = Instantiate(tower);
        
    }

    private void AttemptSelectTower(GameObject tower)
    {
        // Get the tower's cost from its Tower component.
        int cost = tower.GetComponent<Tower>().cost;
        
        // Check if the player has enough money.
        if (Player.main != null && Player.main.money >= cost)
        {
            // Optionally, you can also deduct the money here:
            // Player.main.money -= cost;

            // Then allow selection/placement.
            SelectTower(tower);
        }
        else
        {
            Debug.Log("Not enough money to purchase " + tower.name);
        }
    }

    public void OnTowerButtonPress(GameObject tower)
    {
        AttemptSelectTower(tower);
    }

}
