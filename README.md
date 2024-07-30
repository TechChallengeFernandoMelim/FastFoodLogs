# FastFoodLogs

O repositorio FastFoodLogs tem por objetivo implementar uma Lambda Function responsável por realizar o cadastro de todos os logs no cloudwatch. Os logs são recebidos via uma fila do SQS.

## Variáveis de ambiente
Todas as variáveis de ambiente do projeto visam fazer integração com algum serviço da AWS. Explicaremos a finalidade de cada uma:

- AWS_ACCESS_KEY_DYNAMO: "Access key" da AWS. Recurso gerado no IAM para podermos nos conectar aos serviços da AWS;
- AWS_SECRET_KEY_DYNAMO: "Secret key" da AWS. Recurso gerado no IAM para podermos nos conectar aos serviços da AWS. Deve ser utilizado corretamente com seu par AWS_ACCESS_KEY_DYNAMO;
- LOG_REGION: Região do Log Group criado no Cloudwatch para monitoramento de logs;
- LOG_GROUP: Nome do Log Group criado no Cloudwatch para monitoramento de logs;

Não é necessário conhecer qualquer configuração sobre a fila do SQS da AWS, pois esse serviço foi feito para ser "trigado" quando uma mensagem chega na fila, e isso foi feito pelo terraform no repositório [FastFoodInfra](https://github.com/TechChallengeFernandoMelim/FastFoodInfra).

## Execução do projeto

Para executar com docker, basta executar o seguinte comando na pasta raiz do projeto para gerar a imagem:

``` docker build -t fast_food_logs -f .\Dockerfile . ```

Para subir o container, basta executar o seguinte comando:

``` 
docker run -e AWS_ACCESS_KEY_DYNAMO=""
-e AWS_SECRET_KEY_DYNAMO=""
-e LOG_REGION=""
-e LOG_GROUP=""
-p 8081:8081 -p 8080:8080 fast_food_logs
```

Observação: as variáveis de ambiente não estão com valores para não expor meu ambiente AWS. Para utilizar o serviço corretamente, é necessário definir um valor para as variáveis.

Além disso, como o projeto foi desenvolvido em .NET, também é possível executá-lo pelo Visual Studio ou com o CLI do .NET.

## Testes

Conforme foi solicitado, estou postando aqui as evidências de cobertura dos testes. A cobertura foi calculada via integração com o [SonarCloud](https://sonarcloud.io/) e pode ser vista nesse [link](https://sonarcloud.io/organizations/techchallengefernandomelim/projects). A integração com todos os repositórios poderá ser vista nesse link.
