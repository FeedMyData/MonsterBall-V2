using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

    public enum Item
    {
        cake
    }

    public Item bonus;
    private bool active;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(!active)
            transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 50);
	}

    public void Activated(Vector3 position)
    {
        active = true;
        switch (bonus)
        {
            case Item.cake:
                //Lache le bonus à la position du joueur
                //sendmessage au monstre
                gameObject.SetActive(true);
                transform.rotation = Quaternion.identity;
                this.transform.position = position;
                Debug.Log(position);
                GameControllerF.GetMonster().GetComponent<MonsterControllerF>().ChargeOnCake(transform.position);
                
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        PlayerControllerF player = other.GetComponent<PlayerControllerF>();
        MonsterControllerF monster = other.GetComponent<MonsterControllerF>();

        if ( player != null && !active)
        {
            player.SetBonus(this);
            gameObject.SetActive(false);
        }

        if (monster != null && active)
        {
            Destroy(gameObject);
            monster.GoodCake();
        }
    }

    public Sprite GetSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }
}
