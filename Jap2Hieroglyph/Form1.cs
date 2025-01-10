using System.Windows.Forms;
using System.Linq;
using System.Text.Encodings;
using System.Text;
using System.Globalization;
using System.Diagnostics;

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

            while (i < lang_hiero.Text.Length/HieroByteCount)
            {
                // ヒエログリフは最大三つのコードポイントで構成されているため、総当たりで変換できるか確認する。
                if (isJapContained(CurrentIndex, HieroByteCount)) // 1つのコードポイントで構成される場合
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, HieroByteCount*1);
                    CurrentIndex += HieroByteCount;
                }
                else if (isJapContained(CurrentIndex, HieroByteCount*2)) // 2つのコードポイントで構成される場合
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, HieroByteCount*2);
                    CurrentIndex += HieroByteCount*2;
                }
                else if (isJapContained(CurrentIndex, HieroByteCount*3)) // 3つのコードポイントで構成される場合
                {
                    getHiero = lang_hiero.Text.Substring(CurrentIndex, HieroByteCount*3);
                    CurrentIndex += HieroByteCount*3;
                }

                // 取得した日本語が辞書に存在するか確認し、存在する場合はヒエログリフに変換する
                if (Hiero.dic_hiero.ContainsValue(getHiero))
                {
                    output.Append(Hiero.dic_hiero.First(x => x.Value.Equals(getHiero)).Key);
                }
                else
                    output.Append(getHiero);
                i++;
            }
            lang_jap.Text = output.ToString();
        }

        bool isJapContained(int startIndex, int length)
        {
            return Hiero.dic_hiero.ContainsValue(lang_hiero.Text.Substring(startIndex, length));
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