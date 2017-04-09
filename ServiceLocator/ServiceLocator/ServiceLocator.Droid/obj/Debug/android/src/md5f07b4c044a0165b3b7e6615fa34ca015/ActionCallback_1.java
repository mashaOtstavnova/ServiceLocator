package md5f07b4c044a0165b3b7e6615fa34ca015;


public class ActionCallback_1
	extends md5f07b4c044a0165b3b7e6615fa34ca015.VKCallback_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("VKontakte.ActionCallback`1, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", ActionCallback_1.class, __md_methods);
	}


	public ActionCallback_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ActionCallback_1.class)
			mono.android.TypeManager.Activate ("VKontakte.ActionCallback`1, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
