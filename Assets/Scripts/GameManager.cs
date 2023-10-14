using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State {  get; private set; }
    public enum GameState
    {
        Title = 1,
        PreTurn,
        Draw,
        PlayerTurn,
        UseCard,
        EnemyTurn,
        EndTurn,
    }

    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public TextMeshProUGUI deckSizeText;
    private int _deckSize;
    public int DeckSize
    {
        get
        {
            return _deckSize;
        }
        private set
        {
            _deckSize = value;
            ChangeUIValue(deckSizeText, _deckSize.ToString());
        }
    }

    public TextMeshProUGUI currentCostText;
    private int _currentCost;
    public int CurrentCost
    {
        get
        {
            return _currentCost;
        }
        set
        {
            _currentCost = value;
            ChangeUIValue(currentCostText, _currentCost.ToString());
        }
    }

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion
        
        State = GameState.Title;
    }

    private void Start()
    {

    }

    public void ChangeState(GameState _state)
    {
        State = _state;
        switch (State)
        {
            case GameState.Title:
                OnTitle();
                break;
            case GameState.PreTurn:
                OnPreTurn();
                break;
            case GameState.Draw:
                OnDraw();
                break;
            case GameState.PlayerTurn:
                OnPlayerTurn();
                break;
            case GameState.UseCard:
                OnUseCard();
                break;
            case GameState.EnemyTurn:
                OnEnemyTurn();
                break;
            case GameState.EndTurn:
                OnEndTurn();
                break;
        }
    }

    private void OnTitle()
    {
        // Ÿ��Ʋ ���Խ�


    }

    private void OnPreTurn()
    {
        // �� ���ƿö� �ʱ�ȭ �ؾߵɰ�
        

        // �� ���ƿö� �غ� �ؾߵɰ�


        // �� ���� ī��̱�


    }

    private void OnDraw()
    {
        // ī�带 �̰� �� ��


        // ��� �ٸ� �ൿ�� �����ϰ�


        // ī�� �̱�
    }

    private void OnPlayerTurn()
    {
        // �� ���� ��ư ������ �ִ� ����


        // ī�� ���� �־�ߵ�


        // ī�� ������� �Ѱ��ֱ�


    }

    private void OnUseCard()
    {
        // ī�� ID �о
        

        // �ڽ�Ʈ ����


        // ī�� ȿ�� �ߵ�


    }

    private void OnEnemyTurn()
    {
        // ���� ��� ����


        // ������ ������


    }

    private void OnEndTurn()
    {
        // �� ����� �ʱ�ȭ����ߵɰ�


    }

    public void DrawCard()
    {
        if(deck.Count > 0)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for(int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    private void ChangeUIValue(TextMeshProUGUI _text, string value)
    {
        _text.text = value;
    }
}
