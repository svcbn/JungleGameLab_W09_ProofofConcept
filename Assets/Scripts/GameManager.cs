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
        // 타이틀 진입시


    }

    private void OnPreTurn()
    {
        // 턴 돌아올때 초기화 해야될것
        

        // 턴 돌아올때 준비 해야될것


        // 턴 시작 카드뽑기


    }

    private void OnDraw()
    {
        // 카드를 뽑게 될 때


        // 모든 다른 행동을 제약하고


        // 카드 뽑기
    }

    private void OnPlayerTurn()
    {
        // 턴 종료 버튼 누를수 있는 상태


        // 카드 고를수 있어야됨


        // 카드 사용으로 넘겨주기


    }

    private void OnUseCard()
    {
        // 카드 ID 읽어서
        

        // 코스트 차감


        // 카드 효과 발동


    }

    private void OnEnemyTurn()
    {
        // 공격 대상 선정


        // 데미지 입히기


    }

    private void OnEndTurn()
    {
        // 턴 종료시 초기화해줘야될거


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
