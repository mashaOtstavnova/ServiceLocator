package md5f07b4c044a0165b3b7e6615fa34ca015;


public abstract class VKCallback_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.vk.sdk.VKCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onError:(Lcom/vk/sdk/api/VKError;)V:GetOnError_Lcom_vk_sdk_api_VKError_Handler:VKontakte.IVKCallbackInvoker, VKontakte.Android\n" +
			"n_onResult:(Ljava/lang/Object;)V:GetOnResult_Ljava_lang_Object_Handler:VKontakte.IVKCallbackInvoker, VKontakte.Android\n" +
			"";
		mono.android.Runtime.register ("VKontakte.VKCallback`1, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", VKCallback_1.class, __md_methods);
	}


	public VKCallback_1 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VKCallback_1.class)
			mono.android.TypeManager.Activate ("VKontakte.VKCallback`1, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onError (com.vk.sdk.api.VKError p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.vk.sdk.api.VKError p0);


	public void onResult (java.lang.Object p0)
	{
		n_onResult (p0);
	}

	private native void n_onResult (java.lang.Object p0);

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
