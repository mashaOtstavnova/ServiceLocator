package md5125ebd17ef9509e4cbe1fe290f2647c9;


public class GcmService
	extends md5214eafb7e7b3b7fcc363a68a6358563f.GcmServiceBase
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ServiceLocator.Droid.GcmService, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GcmService.class, __md_methods);
	}


	public GcmService (java.lang.String p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == GcmService.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.GcmService, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public GcmService () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GcmService.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.GcmService, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public GcmService (java.lang.String[] p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == GcmService.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.GcmService, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String[], mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
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
