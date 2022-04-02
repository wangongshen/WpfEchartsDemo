using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WpfEchartsDemo.Entity
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> propertyName)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = propertyName.Body as MemberExpression;
                if (memberExpression != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }

        public void NotifyOfPropertyChange(string p_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
            }
        }
        public abstract bool DisposeViewModelBase();

    }
}
