using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerment : SingleTon<SceneManagerment>
{
    public string SceneTransitionName{get; private set;}   
    
    public void SetTransitionName(string scenetransitionName)
    {
        this.SceneTransitionName = scenetransitionName;
    }
}
