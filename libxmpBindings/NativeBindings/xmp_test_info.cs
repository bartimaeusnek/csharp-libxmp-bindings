namespace libxmpBindings.NativeBindings;

using System.Runtime.CompilerServices;

public partial struct xmp_test_info
{
    public string Name
    {
        get
        {
            unsafe
            {
                fixed (sbyte* ptr = &name.e0)
                {
                    return new string(ptr);
                }
            }
        }
    }

    public string Type
    {
        get
        {
            unsafe
            {
                fixed (sbyte* ptr = &type.e0)
                {
                    return new string(ptr);
                }
            }
        }
    }
    
    [NativeTypeName("char[64]")]
    public _name_e__FixedBuffer name;

    [NativeTypeName("char[64]")]
    public _type_e__FixedBuffer type;

    [InlineArray(64)]
    public partial struct _name_e__FixedBuffer
    {
        public sbyte e0;
    }

    [InlineArray(64)]
    public partial struct _type_e__FixedBuffer
    {
        public sbyte e0;
    }
}
