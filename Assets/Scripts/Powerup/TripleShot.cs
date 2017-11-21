using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : Powerup {
    override public void ActivatePowerOnPlayer(Player player)
    {
        player.ActivateTripleShot();
    }
}
