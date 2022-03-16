# FiniteSoul
장르
시스템 개요 및 상세설명

1. 장르 : 로그라이트 (엘든링)

	- 시작 시 캐릭터 직업 설정 할 수 있도록 할 것
	- 무작위 배치
	- 특정 저장 오브젝트를 이용하지 않을 시 위치값 초기화
	- 게임 오버 될 시에도 유지되는 능력치 존재
	- 저장을 하더라도 사망시 저장이 유지되지 않는 값 존재
	- 씬 이동 (비동기)

2. 디자인 패턴

	- 싱글톤
	- 실행 시 필수적으로 존재하여야 하는 Manager 클래스는 T타입 싱글톤으로 주어 데이터를 이용가능하도록 
		(DontDestroyOnLoad) 로 파괴 불가능하도록 할 것

3. 데이터 설정 및 변경 :
	
	- ExcelToJsonConverter2 를 사용하여 데이터를 설정,변경할 것임
	- 플레이어 데이터 저장, 불러오기는 Json데이터를 생성하여 값을 적용할것(없다면 저장하지 않았거나, 첫시작일시)
	- GameManager에서 가지고 있을 데이터 (플레이어 데이터, 몬스터 데이터)
	- MovableObject(이동 가능 객체) 자식으로 플레이어,몬스터,보스몬스터 값 설정할 것
	- 몇몇 객체들은 파괴 가능하도록 인터페이스를 작성하여 파괴가능한지 체크 할것

4. 구현할 UI 및 데이터 : 
	
	4.1 이동가능한 데이터
	- 플레이어 데이터
	- 몬스터 데이터
	- 보스 몬스터 데이터

	4.2 이동 불가능한 데이터
	- 인터페이스 (파괴 가능한지, 불가능한지)

	4.3 UI
	- 인벤토리
	- 상점
	- 스탯창
	- 소비아이템 사용시(사용 쿨타임)
	- 체력, 스테미너
	- 비동기 씬 로드 (시간이 오래 걸리지 않을 것이기 때문에 3초정도 딜레이를 줄것임)

-----------------------------------------------------------------------------------------------------------------------
수정해야할 사항

1. 플레이어 캐릭터 
	1.1
	- Paladin -> 이동 및 공격, 회전 블렌딩 적용해야함(먼저 이동,공격 플레이어 움직임 구현 후 하는것을 추천)

	1.2
	- Archer -> 이동 및 공격, 회전 블렌딩 적용해야함(먼저 이동,공격 플레이어 움직임 구현 후 하는것을 추천)
	
	1.3
	- 현재 PlayerController 작성 후 , StaticData 작성중에 있음

------------------
2. 몬스터
	2.1 Animator override Controller 사용할것
	- 참고 : https://bbangdeveloper.tistory.com/entry/Animation-Animator-Override-Controller

-------------------
3. 데이터

작성한 데이터를 리스트 형태로 넣어 빼서 쓸 수 있도록 할것
json 읽는 코드 작성하여 게임 시작 시, 값에 넣도록 설정할 것임(GameManager에 데이터 삽입부 작성할 것임) 
LitJson으로 작성중.. ★

참고 - https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=pxkey&logNo=221302704547

	3.1 몬스터와 플레이어 모두 사용하는 데이터

	인덱스
	이름
	캐릭터 타입
	공격력
	방어력
	최대체력
	캐릭터 타입
	공격 타입
	이동속도
	리소스경로

	3.2 플레이어만 가지고 있는 데이터

		3.2.1 변동하지 않는 데이터
		마나
		스테미너

		3.2.2 변동하는 데이터 (GrowthData)
		레벨
		경험치, 최대 경험치
		체력, 최대 체력
		공격력
		방어력

	3.3 몬스터만 가지고 있는 데이터

	드롭경험치
	드롭아이템

	
---------------
4. 애니메이션 블랜딩

★ 데이터 설정 후, 아바타 마스크를 이용하여 애니메이션을 블랜딩 시킬것임
참조 - https://www.youtube.com/watch?v=BStXjU-mJvk
참조 - 책

