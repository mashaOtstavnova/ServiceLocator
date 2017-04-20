package md5d858ccb213f1b7baa8a38afe447463c3;


public class CategoryServiceDialog
	extends mvvmcross.droid.support.v7.appcompat.MvxAppCompatDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateDialog:(Landroid/os/Bundle;)Landroid/app/Dialog;:GetOnCreateDialog_Landroid_os_Bundle_Handler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"";
		mono.android.Runtime.register ("ServiceLocator.Droid.Views.Dialogs.CategoryServiceDialog, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CategoryServiceDialog.class, __md_methods);
	}


	public CategoryServiceDialog () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CategoryServiceDialog.class)
			mono.android.TypeManager.Activate ("ServiceLocator.Droid.Views.Dialogs.CategoryServiceDialog, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public android.app.Dialog onCreateDialog (android.os.Bundle p0)
	{
		return n_onCreateDialog (p0);
	}

	private native android.app.Dialog n_onCreateDialog (android.os.Bundle p0);


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();

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
