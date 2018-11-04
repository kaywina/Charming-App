// Project:			Magic Charms by Anachronic Designs
// Coder(s):		Kevin Afanasiff
// Last Updated:	Nov 4, 2018

/*
 * This script controls posting content to social media networks 
 * (Twitter, Facebook, GooglePlus, Reddit, LinkedIn),
 * and accessing social media sites for the game.
 * */

using UnityEngine;
using System.Collections;

public static class SocialMediaManager {

	static public void OpenTwitterPage () {
		Application.OpenURL ("https://twitter.com/anachronicgames");
	}

	static public void OpenFacebookPage() {
		Application.OpenURL ("https://www.facebook.com/pages/category/Games-Toys/Anachronic-Designs-508626405847222/");
	}

    static public void OpenWebsite()
    {
        Application.OpenURL("https://anachronicdesigns.com/");
    }

    private static string shareLink = "https://anachronicdesigns.com/";

    static public void ShareToTwitter () {
		string tweetText = "I'm using Magic Charms! Check it out: " + shareLink;
		Application.OpenURL ("http://twitter.com/intent/tweet" + "?text=" + WWW.EscapeURL (tweetText) 
			+ "&amp;lang=" + WWW.EscapeURL ("en") + "&amp;via=" + WWW.EscapeURL("MagicCharms") 
			+ "&amp;hashtags=" + WWW.EscapeURL("meditation, selfimprovement"));
	}

	static public void ShareToFacebook () {
		string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" + WWW.EscapeURL(shareLink);
		Application.OpenURL(facebookshare);
	}

	static public void ShareToGooglePlus () {
		string googleShare = "https://plus.google.com/share?url=" + shareLink;
		Application.OpenURL(googleShare);
	}

	static public void ShareToReddit () {
		string redditShare = "http://www.reddit.com/submit/?url=" + shareLink;
		Application.OpenURL(redditShare);
	}

	static public void ShareToLinkedIn () {
		string linkedInShare = "https://www.linkedin.com/shareArticle?mini-true" + "&url=" + shareLink;
		Application.OpenURL(linkedInShare);
	}
}