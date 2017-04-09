package com.vk.sdk.api;


public class VKBatchRequest_ActionBatchRequestListener
	extends com.vk.sdk.api.VKBatchRequest.VKBatchRequestListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onComplete:([Lcom/vk/sdk/api/VKResponse;)V:GetOnComplete_arrayLcom_vk_sdk_api_VKResponse_Handler\n" +
			"n_onError:(Lcom/vk/sdk/api/VKError;)V:GetOnError_Lcom_vk_sdk_api_VKError_Handler\n" +
			"";
		mono.android.Runtime.register ("VKontakte.API.VKBatchRequest+ActionBatchRequestListener, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", VKBatchRequest_ActionBatchRequestListener.class, __md_methods);
	}


	public VKBatchRequest_ActionBatchRequestListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VKBatchRequest_ActionBatchRequestListener.class)
			mono.android.TypeManager.Activate ("VKontakte.API.VKBatchRequest+ActionBatchRequestListener, VKontakte.Android, Version=1.6.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onComplete (com.vk.sdk.api.VKResponse[] p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (com.vk.sdk.api.VKResponse[] p0);


	public void onError (com.vk.sdk.api.VKError p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.vk.sdk.api.VKError p0);

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
