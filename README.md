## About

**Oxide.Ext.Discord** is an Oxide extension which acts as a bridge between Oxide and the Discord API.

Should you encounter a problem or bug with the extension, please feel free to create an issue here. Try to include as much detail as possible, including steps to reproduce the issue. A code example is highly appreciated.

## Upgrading to version 2.0.0
Before upgrading to version 2.0.0 make sure all the discord extension plugins you plan to use support the new version. 
Plugins that were made for version 1.0.0 are **not** compatible with version 2.0.0.

## Installation

To install the extension to your Oxide server, you must follow a few simple steps:
1) Open the server files, and navigate to the "Managed" folder (eg. "RustDedicated_Data/Managed")
2) Download the latest release.
3) Copy the "Oxide.Ext.Discord.dll" file into your "Managed" folder.
4) Restart your server!

## Developer

If you would like to create plugins for the extension please [Click Here](https://github.com/Kirollos/Oxide.Ext.Discord/blob/master/Docs/README.md) to learn more  
**If the link doesn't work please try the readme on the github page**

## Getting your API Key

An API key is used to authenticate requests made to and from Discord.

**Note: DO NOT SHARE YOUR API KEY!**   
Sharing your key may result in punishments from Discord (including a platform-wide ban) if the token is used to abuse the API.

Obtaining an API Key:
1) Visit the official Discord Developers page here: [Discord Developer Documentation](https://discordapp.com/developers/applications/me)
2) Click "New App".  
   ![](https://i.postimg.cc/ZKwQdZZP/1-New-Application.png)
3) Name your app and click create!  
   ![](https://i.postimg.cc/Vk5V9TLx/2-Create-App-Name.png)
4) You will now be redirected to your created app. Click on "Bot" on the left hand side and then "Add Bot".  
   ![](https://i.postimg.cc/htw32rXf/3-Add-Bot.png)
5) A) Under the newly created bot section, Enter the username for your bot and upload an icon.  
   B) Then enable the Presence and Server Members Intent and Message Content Intent.  
   C) To get your Discord API Token click on the "Copy" button.  
   D) This is the token that is used by discord extension plugins.     
   ![](https://i.postimg.cc/WzPFSttB/5-discord-intents.png)
6) A.Next we're going to setup the permissions that bot has in your Discord Server.  
   B.Click on OAuth2 on the left hand side.  
   C. Scroll down till your see scopes and permissions.  
   D. Under Scope select "Bot".  
   E. __(Optional)__ Select "applications.commands"   
   F. Under permissions select which permissions that bot should have.  
   G. Once you have all of this selected click on the copy button.    
   ![](https://i.postimg.cc/ZnXStyHc/image.png)
   __(Optional)__ application.commands selection
   ![](https://i.postimg.cc/RF20DgPZ/optional-application-commands.png)
7) Now it's time to add your new bot to your guild!
   Paste the link from the previous step into the url section of your browser.
   Select which Discord Server you want to invite the bot into and continue and then authorize.  
   ![](https://i.postimg.cc/JnPXqRxm/image.png)
8) Your bot will now be in your discord server.

## Configuration

The discord extension configuration can be found at oxide/discord.config.json. 
The config allows you to modify the command prefixes for commands that use the discord extension command library.

```json
{
  "Commands": {
    "Command Prefixes": [
      "/",
      "!"
    ]
  }
}
```

## Contributing

Want to contribute? Create a fork of the repo and create a pull request for any changes you wish to make! 
