## Packages Used

This project uses the following two packages:

- **Microsoft.Extensions.AI.OpenAI**
  - `IChatClient` – Common chat interface provided by `Microsoft.Extensions.AI`.
  - `AsChatClient()` – Converts the OpenAI client into the common `IChatClient` abstraction used by `Microsoft.Extensions.AI`.

- **OpenAI**
  - `ChatClient` – Creates a client for interacting with the language model.
  - `ApiKeyCredential` – Authenticates requests using an API key.
  - `OpenAIClientOptions` – Configures the OpenAI client (for example, the endpoint when using GitHub Models or OpenAI).