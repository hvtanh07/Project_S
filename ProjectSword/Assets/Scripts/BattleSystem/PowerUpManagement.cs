using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManagement : MonoBehaviour
{
    public List<CardDisplay> playerPowerUpCards;
    public NextCardList playerCardList;
    public List<CardDisplay> enemyPowerUpCards;
    public NextCardList enemyCardList;

    private void OnEnable() {
        GetRandomPlayerPowerUpFromList();
        GetRandomEnemyPowerUpFromList();
    }
    public void GetRandomPlayerPowerUpFromList(){
        foreach(CardDisplay card in playerPowerUpCards){
            card.cardInfo = playerCardList.GetRandomPoint().cardInfo;
            card.ApplyCardInfo();
        }
    }

    public void GetRandomEnemyPowerUpFromList(){
        foreach(CardDisplay card in enemyPowerUpCards){
            card.cardInfo = enemyCardList.GetRandomPoint().cardInfo;
            card.ApplyCardInfo();
        }
    }
}
