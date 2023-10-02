using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public PlayerStateManager playerStateManager;
    public Image imageElement;
    public TextMeshProUGUI textElement;

    private void Update() {
        if (playerStateManager.currentState == playerStateManager.playerIdleState) {
            imageElement.color = Color.white;
            textElement.text = "IDLE";
        } else if (playerStateManager.currentState == playerStateManager.playerWalkState) {
            imageElement.color = Color.green;
            textElement.text = "WALK";
        } else if (playerStateManager.currentState == playerStateManager.playerRunState) {
            imageElement.color = Color.yellow;
            textElement.text = "RUN";
        } else if (playerStateManager.currentState == playerStateManager.playerJumpState) {
            imageElement.color = Color.blue;
            textElement.text = "JUMP";
        } else if (playerStateManager.currentState == playerStateManager.playerFallState) {
            imageElement.color = Color.red;
            textElement.text = "FALL";
        }
    }
}
