using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string _name;
    private string _description;
    private int _cost;

    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
}
