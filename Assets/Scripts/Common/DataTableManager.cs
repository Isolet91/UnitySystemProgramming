using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataTableManager : SingletonBehavior<DataTableManager>
{
    // ������ ���̺� ���ϵ��� ����ִ� ��θ� String������ ����
    private const string DATA_PATH = "DataTable";

    // é�� ������ ���̺� ���ϸ��� ���� String ����
    private const string CHAPTER_DATA_TABLE = "ChapterDataTable";

    // ��� é�� �����͸� ������ �� �ִ� �����̳� ��, �ڷᱸ���� ����
    private List<ChapterData> ChapterDataTable = new List<ChapterData>();


    protected override void Init()
    {
        base.Init();

        LoadChapterDataTable();
    }

    // é�͵��������̺��� �ε��ϴ� �Լ�
    private void LoadChapterDataTable()
    {
         var parsedDataTable = CSVReader.Read($"{DATA_PATH}/{CHAPTER_DATA_TABLE}");

        // var Ÿ�� : ������ Ÿ���� �Ű澲�� �ʾƵ� �˾Ƽ� ������ �ִ� ���� �Ǵ�,
        // Ÿ���� �ν��ϴ� ������ Ÿ��
        // ���������θ� ��� ����
        // �ݵ�� ����� ���ÿ� �ʱ�ȭ(�׷��� ���������� ������ Ÿ���� �������� ����)
        // Ÿ���� ������ ��� ���������� ����� ���� var�� ����ص� ������

        // ���̺��� ��ȸ�ϸ鼭 �� �����͸�
        // ChapterData �ν��Ͻ��� ����
        // ChapterDataTable �����̳ʿ� �־� ��
        foreach(var data in parsedDataTable )
        {
            var chapterData = new ChapterData
            {
                // ������Ʈ Ÿ���̶� ������ ��ü�� ���� 32��Ʈ ��ȣ �ִ� ������ ��ȯ
                ChapterNo = Convert.ToInt32(data["chapter_no"]),                         
                TotalStage = Convert.ToInt32(data["total_stages"]),                      
                ChapterRewardGem = Convert.ToInt32(data["chapter_reward_gem"]),         
                ChapterRewardGold = Convert.ToInt32(data["chapter_reward_gold"]),        
            };

            ChapterDataTable.Add(chapterData);
        }
    }

    // �̷��� �ε��� é�͵��������̺��� ã���� �ϴ� é�͵����͸� �������� �Լ�
    public ChapterData GetChapterData(int chapterNo)
    {
        // Ư�� é�� �ѹ��� é�� ������ ���̺��� �˻��ؼ�
        // �� é�� �ѹ��� �ش��ϴ� �����͸� ��ȯ�ϴ� �Լ�
        // ��ũ ��� -> ��ũ : �˻�, ������ �� �� �����ϰ� ���ִ� ���
        // ���� ��ũ�� ������� �ʴ´ٸ�
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
        // .Where ���ǽ��� true�� ��Ҹ� ���͸�
        // FirstOrDefault() �Լ� : �Ű������� ������ ��� �÷����� ù ��° ��Ҹ� ��ȯ
        return ChapterDataTable.Where(_ => _.ChapterNo == chapterNo).FirstOrDefault();
    }
}

public class ChapterData
{
    public int ChapterNo;               // é�� �ѹ�
    public int TotalStage;              // é�� �� �������� ����
    public int ChapterRewardGem;        // é�͸� Ŭ���� ���� �� �ް� �Ǵ� ����
    public int ChapterRewardGold;       // é�͸� Ŭ���� ���� �� �ް� �Ǵ� ���
}
