package md53b0d231b08557cfa5a88ebccf4d10625;


public abstract class BaseView_1
	extends md5204979768ea66d3a79201c4efd7c602a.MvxCachingFragmentCompatActivity_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MyServiceLocator.Droid.Views.BaseView`1, MyServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseView_1.class, __md_methods);
	}


	public BaseView_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseView_1.class)
			mono.android.TypeManager.Activate ("MyServiceLocator.Droid.Views.BaseView`1, MyServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
