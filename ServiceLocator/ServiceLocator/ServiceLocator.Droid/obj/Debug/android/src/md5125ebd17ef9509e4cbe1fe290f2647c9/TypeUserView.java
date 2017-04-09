package md5125ebd17ef9509e4cbe1fe290f2647c9;


public class TypeUserView
	extends md5e187c0d02747d8043bdf48cfd714108a.BaseView_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ServiceLocator.Droid.TypeUserView, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TypeUserView.class, __md_methods);
	}


	public TypeUserView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == TypeUserView.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.TypeUserView, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
