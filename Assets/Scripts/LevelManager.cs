using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using dotmob;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public static event Action LevelCompleted;

    [SerializeField] private float _minXDistanceBetweenHolders;
    [SerializeField] private Camera _camera;
    [SerializeField] private Holder _holderPrefab;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Button _holderaddbtn;
    [SerializeField] private AudioClip _winClip;

    public GameMode GameMode { get; private set; } = GameMode.Easy;
    public Level Level { get; private set; }

    private readonly List<Holder> _holders = new List<Holder>();

    private readonly Stack<MoveData> _undoStack = new Stack<MoveData>();

    public State CurrentState { get; private set; } = State.None;

    public bool HaveUndo => _undoStack.Count > 0;

    public List<Vector2> firstFourLevelsPositions, holderPositions, easyHolderPositions;

    public Button UndoBtn;

    private void Start()
    {
        UndoBtn.interactable = false;
    }
    private void Awake()
    {
        Instance = this;
        var loadGameData = GameManager.LoadGameData;
        GameMode = loadGameData.GameMode;
        Level = loadGameData.Level;
        LoadLevel();
        CurrentState = State.Playing;
    }
    private void LoadLevel()
    {
        PlayerPrefs.SetInt("HolderAddBtnUsed", 0);
        var list = PositionsForHolders(Level.map.Count, out var width).ToList();
        _camera.orthographicSize = 0.5f * width * Screen.height / Screen.width;
        _camera.orthographicSize = 12.7f;

        for (var i = 0; i < Level.map.Count; i++)
        {
            var levelColumn = Level.map[i];

            var holder = new Holder();

            if (GameMode == GameMode.Easy)
            {
                if (Level.no < 4)
                {
                    holder = Instantiate(_holderPrefab, firstFourLevelsPositions[i], Quaternion.identity);
                    _camera.orthographicSize = 4.45f;

                }
                else
                {
                    holder = Instantiate(_holderPrefab, easyHolderPositions[i], Quaternion.identity);
                    _camera.orthographicSize = 8f;

                }
            }
            else
            {
                holder = Instantiate(_holderPrefab, holderPositions[i], Quaternion.identity);
                _camera.orthographicSize = 9.11f;
            }
            holder.Init(levelColumn.Select(g =>
            {
                var ball = Instantiate(_ballPrefab);
                ball.GroupId = g;
                return ball;
            }));

            _holders.Add(holder);
        }
        if (Level.no < 7 && GameMode == GameMode.Easy)
            _holderaddbtn.interactable = false;
        if (GameMode != GameMode.Easy)
        {
            foreach (Holder h in _holders)
            {
                h.transform.position = new Vector3(h.transform.position.x - 2f, h.transform.position.y + 0.35f, h.transform.position.z);
            }
            Ball[] _b = FindObjectsOfType<Ball>();
            foreach (Ball b in _b)
            {
                b.transform.position = new Vector3(b.transform.position.x - 2f, b.transform.position.y + 0.35f, b.transform.position.z);
            }
        }
    }
    public void OnClickHolderAdd()
    {
        if (PlayerPrefs.GetInt("HolderAddBtnUsed") == 1)
        {
            //HolderRewardedVideoPanel.SetActive(true);
            //OnClickPause(true);
        }
        else
        {

            //var list = PositionsForHolders(_holders.Count + 1, out var width).ToList();
            //var holder = Instantiate(_holderPrefab, list[_holders.Count], Quaternion.identity);
            var holder = new Holder();
            if (GameMode == GameMode.Easy)
            {
                easyHolderPositions.Add(new Vector2((easyHolderPositions[easyHolderPositions.Count - 1].x + 1.8f), easyHolderPositions[easyHolderPositions.Count - 1].y));
                holder = Instantiate(_holderPrefab, easyHolderPositions[_holders.Count], Quaternion.identity);

            }
            else
            {
                holderPositions.Add(new Vector2((holderPositions[holderPositions.Count - 1].x + 1.8f), holderPositions[holderPositions.Count - 1].y));
                holder = Instantiate(_holderPrefab, holderPositions[_holders.Count], Quaternion.identity);
            }
            _holders.Add(holder);

            for (int i = 0; i < _holders.Count; i++)
            {
                //_holders[i].transform.position = list[i];
                if(GameMode==GameMode.Easy)
                    _holders[i].transform.position = easyHolderPositions[i];
                else
                    _holders[i].transform.position = holderPositions[i];
                _holders[i].ResetBallPositions();
            }
            //Debug.LogError("OnClickHolderAdd clicked...");
            PlayerPrefs.SetInt("HolderAddBtnUsed", 1);
            _holderaddbtn.interactable = false;
            //ResetHolderAddOption(1,true);
        }
    }
    public void OnClickUndo()
    {
        if (CurrentState != State.Playing || _undoStack.Count <= 0)
            return;

        var moveData = _undoStack.Pop();
        MoveBallFromOneToAnother(moveData.ToHolder, moveData.FromHolder);


        if (CurrentState!=State.Playing || _undoStack.Count<=0)
            return;

        //int movesLeft = PlayerPrefs.GetInt("UndoMovesLeft");
        //if (movesLeft > 0)
        //{
        //    movesLeft--;
        //    PlayerPrefs.SetInt("UndoMovesLeft", movesLeft);
        //    GamePlayPanel gp = FindObjectOfType<GamePlayPanel>();
        //    gp._undomovestxt.text = PlayerPrefs.GetInt("UndoMovesLeft").ToString();
        //    if (movesLeft == 0)
        //    {
        //        gp.undoBtn.SetActive(false);
        //        gp.undoRewardedBtn.SetActive(true);
        //    }
        //}
        //var moveData = _undoStack.Pop();
        //MoveBallFromOneToAnother(moveData.ToHolder,moveData.FromHolder);
    }

    private void Update()
    {

        if(CurrentState != State.Playing)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            var collider = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
            if (collider != null)
            {
                var holder = collider.GetComponent<Holder>();

                if (holder != null)
                    OnClickHolder(holder);
            }
        }
    }

    private void OnClickHolder(Holder holder)
    {
        var pendingHolder = _holders.FirstOrDefault(h => h.IsPending);

        if (pendingHolder != null && pendingHolder != holder)
        {
            if (holder.TopBall == null || (pendingHolder.TopBall.GroupId == holder.TopBall.GroupId && !holder.IsFull))
            {
                _undoStack.Push(new MoveData
                {
                    FromHolder = pendingHolder,
                    ToHolder = holder,
                    Ball = pendingHolder.TopBall
                });
                MoveBallFromOneToAnother(pendingHolder,holder);
                UndoBtn.interactable = true;
            }
            else
            {
                pendingHolder.IsPending = false;
                holder.IsPending = true;
            }
        }
        else
        {
            if (holder.Balls.Any())
                holder.IsPending = !holder.IsPending;
        }
    }

    private void MoveBallFromOneToAnother(Holder fromHolder,Holder toHolder)
    {
        toHolder.Move(fromHolder.RemoveTopBall());
        CheckAndGameOver();
    }

    private void CheckAndGameOver()
    {
        if (_holders.All(holder =>
        {
            var balls = holder.Balls.ToList();
            return balls.Count == 0 || balls.All(ball => ball.GroupId == balls.First().GroupId);
        }) && _holders.Where(holder => holder.Balls.Any()).GroupBy(holder => holder.Balls.First().GroupId).All(holders => holders.Count()==1)) 
        {
            OverTheGame();
        }
    }

    private void OverTheGame()
    {
        if(CurrentState!=State.Playing)
            return;

        //Debug.LogError("Game Mode : " + GameMode);

        PlayClipIfCan(_winClip);
        CurrentState = State.Over;
      
        ResourceManager.CompleteLevel(GameMode,Level.no);
        PlayerPrefs.SetString("DisplayAd", "NO");

        string lastlevelplayed = "";
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
            lastlevelplayed = "EasyLastLevelPlayed";
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
            lastlevelplayed = "ChallangeLastLevelPlayed";
        else if (PlayerPrefs.GetInt("SelectedMode") == 2)
            lastlevelplayed = "ExpertLastLevelPlayed";

        PlayerPrefs.SetInt(lastlevelplayed, Level.no);

        //int adcounter = PlayerPrefs.GetInt("GamePlayCurrentAdsCounter");
        //PlayerPrefs.SetInt("GamePlayCurrentAdsCounter", adcounter + 1);
        //Debug.LogError(adcounter);
        //if (adcounter % ShowAd.Instance.GamePlayAdDisplayCounter == 0)
        //    PlayerPrefs.SetString("DisplayAd", "YES");
        LevelCompleted?.Invoke();


    }

    private void PlayClipIfCan(AudioClip clip,float volume=0.35f)
    {
        if(!AudioManager.IsSoundEnable || clip ==null)
            return;
        AudioSource.PlayClipAtPoint(clip,Camera.main.transform.position,volume);
    }

    public IEnumerable<Vector2> PositionsForHolders(int count, out float expectWidth)
    {
        expectWidth = 4 * _minXDistanceBetweenHolders;
        if (count <= 6)
        {
            var minPoint = transform.position - ((count - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.right - Vector3.up*1f;

            expectWidth = Mathf.Max(count * _minXDistanceBetweenHolders, expectWidth);

            return Enumerable.Range(0, count)
                .Select(i => (Vector2) minPoint + i * _minXDistanceBetweenHolders * Vector2.right);
        }

        var aspect = (float) Screen.width / Screen.height;

        var maxCountInRow = Mathf.CeilToInt(count / 2f);
        //var maxCountInRow = 5;
        if (GameMode == GameMode.Hard)
            maxCountInRow = 5;
        if ((maxCountInRow + 1) * _minXDistanceBetweenHolders > expectWidth)
        {
            expectWidth = (maxCountInRow + 1) * _minXDistanceBetweenHolders;
        }
        //Debug.LogError(GameMode);
        if (GameMode != GameMode.Hard)
        {

            var height = expectWidth / aspect;
            var list = new List<Vector2>();
            var topRowMinPoint = transform.position + Vector3.up * height / 6f -
                                 ((maxCountInRow - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
            list.AddRange(Enumerable.Range(0, maxCountInRow)
                .Select(i => (Vector2)topRowMinPoint + i * _minXDistanceBetweenHolders * Vector2.right));

            var lowRowMinPoint = transform.position - Vector3.up * height / 6f -
                                 (((count - maxCountInRow) - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
            list.AddRange(Enumerable.Range(0, count - maxCountInRow)
                .Select(i => (Vector2)lowRowMinPoint + i * _minXDistanceBetweenHolders * Vector2.right));
            return list;

        }
        else
        {
            var height = expectWidth / aspect;
            var list = new List<Vector2>();
            var topRowMinPoint = transform.position + Vector3.up * height / 6f -
                                 ((maxCountInRow - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
            topRowMinPoint = new Vector3(-3.6f, 2.2f, 0.0f);
            //Debug.LogError(topRowMinPoint);

            list.AddRange(Enumerable.Range(0, maxCountInRow)
                .Select(i => (Vector2)topRowMinPoint + i * _minXDistanceBetweenHolders * Vector2.right));

            var lowRowMinPoint = transform.position - Vector3.up * height / 6f -
                                 ((maxCountInRow - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;
            lowRowMinPoint = new Vector3(-3.6f, 2.2f - 6.4f, 0.0f);

            //Debug.LogError(lowRowMinPoint);
            list.AddRange(Enumerable.Range(0, maxCountInRow)
                .Select(i => (Vector2)lowRowMinPoint + i * _minXDistanceBetweenHolders * Vector2.right));

            var lowRowEndPoint = transform.position + Vector3.up * height / 6f + lowRowMinPoint * 1.5f -
             (((count - maxCountInRow) - 1) / 2f) * _minXDistanceBetweenHolders * Vector3.right - Vector3.up * 1f;

            lowRowEndPoint = new Vector3(-3.6f, 2.2f - 6.4f - 6.4f, 0.0f);

            //Debug.LogError(lowRowEndPoint);

            list.AddRange(Enumerable.Range(0, count - maxCountInRow * 2)
                .Select(i => (Vector2)lowRowEndPoint + i * _minXDistanceBetweenHolders * Vector2.right));
            return list;

        }

    }


    public enum State
    {
        None,Playing,Over
    }

    public struct MoveData
    {
        public Holder FromHolder { get; set; }
        public Holder ToHolder { get; set; }
        public Ball Ball { get; set; }
    }
}

[Serializable]
public struct LevelGroup:IEnumerable<Level>
{
    public List<Level> levels;
    public IEnumerator<Level> GetEnumerator()
    {
        return levels?.GetEnumerator() ?? Enumerable.Empty<Level>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[Serializable]
public struct Level
{
    public int no;
    public List<LevelColumn> map;
}

[Serializable]
public struct LevelColumn : IEnumerable<int>
{
    public List<int> values;

    public IEnumerator<int> GetEnumerator()
    {
        return values?.GetEnumerator() ?? Enumerable.Empty<int>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}