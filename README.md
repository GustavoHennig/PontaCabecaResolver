# PontaCabecaResolver

Resolve o quebra cabeças: Ponta Cabeça (Estrela) através de algoritmos genéticos, ou força bruta, se você tiver paciência...



Esse é o jogo que o algoritmo resolve, existem inúmeras variações:

<img src="https://raw.githubusercontent.com/GustavoHennig/PontaCabecaResolver/master/quebra-cabeca-ponta-cabeca-aviao.jpg" width="300">

```

POPULAÇÃO INICIAL, NÃO USAR

"BLU2", "WHI2", "BLU1", "YEL1"

"BLU1", "WHI1", "YEL2", "GRE2"

"YEL1", "BLU2", "WHI2", "GRE1"

"BLU1", "YEL1", "GRE2", "WHI2"

"BLU2", "YEL2", "GRE1", "WHI1"

"BLU1", "GRE1", "WHI2", "YEL2"

"BLU1", "YEL2", "WHI2", "GRE1"

"BLU1", "YEL1", "GRE2", "WHI2"

"YEL2", "GRE1", "WHI1", "GRE2"


MAPA DE POSIÇÕES DAS CARTAS:

0 1 2
3 4 5
6 7 8


SOLUÇÃO:


0: YEL1 BLU2 WHI2 BLU1
1: YEL1 GRE2 WHI2 BLU1
2: BLU1 YEL2 WHI2 GRE1
3: WHI1 GRE2 YEL2 GRE1
4: WHI1 BLU2 YEL2 GRE1
5: WHI1 YEL2 GRE2 BLU1
6: YEL1 GRE2 WHI2 BLU1
7: YEL1 BLU2 WHI2 GRE1
8: GRE1 WHI2 YEL2 BLU1


LEGENDA:

1 = Frente do avião
2 = Cauda do avião
YEL = Amarelo
BLU = Azul
WHI = Branco
GRE = Verde

EXEMPLO (ORIENTAÇÃO DAS CARTAS):


YEL1 BLU2 WHI2 BLU1
CIMA DIR  BAIXO ESQ.
=

        FRENTE
        AMARELO
FRENTE            CAUDA 
AZUL              AZUL
        CAUDA
        BRANCO
```
