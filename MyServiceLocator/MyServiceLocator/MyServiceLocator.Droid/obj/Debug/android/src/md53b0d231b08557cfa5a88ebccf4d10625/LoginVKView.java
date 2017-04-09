package md53b0d231b08557cfa5a88ebccf4d10625;


public class LoginVKView
	extends md53b0d231b08557cfa5a88ebccf4d10625.BaseView_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("MyServiceLocator.Droid.Views.LoginVKView, MyServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginVKView.class, __md_methods);
	}


	public LoginVKView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoginVKView.class)
			mono.android.TypeManager.Activate ("MyServiceLocator.Droid.Views.LoginVKView, MyServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
