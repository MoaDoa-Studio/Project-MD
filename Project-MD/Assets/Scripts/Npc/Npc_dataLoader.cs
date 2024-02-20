using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;


// XML ������ �ҷ����� ������ �����ϴ� Ŭ����.
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
            // �ҷ����� ������
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

}

