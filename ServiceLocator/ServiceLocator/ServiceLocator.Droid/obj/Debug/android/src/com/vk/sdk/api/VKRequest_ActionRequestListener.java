package com.vk.sdk.api;


public class VKRequest_ActionRequestListener
	extends com.vk.sdk.api.VKRequest.VKRequestListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_attemptFailed:(Lcom/vk/sdk/api/VKRequest;II)V:GetAttemptFailed_Lcom_vk_sdk_api_VKRequest_IIHandler\n" +
			"n_onComplete:(Lcom/vk/sdk/api/VKResponse;)V:GetOnComplete_Lcom_vk_sdk_api_VKResponse_Handler\n" +
			"n_onError:(Lcom/vk/sdk/api/VKError;)V:GetOnError_Lcom_vk_sdk_api_VKError_Handler\n" +
			"n_onProgress:(Lcom/vk/sdk/api/VKRequest$VKProgressType;JJ)V:GetOnProgress_Lcom_vk_sdk_api_VKRequest_VKProgressType_JJHandler\n" +
			"";
		mono.android.Runtime.register ("VKontakte.API.VKRequest+ActionRequestListener, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", VKRequest_ActionRequestListener.class, __md_methods);
	}


	public VKRequest_ActionRequestListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VKRequest_ActionRequestListener.class)
			mono.android.TypeManager.Activate ("VKontakte.API.VKRequest+ActionRequestListener, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void attemptFailed (com.vk.sdk.api.VKRequest p0, int p1, int p2)
	{
		n_attemptFailed (p0, p1, p2);
	}

	private native void n_attemptFailed (com.vk.sdk.api.VKRequest p0, int p1, int p2);


	public void onComplete (com.vk.sdk.api.VKResponse p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (com.vk.sdk.api.VKResponse p0);


	public void onError (com.vk.sdk.api.VKError p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.vk.sdk.api.VKError p0);


	public void onProgress (com.vk.sdk.api.VKRequest.VKProgressType p0, long p1, long p2)
	{
		n_onProgress (p0, p1, p2);
	}

	private native void n_onProgress (com.vk.sdk.api.VKRequest.VKProgressType p0, long p1, long p2);

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
