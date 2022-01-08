using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using System.Windows.Automation.Peers;

namespace Lab6_OpenCV.Services
{
    /// <summary>
    /// Service allowing to attach to any external application window and elements.
    /// </summary>
    public class UserInterfaceAutomationService
    {
        private AutomationElement _root;

        /// <summary>
        /// Method to detach from current attached window.
        /// </summary>
        public void Detach()
        {
            _root = null;
        }

        /// <summary>
        /// Method checking if service is currently attached to any element.
        /// </summary>
        /// <returns>bool if service is attached</returns>
        public bool IsAttached()
            => _root != null;

        /// <summary>
        /// Method return current attached element
        /// </summary>
        /// <returns></returns>
        public UserInterfaceControl GetAttachedControl()
            => new UserInterfaceControl(_root);

        /// <summary>
        /// Method allowing to attach to expected window by window name.
        /// </summary>
        /// <param name="windowName">Name of window to attach</param>
        /// <returns></returns>
        public UserInterfaceControl AttachToWindow(string windowName)
         => AttachToControl(windowName, AutomationControlType.Window);

        /// <summary>
        /// Method allowing to attach to expected element.
        /// </summary>
        /// <param name="controlTitle">Name of expected element</param>
        /// <param name="type">Type of expected element to easier find</param>
        /// <returns>UserInterfaceControl with most valuable properties from element.</returns>
        /// <exception cref="NotImplementedException">Throw exception when unable to find element.</exception>
        public UserInterfaceControl AttachToControl(string controlTitle, AutomationControlType type)
        {
            Detach();

            AndCondition condition =
                new AndCondition(
                    new PropertyCondition(AutomationElement.NameProperty, controlTitle, PropertyConditionFlags.IgnoreCase),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, getControlType(type)));

            AutomationElement control = AutomationElement.RootElement.FindFirst(TreeScope.Descendants, condition);

            if (control != null)
            {
                _root = control;
                return new UserInterfaceControl(_root);
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Method that allow find expected element by name, automationId or type.
        /// </summary>
        /// <param name="elementName">name of element can be null</param>
        /// <param name="automationId">automationId of element can be null</param>
        /// <param name="type">Type of automation element for easier sort found results</param>
        /// <returns></returns>
        public UserInterfaceControl FindSingle(string elementName, string automationId, AutomationControlType type)
        {
            if (!IsAttached())
            {
                Console.WriteLine("Root element is null");
                return new UserInterfaceControl(); ;

            }

            IEnumerable<Condition> conditions = Enumerable.Empty<Condition>();

            if (!string.IsNullOrWhiteSpace(elementName))
            {
                conditions = conditions.Append(
                    new PropertyCondition(AutomationElement.NameProperty, elementName, PropertyConditionFlags.IgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(automationId))
            {
                conditions = conditions.Append(
                    new PropertyCondition(AutomationElement.AutomationIdProperty, automationId, PropertyConditionFlags.IgnoreCase));
            }

            conditions = conditions.Append(
                new PropertyCondition(AutomationElement.ControlTypeProperty, getControlType(type)));

            Condition condition = null;

            if (conditions.Count() == 1)
            {
                condition = conditions.ElementAt(0);
            }
            else
            {
                condition = new AndCondition(conditions.ToArray());
            }

            AutomationElement foundElement = _root.FindFirst(TreeScope.Descendants, condition);

            if (foundElement != null)
            {
                return new UserInterfaceControl(foundElement);
            }

            return new UserInterfaceControl();
        }

        /// <summary>
        /// Method to invoke (click) given button
        /// </summary>
        /// <param name="control">button to invoke/click</param>
        /// <returns>returning this same element as was given in parameter</returns>
        public UserInterfaceControl Invoke(UserInterfaceControl control)
        {
            AutomationElement element = getAutomationElement(control);

            if (element.Current.IsEnabled)
            {
                object pattern;

                if (element.TryGetCurrentPattern(InvokePattern.Pattern, out pattern))
                {
                    ((InvokePattern)pattern).Invoke();
                }
            }

            return new UserInterfaceControl(element);
        }

        /// <summary>
        /// Method to get AutomationElement from given UserInterfaceControl
        /// </summary>
        /// <param name="control">given element to change to AutomationElement</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Throw exception when unable to find element</exception>
        private AutomationElement getAutomationElement(UserInterfaceControl control)
        {
            if (control == null)
            {
                return null;
            }

            AutomationElement element =
                _root.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(
                        AutomationElement.RuntimeIdProperty, control.RuntimeId, PropertyConditionFlags.None));

            if (element == null)
            {
                throw new NotImplementedException();
            }

            return element;
        }

        /// <summary>
        /// Method that change ControlType to type ID
        /// </summary>
        /// <param name="type">Enum of element type</param>
        /// <returns>Return ControlType converter from Enum</returns>
        private ControlType getControlType(AutomationControlType type)
            => ControlType.LookupById(50000 + (int)type);
    }

    /// <summary>
    /// Class contains most important information from AutomationElement
    /// </summary>
    public class UserInterfaceControl
    {
        public bool IsEnabled;
        public int ProcessId { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string AutomationId { get; set; }
        public int[] RuntimeId { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserInterfaceControl() { }

        /// <summary>
        /// Default constructor with property initializing.
        /// </summary>
        /// <param name="automationElement">Element to convert</param>
        public UserInterfaceControl(AutomationElement automationElement)
        {
            if (automationElement == null)
            {
                return;
            }

            RuntimeId = automationElement.GetRuntimeId();
            ProcessId = automationElement.Current.ProcessId;
            Status = automationElement != null;
            IsEnabled = automationElement.Current.IsEnabled;
            Name = automationElement.Current.Name;
            AutomationId = automationElement.Current.AutomationId;
            Type = automationElement.Current.ControlType.ProgrammaticName;
        }
    }

}


