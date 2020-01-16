using DatEx.Skelya.GUI.CustomCtrls.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatEx.Skelya.GUI.CustomCtrls
{
    #region ■■■■■ Base ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    [TemplatePart(Name = nameof(Part_CommentsList), Type = typeof(ListView))]
    public partial class EventCommentsCtrl : Control
    {
        static EventCommentsCtrl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EventCommentsCtrl), new FrameworkPropertyMetadata(typeof(EventCommentsCtrl)));
        }
    }
    #endregion ■■■■■ Base


    #region ■■■■■ ControlParts ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
    public partial class EventCommentsCtrl
    {
        private ListView Part_CommentsList;
        
        private T FindTemplatePart<T>(String templatePartName) where T : DependencyObject
            => (GetTemplateChild(templatePartName) as T) ?? throw new NullReferenceException(templatePartName);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Part_CommentsList = FindTemplatePart<ListView>(nameof(Part_CommentsList));
            //
            SetUpTemplateParts();
        }

        private void SetUpTemplateParts()
        {
            Part_CommentsList.ItemsSource = VM_Comment.GetTestCollection();
        }
    }
    #endregion ■■■■■ ControlParts
}
