using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Score : MonoBehaviour
{
    public List<int> score;

    // Start is called before the first frame update
    void Start()
    {        

        //WriteToFile("scoreData.txt", score); // ������ ������ � ����
       score = ReadFromFile("scoreData.txt"); // ������ ������ �� �����

        if (score == null)
            score = new List<int>();
        DisplayScore();

    }

    public void WriteToFile(string fileName)
    {
        // �������� ���� � ����� � ����� ���������� ������
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // ����������� ������ ����� � ������ �����
        string[] lines = score.Select(number => number.ToString()).ToArray();

        // ���������� ���������� � ����
        File.WriteAllLines(path, lines);

        Debug.Log($"Data written to file at {path}");
    }

    public List<int> ReadFromFile(string fileName)
    {
        // �������� ���� � �����
        string path = Path.Combine(Application.persistentDataPath, fileName);

        // ���������, ���������� �� ����
        if (File.Exists(path))
        {
            // ������ ������ �� ����� � ������
            string[] lines = File.ReadAllLines(path);

            // ����������� ������ ������ � ����� � ������� ������
            List<int> scores = lines.Select(line => int.Parse(line)).ToList();

            Debug.Log("Data read from file: " + string.Join(", ", scores));
            return scores;
        }
        else
        {
            Debug.LogWarning("File not found at path: " + path);
            return null;
        }
    }


    public string DisplayScore()
    {
        // ��������� ������ �� ��������
        score.Sort((a, b) => b.CompareTo(a));

        // ����� ��� 10 �������� (��� ������, ���� � ������ ������ 10 ���������)
        List<int> topScores = score.Take(10).ToList();

        string result = string.Join("\n", topScores);

        Debug.Log("Top 10 Scores from file:");
        Debug.Log(result);

        return result;
    }

}
