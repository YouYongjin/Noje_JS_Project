using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //����Ƽ UI ����
using UnityEngine.Networking; //����Ƽ Networking ���
public class basicMain : MonoBehaviour
{
    public Button Hello;    //Hello ��ư ����
    public string host;     // �ּ� ���� ����
    public int port;        //��Ʈ ��ȣ ���� (0 ~ 25000)

    void Start()
    {
        this.Hello.onClick.AddListener(() =>
        {
            var url = string.Format("{0}:{1}/", host, port);
            Debug.Log(url);
            StartCoroutine(this.GetBasic(url, (raw) =>
            {
                Debug.LogFormat("{0}", raw);
            }));
        });

    }
    private IEnumerator GetBasic(string url, System.Action<string>callback)
    {
        var webRequest = UnityWebRequest.Get(url);      //Get�� ���� �� ��û URL �Լ�
        yield return webRequest.SendWebRequest();       //��û�� ���� ���ƿö����� ���

        Debug.Log("----->" + webRequest.downloadHandler.text);      // �ٿ�ε� �ڵ鷯 �ؽ�Ʈ �α�

        if(webRequest.result == UnityWebRequest.Result.ConnectionError     //���� Ŀ�ؼǿ� ������ ���� ��
         || webRequest.result == UnityWebRequest.Result.ConnectionError)     //�������ݿ� ������ ���� ��
        {
            Debug.Log("��Ʈ��ũ ȯ���� ���� �ʾ� ��� �Ұ���");
        }
        else
        {
            callback(webRequest.downloadHandler.text);
        }
    }
}
