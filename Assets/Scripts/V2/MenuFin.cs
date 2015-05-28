using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuFin : MonoBehaviour {

    public GameObject defaultButton;

    private Text bluWin, redWin, tie;
    private Text txtJ1,txtJ2,txtJ3,txtJ4;
    private Text rewardJ1, rewardJ2, rewardJ3, rewardJ4;

	// Use this for initialization
	void Start () {
        if (defaultButton != null)
            EventSystem.current.SetSelectedGameObject(defaultButton);

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitMenu()
    {
        Text[] tabTxt = GetComponentsInChildren<Text>();
        //recupère toutes les infos et choisi les meilleurs

        for (int i = 0; i < tabTxt.Length; i++)
        {
            if (tabTxt[i].name.Contains("TxtJ1"))
                txtJ1 = tabTxt[i];
            else if (tabTxt[i].name.Contains("TxtJ2"))
                txtJ2 = tabTxt[i];
            else if (tabTxt[i].name.Contains("TxtJ3"))
                txtJ3 = tabTxt[i];
            else if (tabTxt[i].name.Contains("TxtJ4"))
                txtJ4 = tabTxt[i];
            else if (tabTxt[i].name.Contains("RewardJ1"))
                rewardJ1 = tabTxt[i];
            else if (tabTxt[i].name.Contains("RewardJ2"))
                rewardJ2 = tabTxt[i];
            else if (tabTxt[i].name.Contains("RewardJ3"))
                rewardJ3 = tabTxt[i];
            else if (tabTxt[i].name.Contains("RewardJ4"))
                rewardJ4 = tabTxt[i];
            else if (tabTxt[i].name.Contains("BluWin"))
                bluWin = tabTxt[i];
            else if (tabTxt[i].name.Contains("RedWin"))
                redWin = tabTxt[i];
            else if (tabTxt[i].name.Contains("Tie"))
                tie = tabTxt[i];
        }

        if (GameControllerF.GetPlayer(1).GetComponent<PlayerControllerF>().team == GameControllerF.Team.Blu)
            txtJ1.color = Color.blue;
        else
            txtJ1.color = Color.red;

        if (GameControllerF.GetPlayer(2).GetComponent<PlayerControllerF>().team == GameControllerF.Team.Blu)
            txtJ2.color = Color.blue;
        else
            txtJ2.color = Color.red;

        if (GameControllerF.GetPlayer(3).GetComponent<PlayerControllerF>().team == GameControllerF.Team.Blu)
            txtJ3.color = Color.blue;
        else
            txtJ3.color = Color.red;

        if (GameControllerF.GetPlayer(4).GetComponent<PlayerControllerF>().team == GameControllerF.Team.Blu)
            txtJ4.color = Color.blue;
        else
            txtJ4.color = Color.red;
    }

    public void Replay()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    public void WhoWin(int blu,int red)
    {
        if (blu > red)
        {
            bluWin.enabled = true;
            redWin.enabled = false;
            tie.enabled = false;
        }
        else if (blu < red)
        {
            bluWin.enabled = false;
            redWin.enabled = true;
            tie.enabled = false;
        }
        else
        {
            bluWin.enabled = false;
            redWin.enabled = false;
            tie.enabled = true;
        }
    }

    public void RewardPlayer()
    {
        PlayerControllerF[] tabPlayer = new PlayerControllerF[4];

        tabPlayer[0] = GameControllerF.GetPlayer(1).GetComponent<PlayerControllerF>();
        tabPlayer[1] = GameControllerF.GetPlayer(2).GetComponent<PlayerControllerF>();
        tabPlayer[2] = GameControllerF.GetPlayer(3).GetComponent<PlayerControllerF>();
        tabPlayer[3] = GameControllerF.GetPlayer(4).GetComponent<PlayerControllerF>();

        int coupDonne = -1, coupRecu = -1, coupFort = -1, mangeJoueur = -1, sautBut = -1, coupVide = -1, marqueBut = -1;

        //Coups donnés
        int maxCoupsDonnes = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].coupsDonnes > maxCoupsDonnes)
            {
                maxCoupsDonnes = tabPlayer[i].coupsDonnes;
                coupDonne = i;
            }
        }

        if (coupDonne == 0)
        {
            rewardJ1.text += "Je tape beaucoup\n";
        }
        else if (coupDonne == 1)
        {
            rewardJ2.text += "Je tape beaucoup\n";
        }
        else if (coupDonne == 2)
        {
            rewardJ3.text += "Je tape beaucoup\n";
        }
        else if (coupDonne == 3)
        {
            rewardJ4.text += "Je tape beaucoup\n";
        }

        //Coups reçus
        int maxCoupsRecus = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].coupsRecus > maxCoupsRecus)
            {
                maxCoupsRecus = tabPlayer[i].coupsRecus;
                coupRecu = i;
            }
        }

        if (coupRecu == 0)
        {
            rewardJ1.text += "Je me fait taper beaucoup\n";
        }
        else if (coupRecu == 1)
        {
            rewardJ2.text += "Je me fait taper beaucoup\n";
        }
        else if (coupRecu == 2)
        {
            rewardJ3.text += "Je me fait taper beaucoup\n";
        }
        else if (coupRecu == 3)
        {
            rewardJ4.text += "Je me fait taper beaucoup\n";
        }

        //Coups forts

        int maxCoupsFort = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].coupsCharges > maxCoupsFort)
            {
                maxCoupsFort = tabPlayer[i].coupsCharges;
                coupFort = i;
            }
        }

        if (coupFort == 0)
        {
            rewardJ1.text += "Je tape comme une brute\n";
        }
        else if (coupFort == 1)
        {
            rewardJ2.text += "Je tape comme une brute\n";
        }
        else if (coupFort == 2)
        {
            rewardJ3.text += "Je tape comme une brute\n";
        }
        else if (coupFort == 3)
        {
            rewardJ4.text += "Je tape comme une brute\n";
        }

        //Mange Joueur

        int maxMangeJoueur = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].joueurMange > maxMangeJoueur)
            {
                maxMangeJoueur = tabPlayer[i].joueurMange;
                mangeJoueur = i;
            }
        }

        if (mangeJoueur == 0)
        {
            rewardJ1.text += "Je suis casse-croute à mi-temps\n";
        }
        else if (mangeJoueur == 1)
        {
            rewardJ2.text += "Je suis casse-croute à mi-temps\n";
        }
        else if (mangeJoueur == 2)
        {
            rewardJ3.text += "Je suis casse-croute à mi-temps\n";
        }
        else if (mangeJoueur == 3)
        {
            rewardJ4.text += "Je suis casse-croute à mi-temps\n";
        }

        //Saut But

        int maxSautBut = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].sautBut > maxSautBut)
            {
                maxSautBut = tabPlayer[i].sautBut;
                sautBut = i;
            }
        }

        if (sautBut == 0)
        {
            rewardJ1.text += "Je suis un boulet... de canon\n";
        }
        else if (sautBut == 1)
        {
            rewardJ2.text += "Je suis un boulet... de canon\n";
        }
        else if (sautBut == 2)
        {
            rewardJ3.text += "Je suis un boulet... de canon\n";
        }
        else if (sautBut == 3)
        {
            rewardJ4.text += "Je suis un boulet... de canon\n";
        }

        //Coups Vides

        int maxCoupsVides = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].coupsVide > maxCoupsVides)
            {
                maxCoupsVides = tabPlayer[i].coupsVide;
                coupVide = i;
            }
        }

        if (coupVide == 0)
        {
            rewardJ1.text += "Je tape dans le vide\n";
        }
        else if (coupVide == 1)
        {
            rewardJ2.text += "Je tape dans le vide\n";
        }
        else if (coupVide == 2)
        {
            rewardJ3.text += "Je tape dans le vide\n";
        }
        else if (coupVide == 3)
        {
            rewardJ4.text += "Je tape dans le vide\n";
        }


        int maxMarqueBut = 0;
        for (int i = 0; i < tabPlayer.Length; i++)
        {
            if (tabPlayer[i].marqueBut > maxMarqueBut)
            {
                maxMarqueBut = tabPlayer[i].marqueBut;
                marqueBut = i;
            }
        }

        if (marqueBut == 0)
        {
            rewardJ1.text += "Je marque comme un dieu\n";
        }
        else if (marqueBut == 1)
        {
            rewardJ2.text += "Je marque comme un dieu\n";
        }
        else if (marqueBut == 2)
        {
            rewardJ3.text += "Je marque comme un dieu\n";
        }
        else if (marqueBut == 3)
        {
            rewardJ4.text += "JJe marque comme un dieu\n";
        }

    }
}
