﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator.Config.UI.MainMenu
{
    public class Language
    {
        public LanguageElements English { get; set; }
        public LanguageElements Ukrainian { get; set; }

        public Language()
        {
            English = new LanguageElements
            {
                Name = "English",

                Title = "Currency calculator",
                CashAmount = "Enter cash amount:",
                OriginalCurrency = "Choose original currency:",
                SecondCurrency = "Choose second currency:",
                Exchange = "Today's money rate:",
                Result = "Result:",
                Convert = "Convert",
            };


            Ukrainian = new LanguageElements
            {
                Name = "Українська",

                Title = "Валютний калькулятор",
                CashAmount = "Введіть кількість грошей:",
                OriginalCurrency = "Оберіть валюту з якої хочете конвертувати:",
                SecondCurrency = "Оберіть валюту до якої хочете перейти:",
                Exchange = "Курс на сьогодні:",
                Result = "Результат:",
                Convert = "Конвертувати",
            };
        }
    }
    public class LanguageElements
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string CashAmount { get; set; }
        public string OriginalCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public string Exchange { get; set; }
        public string Result { get; set; }
        public string Convert { get; set; }

    }
}
