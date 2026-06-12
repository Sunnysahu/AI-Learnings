## Create Console App

Run this code to pulling and install Ollama Docker image:

```
docker run -d --name ollama -p 11434:11434 -v ollama:/root/.ollama ollama/ollama
```

Then run this code to pull the qwen3 model:

```
docker exec -it ollama ollama pull qwen3
```


To check container is running then

```
http://localhost:11434/
```

If you hit the url the you will see the models :

In CMD
```
curl http://localhost:11434/api/tags
```

in Browser
```
http://localhost:11434/api/tags
```



