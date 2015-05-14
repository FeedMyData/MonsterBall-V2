using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public abstract class AbstractSound : MonoBehaviour
{

    public enum Action
    {
        But,
        Impact,
        Course,
        Objet,
        RemiseEnJeu,
        TransformationBalleMonstre,
        TransformationMonstreBall,
        Grognement,
        RecracheJoueur,
        CoupBalle,
        CoupRecu,
        EjectBut,
        MarqueBut,
        Poursuivi,
        Victoire,
        WilhemScream,
        Yeah,
        TroisDeuxUn,
        Debut,
        Dialogue,
        Match
    }
}
