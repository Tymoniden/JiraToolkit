# JiraToolkit

## Usage
The jira toolkit application is used to provide an easy way to manage multiple jira environments. It is possible to fill in multiple different jira urls and prefixes for tickets. The textboxes for each environment allow the user to enter a number, press enter and jump directly to the ticket, without opening jira and manually searching for the tickets.

![alt text](./Images/application.png "Example configuration, with two jiras and three ticket prefixes for each jira.")


## Configuration

Use the configuration.json to configure the application, fill it with jira environments and ticket prefixes. An example configuration is included after installation in the application folder.

### Example Configuration

```json
{
  "Environments" :[
    {
      "Name": "My Jira",
      "Prefixes": [
        "PREFIX1",
        "PREFIX2",
        "PREFIX3"
      ],
      "RootUrl": "https://Url.To.My.Jira.com/"
    },
    {
      "Name": "My Jira Two",
      "Prefixes": [
        "PREFIX4",
        "PREFIX5",
        "PREFIX6"
      ],
      "RootUrl": "https://Url.To.My.JiraTwo.com/"
    }

  ]
}
```