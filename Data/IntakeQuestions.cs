﻿using Microsoft.AspNetCore.Components;
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
                afterviewStrings = (MarkupString)"<h1>桜　猫　電車</h1>"
            } },

        };

    }


}
