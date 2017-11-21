using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Powerup {
    override public void ActivatePowerOnPlayer(Player player) {
        player.ActivateSpeedBoost();
    }
}
