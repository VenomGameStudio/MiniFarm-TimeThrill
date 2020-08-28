using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrauctionTrigger : MonoBehaviour
{
    public Instraction instraction; 

    public void TriggerInstruction()
    {
        FindObjectOfType<InstructionManager>().StartInstruction(instraction);
    }
}
