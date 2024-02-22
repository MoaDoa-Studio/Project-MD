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
    [SerializeField]
    private float W_affinity;
    [SerializeField]
    private float F_affinity;
    [SerializeField]
    private float E_affinity;
    [SerializeField]
    private float G_affinity;
    [SerializeField]
    private int level = 1; // 정령 1레벨 초기화.
    
    private string state;
    [SerializeField]
    private float hunger;
    [SerializeField]
    private float levelingexp;

    private NpcAI npcAI;
    private GameObject prefab;
    private Npc_Info_UI npcInfo;
    private Npc_datamanager npcdata;
    private Npc_dataLoader npcLoader;
    private Level_Data balance;
    List<float> affinity = new List<float>();

    int[] firstMaxProbablities = { 5, 15, 30, 60, 5 };
    int[] secondMaxProbablities = { 5, 15, 60, 15, 5 };
    int[] elseProbablities = { 5, 60, 30, 5 };

    float firstMax = int.MinValue;
    float secondMax = int.MinValue;


    // Start is called before the first frame update
    void Start()
    {
        npcLoader = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_dataLoader>();
       // balance = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Level_Data>();
       
        npcdata = GameObject.FindGameObjectWithTag("NpcManager").GetComponent<Npc_datamanager>();
       
        npcdata.Setaschild(this.gameObject);

        // 특성값을 만들어내는 함수.
        attribute_Affinity();
        GetWeightedRandom();


        hunger = npcLoader.levelDatas[1].hunger;
        levelingexp = npcLoader.levelDatas[1].ReqLev;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 정령 생성시 데이터 호출 및 저장.
    public void Get_Infovalue(int _number, string _slimeName)
    {
        names = _slimeName; //진화 1폼이름으로 설정
        jin_Id = _number;
    }
        
    

    // 기본 특성값 부여.
    private void attribute_Affinity()
    {
        string input = npcLoader.npcDatas[jin_Id].EF1;
        
        // 문자열의 첫번째로 정령의 카테고리를 분류.
        char firstChar = input[0];
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
            G_affinity = 3f;

            affinity.Add(W_affinity);
            affinity.Add(F_affinity);
            affinity.Add(E_affinity);
            affinity.Add(G_affinity);

            return;
        }

    }

    // 추가 특성값 부여.
    private void GetWeightedRandom()
    {
        for (int i = 0; i < affinity.Count; i++)
        {
            float num = affinity[i];

            if (num > firstMax)
            {
                secondMax = firstMax;
                firstMax = num;

                int currentValue = 0;
                int newValue = GetMaxWeightedRandoms(currentValue, firstMaxProbablities); 
                firstMax += newValue;
            }
            else if (num > secondMax && num != firstMax)
            {
                secondMax = num;

                int currentValue = 0;
                int newValue = GetsecondWeightedRandoms(currentValue, secondMaxProbablities);
                secondMax += newValue;
            }
            else
            {
                int currentValue = 0;
                int newValue = GetelseWeightedRandoms(currentValue, elseProbablities);
                // 이 값을 어떻게 사용하실지는 여기서부터는 명확하지 않습니다. 
                num += newValue;
            }

        }
    }


    // 주스탯 합산값.
    private int GetMaxWeightedRandoms(int currentValue, int[] probabilities) 
    {
        int randomNumber = Random.Range(1, 101); // 1부터 100까지의 난수

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
        int randomNumber = Random.Range(1, 101); // 1부터 100까지의 난수

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
   

    private void OnMouseDown()
    {
        
    }
}
