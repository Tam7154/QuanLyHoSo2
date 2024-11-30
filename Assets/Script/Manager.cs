using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

    //FirebaseFirestore db;

    void Awake()
    {
        Instance = this;
    }


}
