using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


// XML ������ �ҷ����� ������ �����ϴ� Ŭ����.
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
            // �ҷ����� ������
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
             * 1. ���� ID ��
             * 2. ���� ���� �̸�
             * 3. ������ ��ȭ ����ġ
             * 4. ������ ������ ��ġ
             * 5. ��ſ� ������ ���귮
             * 6. ����
               7. ���� ���� ����
                    ���� ���� �� 5������ ���ԵǾ����
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
            // �ҷ����� ������
            Level_Data levelData = ScriptableObject.CreateInstance<Level_Data>();
            levelData.JinLev = int.Parse(xmlNode.SelectSingleNode("JinLev").InnerText);
            levelData.ReqLev = int.Parse(xmlNode.SelectSingleNode("ReqLev").InnerText);
            levelData.hunger = int.Parse(xmlNode.SelectSingleNode("hunger").InnerText);
        
            levelDatas.Add(levelData);
        }
    }
}

