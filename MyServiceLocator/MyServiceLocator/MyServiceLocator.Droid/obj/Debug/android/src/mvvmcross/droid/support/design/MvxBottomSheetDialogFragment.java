package mvvmcross.droid.support.design;


public abstract class MvxBottomSheetDialogFragment
	extends md5e7658b1e01f422056e23c5c69cc2b3ca.MvxEventSourceBottomSheetDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("MvvmCross.Droid.Support.Design.MvxBottomSheetDialogFragment, MvvmCross.Droid.Support.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MvxBottomSheetDialogFragment.class, __md_methods);
	}


	public MvxBottomSheetDialogFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MvxBottomSheetDialogFragment.class)
			mono.android.TypeManager.Activate ("MvvmCross.Droid.Support.Design.MvxBottomSheetDialogFragment, MvvmCross.Droid.Support.Design, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
