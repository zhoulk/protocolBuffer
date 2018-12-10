//===================================================
//作    者：#AuthorName# 
//创建时间：#CreateTime#
//备    注：
//===================================================

using Google.Protobuf;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SearchRequest search = new SearchRequest();
        search.ResultPerPage = 100;
        search.PageNumber = 100;
        byte[] temp = search.ToByteArray();

        Debug.Log(search.ToString());
        Debug.Log(search.ToByteArray().Length);

        SearchRequest search2 = SearchRequest.Parser.ParseFrom(temp);
        Debug.Log(search2.PageNumber);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
