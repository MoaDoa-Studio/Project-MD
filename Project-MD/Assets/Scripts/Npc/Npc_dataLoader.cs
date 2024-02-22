using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


// XML 파일을 불러오고 데이터 저장하는 클래스.
public class Npc_dataLoader : MonoBehaviour
{
    public string npc_Xml;
    public string levelTable_Xml;

    public List<Npc_Data> npcDatas = new List<Npc_Data>();
    public List<Level_Data> levelDatas = new List<Level_Data>();

    private void Start()
    {
        load_NpcData(npc_Xml);
        load_NpcLevelData(levelTable_Xml);
        
    }
    private void load_NpcData(string fileName)
    {
        Debug.Log("File Path: " + "Xml/Jintable");
        TextAsset xmlAsset = Resources.Load<TextAsset>("Xml/"+ fileName);
        
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlAsset.text);
      
        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//text");

        foreach (XmlNode xmlNode in xmlNodeList)
        {
            // 불러오는 데이터
            Npc_Data npcData = ScriptableObject.CreateInstance<Npc_Data>();
            npcData.name = xmlNode.SelectSingleNode("name").InnerText;
            npcData.Jin_id = int.Parse(xmlNode.SelectSingleNode("Jin_id").InnerText);
            npcData.F_affinity = int.Parse(xmlNode.SelectSingleNode("F_affinity").InnerText);
            npcData.W_affinity = int.Parse(xmlNode.SelectSingleNode("W_affinity").InnerText);
            npcData.E_affinity = int.Parse(xmlNode.SelectSingleNode("E_affinity").InnerText);
            npcData.G_affinity = int.Parse(xmlNode.SelectSingleNode("G_affinity").InnerText);
            npcData.personality = xmlNode.SelectSingleNode("personality").InnerText;
            npcData.D1 = xmlNode.SelectSingleNode("D1").InnerText;
            npcData.D2 = xmlNode.SelectSingleNode("D2").InnerText;
            npcData.D3 = xmlNode.SelectSingleNode("D3").InnerText;
            npcData.Ef1 = xmlNode.SelectSingleNode("EF1").InnerText;
            npcData.Ef1 = xmlNode.SelectSingleNode("EF2").InnerText;
            npcData.Ef1 = xmlNode.SelectSingleNode("EF3").InnerText;

            npcDatas.Add(npcData);

            /*
             * 1. 고유 ID 값
             * 2. 정령 고유 이름
             * 3. 레벨별 진화 경험치
             * 4. 레벨별 만복도 수치
             * 5. 즐거움 에너지 생산량
             * 6. 성격
               7. 정령 세부 설명
                    세부 설명에 총 5가지가 포함되어야함
             */
        }

    }

    private void load_NpcLevelData(string fileName)
    {
        TextAsset xmlAsset = Resources.Load<TextAsset>("Xml/" + fileName);

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(xmlAsset.text);

        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//text");

        foreach (XmlNode xmlNode in xmlNodeList)
        {
            // 불러오는 데이터
            Level_Data levelData = ScriptableObject.CreateInstance<Level_Data>();
            levelData.JinLev = int.Parse(xmlNode.SelectSingleNode("JinLev").InnerText);
            levelData.ReqLev = int.Parse(xmlNode.SelectSingleNode("ReqLev").InnerText);
            levelData.hunger = int.Parse(xmlNode.SelectSingleNode("hunger").InnerText);
        
            levelDatas.Add(levelData);
        }
    }
}

