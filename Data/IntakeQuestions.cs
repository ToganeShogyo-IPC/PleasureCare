using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisuCare.Data
{
    public class IntakeQuestions
    {
        public MarkupString question { get; set; }
        public string voicealt { get; set; }
        public int score { get; set; } = 0;
        public bool is_button { get; set; } = false;
        public bool is_continue { get; set; } = false;
        public MarkupString afterviewStrings { get; set; }
        public List<string>? Trueanswers { get; set; }
    }

    public class questionsA
    {
        public List<IntakeQuestions> question = new List<IntakeQuestions>()
        {
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>今日は何年ですか？</h2>",
                    voicealt = "今日は何年ですか",
                    score = 1,
                    Trueanswers = new List<string>() { DateTime.Now.ToString("yyyy") }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>今日は何月、何日、何曜日ですか？</h2>",
                    voicealt = "今日は何月、何日、何曜日ですか",
                    score = 3,
                    Trueanswers = new List<string>()
                        { DateTime.Now.ToString("M月"), DateTime.Now.ToString("dd日"), DateTime.Now.ToString("dddd") }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>これから表示する言葉を読み上げてください。</h2><h3>後の設問でもう一度聞きますので、覚えてください。</h3>",
                    voicealt="これから表示する言葉を読み上げてください。後の設問でもう一度聞きますので、覚えてください。",
                    score = 3,
                    is_button = true,
                    afterviewStrings = (MarkupString)"電卓　マウス　とっちー",
                    Trueanswers = new List<string>() { "電卓", "マウス", "とっちー", "でんたく", "まうす", "デンタク", "トッチー", "栃" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>100-7はいくつですか？</h2>",
                    voicealt ="100ひく7はいくつですか",
                    score = 1,
                    Trueanswers = new List<string>() { "93" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>93-7はいくつですか？</h2>",
                    voicealt ="93ひく7はいくつですか",
                    score = 1,
                    is_continue = true,
                    Trueanswers = new List<string>() { "86" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>86-7はいくつですか？</h2>",
                    voicealt ="86ひく7はいくつですか",
                    score = 1,
                    is_continue = true,
                    Trueanswers = new List<string>() { "79" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>これから表示する数字を逆から読み上げてください。<h2>",
                    voicealt ="これから表示する数字を逆から読み上げてください。",
                    score = 3,
                    is_button = true,
                    afterviewStrings = (MarkupString)"682",
                    Trueanswers = new List<string>() { "286" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>設問3の単語を3つ思い出して読み上げてください。</h2>",
                    voicealt ="設問3の単語を3つ思い出して読み上げてください。",
                    score = 3,
                    Trueanswers = new List<string>() { "電卓", "マウス", "とっちー", "でんたく", "まうす", "デンタク", "トッチー", "栃" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>野菜の名前をできるだけ多く読み上げてください。</h2>",
                    voicealt="野菜の名前をできるだけ多く読み上げてください。",
                    score = 5,
                    Trueanswers = new List<string>() { "Variable" }
                }
            },
        };
    }
}