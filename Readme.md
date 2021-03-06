# Hackathon Submission Entry form

## Team name
⟹ SUGEC

## Category
⟹ The best enhancement to the Sitecore Admin (XP) for Content Editors & Marketers

## Description
⟹ Why this module?
During the reviewing process of content, users review and verify content before it is published. No matter what their roles are, they need to own credentials to log in into the system and perform their duties. 
There is a need where external users may be able to review content and provide feedback to authors in order to avoid unwanted content to be live. This Module, External Reviewers, let users that are not part of the CMS add comments to items and interact with other reviewers.

Basically, the content authors generates a temporarily URL that will be exposed to external users and will be able to see a preview of a draft page and add comments to it. The link will have a expiration date which means that the link won't be permanent will have a period of time to be "public".

An agent was implemented to remove the expired links if there is no manual deletion.

![External Reviwers](docs/images/external-reviewers.png?raw=true "External Reviewers")

   
## Video link
⟹ Here is the presentation of this module. 

⟹ [Replace this Video link](#video-link)


## Installation instructions
⟹ Please follow this instructions to install this module:

1. Sitecore package [package](resources/Hackathon2021-SUGEC-1.0.zip)
	- Use the Sitecore Installation Wizard to upload the package and install the module. This will install all Sitecore items and files necessary to run the new site in a new instance of Sitecore 10.1. 
	
2. Publish Site
	- Run a smart publish of the site
	
3. 

### Configuration
⟹ This module requires one additional step to have everything working.

1. Locate /App_Config/Include/Feature/Fature.ExternalReviewers  
2. Search for a setting named "Feature.ExternalReviewers.CDsHostname"
3. Update the value with your CD hostname. In case you are running the application in a standalone environment, you can leave this value empty, it will default your site's hostname.
``` 
<settings> 
 	<setting name="Feature.ExternalReviewers.CDsHostname" value="" >
<settings>```

## Usage instructions
⟹ For Administrators
First, let's make sure the website is up and running. Go to https://yourdomain/ and see if the Sitecore Hackathon home page is displayed.

Once the content authors  start generating links for external reviewers, those links will be stored under "/sitecore/system/external reviews"

![Ad Step 1](docs/images/ad-step1.png?raw=true "Ad Step 1")

The agent that will remove expired links is defined in the "App_Config/Include/Feature/Fature.ExternalReviewers.config". The default interval is 2 hours, you can update it as required.

![Ad Step 2](docs/images/ad-step2.png?raw=true "Ad Step 2")


⟹ For Content Authors
As an editor, you will probably create or edit a page.
Let's edit a page that is already in a workflow state. 

![CA Step 1](docs/images/ca-step1.png?raw=true "CA Step 1")

Before we submit our changes for approval, we could create a preview of the page and share with others that are not part of the system as users. Just select the item and locate within the Publish ribbon tab a button called "Create Review". 

![CA Step 2](docs/images/ca-step2.png?raw=true "CA Step 2")

Once you click on the button, a dialog box will be prompted and will ask for an expiration date. Select a date prior the current date and save it.

![CA Step 3](docs/images/ca-step3.png?raw=true "CA Step 3")

It will generate a link which you can share with external reviewers that can be part of the reviewing proccess. 

![CA Step 4](docs/images/ca-step4.png?raw=true "CA Step 4")

Navigate to the generated URL and you will see the draft version of the page, which is not live yet. 

![CA Step 5](docs/images/ca-step5.png?raw=true "CA Step 5")


⟹ For External Reviewers
Since your are not part of the system and you don;t have credentials to access to Sitecore, someone would share an external link with you via email or any other method.  

![ER Step 1](docs/images/er-step1.png?raw=true "ER Step 1")

Open the shared link and you will be able to see the page in draft status. 

![ER Step 2](docs/images/er-step2.png?raw=true "ER Step 2")

At the right, you will see a Comments panel where you can see what other have commented.

![ER Step 3](docs/images/er-step3.png?raw=true "ER Step 3")

If you want to interact with other reviewers, you must provide a user name and the comment box will be available to add text.

![ER Step 4](docs/images/er-step4.png?raw=true "ER Step 4")


## Comments
There are a lot that can be improved but this is just the starting point.

Enjoy this module ;)

![Hack the Experience](docs/images/SCHackathon.png?raw=true "Hack the Experience")
