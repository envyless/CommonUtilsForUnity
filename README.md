# CommonUtilsForUnity

Unity 작업중 항상 쓰면서 자주 쓰는 녀석들을 모듈화 하여 베이스 단의 구현들을 모아놓았다.

# feature

- Direct Compute 등을 사용할때 쓰이는 자료 구조및 설계 베이스
- Line Drawer 등을 이용하여 런타임에서도 자유롭게 백터등의 디버깅이 가능하다.
- Quad Tree 를 이용하여 NavMesh를 저장 및 인덱싱 하기
- Performance Checker, but 조심해야 한다. jit이기 때문에 정확하게 체크하고 싶다면 benchmark를 이용해야함.
- LSRecord 모듈 : 해당 모듈을 베이스로 다시보기를 만들었다. 원하는 모듈은 어트리뷰트로 지원하여 해당 함수에 쓰인 변수들을 저장하는 건 어떨까 이랬지만...  
  여러가지면에서 이 디자인이 낫다고 생각된다. 이유는 게임은 너무나도 가지각색이기에..
- Conditinal 걸려있는 LSLogger
- InGameDebugConsole : 빌드에서도 콘솔로 보자

For Unity 이기에 모듈화한 서버 코드는 올라가지 않는다.
FlappyBirdWorld 의 서버작업이 끝나면 따로 관리할 예정이다.
