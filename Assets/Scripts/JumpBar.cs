using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpBar : MonoBehaviour
{
    public Slider slider;
    public GameObject player;
    private PlayerController playerController;


    void Start() {
        playerController = player.GetComponent<PlayerController>();
        slider.maxValue = playerController.playerJumpPower;
        slider.value = playerController.playerJumpPower;
    }
    
    public void setJumpbar(int jumpPower) {
        slider.value = jumpPower;
    }
}
