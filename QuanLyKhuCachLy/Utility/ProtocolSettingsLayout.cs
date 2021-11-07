using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace QuanLyKhuCachLy.Utility
{
    public class ProtocolSettingsLayout
    {
        public static readonly DependencyProperty MVVMHasErrorProperty = DependencyProperty.RegisterAttached("MVVMHasError",
                                                                        typeof(bool),
                                                                        typeof(ProtocolSettingsLayout),
                                                                        new FrameworkPropertyMetadata(false,
                                                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                                                      null,
                                                                                                      CoerceMVVMHasError));

        public static bool GetMVVMHasError(DependencyObject d)
        {
            return (bool)d.GetValue(MVVMHasErrorProperty);
        }

        public static void SetMVVMHasError(DependencyObject d, bool value)
        {
            d.SetValue(MVVMHasErrorProperty, value);
        }

        private static object CoerceMVVMHasError(DependencyObject d, Object baseValue)
        {
            bool ret = (bool)baseValue;

            if (BindingOperations.IsDataBound(d, MVVMHasErrorProperty))
            {
                if (GetHasErrorDescriptor(d) == null)
                {
                    DependencyPropertyDescriptor desc = DependencyPropertyDescriptor.FromProperty(Validation.HasErrorProperty, d.GetType());
                    desc.AddValueChanged(d, OnHasErrorChanged);
                    SetHasErrorDescriptor(d, desc);
                    ret = System.Windows.Controls.Validation.GetHasError(d);
                }
            }
            else
            {
                if (GetHasErrorDescriptor(d) != null)
                {
                    DependencyPropertyDescriptor desc = GetHasErrorDescriptor(d);
                    desc.RemoveValueChanged(d, OnHasErrorChanged);
                    SetHasErrorDescriptor(d, null);
                }
            }

            return ret;
        }

        private static readonly DependencyProperty HasErrorDescriptorProperty = DependencyProperty.RegisterAttached("HasErrorDescriptor",
                                                                                typeof(DependencyPropertyDescriptor),
                                                                                typeof(ProtocolSettingsLayout));

        private static DependencyPropertyDescriptor GetHasErrorDescriptor(DependencyObject d)
        {
            var ret = d.GetValue(HasErrorDescriptorProperty);
            return ret as DependencyPropertyDescriptor;
        }

        private static void OnHasErrorChanged(object sender, EventArgs e)
        {
            DependencyObject d = sender as DependencyObject;

            if (d != null)
            {
                d.SetValue(MVVMHasErrorProperty, d.GetValue(Validation.HasErrorProperty));
            }
        }

        private static void SetHasErrorDescriptor(DependencyObject d, DependencyPropertyDescriptor value)
        {
            var ret = d.GetValue(HasErrorDescriptorProperty);
            d.SetValue(HasErrorDescriptorProperty, value);
        }
    }
}
