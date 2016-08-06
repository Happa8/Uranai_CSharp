using System;
using System.Text.RegularExpressions;

namespace Uranai_CSharp
{
	class Program
	{
		//母音の名前の数値化
		static int nameNumber_1;
		static int nameNumber_2;
		//計算結果
		static double answer;
		//繰り返し用
		static int count;

		static void Main(string[] args)
		{
			descriptionFirst();
			switchMode();
		}

		static void descriptionFirst()
		{
			count = 0;

			Console.WriteLine("一時期はやった\"電卓だけでできる恋占い\"をC#で組んでみました");
			Console.WriteLine("【二人の名前を数値化し、相性を見る】");
			Console.WriteLine("という占いです。");
			Console.WriteLine("以下の指示に従っていただけばいけるはずです");
		}

		static void switchMode()
		{
			Console.WriteLine("名前入力モードを選択してください");
			Console.WriteLine("\t1:数値入力モード\n\t2:ローマ字入力モード\n\t3:ひらがな入力モード");
			Console.Write("Mode>");
			string ans = Console.ReadLine();
			if(Regex.IsMatch(ans, @"^[1-3]$"))
			{
				switch (ans)
				{
					case "1":
						funcScan();
						break;
					case "2":
						funcScanStr();
						break;
					case "3":
						funcScanHira();
						break;
				}
			}
			else
			{
				Console.WriteLine("入力された値が不正です\n入力しなおしてください\n");
				switchMode();
			}
		}

		//実際の計算及び数値入力
		static void funcScan()
		{
			//簡易説明
			Console.WriteLine("占う二人の名前の母音を以下の式を使い数値化します");
			Console.WriteLine("\tあ=1");
			Console.WriteLine("\tい=2");
			Console.WriteLine("\tう=3");
			Console.WriteLine("\tえ=4");
			Console.WriteLine("\tお=5");
			Console.WriteLine("\tん=0");
			Console.WriteLine("ex: 山田 太郎 → やまだたろう → ああああおう → 111153");

		//数値入力
		FIRSTNAME:
			Console.WriteLine("一人目の数値を入力してください");
			Console.Write(">");
			string nameNumber_1S = Console.ReadLine();
			if (System.Text.RegularExpressions.Regex.IsMatch(nameNumber_1S, @"^[0-5]+$"))
			{
				nameNumber_1 = int.Parse(nameNumber_1S);
			}
			else
			{
				Console.WriteLine("0~5の数字を入力してください");
				goto FIRSTNAME;
			}

		SECONDNAME:
			Console.WriteLine("二人目の数値を入力してください");
			Console.Write(">");
			string nameNumber_2S = Console.ReadLine();
			if (System.Text.RegularExpressions.Regex.IsMatch(nameNumber_2S, @"^[0-5]+$"))
			{
				nameNumber_2 = int.Parse(nameNumber_2S);
			}
			else
			{
				Console.WriteLine("0~5の数字を入力してください");
				goto SECONDNAME;
			}

			funcCalc();
		}

