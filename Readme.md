# Neural Network made from scratch

Fiz essa rede neural a partir do zero para fins de estudo. Você é livre para alterar e usar conforme sua necessidade. 

Você pode acessar ```ConsoleApp.Example``` para verificar um exemplo de uso da rede neural. 

Perceba que é necessário configurar um ```DataManager``` e um ```Trainer```. 

Tentei deixar as classes o mais autoexplicativo quanto fosse possível, mas posso ter falhado em algum momento, então vou explicar algumas classes abaixo que podem introduzir uma ideia do funcionamento de todo o código:

```NeuronGenerator```: ele gera os neurônios pra você. Você poderia gerar neurônio por neurônio se quisesse. Esse projeto: ```MyNeuralNetwork.Tests.Utils``` possui um exemplo de como gerar neurônios. 

```NNGenerator```: ele gera uma rede neural inteira pra você. Você só precisa passar alguns parâmetros para configuração. Se você estiver habituado a usar modelos de redes neurais você vai entender o que está acontoecendo no método ```Generate```. ```Relu``` ou ```Sigmoid``` ou ```Tanh``` são classes de ativação da rede. 

```SynapseManager```: ele gerencia a interação entre os neurônios. Ele envia a informação entre as camadas de neurônios e transmite também informações de pesos e bias. Dê uma explorada no código para melhor entendimento. 

```FeedForward``` e ```Backpropagation```: essas classes não possuem substituições, mas eu deixei acessíveis a mais alto nível caso o usuário quisesse modificar o funcionamento de qualquer uma delas. Se você está habituado ao uso de redes neurais, vai se identificar com os termos. 