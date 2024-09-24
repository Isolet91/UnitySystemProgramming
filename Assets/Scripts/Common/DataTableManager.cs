using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehavior<DataTableManager>
{
    // 데이터 테이블 파일들이 들어있는 경로를 String값으로 설정
    private const string DATA_PATH = "DataTable";

    // 챕터 데이터 테이블 파일명을 갖는 String 변수
    private const string CHAPTER_DATA_TABLE = "ChapterDataTable";

    // 모든 챕터 데이터를 저장할 수 있는 컨테이너 즉, 자료구조를 선언
    private List<ChapterData> ChapterDataTable = new List<ChapterData>();


    protected override void Init()
    {
        base.Init();

        LoadChapterDataTable();
    }

    // 챕터데이터테이블을 로드하는 함수
    private void LoadChapterDataTable()
    {
         var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{CHAPTER_DATA_TABLE}");

        // var 타입 : 데이터 타입을 신경쓰지 않아도 알아서 변수에 있는 값을 판단,
        // 타입을 인식하는 데이터 타입
        // 지역변수로만 사용 가능
        // 반드시 선언과 동시에 초기화(그래야 실질적으로 데이터 타입이 정해지기 때문)
        // 타입이 복잡한 경우 지역변수로 사용할 때는 var을 사용해도 괜찮다

        // 테이블을 순회하면서 각 데이터를
        // ChapterData 인스턴스로 만들어서
        // ChapterDataTable 컨테이너에 넣어 줌
        foreach(var data in parsedDataTable )
        {
            var chapterData = new ChapterData
            {
                // 오브젝트 타입이라 지정된 개체의 값을 32비트 부호 있는 정수로 변환
                ChapterNo = Convert.ToInt32(data["chapter_no"]),                         
                TotalStage = Convert.ToInt32(data["total_stages"]),                      
                ChapterRewardGem = Convert.ToInt32(data["chapter_reward_gem"]),         
                ChapterRewardGold = Convert.ToInt32(data["chapter_reward_gold"]),        
            };

            ChapterDataTable.Add(chapterData);
        }
    }

    // 이렇게 로드한 챕터데이터테이블에서 찾고자 하는 챕터데이터만 가져오는 함수
    public ChapterData GetChapterData(int chapterNo)
    {
        // 특정 챕터 넘버로 챕터 데이터 테이블을 검색해서
        // 그 챕터 넘버에 해당하는 데이터를 반환하는 함수
        // 링크 사용 -> 링크 : 검색, 변경을 좀 더 용이하게 해주는 기능
        // 만약 링크를 사용하지 않는다면
        /*foreach(var item in ChapterDataTable)
        {
            if(item.ChapterNo == chapterNo)
            {
                return item;
            }
        }
        return null;
        */
        
        // Linq
        // .Where 조건식이 true인 요소만 필터링
        // FirstOrDefault() 함수 : 매개변수가 생략된 경우 컬렉션의 첫 번째 요소를 반환
        return ChapterDataTable.Where(_ => _.ChapterNo == chapterNo).FirstOrDefault();
    }
}

public class ChapterData
{
    public int ChapterNo;               // 챕터 넘버
    public int TotalStage;              // 챕터 내 스테이지 개수
    public int ChapterRewardGem;        // 챕터를 클리어 했을 시 받게 되는 보석
    public int ChapterRewardGold;       // 챕터를 클리어 했을 시 받게 되는 골드
}
