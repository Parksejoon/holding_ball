using System.IO;
using System.Collections;

public class Indexer
{
	// 일반
	private Parser	parser;
	private string	dataPath = "Assets/Resources/Data/ColorIndex.txt";


	// 생성자
	public Indexer()
	{
		parser = new Parser();
	}

	// 인덱스 저장
	public void SetColorIndex(int index)
	{
		FileStream		fs = new FileStream(dataPath, FileMode.Open);
		StreamReader	sr = new StreamReader(fs);

		string source = sr.ReadLine();
		for (int i = 0; i < index; i++)
		{
			source = sr.ReadLine();
		}

		string[] indexes = source.Split();

		parser.SetColorIndex(int.Parse(indexes[0]), int.Parse(indexes[1]), int.Parse(indexes[2]), int.Parse(indexes[3]), int.Parse(indexes[4]));
	}
}
