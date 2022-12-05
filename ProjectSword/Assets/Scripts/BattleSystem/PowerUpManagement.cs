using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManagement : MonoBehaviour
{
    public List<CardDisplay> playerPowerUpCards;
    public NextCardList playerCardList;
    public List<CardDisplay> enemyPowerUpCards;
    public NextCardList enemyCardList;
    [SerializeField] EnemyCreator enemyCreator;

    private void OnEnable() {
        GetRandomPlayerPowerUpFromList();
        GetRandomEnemyPowerUpFromList();
    }
    public void GetRandomPlayerPowerUpFromList(){
        foreach(CardDisplay card in playerPowerUpCards){
            UnlockPoint randomPoint = playerCardList.GetRandomPoint();
            card.cardInfo = randomPoint.cardInfo;
            card.ApplyCardInfo();
            card.pointOfFunction = randomPoint;
        }
        playerCardList.GetlistOnnextUnlockedPoint();
    }

    public void GetRandomEnemyPowerUpFromList(){
        foreach(CardDisplay card in enemyPowerUpCards){
            UnlockPoint randomPoint = enemyCardList.GetRandomPoint();
            card.cardInfo = randomPoint.cardInfo;
            card.ApplyCardInfo();
            card.pointOfFunction = randomPoint;
        }
        enemyCardList.GetlistOnnextUnlockedPoint();
    }
    public void ActivateCard(){
        enemyCreator.UpdateUnlockedTypes();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
