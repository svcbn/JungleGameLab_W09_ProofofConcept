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

    List<GameObject> cardsInDeck = new List<GameObject>();
    List<GameObject> cardsInHand = new List<GameObject>();
    List<GameObject> cardsInDiscard = new List<GameObject>();

    public Transform cardPanel;

    public GameObject cardPrefab;

    Color[] colors =   {new Color(0.6161445f, 0.7510408f, 0.8113208f),
                        new Color(1f, 0.572549f, 0.6017858f),
                        new Color(1f, 0.9771625f, 0.6367924f)};

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
            deckSizeText.text = "Deck\n" + _deckSize;
        }
    }

    public TextMeshProUGUI costText;
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
            costText.text = _currentCost + " / " + _maxCost;
        }
    }

    private int _maxCost;
    public int MaxCost
    {
        get
        {
            return _maxCost;
        }
        set
        {
            _maxCost = value;
            costText.text = _currentCost + " / " + _maxCost;
        }
    }

    public TextMeshProUGUI discardSizeText;
    private int _discardSize;
    public int DiscardSize
    {
        get
        {
            return _discardSize;
        }
        set
        {
            _discardSize = value;
            discardSizeText.text = "Discard\n" + _discardSize;
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

    }

    private void Start()
    {

        ChangeState(GameState.Title);
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
        MaxCost = 3;
        CurrentCost = MaxCost;

        // 타이틀 진입시
        AddCard();

        ChangeState(GameState.PreTurn);
    }

    private void OnPreTurn()
    {
        // 턴 돌아올때 초기화 해야될것
        CurrentCost = MaxCost;

        // 턴 돌아올때 준비 해야될것


        // 턴 시작 카드뽑기
        DrawCard(true);

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

        Discard();

        ChangeState(GameState.PreTurn);
    }

    

    public void DrawCard(bool isTurnStart)
    {

        if(isTurnStart)
        {
            Discard();

            for(int i = 0; i < 4; i++)
            {
                if(cardsInDeck.Count < 1)
                {
                    RefillDeck();
                }

                cardsInHand.Add(cardsInDeck[0]);
                cardsInHand[cardsInHand.Count - 1].transform.SetParent(cardPanel);

                cardsInDeck.Remove(cardsInDeck[0]);
                DeckSize--;
            }

            ChangeState(GameState.PlayerTurn);
        }
        else
        {
            if(cardsInHand.Count > 5)
            {
                return;
            }

            if (cardsInDeck.Count < 1)
            {
                RefillDeck();
            }

            cardsInHand.Add(cardsInDeck[0]);
            cardsInHand[cardsInHand.Count - 1].transform.SetParent(cardPanel);

            cardsInDeck.Remove(cardsInDeck[0]);
            DeckSize--;
        }
    }

    private void Discard()
    {
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            cardsInHand[i].transform.SetParent(this.transform, false);
            cardsInDiscard.Add(cardsInHand[i]);
            DiscardSize++;
        }
        cardsInHand.Clear();
    }

    private void AddCard()
    {
        // 원래는 여기서 굳이안해도되는데 임시로하는거임
        for(int i = 0; i < 18; i++)
        {
            GameObject card = Instantiate(cardPrefab, this.transform);
            var texts = card.GetComponentsInChildren<TextMeshProUGUI>();
            texts[texts.Length - 1].text = Random.Range(0, 4).ToString();
            var panels = card.GetComponentsInChildren<Image>();

            int tempColor = Random.Range(0, 3);
            panels[1].color = colors[tempColor];
            panels[3].color = colors[tempColor];

            cardsInDeck.Add(card);
            DeckSize++;
        }
    }


    private void RefillDeck()
    {
        for(int i = 0; i < cardsInDiscard.Count; i++)
        {
            cardsInDeck.Add(cardsInDiscard[i]);
            DiscardSize--;
            DeckSize++;
        }
        
        cardsInDiscard.Clear();
        cardsInDeck =  ShuffleDeck(cardsInDeck);
    }

    private List<GameObject> ShuffleDeck(List<GameObject> _deck)
    {
        // 덱 순서 섞어주기
        List<GameObject> listToReturn = new List<GameObject>();

        System.Random random = new();

        for(int i = 0; i < _deck.Count; i++)
        {
            int index = random.Next(0, i + 1);
            listToReturn.Insert(index, _deck[i]);
        }

        return listToReturn;
    }

    public void OnTurnEndButtonClicked()
    {
        if (State != GameState.PlayerTurn) return;

        // 원랜 enemy 차례지만 그냥 넘겨버림
        ChangeState(GameState.EndTurn);
    }
}
