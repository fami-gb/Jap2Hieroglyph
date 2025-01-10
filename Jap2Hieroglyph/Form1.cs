using System.Windows.Forms;
using System.Linq;
using System.Text.Encodings;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Jap2Hieroglyph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

         Hieroglyph Hiero = new Hieroglyph();

        private void lang_jap_TextChanged(object sender, EventArgs e)
        {
            if (lang_jap.ReadOnly == true) return;
            string output = "";
            foreach (char c in lang_jap.Text)
            {
                bool canConvert = Hiero.dic_hiero.ContainsKey(c.ToString());
                output += canConvert ? Hiero.dic_hiero[c.ToString()] : c.ToString();
            }
            lang_hiero.Text = output;
        }

        private void lang_hiero_TextChanged(object sender, EventArgs e)
        {
            if (lang_hiero.ReadOnly == true) return;
            string getHiero = "";
            StringBuilder output = new StringBuilder();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding shiftjisEnco = Encoding.GetEncoding("shift_jis");
            int HieroByteCount = shiftjisEnco.GetByteCount("𓄿");
            int CurrentIndex = 0;
            int i = 0;
            int NumCnt = lang_hiero.Text.Count(char.IsDigit);
            int AlphabetCnt = lang_hiero.Text.Count(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));

            while (i < (lang_hiero.Text.Length + NumCnt + AlphabetCnt)/HieroByteCount)
            {
                // ヒエログリフは最大三つのコードポイントで構成されているため、総当たりで変換できるか確認する。
                if (Contains1byteChar(lang_hiero.Text.Substring(CurrentIndex, 1))) // 数字orアルファベットかどうか
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, 1); 
                    CurrentIndex++;
                }
                else if (ContainsJap(lang_hiero.Text.Substring(CurrentIndex, 2))) // 日本語かどうか
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, 2);
                    CurrentIndex += 2;
                }
                else if (isConvert2Hiero(CurrentIndex, HieroByteCount)) // 1つのコードポイントで構成される場合
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, HieroByteCount*1);
                    CurrentIndex += HieroByteCount;
                }
                else if (isConvert2Hiero(CurrentIndex, HieroByteCount*2)) // 2つのコードポイントで構成される場合
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, HieroByteCount*2);
                    CurrentIndex += HieroByteCount*2;
                }
                else if (isConvert2Hiero(CurrentIndex, HieroByteCount*3)) // 3つのコードポイントで構成される場合
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, HieroByteCount*3);
                    CurrentIndex += HieroByteCount*3;
                }

                // 取得した日本語が辞書に存在するか確認し、存在する場合はヒエログリフに変換する
                if (Hiero.dic_hiero.ContainsValue(getHiero))
                    output.Append(Hiero.dic_hiero.First(x => x.Value.Equals(getHiero)).Key);
                else
                    output.Append(getHiero);
                i++;
            }
            lang_jap.Text = output.ToString();
        }

        bool isConvert2Hiero(int startIndex, int length)
        {
            return Hiero.dic_hiero.ContainsValue(lang_hiero.Text.Substring(startIndex, length));
        }

        bool ContainsJap(string str)
        {
            // 漢字の範囲を定義する正規表現パターン
            string patternKanji = @"\p{IsCJKUnifiedIdeographs}";
            // ひらがなの正規表現パターン
            string patternHiragana = "[\u3040-\u309F]";
            // カタカナの正規表現パターン
            string patternKatakana = "[\u30A0-\u30FF]";

            // 正規表現を使用して一致を確認
            return Regex.IsMatch(str, patternKanji)    ||
                   Regex.IsMatch(str, patternHiragana) ||
                   Regex.IsMatch(str, patternKatakana);
        }

        bool Contains1byteChar(string str)
        {
            // アルファベットの正規表現パターン
            string patternAlphabet = "[A-Za-z]";
            // 数字の正規表現パターン
            string patternNum = "[0-9]";

            // 正規表現を使用して一致を確認
            return Regex.IsMatch(str, patternAlphabet) ||
                   Regex.IsMatch(str, patternNum);
        }

        /*private string ToCodePoint(string hiero)
        {
            todo:
             ・まず文字列を受け取る際に、ヒエログリフに変換できない文字を取り除く。-> ifで変換の可不可を確認し取り除く。
             ・変換方法は文字コードから上の辞書へ逆引きを行い変換。
             
        }*/

        private void btn_SwitchLang_Click(object sender, EventArgs e)
        {
            lang_jap.ReadOnly = !lang_jap.ReadOnly;
            lang_hiero.ReadOnly = !lang_hiero.ReadOnly;
        }
    }

}