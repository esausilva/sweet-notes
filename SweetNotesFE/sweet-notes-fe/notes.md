# Docker

To build Docker image

```
docker build -t esausilva/sweetnotes.fe .
```

To run the image

```
docker run -p 3050:3050 --name SweetNotes.Fe esausilva/sweetnotes.fe
```

# Docker Compose

To build Docker image

```
COMPOSE_DOCKER_CLI_BUILD=1 DOCKER_BUILDKIT=1 docker-compose build
```

Ro run the image

```
docker-compose up
```

### Links

https://nextjs.org/docs/pages/building-your-application/deploying#docker-image

https://github.com/vercel/next.js/blob/canary/examples/with-docker/README.md

https://stackoverflow.com/questions/70608086/i-am-getting-error-while-converting-my-next-js-project-to-docker
