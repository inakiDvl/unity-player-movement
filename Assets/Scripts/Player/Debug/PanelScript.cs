using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{   
    //              stete manager script       
    public PlayerStateManager playerStateManager;
    //              panel enablers
    public bool enableStatePanel;
    public bool enableGroundedPanel;
    public bool enableInteractablePanel;
    //              state panel
    public GameObject statePanel;
    private Image statePanelImageElement;
    private TextMeshProUGUI statePanelTextElement;
    //              grounded panel
    public GameObject groundedPanel;
    private Image groundedPanelImageElement;
    private TextMeshProUGUI groundedPanelTextElement;
    //              interactable panel
    public GameObject interactablePanel;
    private Image interactablePanelImageElement;
    private TextMeshProUGUI interactablePanellTextElement;

    private void Start() {
        EnablePanels();
        //              state panel
        statePanelImageElement = statePanel.GetComponent<Image>();
        statePanelTextElement = statePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //              grounded panel
        groundedPanelImageElement = groundedPanel.GetComponent<Image>();
        groundedPanelTextElement = groundedPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //              interactable panel
        interactablePanelImageElement = interactablePanel.GetComponent<Image>();
        interactablePanellTextElement = interactablePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        DisablePanels();
        ManageStatePanel();
        ManageGroundedPanel();
        ManageInteractablePanel();
    }

    private void EnablePanels()
    {
        //              state panel
        enableStatePanel = true;
        //              grounded panel
        enableGroundedPanel = true;
        //              interactable panel
        enableInteractablePanel = true;
    }

    private void DisablePanels() 
    {
        //              state panel
        if (!enableStatePanel) {
            statePanel.SetActive(false);
        } else {
            statePanel.SetActive(true);
        }
        //              grounded panel
        if (!enableGroundedPanel) {
            groundedPanel.SetActive(false);
        } else {
            groundedPanel.SetActive(true);
        }
        //              interactable panel
        if (!enableInteractablePanel) {
            interactablePanel.SetActive(false);
        } else {
            interactablePanel.SetActive(true);
        }
    }

    private void ManageStatePanel() 
    {
        statePanelImageElement.color = Color.gray;

        if (playerStateManager.currentState == playerStateManager.playerIdleState) {
            statePanelTextElement.text = "IDLE";
        } else if (playerStateManager.currentState == playerStateManager.playerWalkState) {
            statePanelTextElement.text = "WALK";
        } else if (playerStateManager.currentState == playerStateManager.playerRunState) {
            statePanelTextElement.text = "RUN";
        } else if (playerStateManager.currentState == playerStateManager.playerJumpState) {
            statePanelTextElement.text = "JUMP";
        } else if (playerStateManager.currentState == playerStateManager.playerFallState) {
            statePanelTextElement.text = "FALL";
        } else if (playerStateManager.currentState == null){
            statePanelImageElement.color = Color.yellow;
            statePanelTextElement.text = "NULL";
        } else {
            statePanelImageElement.color = Color.red;
            statePanelTextElement.text = "ERROR";
        }
    }

    private void ManageGroundedPanel() 
    {
        statePanelImageElement.color = Color.gray;
        
        if (playerStateManager.IsGrounded() == true) {
            groundedPanelImageElement.color = Color.green;
            groundedPanelTextElement.text = "GROUNDED";
        } else if (playerStateManager.IsGrounded() == false) {
            groundedPanelImageElement.color = Color.blue;
            groundedPanelTextElement.text = "NOT GROUNDED";
        } else {
            statePanelImageElement.color = Color.red;
            statePanelTextElement.text = "ERROR";
        }
    }

    private void ManageInteractablePanel()
    {
        interactablePanelImageElement.color = Color.gray;

        if (playerStateManager.closestInteractable != null) {
            interactablePanellTextElement.text = playerStateManager.closestInteractable.name.ToUpper();
        } else if (playerStateManager.closestInteractable == null) {
            interactablePanelImageElement.color = Color.yellow;
            interactablePanellTextElement.text = "NULL";
        } else {
            interactablePanelImageElement.color = Color.red;
            interactablePanellTextElement.text = "ERROR";
        }
    }

}
