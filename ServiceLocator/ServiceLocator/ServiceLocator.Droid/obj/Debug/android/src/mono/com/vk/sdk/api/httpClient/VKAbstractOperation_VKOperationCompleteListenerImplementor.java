package mono.com.vk.sdk.api.httpClient;


public class VKAbstractOperation_VKOperationCompleteListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.vk.sdk.api.httpClient.VKAbstractOperation.VKOperationCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onComplete:()V:GetOnCompleteHandler:VKontakte.API.HTTPClient.VKAbstractOperation/IVKOperationCompleteListenerInvoker, VKontakte.Android\n" +
			"";
		mono.android.Runtime.register ("VKontakte.API.HTTPClient.VKAbstractOperation+IVKOperationCompleteListenerImplementor, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", VKAbstractOperation_VKOperationCompleteListenerImplementor.class, __md_methods);
	}


	public VKAbstractOperation_VKOperationCompleteListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VKAbstractOperation_VKOperationCompleteListenerImplementor.class)
			mono.android.TypeManager.Activate ("VKontakte.API.HTTPClient.VKAbstractOperation+IVKOperationCompleteListenerImplementor, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onComplete ()
	{
		n_onComplete ();
	}

	private native void n_onComplete ();

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
