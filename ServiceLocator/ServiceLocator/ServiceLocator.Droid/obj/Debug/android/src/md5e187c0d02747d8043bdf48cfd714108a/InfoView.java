package md5e187c0d02747d8043bdf48cfd714108a;


public class InfoView
	extends md5e187c0d02747d8043bdf48cfd714108a.BaseView_1
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"";
		mono.android.Runtime.register ("ServiceLocator.Droid.Views.InfoView, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", InfoView.class, __md_methods);
	}


	public InfoView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == InfoView.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.Views.InfoView, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);

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
