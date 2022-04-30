using System.Windows.Forms;
using System.Linq;
using System.Text.Encodings;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using WMPLib;

namespace Jap2Hieroglyph
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer _mediaPlayer = new WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        readonly Dictionary<string, string> dic_hiero = new()
        {
            { "あ", "\U0001313f" },
            { "い", "\U000131cc" },
            { "う", "\U00013171" },
            { "え", "\U000131cb" },
            { "お", "\U0001336f" },

            { "か", "\U000133a1\U0001313f" },
            { "き", "\U000133a1\U000131cc" },
            { "く", "\U000133a1\U00013171" },
            { "け", "\U000133a1\U000131CB" },
            { "こ", "\U000133a1\U0001336F" },

            { "さ", "\U000132F4\U0001313f" },
            { "し", "\U000132F4\U000131cc" },
            { "す", "\U000132F4\U00013171" },
            { "せ", "\U000132F4\U000131CB" },
            { "そ", "\U000132F4\U0001336F" },

            { "た", "\U000133CF\U0001313f" },
            { "ち", "\U000133CF\U000131cc" },
            { "つ", "\U000133CF\U00013171" },
            { "て", "\U000133CF\U000131CB" },
            { "と", "\U000133CF\U0001336F" },

            { "な", "\U00013216\U0001313f" },
            { "に", "\U00013216\U000131cc" },
            { "ぬ", "\U00013216\U00013171" },
            { "ね", "\U00013216\U000131CB" },
            { "の", "\U00013216\U0001336F" },

            { "は", "\U00013254\U0001313f" },
            { "ひ", "\U00013254\U000131cc" },
            { "ふ", "\U00013254\U00013171" },
            { "へ", "\U00013254\U000131CB" },
            { "ほ", "\U00013254\U0001336F" },

            { "ま", "\U00013153\U0001313f" },
            { "み", "\U00013153\U000131cc" },
            { "む", "\U00013153\U00013171" },
            { "め", "\U00013153\U000131CB" },
            { "も", "\U00013153\U0001336F" },

            { "ら", "\U0001308B\U0001313f" },
            { "り", "\U0001308B\U000131cc" },
            { "る", "\U0001308B\U00013171" },
            { "れ", "\U0001308B\U000131CB" },
            { "ろ", "\U0001308B\U0001336F" },

            { "や", "\U000131CC\U0001313F" },
            { "ゆ", "\U000131CC\U00013171" },
            { "よ", "\U000131CC\U0001336F" },

            { "わ", "\U00013171\U0001313F" },
            { "を", "\U0001336F" },
            { "ん", "\U00013216" },

            { "が", "\U000133BC\U0001313F" },
            { "ぎ", "\U000133BC\U000131CC" },
            { "ぐ", "\U000133BC\U00013171" },
            { "げ", "\U000133BC\U000131CB" },
            { "ご", "\U000133BC\U0001336F" },

            { "ざ", "\U00013283\U0001313F" },
            { "じ", "\U00013283\U000131CC" },
            { "ず", "\U00013283\U00013171" },
            { "ぜ", "\U00013283\U000131CB" },
            { "ぞ", "\U00013283\U0001336F" },

            { "だ", "\U000130A9\U0001313F" },
            { "ぢ", "\U000130A9\U000131CC" },
            { "づ", "\U000130A9\U00013171" },
            { "で", "\U000130A9\U000131CB" },
            { "ど", "\U000130A9\U0001336F" },

            { "ば", "\U000130C0\U0001313F" },
            { "び", "\U000130C0\U000131CC" },
            { "ぶ", "\U000130C0\U00013171" },
            { "べ", "\U000130C0\U000131CB" },
            { "ぼ", "\U000130C0\U0001336F" },

            { "ぱ", "\U000132AA\U0001313F" },
            { "ぴ", "\U000132AA\U000131CC" },
            { "ぷ", "\U000132AA\U00013171" },
            { "ぺ", "\U000132AA\U000131CB" },
            { "ぽ", "\U000132AA\U0001336F" },

            { "ぁ", "\U000130ED\U0001313f" },
            { "ぃ", "\U000130ED\U000131cc" },
            { "ぅ", "\U000130ED\U00013171" },
            { "ぇ", "\U000130ED\U000131cb" },
            { "ぉ", "\U000130ED\U0001336f" },
            { "ゃ", "\U000130ED\U000131CC\U0001313F" },
            { "ゅ", "\U000130ED\U000131CC\U00013171" },
            { "ょ", "\U000130ED\U000131CC\U0001336F" },
            { "っ", "\U000130ED\U000133cf\U00013362" },
        };

        private void lang_jap_TextChanged(object sender, EventArgs e)
        {
            if (lang_jap.ReadOnly == true) return;
            string output = "";
            foreach (char c in lang_jap.Text)
            {
                bool canConvert = dic_hiero.ContainsKey(c.ToString());
                output += canConvert ? dic_hiero[c.ToString()] : c.ToString();
            }
            lang_hiero.Text = output;
        }

        private void lang_hiero_TextChanged(object sender, EventArgs e)
        {
            if (lang_hiero.ReadOnly == true) return;

            string output = "";
            string getHiero = "";
            int rest_of_length = 0;

            for (int i = 0; i < lang_hiero.Text.Length / 2; i++)
            {
                rest_of_length = lang_hiero.Text.Length - (i * 2);
                if (rest_of_length >= 6 && dic_hiero.ContainsValue(lang_hiero.Text.Substring(i * 2, 6)))
                {
                    // Convert from a hieroglyph consisting of three code-points to corresponded Japanese
                    getHiero = lang_hiero.Text.Substring(i * 2, 2*3);
                    i += 2;
                }
                else if (rest_of_length >= 4 && dic_hiero.ContainsValue(lang_hiero.Text.Substring(i * 2, 4)))
                {
                    // Convert from a hieroglyph consisting of two code-points to corresponded Japanese
                    getHiero = lang_hiero.Text.Substring(i * 2, 2*2);
                    i += 1;
                }
                else if (dic_hiero.ContainsValue(lang_hiero.Text.Substring(i * 2, 2)))
                    // Convert from a hieroglyph consisting of one code-point to corresponded Japanese. 
                    getHiero = lang_hiero.Text.Substring(i * 2, 2*1);
                else
                {
                    getHiero += lang_hiero.Text.Substring(i * 2, 2*(1/2));
                    lang_hiero.Text = lang_hiero.Text.Remove(i * 2, 1);
                }

                bool isHiero = dic_hiero.ContainsValue(getHiero);
                output += isHiero ? dic_hiero.First(x => x.Value.Equals(getHiero)).Key : getHiero;
            }
            lang_jap.Text = output;
        }

        private void btn_SwitchLang_Click(object sender, EventArgs e)
        {
            lang_jap.ReadOnly = !lang_jap.ReadOnly;
            lang_hiero.ReadOnly = !lang_hiero.ReadOnly;
        }
    }

}