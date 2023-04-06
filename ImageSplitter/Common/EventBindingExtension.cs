using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows;

namespace ImageSplitter.Common
{
    public class EventBindingExtension : MarkupExtension
    {
        private readonly string _commandName;

        public EventBindingExtension(string command)
        {
            _commandName = command;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            MethodInfo method = GetType().GetMethod("InvokeCommand", BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null && serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget)
            {
                object targetProperty = provideValueTarget.TargetProperty;
                if (targetProperty is EventInfo)
                {
                    Type eventHandlerType = (targetProperty as EventInfo).EventHandlerType;
                    return method.CreateDelegate(eventHandlerType, this);
                }
                if (targetProperty is MethodInfo)
                {
                    ParameterInfo[] parameters = (targetProperty as MethodInfo).GetParameters();
                    if (parameters.Length >= 1)
                    {
                        Type parameterType = parameters[1].ParameterType;
                        return method.CreateDelegate(parameterType, this);
                    }
                }
            }
            throw new InvalidOperationException("The EventBinding markup extension is valid only in the context of events.");
        }

        private void InvokeCommand(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(_commandName) && sender is FrameworkElement frameworkElement)
            {
                object dataContext = frameworkElement.DataContext;
                if (dataContext != null)
                {
                    PropertyInfo property = dataContext.GetType().GetProperty(_commandName);
                    if (property != null && property.GetValue(dataContext) is ICommand command && command.CanExecute(args))
                    {
                        command.Execute(args);
                    }
                }
            }
        }
    }
}
