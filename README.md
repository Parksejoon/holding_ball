Holding Ball
===

Holding ball 프로젝트는 개인적인 학습을 목적으로 개발된 간단한 아케이드 게임입니다.
***

## 게임 규칙

### 조작
	화면을 길게 터치하면 외곽선이 생성되며 외곽선이 점점커집니다.
    이 외곽선이 다른 홀더에 닿는 순간 손을 떼면 외곽선이 사라집니다.
    이때 외곽선 위의 탄막들 중 가장 가까운 홀더를 향해 날아갑니다.

### 득점 방식 
    외곽선이 사라지기 시작할 때 그 위에 있던 홀더의 갯수와 거리에 비례하여 점수가 올라갑니다.

### 게임 오버
	붉은색 벽 또는 레이저에 공이 닿았을 때 게임이 종료됩니다.

### 스킬
	벽에 닿을 때 마다 스킬이 초기화 되며, 스킬은 드래그를 하여 사용하고 사용시 해당 방향으로 공을 1회 드래그할 수 있습니다.

## 게임 오브젝트

### 벽
    벽은 100점마다 생성되며 레이저에 맞을 시 HP가 감소하여 0이되면 사라집니다.
    또한 돌고있는 궤도에 따라서 최대 HP가 다릅니다.

### 레이저
    레이저는 일정 시간마다 발사되며 게임이 오래 진행될수록 많고 빠른속도의 레이저가 발사됩니다.

### 스팟 포인트
    스팟 포인트는 일정 시간마다 생성되며 공이 이 경계에 있을 때 밝기가 점점 밝아지며 밝기가 최대가 되면 잭팟이 터집니다.
    잭팟은 랜덤으로 하나의 궤도를 HP1의 상태로 복구시켜줍니다.

### 홀더
    홀더는 다양한 방식으로 중앙에서 나오는 탄막입니다.
    이 홀더를 통해 점수를 얻고 이동을 할 수 있습니다.

