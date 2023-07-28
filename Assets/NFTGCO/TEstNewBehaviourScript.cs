using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEstNewBehaviourScript : MonoBehaviour
{
    //integer and return another integer

    //3 * 2 * 1 = 6
    //factorial of 3 = 3 * 2 * 1 = 6


    void Start()
    {
        ReturnFactorial(3);
    }

    private int ReturnFactorial(int number)
    {
        int result = 1;
        for(int i = number; i > 0; i--) 
            result *= i; 
        
        return result;
    }
}