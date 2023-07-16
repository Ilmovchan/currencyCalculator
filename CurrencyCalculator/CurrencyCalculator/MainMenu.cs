﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Globalization;
using CurrencyCalculator.Config.MainMenu.Language;

namespace CurrencyCalculator
{
    public partial class MainMenu : Form
    {
        private static CurrencyResponse currencyResponse;
        private Settings settingsForm;

        public MainMenu()
        {
            InitializeComponent();

            if (currencyResponse == null)
            {
                currencyResponse = GetResponse();
            }

            ChangeLanguage(Convert.ToString(Properties.Settings.Default["Language"]));
            ChangeColorTheme(Convert.ToString(Properties.Settings.Default["ColorTheme"]));
            ChangeDefaultOriginalCurrency(Convert.ToString(Properties.Settings.Default["DefaultOriginalCurrency"]));

        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            double firstCurrencyValue = GetCurrencyValue(OriginalCurrencyField.Text);
            double secondCurrencyValue = GetCurrencyValue(SecondCurrencyField.Text);

            Double.TryParse(CashAmountField.Text, out double cashAmount);

            double resultValue = CurrencyConvert(firstCurrencyValue, secondCurrencyValue, cashAmount);

            ResultField.Text = Convert.ToString(Math.Round(resultValue, (int)Properties.Settings.Default["NumbersAfterSeparator"]));
            ExchangeField.Text = OriginalCurrencyField.Text + "/" + SecondCurrencyField.Text + ": " + Convert.ToString(Math.Round(firstCurrencyValue/secondCurrencyValue , (int)Properties.Settings.Default["NumbersAfterSeparator"]));
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new Settings();
                settingsForm.Show();
            }
            else
            {
                settingsForm.BringToFront();
            }
        }

        private double CurrencyConvert(double firstValue, double secondValue, double cashAmount)
        {
            if (cashAmount > 0) return cashAmount * (secondValue / firstValue);
            else return 0;
        }

        private double GetCurrencyValue(string originalCurrency)
        {
            CurrencyInfo currencyInfo = currencyResponse.rates;
            PropertyInfo[] properties = typeof(CurrencyInfo).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == originalCurrency)
                {
                    object value = property.GetValue(currencyInfo);
                    if (value != null)
                    {
                        return Convert.ToDouble(value);
                    }
                }
            }

            return 0;
        }

        private CurrencyResponse GetResponse()
        {
            string url = "https://openexchangerates.org/api/latest.json?app_id=5b79ee6f285c4818b7fb7acd54c174b6";
            CurrencyResponse currencyResponse;

            HttpWebRequest httpsWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpsWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string response = streamReader.ReadToEnd();
                currencyResponse = JsonConvert.DeserializeObject<CurrencyResponse>(response);
            }

            return currencyResponse;
        }

        public void ChangeLanguage(string language)
        {

            LanguageTest languageData = new LanguageTest();

            string selectedLanguage = Convert.ToString(Properties.Settings.Default["Language"]);
            LanguageText languageText = typeof(LanguageTest).GetProperty(selectedLanguage)?.GetValue(languageData) as LanguageText;
            this.Text = languageText?.Title;
            CashAmountLabel.Text = languageText?.CashAmount;
            OriginalCurrencyLabel.Text = languageText?.OriginalCurrency;
            SecondCurrencyLabel.Text = languageText?.SecondCurrency;
            ResultLabel.Text = languageText?.Result;
            ExchangeLabel.Text = languageText?.Exchange;
            ConvertButton.Text = languageText?.Convert;
        }

        public void ChangeColorTheme(string colorTheme)
        {
            if (colorTheme == "LIGHT")
            {
                this.BackColor = System.Drawing.Color.WhiteSmoke;

                CashAmountField.BackColor = System.Drawing.SystemColors.ScrollBar;
                OriginalCurrencyField.BackColor = System.Drawing.SystemColors.ScrollBar;
                SecondCurrencyField.BackColor = System.Drawing.SystemColors.ScrollBar;

                ResultField.BackColor = System.Drawing.SystemColors.ScrollBar;
                ExchangeField.BackColor = System.Drawing.SystemColors.ScrollBar;

                CashAmountField.ForeColor = System.Drawing.Color.Black;
                OriginalCurrencyField.ForeColor = System.Drawing.Color.Black;
                SecondCurrencyField.ForeColor = System.Drawing.Color.Black;

                ResultField.ForeColor = System.Drawing.Color.Black;
                ExchangeField.ForeColor = System.Drawing.Color.Black;

                CashAmountLabel.ForeColor = System.Drawing.Color.Black;
                OriginalCurrencyLabel.ForeColor = System.Drawing.Color.Black;
                SecondCurrencyLabel.ForeColor = System.Drawing.Color.Black;

                ResultLabel.ForeColor = System.Drawing.Color.Black;
                ExchangeLabel.ForeColor = System.Drawing.Color.Black;

                ConvertButton.BackColor = System.Drawing.Color.PowderBlue;
                ConvertButton.ForeColor = System.Drawing.Color.Black;
                ConvertButton.FlatAppearance.BorderColor = System.Drawing.Color.PowderBlue;

                CashAmountField.BorderStyle = BorderStyle.None;

                SettingsButton.Image = Image.FromFile("../../../icons/settings_new_icon_grey.png");
            }
            else if (colorTheme == "DARK")
            {
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(42)))), ((int)(((byte)(47)))));

                CashAmountField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(42)))), ((int)(((byte)(47)))));
                OriginalCurrencyField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(42)))), ((int)(((byte)(47)))));
                SecondCurrencyField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(42)))), ((int)(((byte)(47)))));

                ResultField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(42)))), ((int)(((byte)(47)))));
                ExchangeField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(42)))), ((int)(((byte)(47)))));

                CashAmountField.ForeColor = System.Drawing.Color.White;
                OriginalCurrencyField.ForeColor = System.Drawing.Color.White;
                SecondCurrencyField.ForeColor = System.Drawing.Color.White;

                ResultField.ForeColor = System.Drawing.Color.White;
                ExchangeField.ForeColor = System.Drawing.Color.White;

                CashAmountLabel.ForeColor = System.Drawing.Color.White;
                OriginalCurrencyLabel.ForeColor = System.Drawing.Color.White;
                SecondCurrencyLabel.ForeColor = System.Drawing.Color.White;

                ResultLabel.ForeColor = System.Drawing.Color.White;
                ExchangeLabel.ForeColor = System.Drawing.Color.White;

                ConvertButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(74)))), ((int)(((byte)(232)))));
                ConvertButton.ForeColor = System.Drawing.Color.White;
                ConvertButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(74)))), ((int)(((byte)(232)))));

                CashAmountField.BorderStyle = BorderStyle.FixedSingle;

                SettingsButton.Image = Image.FromFile("../../../icons/settings_new_icon_white.png");
            }

        }

        public void ChangeDefaultOriginalCurrency(string defaultOriginalCurrency)
        {
            OriginalCurrencyField.Text = defaultOriginalCurrency;
        }
    }
}
