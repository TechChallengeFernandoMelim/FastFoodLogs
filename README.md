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

A execução do projeto pode ser feita buildando o dockerfile na raiz do repositório e depois executando a imagem gerada em um container. O serviço foi testado sendo executado direto pelo visual Studio e pela AWS.

## Testes

