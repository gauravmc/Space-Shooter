using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Powerup {
    override public void ActivatePowerOnPlayer(Player player) {
        player.ActivateShield();
    }
}
