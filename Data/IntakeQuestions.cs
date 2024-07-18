using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileCare.Data
{
    public class IntakeQuestions
    {
        public MarkupString question { get; set; }
        public int score { get; set; } = 0;
        public bool is_button { get; set; } = false;
        public bool is_continue { get; set; } = false;
        public MarkupString afterviewStrings { get; set; }
    }

    public class questionsA {
        public List<IntakeQuestions> question = new List<IntakeQuestions>()
        {
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>今日は何年ですか？</h2>",
                score = 1
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>今日は何月何日、何曜日ですか？</h2>",
                score = 3
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>これから表示する三つの言葉を読み上げてください。<h2><h3>後の設問でもう一度聞きますので、覚えてください。</h3>",
                score = 3,
                is_button = true,
                afterviewStrings = (MarkupString)"桜　猫　電車"
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>100から7を引くといくつになりますか？</h2>",
                score = 1,
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>93から7を引くといくつになりますか？</h2>",
                score = 1,
                is_continue = true,
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>86から7を引くといくつになりますか？</h2>",
                score = 1,
                is_continue = true,
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>これから表示する三つの数字を読み上げてください。<h2>",
                score = 3,
                is_button = true,
                afterviewStrings = (MarkupString)"682"
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>3の設問の単語を3つ思い出してもう一度読み上げてください。</h2>",
                score = 1,
            } },
            {new IntakeQuestions(){
                question = (MarkupString)"<h2>知っている野菜をできるだけ多く読み上げてください。</h2>",
                score = 5,
            } },
        };
    }
}
