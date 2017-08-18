﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using CameraControl.Core.Wpf;
using Capture.Workflow.Core.Classes;
using Capture.Workflow.Wpf.Controls;
using MahApps.Metro.Controls;

namespace Capture.Workflow.Wpf.Converters
{
    public class ParameterToControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var property = value as CustomProperty;
            if (property != null)
                switch (property.PropertyType)
                {
                    case CustomPropertyType.Number:
                        NumericUpDown numericUpDown = new NumericUpDown();
                        numericUpDown.Minimum = property.RangeMin;
                        numericUpDown.Maximum = property.RangeMax;
                        numericUpDown.DataContext = property;
                        numericUpDown.SetBinding(NumericUpDown.ValueProperty, "Value");
                        return numericUpDown;
                    case CustomPropertyType.String:
                        {
                            TextBox text = new TextBox();
                            text.DataContext = property;
                            text.SetBinding(TextBox.TextProperty, "Value");
                            return text;
                        }
                    case CustomPropertyType.ParamString:
                        {
                            TextBox text = new TextBox();
                            text.DataContext = property;
                            text.SetBinding(TextBox.TextProperty, "Value");
                            return text;
                        }
                    case CustomPropertyType.Color:
                        var colorpicker = new Colorpicker();
                        colorpicker.DataContext = property;
                        colorpicker.SetBinding(Colorpicker.SelectedColorProperty, new Binding("Value") { Mode = BindingMode.TwoWay });
                        return colorpicker;
                    case CustomPropertyType.ValueList:
                        {
                            ComboBox comboBox = new ComboBox();
                            comboBox.DataContext = property;
                            comboBox.SetBinding(ComboBox.ItemsSourceProperty, "ValueList");
                            comboBox.SetBinding(ComboBox.SelectedItemProperty, "Value");
                            return comboBox;
                        }
                    case CustomPropertyType.Variable:
                        {
                            property.InitVaribleList();
                            ComboBox comboBox = new ComboBox();
                            comboBox.DataContext = property;
                            comboBox.SetBinding(ComboBox.ItemsSourceProperty, "ValueList");
                            comboBox.SetBinding(ComboBox.SelectedItemProperty, "Value");
                            return comboBox;
                        }
                    case CustomPropertyType.View:
                        {
                            property.InitViewList();
                            ComboBox comboBox = new ComboBox();
                            comboBox.DataContext = property;
                            comboBox.SetBinding(ComboBox.ItemsSourceProperty, "ValueList");
                            comboBox.SetBinding(ComboBox.SelectedItemProperty, "Value");
                            return comboBox;
                        }
                    case CustomPropertyType.Icon:
                        var iconicker = new IconPicker();
                        iconicker.DataContext = property;
                        iconicker.SetBinding(IconPicker.SelectedIconProperty, new Binding("Value") { Mode = BindingMode.TwoWay });
                        return iconicker;
                    case CustomPropertyType.Bool:
                        CheckBox checkBox = new CheckBox();
                        checkBox.DataContext = property;
                        checkBox.SetBinding(CheckBox.IsCheckedProperty, "Value");
                        return checkBox;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
