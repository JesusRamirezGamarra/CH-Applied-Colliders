// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;



public class PlayerData : MonoBehaviour
{
    private int NumberOfCoin = 0;

    public int CountUp( int value){
        return NumberOfCoin+=value;
    }

    public int CountDown( int value){
        return NumberOfCoin-=value;
    }
}
