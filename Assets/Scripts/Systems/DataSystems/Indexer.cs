using System.IO;

public class Indexer
{
	// 일반 변수
	private Parser	parser;             // 파서
	private string  dataPath;			// 데이터 경로


	// 생성자
	public Indexer()
	{
		parser = new Parser();
		dataPath = parser.PathForFile() + "/Resources/Data/ColorIndex.txt";
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
