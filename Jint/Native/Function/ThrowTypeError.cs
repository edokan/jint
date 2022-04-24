﻿using Jint.Runtime;
using Jint.Runtime.Descriptors;

namespace Jint.Native.Function
{
    public sealed class ThrowTypeError : FunctionInstance
    {
        public ThrowTypeError(Engine engine, Realm realm)
            : base(engine, realm, null)
        {
            _length = PropertyDescriptor.AllForbiddenDescriptor.NumberZero;
            _nameDescriptor = new PropertyDescriptor(JsString.Empty, PropertyFlag.AllForbidden);
            _environment = realm.GlobalEnv;
            PreventExtensions();
        }

        protected internal override JsValue Call(JsValue thisObject, JsValue[] arguments)
        {
            ExceptionHelper.ThrowTypeError(_realm);
            return null;
        }
    }
}