		//入力ローマ字版
		static void funcScanStr()
		{
			Console.WriteLine("占う二人の名前をローマ字で入力してください");
			Console.WriteLine("------注意点------");
			Console.WriteLine("・苗字と名前の間にスペースを入れないでください");
			Console.WriteLine("・「ん」は「nn」と入力してください");
			Console.WriteLine("・半角英字で入力してください");
			Console.WriteLine("・小文字で入力してください");
			Console.WriteLine("ex: 金田一耕助 → kinndaichikousuke");

		FIRSTNAME:
			Console.WriteLine("一人目の名前を入力してください");
			Console.Write(">");
			string name1S = Console.ReadLine();
			if(Regex.IsMatch(name1S, @"^[A-Za-z]+$"))
			{
				MatchCollection RegixCollection1 = Regex.Matches(name1S, @"([aiueo]|nn)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				string answerS = "";
				foreach (Match tmp in RegixCollection1)
				{
					switch (tmp.Value.ToString())
					{
						case "a":
							answerS = answerS + "1";
							break;
						case "i":
							answerS = answerS + "2";
							break;
						case "u":
							answerS = answerS + "3";
							break;
						case "e":
							answerS = answerS + "4";
							break;
						case "o":
							answerS = answerS + "5";
							break;
						case "nn":
							answerS = answerS + "0";
							break;
						default:
							answerS = answerS + "0";
							break;
					}
				}
				nameNumber_1 = int.Parse(answerS);
			}
			else
			{
				Console.WriteLine("入力された値が不正です\n入力しなおしてください\n");
				goto FIRSTNAME;
			}

		SECONDNAME:
			Console.WriteLine("二人目の名前を入力してください");
			Console.Write(">");
			string name2S = Console.ReadLine();
			if (Regex.IsMatch(name2S, @"^[A-Za-z]+$"))
			{
				MatchCollection RegixCollection2 = Regex.Matches(name2S, @"([aiueo]|nn)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
				string answerS = "";
				foreach (Match tmp in RegixCollection2)
				{
					switch (tmp.Value.ToString())
					{
						case "a":
							answerS = answerS + "1";
							break;
						case "i":
							answerS = answerS + "2";
							break;
						case "u":
							answerS = answerS + "3";
							break;
						case "e":
							answerS = answerS + "4";
							break;
						case "o":
							answerS = answerS + "5";
							break;
						case "nn":
							answerS = answerS + "0";
							break;
						default:
							answerS = answerS + "0";
							break;
					}
				}
				nameNumber_2 = int.Parse(answerS);
			}
			else
			{
				Console.WriteLine("入力された値が不正です\n入力しなおしてください\n");
				goto SECONDNAME;
			}

			funcCalc();
		}

		static int convertHiraganaToNum(string name)
		{
			name = Regex.Replace(name, @".[ぁゃゎ]", "1");
			name = Regex.Replace(name, @".ぃ", "2");
			name = Regex.Replace(name, @".[ぅゅ]", "3");
			name = Regex.Replace(name, @".ぇ", "4");
			name = Regex.Replace(name, @".[ぉょ]", "5");
			name = Regex.Replace(name, @"っ", "");
			name = Regex.Replace(name, @"[あかがさざただなはばぱまやらわ]", "1");
			name = Regex.Replace(name, @"[いきぎしじちぢにひびぴみりゐ]", "2");
			name = Regex.Replace(name, @"[うくぐすずつづぬふぶぷむゆる]", "3");
			name = Regex.Replace(name, @"[えけげせぜてでねへべぺめれゑ]", "4");
			name = Regex.Replace(name, @"[おこごそぞとどのほぼぽもよろを]", "5");
			name = Regex.Replace(name, @"ん", "0");
			int res = int.Parse(name);
			return res;
		}

		static void funcScanHira()
		{
			Console.WriteLine("占う二人の名前をひらがな入力してください");

		FIRSTNAME:
			Console.WriteLine("一人目の名前を入力してください");
			Console.Write(">");
			string name1S = Console.ReadLine();
			if(Regex.IsMatch(name1S, @"^[ぁ-ゞ]+$"))
			{
				nameNumber_1 = convertHiraganaToNum(name1S);
			}
			else
			{
				Console.WriteLine("入力された値が不正です\n入力しなおしてください\n");
				goto FIRSTNAME;
			}

		SECONDNAME:
			Console.WriteLine("二人目の名前を入力してください");
			Console.Write(">");
			string name2S = Console.ReadLine();
			if (Regex.IsMatch(name2S, @"^[ぁ-ゞ]+$"))
			{
				nameNumber_2 = convertHiraganaToNum(name2S);
			}
			else
			{
				Console.WriteLine("入力された値が不正です\n入力しなおしてください\n");
				goto SECONDNAME;
			}

			funcCalc();
		}

		static void funcCalc()
		{
			//計算
			answer = nameNumber_1 + nameNumber_2;

			while(answer > 1)
			{
				answer /= 2;
			}

			//パーセンテージ化
			answer *= 100;
			answer = Math.Round(answer);

			//結果発表
			Console.WriteLine("二人の相性は" + answer + "％です\n");

			askContinue();

		}

		static void askContinue()
		{
			Console.WriteLine("続けますか？");
			Console.WriteLine("うん、もっとやりたい:0");
			Console.WriteLine("いいや、もううんざり:1");
			Console.Write("Your Answer:");
			count = int.Parse(Console.ReadLine());

			if (count == 0)
			{
				Console.WriteLine("それじゃ、再び始めますね\n");
				switchMode();
			}else if (count == 1)
			{
				Console.WriteLine("Thank you for using this app");
				Console.WriteLine("See you again");

				System.Threading.Thread.Sleep(2000);
			}else
			{
				Console.WriteLine("正しい文字を入力してください");
				askContinue();
			}
		}
	}
}
