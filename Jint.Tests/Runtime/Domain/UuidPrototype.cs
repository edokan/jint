using Jint.Native;
using Jint.Runtime;
using Jint.Runtime.Descriptors;
using Jint.Runtime.Interop;

namespace Jint.Tests.Runtime.Domain
{
    internal sealed class UuidPrototype : UuidInstance
    {
        private UuidPrototype(Engine engine) : base(engine)
        {
        }

        private UuidInstance EnsureUuidInstance(JsValue thisObj)
        {
            return thisObj.TryCast<UuidInstance>(value =>
            {
                throw new JavaScriptException(Engine.Realm.Intrinsics.TypeError, "Invalid Uuid");
            });
        }

        private JsValue ToGuidString(JsValue thisObj, JsValue[] arguments) => EnsureUuidInstance(thisObj).PrimitiveValue.ToString();

        private JsValue ValueOf(JsValue thisObj, JsValue[] arguments) => EnsureUuidInstance(thisObj).PrimitiveValue;

        public static UuidPrototype CreatePrototypeObject(Engine engine, UuidConstructor ctor)
        {
            var obj = new UuidPrototype(engine)
            {
                PrimitiveValue = JsUuid.Empty,
                _prototype = engine.Realm.Intrinsics.Object.PrototypeObject,
            };

            obj.FastSetProperty("constructor", new PropertyDescriptor(ctor, false, false, true));

            return obj;
        }

        public void Configure()
        {
            FastSetProperty("toString", new PropertyDescriptor(new ClrFunctionInstance(Engine, "toString", ToGuidString), true, false, true));
            FastSetProperty("valueOf", new PropertyDescriptor(new ClrFunctionInstance(Engine, "valueOf", ValueOf), true, false, true));
        }
    }
}
