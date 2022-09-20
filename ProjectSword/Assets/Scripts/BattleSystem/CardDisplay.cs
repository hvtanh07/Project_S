using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Cards cardInfo;

    public Text nameText;
    public Text descripText;

    public Image artWorkImage;

    public UnlockPoint pointOfFunction;

    // Start is called before the first frame update
    public void ApplyCardInfo()
    {
        nameText.text = cardInfo.cardName;
        descripText.text = cardInfo.description;
        artWorkImage.sprite = cardInfo.artWork;
    }

    public void ActivateCard()
    {
        pointOfFunction.ActivatePoint();
    }


}
