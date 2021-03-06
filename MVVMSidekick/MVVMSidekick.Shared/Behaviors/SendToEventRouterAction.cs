﻿using System;
#if ! NETFX_CORE
using Microsoft.Expression.Interactivity.Core;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

#else
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using System.Reflection;
#endif
using MVVMSidekick.EventRouting;
using MVVMSidekick.Utilities;


namespace MVVMSidekick.Behaviors
{

#if NETFX_CORE
    public class SendToEventRouterAction :DependencyObject , Microsoft.Xaml.Interactivity.IAction
#else
    public class SendToEventRouterAction : TriggerAction<DependencyObject>
#endif
    {



		/// <summary>
		/// Gets or sets the name of the event routing.
		/// </summary>
		/// <value>
		/// The name of the event routing.
		/// </value>
        public string EventRoutingName
        {
            get { return (string)GetValue(EventRoutingNameProperty); }
            set { SetValue(EventRoutingNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EventRoutingName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventRoutingNameProperty =
            DependencyProperty.Register("EventRoutingName", typeof(string), typeof(SendToEventRouterAction), new PropertyMetadata(null));





        public EventRouter EventRouter
        {
            get { return (EventRouter)GetValue(EventRouterProperty); }
            set { SetValue(EventRouterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EventRouter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventRouterProperty =
            DependencyProperty.Register("EventRouter", typeof(EventRouter), typeof(SendToEventRouterAction), new PropertyMetadata(null));




		/// <summary>
		/// Gets or sets the type of the event object.
		/// </summary>
		/// <value>
		/// The type of the event object.
		/// </value>
        public System.Type EventObjectType
        {
            get { return (Type)GetValue(EventObjectTypeProperty); }
            set { SetValue(EventObjectTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EventObjectType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventObjectTypeProperty =
            DependencyProperty.Register("EventObjectType", typeof(Type), typeof(SendToEventRouterAction), new PropertyMetadata(typeof(object)));




		/// <summary>
		/// Gets or sets the event object.
		/// </summary>
		/// <value>
		/// The event object.
		/// </value>
        public System.Object EventObject
        {
            get { return (System.Object)GetValue(EventObjectProperty); }
            set { SetValue(EventObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EventObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EventObjectProperty =
            DependencyProperty.Register("EventObject", typeof(System.Object), typeof(SendToEventRouterAction), new PropertyMetadata(null));





#if NETFX_CORE
        public object Execute(object sender, object parameter)
        {
           if (EventObject != null)
			{
				if (!EventObjectType.GetTypeOrTypeInfo().IsAssignableFrom(EventObject.GetType().GetTypeInfo ()))
				{
					return null;
				}
			}

			var targetEventRouter = EventRouter ?? EventRouter.Instance;

			targetEventRouter.RaiseEvent(this, EventObject, EventObjectType, EventRoutingName);
			return null;
        }
#else

		/// <summary>
		/// Invokes the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
        protected override void Invoke(object parameter)
        {
            if (EventObject != null)
            {
                if (!EventObjectType.GetTypeOrTypeInfo().IsAssignableFrom(EventObject.GetType()))
                {
                    return;
                }
            }

            var targetEventRouter = EventRouter ?? EventRouter.Instance;

            targetEventRouter.RaiseEvent(this, EventObject, EventObjectType, EventRoutingName);

        }
#endif
    }
}
