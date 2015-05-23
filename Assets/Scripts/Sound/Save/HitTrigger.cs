/*using UnityEngine;
using System.Collections;

public class HitTrigger : AkTriggerBase {

    PlayerControllerF player;
    MonsterControllerF monster;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerControllerF>();
        monster = GetComponent<MonsterControllerF>();

        if (player != null)
        {
            player.OnClickHitEvent += Activate;
        }

        if (monster != null)
        {
            monster.OnClickHitEvent += Activate;
        }
        
    }

    // Update is called once per frame
    void Activate()
    {
        triggerDelegate(player.gameObject);
    }
}
*/