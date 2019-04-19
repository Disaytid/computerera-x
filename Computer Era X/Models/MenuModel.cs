using System;
using System.Diagnostics;
using System.Windows.Controls;
using Computer_Era_X.DataTypes.Enums;
using Computer_Era_X.DataTypes.Interfaces;

namespace Computer_Era_X.Models
{
    public static class MenuModel
    {
        public static StackPanel GetScenarioSettings(IScenario scenario)
        {
            StackPanel stackPanel = new StackPanel();
            for (int i = 0; scenario.Settings.Count > i; i++)
            {
                Setting setting = scenario.Settings[i];
                Label label;
                switch (setting.Type)
                {
                    case TypeSettingsData.Bool:
                        CheckBox checkBox = new CheckBox()
                        {
                            Content = setting.Name,
                            IsChecked = bool.Parse(setting.Value),
                            Tag = i,
                        };
                        stackPanel.Children.Add(checkBox);
                        break;
                    case TypeSettingsData.Integer:
                        label = new Label()
                        {
                            Content = setting.Name + ": ",
                        };
                        stackPanel.Children.Add(label);
                        TextBox textBox = new TextBox()
                        {
                            Text = setting.Value,
                            Tag = i,
                        };
                        stackPanel.Children.Add(textBox);
                        break;
                    case TypeSettingsData.Double:
                        goto case TypeSettingsData.Integer;
                    case TypeSettingsData.String:
                        goto case TypeSettingsData.Integer;
                    case TypeSettingsData.List:
                        label = new Label()
                        {
                            Content = setting.Name + ": ",
                        };
                        stackPanel.Children.Add(label);
                        ComboBox comboBox = new ComboBox()
                        {
                            ItemsSource = setting.Values,
                            Tag = i,
                        };
                        stackPanel.Children.Add(comboBox);
                        break;
                }
            }
            return stackPanel;
        }
        public static void SetScenarioSettings(IScenario scenario, StackPanel stackPanel)
        {
            for (int i = 0; stackPanel.Children.Count > i; i++)
            {
                switch (stackPanel.Children[i])
                {
                    case Label _:
                        continue;
                    case CheckBox _:
                        var checkBox = ((CheckBox)stackPanel.Children[i]);
                        Debug.Assert(checkBox.IsChecked != null, "checkBox.IsChecked != null");
                        scenario.Settings[(int)checkBox.Tag].Value = checkBox.IsChecked.Value.ToString();
                        break;
                    case TextBox _:
                        var textBox = ((TextBox)stackPanel.Children[i]);
                        var setting = scenario.Settings[(int)textBox.Tag];
                        switch (setting.Type)
                        {
                            case TypeSettingsData.Integer:
                                if (!int.TryParse(textBox.Text, out _))
                                { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.InvalidScenarioConfigurationValue + ": " + textBox.Text, MessageBoxType.Warning); }
                                setting.Value = textBox.Text;
                                break;
                            case TypeSettingsData.Double:
                                if (!double.TryParse(textBox.Text, out _))
                                { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.InvalidScenarioConfigurationValue + ": " + textBox.Text, MessageBoxType.Warning); }
                                setting.Value = textBox.Text;
                                break;
                            case TypeSettingsData.String:
                                setting.Value = textBox.Text;
                                break;
                            case TypeSettingsData.Bool:
                                break;
                            case TypeSettingsData.List:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case ComboBox _:
                        var comboBox = ((ComboBox)stackPanel.Children[i]);
                        if (comboBox.SelectedItem == null)
                        { Views.MessageBox.Show(Properties.Resources.NewGame, Properties.Resources.NoValueSelectedInScenarioSettings, MessageBoxType.Warning); }

                        if (comboBox.SelectedItem != null)
                            scenario.Settings[(int)comboBox.Tag].Value = comboBox.SelectedItem.ToString();
                        break;
                }
            }
        }
    }
}
