using System;
namespace DemoLibrary
{
    public class MyClass
    {
        public MyClass()
        {
        }

        public static T FacrotyMethod<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
