# 캡스톤 디자인
## 프로젝트 명
- ARCore을 이용한 보드게임
## 개발환경
1. IDE
    - Visual Studio
1. game Engine
    - Unity
1. SDK
    - ARcore
1. PlatForm
    - Android
## 프로젝트 내용
안드로이드 상에서 게임을 진행합니다. 이때 게임 오브젝트들을 ARcore을 이용하여 생성하고, 그 생성된 오브젝트는 각 단계별 맵이 됩니다.게임 플레이는 시작 타워에서 발사하는 레이저가(“RayCast”를 이용하여 구현) 거울에 반사되어 마지막 목표 오브젝트에 부딛치면 게임을 클리어하게 됩니다. 클리어 하면 성공여부 및 시간을 PlayerPrefebs를 이용하여 핸드폰 내부에 저장하게 되고, 다시 플레이시 상단에 저장된 시간을 보여주고, 스테이지에서 클리어 한 부분을 보여줍니다.

## WorkFlow
게임을 시작하면 2개의 버튼이 있습니다. 종료 및 시작입니다.
종료를 누르면 프로그램을 종료시킵니다.
시작을 누르면, 스테이지 선택 창이 팝업되고, 팝업된 창에서 “Cancel” 버튼을 누르면 다시 시작 창으로 돌아가고, 스테이지를 누르면 그에 맞는 스테이지 씬으로 전환됩니다.

전환 후 DetectPlain함수를 이용하여 현재 카메라에 들어온 평면(바닥 및 벽)을 인식 후
인식된 곳을 터치하여 Prefeb(맵)을 생성합니다.

생성되면 타워 객체(레이저 시작지점)에서 레이저를 발사하고, 레이저가 Hit 한 객체의 Tag가 “Reflector”이라면 레이저를 반사하고(reflector를 이용하여 반사각으로 반사됩니다.) 만약 Tag 가 “Reflector가 아니라면 Ray를 멈춥니다. 마지막으로 Tag가 “Goal”이라면 클리어가 되고, PlayerPrefeb 함수를 이용하여 클리어 여부를 및 최고점수 판단 후 저장합니다.
그 후 Stage Clear 창을 팝업 시키고, Restart, Next, Home 버튼 중 1개를 클릭합니다.
이때 Next를 클릭한다면 다음 스테이지 씬을 호출하고, Restart를 클릭한다면 현재 씬을 재호출하고, Home을 클릭한다면 시작 씬을 호출합니다.

![workflow](https://i.ibb.co/qkQBSW1/workflow.png)

## UI
1. Stage 선택 화면
  - ![Stage](https://i.ibb.co/7Ck0Lr0/aa.png)
3. Ingame 화면
  - |플레이중 | 클리어|
    |-------|--------|
    |![play](https://i.ibb.co/2kJ1bD8/ingame.png)|![clear](https://i.ibb.co/wsnHkFG/clear.png)|
## 기대효과
1. 사용자가 이제 핸드폰안에서만 게임을 하는게 아닌 AR을 이용하여 게임을 할 수 있습니다.

2. 현재 구현한 오브젝트들이 얼마 없지만, 더 실감나는 오브젝트 및 다양한 반사판을 구현한다면 더 실감나는 게임을 사용자가 경험할 수 있을 것 같습니다.

## 소감
입학시 캡스톤 디자인은 게임이 아닌 다른 것으로 구현하려고 하였습니다. 
하지만 , “게임프로그래밍실습”이라는 수업을 수강하고 Unity를 접하고, 지인과 같이 Unity를 이용하여 토이 프로젝트를 진행시 게임을 제작하는 것이 재미가 있었습니다. 
그리하여 기존의 생각과 다르게 게임을 구현하게 되었습니다. 
아직 부족한 부분이 많이 보이는 프로젝트였지만, 이번 프로젝트를 진행하면서 많은 발전이 있어 좋은 경험 이였다고 생각합니다.

## 참조
1. Unity 기능 : https://docs.unity3d.com/kr/2018.4/Manual/index.html 
1. C# 기능 : https://docs.microsoft.com/ko-kr/dotnet/csharp/ 
1. Arcore : https://github.com/google-ar/arcore-unity-sdk 
1. 배경음악 : orion – beach wave http://asq.kr/E15Z2126E8Y7 
  
