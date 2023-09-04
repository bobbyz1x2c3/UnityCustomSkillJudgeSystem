
using System;

namespace DataClass.test
{
    public class Listen<T>
    {
        private T _value;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!value.Equals(_value))
                {
                    
                    try
                    {
                        OnValueChanged();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        
                    }
                }
            }
        }
        
        public Listen(T val){
            _value = val;
        }
        public delegate void func();

        public event func OnValueChanged;
    }
}