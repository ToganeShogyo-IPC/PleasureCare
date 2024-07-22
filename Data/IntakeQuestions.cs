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
                    score = 1,
                    Trueanswers = new List<string>() { DateTime.Now.ToString("yyyy") }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>今日は何月何日、何曜日ですか？</h2>",
                    score = 3,
                    Trueanswers = new List<string>()
                        { DateTime.Now.ToString("M月"), DateTime.Now.ToString("dd日"), DateTime.Now.ToString("dddd") }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>これから表示する三つの言葉を読み上げてください。<h2><h3>後の設問でもう一度聞きますので、覚えてください。</h3>",
                    score = 3,
                    is_button = true,
                    afterviewStrings = (MarkupString)"桜　猫　電車",
                    Trueanswers = new List<string>() { "桜", "猫", "電車", "さくら", "ねこ", "でんしゃ", "サクラ", "ネコ", "デンシャ" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>100から7を引くといくつになりますか？</h2>",
                    score = 1,
                    Trueanswers = new List<string>() { "93" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>93から7を引くといくつになりますか？</h2>",
                    score = 1,
                    is_continue = true,
                    Trueanswers = new List<string>() { "86" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>86から7を引くといくつになりますか？</h2>",
                    score = 1,
                    is_continue = true,
                    Trueanswers = new List<string>() { "79" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>これから表示する三つの数字を逆から読み上げてください。<h2>",
                    score = 3,
                    is_button = true,
                    afterviewStrings = (MarkupString)"682",
                    Trueanswers = new List<string>() { "286" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>設問3の単語を3つ思い出してもう一度読み上げてください。</h2>",
                    score = 1,
                    Trueanswers = new List<string>() { "桜", "猫", "電車" }
                }
            },
            {
                new IntakeQuestions()
                {
                    question = (MarkupString)"<h2>知っている野菜をできるだけ多く読み上げてください。</h2>",
                    score = 5,
                    Trueanswers = new List<string>() { "Variable" }
                }
            },
        };
    }
}