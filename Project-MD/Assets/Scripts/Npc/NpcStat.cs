using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NPC 스탯 관리.
public class NpcStat : MonoBehaviour
{
    public NpcDatabaseSO npc_database;

    [SerializeField]
    private string names;
    [SerializeField]
    private int jin_Id;
    public float productivity;
    [SerializeField]
    private float W_affinity;
    [SerializeField]
    private float F_affinity;
    [SerializeField]
    private float E_affinity;
    [SerializeField]
    private float G_affinity;
    [SerializeField]
    public int level = 1; // 정령 1레벨 초기화.
   [SerializeField]
    private float hunger_Req;
    [SerializeField]
    private float levelingexp; // 레벨업에 필요한 경험치
    [SerializeField]
    private string personality;
    [SerializeField]
    private string D1;
    [SerializeField]
    private string D2;
    [SerializeField]
    private string D3;

    public float nowexp; // 정령 현재 경험친
    private GameObject npc_UI;
    private Npc_Info_UI npc_info;
    private Npc_datamanager npcdata;
    private Npc_dataLoader npcLoader;
    private Level_Data balance;
    List<float> affinity = new List<float>();

    int[] firstMaxProbablities = { 5, 15, 30, 60, 5 };
    int[] secondMaxProbablities = { 5, 15, 60, 15, 5 };
    //int[] elseProbablities = { 5, 60, 30, 5 };

    public int PID; // 유일 키
    void Start()
    {        
        npc_info = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Npc_Info_UI>();    // 정령 UI.
        npcLoader = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_dataLoader>(); // 정령 interface 호출.
        npcdata = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_datamanager>();  //전체 정령 관리 스크립트.
        npcdata.Setaschild(this.gameObject);

        attribute_Affinity();
        GetWeightedRandom();
        set_JinInfo(); 

        hunger_Req = npcLoader.levelDatas[level].hunger;
        levelingexp = npcLoader.levelDatas[level + 1].ReqLev;
    }

    void Update()
    {
        control_Leveling();
    }

    public void SetPrimaryID(int num)
    {
        Debug.Log("SetPID func");
        this.PID = num;
    }
    public int GetPrimaryID()
    {
        return PID;
    }

    private void control_Leveling()
    {
        if(nowexp >= levelingexp)
        {
            nowexp = 0;
            level++;
            levelingexp = npcLoader.levelDatas[level + 1 ].ReqLev;
            npc_info.get_Values(names, level, personality, productivity, hunger_Req, levelingexp, W_affinity, F_affinity, E_affinity, G_affinity, D1,D2,D3, this.gameObject);
         }
    }

    // 미구현
    public void UI_addExp(float _exp)
    {
        nowexp += _exp;
        npc_info.gain_Exp(nowexp);
    }

    // 정령 생성시 데이터 호출 및 저장.
    public void Get_Infovalue(int _number, string _slimeName)
    {
        names = _slimeName; //진화 1폼이름으로 설정
        jin_Id = _number;
    }
    
    public void set_JinInfo()
    {
        personality = npcLoader.npcDatas[jin_Id - 1].personality;
        D1 = npcLoader.npcDatas[jin_Id - 1].D1;
        D2 = npcLoader.npcDatas[jin_Id - 1].D2;
        D3 = npcLoader.npcDatas[jin_Id - 1].D3;
    }

    // 기본 특성값 부여.
    private void attribute_Affinity()
    {
        string input = npcLoader.npcDatas[jin_Id - 1].EF1;
        
        // 문자열의 첫번째로 정령의 카테고리를 분류.
        char firstChar = input[0];
        //Debug.Log("슬라임의 첫번쨰 글자는 : " + firstChar);
        int classification;

        switch (firstChar)
        {
            case 'W':
                classification = 0;
              
                break;

            case 'F':
                classification = 1;
                
                break;

            case 'E':
                classification = 2;
                
                break;

            case 'G':
                classification = 3;
                
                break;

            default:
                classification = -1; break;

        }

        if(classification % 2 == 0)
        {
            if(classification == 0)
            {
                W_affinity = 7f;
                E_affinity = 5f;
            }
            else
            {
                E_affinity = 7f;
                W_affinity = 5f;
            }

            F_affinity = 3f;
            G_affinity = 3f;

            //Debug.Log("물, 불, 전기, 땅 특성값은 :" + W_affinity + "," + F_affinity + ", " + E_affinity + ", " + G_affinity);
            affinity.Add(W_affinity);
            affinity.Add(F_affinity);
            affinity.Add(E_affinity);
            affinity.Add(G_affinity);
            return;
        }
        else
        {
            if(classification == 1)
            {
                F_affinity = 7f;
                G_affinity = 5f;
            }
            else
            {
                G_affinity = 7f;
                F_affinity = 5f;
            }

            W_affinity = 3f;
            E_affinity = 3f;

            affinity.Add(W_affinity);
            affinity.Add(F_affinity);
            affinity.Add(E_affinity);
            affinity.Add(G_affinity);

           // Debug.Log("물, 불, 전기, 땅 특성값은 :" + W_affinity + "," + F_affinity + ", " + E_affinity + ", " + G_affinity);
            return;
        }

    }
    
