﻿//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;

//namespace WpfApp1
//{
//    public class ListBoxSelectionBehavior : Behavior<ListBox>
//    {
//        public static readonly System.Windows.DependencyProperty SelectedItemsProperty =
//            DependencyProperty.Register(nameof(SelectedItems), typeof(IList),
//                typeof(ListBoxSelectionBehavior),
//                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
//                    OnSelectedItemsChanged));

//        private static void OnSelectedItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
//        {
//            var behavior = (ListBoxSelectionBehavior)sender;
//            if (behavior._modelHandled) return;

//            if (behavior.AssociatedObject == null)
//                return;

//            behavior._modelHandled = true;
//            behavior.SelectItems();
//            behavior._modelHandled = false;
//        }

//        public static explicit operator ListBoxSelectionBehavior(DependencyObject v)
//        {
//            throw new NotImplementedException();
//        }

//        private bool _viewHandled;
//        private bool _modelHandled;

//        public IList SelectedItems
//        {
//            get => (IList)GetValue(SelectedItemsProperty);
//            set => SetValue(SelectedItemsProperty, value);
//        }

//        // Propagate selected items from model to view
//        private void SelectItems()
//        {
//            _viewHandled = true;
//            AssociatedObject.SelectedItems.Clear();
//            if (SelectedItems != null)
//            {
//                foreach (var item in SelectedItems)
//                    AssociatedObject.SelectedItems.Add(item);
//            }
//            _viewHandled = false;
//        }

//        // Propagate selected items from view to model
//        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs args)
//        {
//            if (_viewHandled) return;
//            if (AssociatedObject.Items.SourceCollection == null) return;
//            SelectedItems = AssociatedObject.SelectedItems.Cast<object>().ToArray();
//        }

//        // Re-select items when the set of items changes
//        private void OnListBoxItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
//        {
//            if (_viewHandled) return;
//            if (AssociatedObject.Items.SourceCollection == null) return;
//            SelectItems();
//        }

//        protected override void OnAttached()
//        {
//            base.OnAttached();

//            AssociatedObject.SelectionChanged += OnListBoxSelectionChanged;
//            ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged += OnListBoxItemsChanged;
//        }

//        /// <inheritdoc />
//        protected override void OnDetaching()
//        {
//            base.OnDetaching();

//            if (AssociatedObject != null)
//            {
//                AssociatedObject.SelectionChanged -= OnListBoxSelectionChanged;
//                ((INotifyCollectionChanged)AssociatedObject.Items).CollectionChanged -= OnListBoxItemsChanged;
//            }
//        }
//    }
//}
