using System.Web;
using System.Web.SessionState;

public class Global : System.Web.HttpApplication
{

	#region " Component Designer Generated Code "

	public Global() : base()
	{

		//This call is required by the Component Designer.
		InitializeComponent();

		//Add any initialization after the InitializeComponent() call

	}

	//Required by the Component Designer
	private System.ComponentModel.IContainer components;

	//NOTE: The following procedure is required by the Component Designer
	//It can be modified using the Component Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();
	}

	#endregion

	protected void Application_Start(object sender, EventArgs e)
	{
		// Fires when the application is started
	}

	protected void Session_Start(object sender, EventArgs e)
	{
		// Fires when the session is started
	}

	protected void Application_BeginRequest(object sender, EventArgs e)
	{
		// Fires at the beginning of each request
	}

	protected void Application_AuthenticateRequest(object sender, EventArgs e)
	{
		// Fires upon attempting to authenticate the use
	}

	protected void Application_Error(object sender, EventArgs e)
	{
		// Fires when an error occurs
	}

	protected void Session_End(object sender, EventArgs e)
	{
		// Fires when the session ends
	}

	protected void Application_End(object sender, EventArgs e)
	{
		// Fires when the application ends
	}

}
