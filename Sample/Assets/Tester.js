#pragma strict

function Start () {
//	AdBannerObserver.Initialize("a150b64dbe06284", "test_device_code_here", 30.0,
//								AdBannerObserver.LayoutGravity.BOTTOM|AdBannerObserver.LayoutGravity.CENTER);	// Publisher ID
    AdBannerObserver.Initialize("be251ef920884ee2", "test_device_code_here", 30.0,
								AdBannerObserver.LayoutGravity.BOTTOM|AdBannerObserver.LayoutGravity.CENTER); // Mediation ID
}

function Update () {
	if( Input.GetKey(KeyCode.Escape) ){
		Application.Quit();
	}
}

