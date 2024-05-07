# FastFoodLogs

O repositorio FastFoodLogs tem por objetivo implementar uma Lambda Function respons�vel por realizar o cadastro de todos os logs no cloudwatch. Os logs s�o recebidos via uma fila do SQS.

## Vari�veis de ambiente
Todas as vari�veis de ambiente do projeto visam fazer integra��o com algum servi�o da AWS. Explicaremos a finalidade de cada uma:

- AWS_ACCESS_KEY_DYNAMO: "Access key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS;
- AWS_SECRET_KEY_DYNAMO: "Secret key" da AWS. Recurso gerado no IAM para podermos nos conectar aos servi�os da AWS. Deve ser utilizado corretamente com seu par AWS_ACCESS_KEY_DYNAMO;
- LOG_REGION: Regi�o do Log Group criado no Cloudwatch para monitoramento de logs;
- LOG_GROUP: Nome do Log Group criado no Cloudwatch para monitoramento de logs;

N�o � necess�rio conhecer qualquer configura��o sobre a fila do SQS da AWS, pois esse servi�o foi feito para ser "trigado" quando uma mensagem chega na fila, e isso foi feito pelo terraform no reposit�rio [FastFoodInfra](https://github.com/TechChallengeFernandoMelim/FastFoodInfra).

## Execu��o do projeto

A execu��o do projeto pode ser feita buildando o dockerfile na raiz do reposit�rio e depois executando a imagem gerada em um container. O servi�o foi testado sendo executado direto pelo visual Studio e pela AWS.

## Testes

