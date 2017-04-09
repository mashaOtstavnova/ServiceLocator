package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("ServiceLocator.Droid.ServiceLocatorApplication, ServiceLocator.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", otstavnova.ServiceLocator.ServiceLocator.class, otstavnova.ServiceLocator.ServiceLocator.__md_methods);
		
	}
}
