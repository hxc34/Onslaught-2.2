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

    [SerializeField] private GameObject buildMenuPanel;    // Your build menu (e.g., panel with tower buttons)
    [SerializeField] private GameObject upgradeMenuPanel;  // Your upgrade menu panel (with tower stats, upgrade button, etc.)

    public TowerUpgrades selectedTowerUpgrades;

    
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

        if (placingTower == null && Input.GetMouseButtonDown(0) && (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()))
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
                selectedTower = null;
            }
            if (buildMenuPanel != null)
                buildMenuPanel.SetActive(true);
            if (upgradeMenuPanel != null)
                upgradeMenuPanel.SetActive(false);
        }
        else
        {
            GameObject clickedTower = hit.collider.gameObject;
            
            // If the clicked tower is the one already selected, unselect it.
            if (selectedTower == clickedTower)
            {
                GameObject range = selectedTower.transform.GetChild(1).gameObject;
                range.GetComponent<SpriteRenderer>().enabled = false;
                //Debug.Log("Unselected tower: " + selectedTower.name);
                selectedTower = null;
                // Optionally, you can also decide to show the build menu again here.
                if (buildMenuPanel != null)
                    buildMenuPanel.SetActive(true);
                if (upgradeMenuPanel != null)
                    upgradeMenuPanel.SetActive(false);
            }
            else
            {
                if (selectedTower != null)
                {
                    GameObject range = selectedTower.transform.GetChild(1).gameObject;
                    range.GetComponent<SpriteRenderer>().enabled = false;
                }
                selectedTower = clickedTower;
                // Store the selected tower's TowerUpgrades component
                selectedTowerUpgrades = selectedTower.GetComponent<TowerUpgrades>();

                GameObject newRange = selectedTower.transform.GetChild(1).gameObject;
                newRange.GetComponent<SpriteRenderer>().enabled = true;
               // Debug.Log("Selected tower: " + selectedTower.name);

                // (Optional) Show your upgrade menu and update its UI with selectedTowerUpgrades.
                ShowUpgradeUI(selectedTower);
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

    public void UpgradeSelectedTower()
    {
        if (selectedTowerUpgrades != null)
        {
            int current = selectedTowerUpgrades.currentLevel;
            if (current < selectedTowerUpgrades.levels.Length)
            {
                int nextCost = selectedTowerUpgrades.levels[current].cost;
                // Check if the player has enough money (this should always be true if the button was interactable)
                if (Player.main.money >= nextCost)
                {
                    // Deduct the cost
                    Player.main.money -= nextCost;
                    Debug.Log("Deducted $" + nextCost + " for upgrade. New balance: $" + Player.main.money);
                    
                    // Perform the upgrade
                    selectedTowerUpgrades.Upgrade();

                    // Refresh the upgrade UI
                    UpgradeMenuUI upgradeUI = upgradeMenuPanel.GetComponent<UpgradeMenuUI>();
                    if (upgradeUI != null)
                    {
                        upgradeUI.RefreshUI();
                        if (upgradeUI.isHovered)
                        {
                            upgradeUI.UpdateDeltaPreview();
                        }
                    }

                    // Optionally, update any money UI here
                }
                else
                {
                    Debug.Log("Not enough money to upgrade!");
                }
            }
            else
            {
                Debug.Log("Tower is already maxed out.");
            }
        }
        else
        {
            Debug.LogWarning("No tower selected for upgrade!");
        }
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

    private void ShowUpgradeUI(GameObject tower)
    {
        if (buildMenuPanel != null)
        {
            buildMenuPanel.SetActive(false);
        }
    if (upgradeMenuPanel != null)
    {
        upgradeMenuPanel.SetActive(true);
        // Assuming your upgrade menu panel has the UpgradeMenuUI script on it:
        UpgradeMenuUI upgradeUI = upgradeMenuPanel.GetComponent<UpgradeMenuUI>();
        if (upgradeUI != null)
        {
            // Pass the selected tower's TowerUpgrades component
            upgradeUI.towerUpgrades = tower.GetComponent<TowerUpgrades>();
            upgradeUI.RefreshUI();
        }
        }
    }

}
