# Create Console App

## Start Ollama with Docker

Run the following command to download and start the Ollama Docker container:

```bash
docker run -d --name ollama -p 11434:11434 -v ollama:/root/.ollama ollama/ollama
```

## Pull the Qwen3 Model

After the container is running, download the Qwen3 model:

```bash
docker exec -it ollama ollama pull qwen3
```

## Verify Ollama is Running

Open the following URL in your browser:

```
http://localhost:11434/
```

If Ollama is running successfully, the endpoint should return a response.

## View Installed Models

### Using the Command Line

```bash
curl http://localhost:11434/api/tags
```

### Using a Browser

Open:

```
http://localhost:11434/api/tags
```

You should see a JSON response similar to:

```json
{
  "models": [
    {
      "name": "qwen3"
    }
  ]
}
```

## Troubleshooting

To verify that the Ollama container is running:

```bash
docker ps
```

You should see a running container named `ollama`.



# NOTE : 

```
OllamaSharp
OllamaSharp.OllamaApiClientExtensions.GenerateAsync(...)
Microsoft.Extensions.AI
Microsoft.Extensions.AI.EmbeddingGeneratorExtensions.GenerateAsync(...)

So when you call:

ollama.GenerateAsync(...)

👉 C# says: “Which GenerateAsync do you mean?”

```