﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;

namespace Autodesk.AutoCAD.DatabaseServices
{
    public abstract class GripOverrule<T> : GripOverrule where T : Entity
    {
        private readonly RXClass _targetClass = RXObject.GetClass(typeof(T));
        OverruleStatus _status = OverruleStatus.Off;

        public OverruleStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (value == OverruleStatus.On && _status == OverruleStatus.Off)
                {
                    Overrule.AddOverrule(_targetClass, this, true);
                    _status = OverruleStatus.On;
                }
                else if (value == OverruleStatus.Off && _status == OverruleStatus.On)
                {
                    Overrule.RemoveOverrule(_targetClass, this);
                    _status = OverruleStatus.Off;
                }
            }
        }

        protected GripOverrule(OverruleStatus status = OverruleStatus.On)
        {
            Status = status;
        }

    }
}