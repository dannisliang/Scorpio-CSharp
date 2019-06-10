﻿using System;
using System.Collections.Generic;
using Scorpio.Function;
using Scorpio.Tools;
namespace Scorpio.Userdata {
    /// <summary> 普通Object Type类型 </summary>
    public class ScriptUserdataType : ScriptUserdata {
        protected UserdataType m_UserdataType;
        protected Dictionary<string, ScriptValue> m_Methods = new Dictionary<string, ScriptValue>();
        public ScriptUserdataType(Type value, UserdataType type) {
            this.m_Value = value;
            this.m_ValueType = value;
            this.m_UserdataType = type;
        }
        public override Type ValueType { get { return Util.TYPE_TYPE; } }
        public override ScriptValue Call(ScriptValue thisObject, ScriptValue[] parameters, int length) {
            return new ScriptValue(m_UserdataType.CreateInstance(parameters, length));
        }
        public override ScriptValue GetValue(string key) {
            if (m_Methods.ContainsKey(key)) return m_Methods[key];
            var ret = m_UserdataType.GetValue(null, key);
            if (ret is UserdataMethod) {
                return m_Methods[key] = new ScriptValue(new ScriptStaticMethodFunction((UserdataMethod)ret));
            }
            return ScriptValue.CreateObject(ret);
        }
        public override void SetValue(string key, ScriptValue value) {
            m_UserdataType.SetValue(null, key, value);
        }
        public override string ToString() { return m_ValueType.Name; }
    }
}