using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


// XML 파일을 불러오고 데이터 저장하는 클래스.
public class Npc_dataLoader : MonoBehaviour
{
    public string npc_Xml;

    private void Start()
    {
        load_NpcDAta(npc_Xml);
    }
    private void load_NpcDAta(string fileName)
    {
        TextAsset xmlAsset = Resources.Load<TextAsset>(fileName);

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(xmlAsset.text);

        XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//text");

        foreach (XmlNode xmlNode in xmlNodeList)
        {
            // 불러오는 데이터
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

}