    private List<int> FindMaxAndSecondMax(List<float> numbers)
    {
        float max = int.MinValue;
        float secondMax = int.MinValue;
        int maxIndex = -1;
        int secondMaxIndex = -1;

        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] > max)
            {
                secondMax = max;
                secondMaxIndex = maxIndex;

                max = numbers[i];
                maxIndex = i;
            }
            else if (numbers[i] > secondMax && numbers[i] != max)
            {
                secondMax = numbers[i];
                secondMaxIndex = i;
            }
        }

        List<int> result = new List<int> { maxIndex, secondMaxIndex }; // Max값, 두번째 값.
        return result;
    }

    // 추가 특성값 부여.
    private void GetWeightedRandom()
    {
        //Debug.Log("특성의 총 갯수는 : " + affinity.Count);
        
        List<int> result = FindMaxAndSecondMax(affinity);
        int Maxindex = result[0];
        int secondMaxindex = result[1];
        //Debug.Log("리스트 가장 큰, 둘쨰 " +  Maxindex +", "+ secondMaxindex);
        int maxAddValue = 0;
        int addValue = GetMaxWeightedRandoms(maxAddValue, firstMaxProbablities);
        int secondAddValue = 0;
        int add2Value = GetsecondWeightedRandoms(secondAddValue, secondMaxProbablities);

        //Debug.Log("주스탯, 부스탯 더한값 :" + addValue + ", " + add2Value);
        affinity[Maxindex] += addValue;
        affinity[secondMaxindex] += add2Value;

        W_affinity = affinity[0];
        F_affinity = affinity[1];
        E_affinity = affinity[2];
        G_affinity = affinity[3];
    }
    
    // 주스탯 합산값.
    private int GetMaxWeightedRandoms(int currentValue, int[] probabilities) 
    {
        int randomNumber = Random.Range(0, 100); // 1부터 100까지의 난수

        // 확률에 따라 값을 조정
        if (randomNumber <= probabilities[0])
        {
            return currentValue + 3;
        }
        else if (randomNumber <= probabilities[1])
        {
            return currentValue + 2;
        }
        else if (randomNumber <= probabilities[2])
        {
            return currentValue + 1;
        }
        else if (randomNumber <= probabilities[3])
        {
            return currentValue;
        }
        else
        {
            return currentValue - 1;
        }

    }
    // 부스탯 합산값.
    private int GetsecondWeightedRandoms(int currentValue, int[] probabilities)
    {
        int randomNumber = Random.Range(0, 100); // 1부터 100까지의 난수

        // 확률에 따라 값을 조정
        if (randomNumber <= probabilities[0])
        {
            return currentValue + 2;
        }
        else if (randomNumber <= probabilities[1])
        {
            return currentValue + 1;
        }
        else if (randomNumber <= probabilities[2])
        {
            return currentValue + 0;
        }
        else if (randomNumber <= probabilities[3])
        {
            return currentValue -1;
        }
        else
        {
            return currentValue - 2;
        }

    }
    private int GetelseWeightedRandoms(int currentValue, int[] probabilities)
    {
        int randomNumber = Random.Range(1, 101); // 1부터 100까지의 난수

        // 확률에 따라 값을 조정
        if (randomNumber <= probabilities[0])
        {
            return currentValue + 1;
        }
        else if (randomNumber <= probabilities[1])
        {
            return currentValue + 0;
        }
        else if (randomNumber <= probabilities[2])
        {
            return currentValue - 1;
        }
        else
        {
            return currentValue - 2;
        }

    }    
    #region Get/Set 함수
    public string GetName()
    {
        return names;
    }
    public int GetLevel()
    {
        return level;
    }
    public string GetPersonality()
    {
        return personality;
    }
    public int GetJinId()
    {
        return jin_Id;
    }
    #endregion
    // 마우스 버튼을 눌렀을 때
    private void OnMouseDown()
    {
        npc_info.get_Values(names, level, personality, productivity, hunger_Req, levelingexp, W_affinity, F_affinity, E_affinity, G_affinity, D1,D2,D3, this.gameObject);

        FindAnyObjectByType<Camera>().GetComponent<CameraMove>().npc_CameraMove(this.gameObject);
    }
}
