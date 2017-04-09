package otstavnova.ServiceLocator;


public class ServiceLocator_TokenTracker
	extends com.vk.sdk.VKAccessTokenTracker
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVKAccessTokenChanged:(Lcom/vk/sdk/VKAccessToken;Lcom/vk/sdk/VKAccessToken;)V:GetOnVKAccessTokenChanged_Lcom_vk_sdk_VKAccessToken_Lcom_vk_sdk_VKAccessToken_Handler\n" +
			"";
		mono.android.Runtime.register ("ServiceLocator.Droid.ServiceLocatorApplication+TokenTracker, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ServiceLocator_TokenTracker.class, __md_methods);
	}


	public ServiceLocator_TokenTracker () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ServiceLocator_TokenTracker.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.ServiceLocatorApplication+TokenTracker, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onVKAccessTokenChanged (com.vk.sdk.VKAccessToken p0, com.vk.sdk.VKAccessToken p1)
	{
		n_onVKAccessTokenChanged (p0, p1);
	}

	private native void n_onVKAccessTokenChanged (com.vk.sdk.VKAccessToken p0, com.vk.sdk.VKAccessToken p1);

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
