# FiniteSoul
장르
시스템 개요 및 상세설명

1. 장르 : 로그라이트 (엘든링)

	- 시작 시 캐릭터 직업 설정 할 수 있도록 할 것
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
진행 사항

0. 작업 시작 (대략적인 기획)
22.03.14

1. 플레이어 모델 애니메이션 수정(Paladin, Archer)
	-> (Paladin)검, 방패 / (Archer) 활, 화살 프리팹 삽입함 -> 그에 맞게 애니메이션 조정
	
22.03.15
	
2. 데이터 설정 (Excel, Json)
	-> 플러그인 ExcelToJsonConvert2Debug 로 엑셀 파일을 Json 파일로 변환
	-> ResourceUtil 에 데이터 삽입 및 변환시키는 InsertDataSetting 메소드 작성, GameManager.Awake() 시 실행되도록
	-> MoveableObject 및 PlayerController등 스텟 삽입에 필요한 데이터 추가 작성
	-> 데이터를 로드하는 방법은 모든데이터가 저장되어 있는 FullData 에서 Linq 의 Where 로 값을 하나씩 찾아 resourcePath로 생성 후,
		 생성된 오브젝트에 데이터를 넣어주는 방식
	
	 /참고사항/★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
   	 // jsonUtility는 배열일 시 못가져옴,
   	 // Litjson은 값을 따로따로 찾는 것이기때문에 값을 넣을 수는 있지만, 한꺼번에 넣기가 번거로움
   	 // 그리하여 NewtonSoft.json 을 사용했음 (변환부 : GameManager)
	 	-> JsonConvert(NewtonSoft.json)은 배열이 아닌 단일값만 찾을시 추가생성자가 존재한다면 매개변수가 없는 생성자도 있어야 오류가 출력되지 않음
		-> JsonSerializationException: Cannot deserialize the current JSON object (e.g. {"name":"value"})    
			 - 만약 위의 오류가 출력된다면 너가 가져오려는 데이터가 배열로 된 데이터가 아닌데 왜 배열로 가져올라고 하냐 라고 하는것
	 
22.03.16 ~ 22.03.17

3. 플레이어 애니메이션 삽입 및 스크립트 생성
	-> 생성시 캐릭터 움직임 작동확인 목적 작성
	-> 회피기동(Evasion)
		-> 플레이어가 S키를 누르고있을경우 회피기동 전환을 하지못하도록 설정
		-> 플레이어 Evasion 상태일시 이동속도, 이동방향은 Evasion이 끝날때까지 값이 일시 고정
	

22.03.21 ~ 22.03.22

4. 로드씬 및 시작씬 설정
	-> StartSceneAdjust 스크립트를 최상위 Canvas 에 넣어 시작씬 관리
	-> 추가 버튼 이벤트 바인딩 및 버튼,타이틀,배경 자리 셋팅(코루틴)
	-> ResourceUtil 에 ( 이어하기가 가능한지 확인하는 기능, 데이터찾기 기능 ) 추가
	-> PlayerVolatilityData (플레이어 휘발성 데이터) 추가	<데이터 로드시 사용할 데이터>
	
22.03.23 ~ 22.03.24

4. 맵 제작(Terrain)
	-> Terrain으로 제작
	-> 플레이어 회전부분 에러 수정
	
22.03.25

5. 씬전환시 데이터 설정 및 추가 설정, 몬스터 데이터 설정, InGameManager 설정
	-> FadeIn() 기능 추가
	-> FadeIn() 이후 해당 씬에 플레이어 생성 및 데이터 설정
	-> 몬스터데이터 추가, InGameManager 작성 , 몬스터 스폰위치(ScriptableObject)
	-> 씬 비동기 로드 완료후, 몬스터 생성/플레이어 생성
	-> 몬스터/플레이어 생성시, 위치값 지정, 데이터 삽입,스텟 설정, 레이어/태그 설정, InGameManager 등록
	-> 풀링 매니저 추가, 해당 풀링은 RecyclePooling 인터페이스를 가지고 있는 오브젝트만 사용할수 있도록 
	-> MoveableObject 클래스에 RequireComponent 를 사용하여 Rigidbody를 안넣는 경우 방지, MonsterController 도 NavMeshAgent 를 안넣는 경우 
	
22.03.28 ~ 22.03.30

6. 몬스터 애니메이션 작성, NavMesh 필요 기능 추가 , 상태머신 수정, 플레이어 레이어 마스크 수정, 플레이어 자연회복기능 추가
	-> 상태머신 및 상태를 최상위 부모가 들고있고 대리자로 상태머신(FSM)에 넘겨주는 방식으로 애니메이션을 변경하도록 설정하였음
	-> 몬스터 애니메이션은 Mutant만 작성후, Warrok의 애니메이션은 Animator override Controller 로 작성하여 애니메이터 작성을 최소화 하였음
	-> 몬스터 시야 관련 메소드 작성, 시야각/시야 거리에 따라 몬스터가 플레이어를 인식할수 있도록 설정
	-> 타깃 설정은 Update로 호출하며 target을 찾을 경우 더 호출하지 않도록 하여 사용을 최소화하였음 (RayCastHit 으로 플레이어를 찾았음)
	-> 플레이어가 이동중 공격시 하반신 움직임 애니메이션을 블랜드 트리로 엮었음 (++ 좌 우측 뒤로이동, ++ 좌 우측 앞으로 이동, ++ 좌 우측 뛰기, ++ 좌 우측 뒤로뛰기)
	-> 플레이어 연속공격 애니메이션 및  추가, 몬스터 연속 피격 애니메이션 추가
	-> 스테미너가 깎였을시 시간에 따라 회복하는 기능 
	
	/참고사항/
	// Base 컴포넌트를 상속받는 child 컴포넌트가 있을때
	// child 컴포넌트의 객체에 Getcomponent<Base>() 를 할 경우,
	// 해당 컴포넌트를 가지고 있는것으로 인식됨
	
	/참고사항/
	// 1 << LayerMask.NameToLayer("Player")      : 플레이어 레이어를 들고있는 애만 찾겠다
	// (-1) << LayerMask.NameToLayer("Player")   : 플레이어 레이어만 무시하고 찾는다
	// 레이어마스크 값에 비트연산으로 입력하지않으면 값이 정상적으로 실행되지 않음    참고※
	
	/참고사항/
	// 만약 두개의 오브젝트가 Rigidbody 의 Is Kinematic 체크가 해제되어있다면 정의되지 않은 동작이 발생할수 있음
	// Is Kinematic : 물리법칙에 영향을 받지않도록함 -> 체크되어있다면
	
	/ 참고사항 /
	// NavmeshAgent 를 실행할때 SetDestination를 통해 목적지를 설정했다면 , 실행하지 않으려 할때 ResetPath 로 초기화를 시켜야한다
		 초기화 시키지 않을시 해당 목적지까지 이동 완료 후에 멈추는 버그 발생
	
22.04.04 ~ 22.04.07
	
7. 데미지 처리, 피격 애니메이션 전환
	-> Trigger 상태인 Collider를 공격시 자동으로 켜지고 꺼지도록 애니메이션에 삽입함
	-> AttackController 를 추가하여 해당 컨트롤러에서 데미지 연산
		-> AttackController 는 각각 공격 콜라이더의 오브젝트에 AddComponent 시켜 OnTriggerEnter 로 진행
	-> 데미지 연산후 해당 객체의 HurtState 상태로 전환
	-> 플레이어는 피격후 Hurt애니메이션 종료전까지 움직일 수 없음 (MoveableObject 의 NotToMove 로 설정중)
	
22.04.11
	
8. UI 작성, 원본데이터 복사되는 경우 수정
	-> UI를 총괄하는 UIManager를 만들어 UI를 On/Off, 데이터를 불러오기 쉽도록 작성
		-> UIManager에 스테이지 전환 및 특정 이벤트시 UI 비/활성/천천히 활성/천천히 비활성 기능을 추가하였음
	-> 체력바,스테미너바,마나바 UI 추가
		-> 체력,스테미너,마나 UI 데이터변경은 InGameManager의 Player값이 Null이 아니고 해당 스크립트의 데이터가 전과 다를때 실행
		-> 체력바, 스테미너바, 마나바 는 캐릭터가 올린 능력치에따라 길이도 변경되도록 작성하였음
	-> 소지품 UI 추가
	-> 데이터를 클래스에 리스트형식으로 담아두었기에 그대로 값을 사용할 경우 원본값이 복사되어 값이 변경될때 원본값도 같이 복사되기 때문에
	- 객체가 여러개이고 데이터가 서로 달라야하는 객체의 경우 값을 생성자로 하나하나 삽입하여 원본값 복사를 막아주었음
	-> 룬 보유고 UI 추가
	-> 플레이어 장비 + 스탯창 추가
		-> 장비창의 플레이어는 RawTexture(플레이어 앞의 카메라)로 모양을 가져옴
	-> 플레이어의 소모품창과 장비창이 연동되도록 설정
	-> 스테이지 이동시 잠시 해당 스테이지를 출력하는 윈도우를 
	
	
	/ 참고사항 /
	// 이번 게임에 적용된 모든 Json 데이터를 FullData에 삽입후, 데이터 값을 변경시켜야하는 객체의 경우 생성자로 값을 하나하나 재설정하여
	/-------------- 원본값이 변경되지 않도록 설정하였음.	-> 값을 변경시켜야 하는 값에 경우 앞에 Use 를 별도로 붙였음
	/-------------- 단. 플레이어는 게임내 하나만 존재하고 있기에 그냥 원본데이터를 사용하도록 하였음
	
	
22.04.12 ~ 22.04.15
	
9. 저장기능 작성, 스테이터스 능력치 변경
	-> 상호작용 기능을 전달받거나, On/Off , 상호작용 실행을 하는 기능 추가
		-> 상호작용을 받는 객체에 특정 스크립트를 넣어 상호작용을 하는 스크립트끼리 확인이 가능하도록 하였음
		-> 상호작용시 상호작용 UI가 출력되며 해당 텍스트의 길이가 일정이상을 넘어설경우 그에 따른 UI 이미지도 같이 
	-> 모닥불에 상호작용하였을시 플레이어 이동불가 + 카메라회전 불가 설정
	-> 모닥불(Status) 상호작용 가능하도록 수정
	-> 스테이터스 소비룬이 최대소모룬보다 많을시 능력치 업그레이드 기능 추가
		-> 변경시 기본 능력치보다 값을 올렸을때 색상변경 기능 추가
		-> 변경사항 적용 버튼 클릭시 변경된값으로 능력치 변경후, 기본값이 변경된값으로 설정됨
		-> 변경사항을 적용하지 않고 나갈시 본래의 기본값으로 리턴 설정됨
	-> 플레이어가 모닥불에 상호작용시 게임 데이터 저장(모닥불 이용후 탈출시 데이터 저장)
	-> 항상 SaveData에서 값을 찾아오고 , 새게임일시 SaveData를 초기값으로 덮어씌운뒤 시작한다
	
	/ 참고사항 /
	-> 런타임중 데이터를 변경하여 바로 사용하고자 할때는 AssetDatabase.Refresh(); 를 하여 새로고침을 해야 즉시 값이 변경된다
	
22.04.18
	
10. 캐릭터 선택창 구현 및 (선택된 캐릭터)데이터 전달, 캐릭터 시네머신 설정
	-> 플레이어를 바라보는 캐릭터는 스테이지 변경시 VirtualCam, 메인카메라(in CineMachineBrain)을 생성하여 플레이어를 따라가도록 설정
		-> VirtualCam 생성시 Start에서 Follow,LookAt값을 InGameManager.Instance.Player.transform로 설정
	-> 시작씬에서 새로하기 버튼클릭시 저장된 타임라인이 실행되며 캐릭터 선택창으로 전환 (DolyTrack 사용)
	-> 캐릭터 창 설정
		-> 캐릭터창에서 (시작씬으로 돌아가기)버튼 클릭시 캐릭터창이 즉시 종료되며 시작씬으로 변경
		-> 캐릭터창에서 (캐릭터 변경)버튼 클릭시 다음 캐릭터위치로 카메라 이동
		-> 캐릭터창에서 (캐릭터 선택)버튼 클릭시 해당 캐릭터로 즉시 게임시작 / 다음씬으로 전환
		-> 해당 캐릭터의 기본능력치 스테이터스창에 표시
	
22.04.19
	
11. 궁수(발사체) 설정, 파티클(이펙트) 추가, DyingUI 추가, 드랍 룬 작성
	-> 발사체(화살) 데이터 및 설정
		-> 해당 발사체에 ReQuirmentComponet 를 사용하여 강제로 AttackController를 추가하였음
		-> 발사체.GetComponent<AttackController>().staticData = maindata 로 설정하여 데이터 값을 넘겨주었음
		-> 화살을 플레이어의 정면방향으로 발사 (화살 활성화시 hit.point 방향으로 Lerp 이동)
			-> Vector3.MoveTowards 메소드로 거리가 다르더라도 동일한 속도가 적용되도록 설정
			-> 화살이 몬스터가 아닌 다른 오브젝트에 맞을 경우 데미지 체크를 하지않도록 해당 AttackController의 checkBool = false 로 지정
	-> 파티클(이펙트)
		-> 파티클베이스 - 파티클자식 스크립트를 생성하여 ParticleSystem.IsPlaying을 코루틴으로 실행시켜 거짓일 경우 풀링매니저에 반환
			-> Arrow(이펙트)파티클 출력
				-> Arrow 발사체일 경우 닿았을시 other.bounds.ClosestPoint(transform.position) 로 충돌지점 위치값,
					각도계산(플레이어 , other로테이션)을 하여 파티클 출력
				-> Arrow 발사체는 생성시 TrailRenderer 로 인하여 궤적이 남도록 출력 (원형을 그리는 Material 삽입)
			-> 몬스터Attack(이펙트)파티클 출력
				-> 플레이어가 공격을 받았을시 충돌지점에서 파티클 출력
			-> Sword(이펙트)파티클 출력
				-> 플레이어가 공격에 성공했을시 충돌지점에서 파티클 출력
					-> 해당 파티클은 손목의 회전각도에 따라 출력되는 Rotation 값이 다르도록 출력
	-> DyingUI 추가
		-> 플레이어가 체력이 0이 되었을시, 이동 불가 설정 이후 DyingUIWindow가 출력 (AttackController 에서 실행)
			-> DyingUIWindow가 출력된 이후 일정 시간뒤 해당 스테이지로 재로드
			-> InGameManager 데이터 초기화 후 재로드
				-> 모든 데이터 GetPush로 풀링에 삽입
	-> 캐릭터 사망시 룬(현재 가지고있는) 드랍
		-> 룬 상호작용 가능 데이터 작성
		-> 사망시 룬에 데이터를 삽입하고 전에 저장했던 위치값으로 플레이어를 생성
			-> 저장했던 위치값이 존재하지 않는다면 Vector.zero 값으로 생성
		-> 현재 들고있는 currentRune 만큼 드랍, 들고있는 룬이 0이라면 드랍하지 않음
			-> 이후 플레이어가 가지고 있는 룬은 0으로 설정
		-> 룬을 먹지 못하고 사망했을시, 전에 획득하지 못한 룬은 사라짐
	
	
	/ 참고사항 /
	// NavmeshAgent를 돌리고 있는 오브젝트가 있을때 오브젝트가 SetDestination을 통해 회전하지만, 거기에서 추가로
		 오브젝트에 로테이션 회전을 줄 경우 이상한 회전값이 출력될 수 있음
	
	/ 참고사항 /
	// OnTriggerEnter 에서 Trigger인 콜라이더를 체크하려고 할때, 콜라이더가 IsTrigger가 체크된 콜라이더 하나만 있다면 문제가 없지만
		IsTrigger가 체크되지않은 콜라이더가 더 존재한다면 체크되지않고 오브젝트가 멈추는 버그가 발생할 수 있음
	
	/ 참고사항 / 
	// GetComponentInChildren 을 실행할때 <Image>를 가져온다고 한다면 만약 자신도 가지고있다면  자신이 가지고있는것도 포함이된다
		- enabled가 꺼져있어도 읽고, 아예 없어야 읽지 않는다..  이건 확실하지 않음   신뢰 ㄴㄴ
	

22.04.20 ~ 22.04.23
	
12. 상점 및 인벤토리 구현, 인벤토리 저장데이터 추가
	-> 상점 + 인벤토리 데이터 및 설정
		-> 인벤토리
			-> SlotGenerator 클래스에서 인벤토리,상점 스크롤뷰 자식 Content에 인벤토리 슬롯 생성
				-> SlotGenerator 에서 상점에 넣어줄 기타 아이템(Etc)를 제외한 모든 아이템 데이터를 삽입
			-> 인벤토리는 스크롤뷰로 설정하여 인벤토리를 스크롤 할수있도록 설정
			-> IPointerEnterHandler로 마우스 위치값이 상점슬롯이나 인벤토리 슬롯에 위치했을 경우 설명박스로 해당 아이템 설명 표시
		-> 구매,판매창
			-> 상점 슬롯을 클릭했을시 아이템 구매창이 출력
			-> 인벤토리 슬롯을 클릭했을시 아이템 판매창이 출력
		-> 데이터 설정
			-> 인벤토리 윈도우 스크립트에 인벤토리에 아이템값 추가 및 제거(+ 이미지 재설정, 최대 개수 초과시 다음 슬롯에 저장)하는 메소드 생성
				-> 장비 아이템일경우 인벤토리 슬롯에 최대 1개만 가지고있을수 있으며 그 외의 아이템의 경우 99개까지 한 슬롯에 소지가능
					-> 소지아이템 개수가 그 이상으로 넘어갈경우 가장 첫번째로 비어있는 슬롯에 남은 갯수 삽입
			-> 또한 플레이어가 사망했을시 해당 아이템 데이터를 저장
			-> 소지품의 소비아이템 슬롯은 인벤토리에 있는 소비아이템과 데이터가 연동되도록 설정
				-> 소지품의 소비아이템 변경은 Z 키 입력시 인벤토리에 있는 다른 소비아이템으로 변경
				-> 소지품의 소비아이템 사용은 Q 키 입력시 현재 소지품에 착용중인 물약 사용
					-> 사용시 해당 인벤토리에 있는 아이템과 데이터가 연동되어 작동
						-> ex) 소지품이나 인벤토리의 갯수가 차감될경우 서로의 데이터값이 모두 변경
					-> 물약아이템 사용시 마시는 포션에따라 Material 색상 변경
					-> 물약 사용시 두번에 걸쳐 회복 (피격받았을 경우 회복 중단)

	/ 참고사항 /
	// 스크롤뷰를 설정할때 해당 RectTransform의 회전값이 변경되어있다면 인식방향이 회전하기 전의 방향으로 인식하여 버그가 발생할수있음
		- 스크롤뷰 설정할때는 되도록이면 로테이션값 변경하지 말것
	
22.04.24 ~ 22.04.28
	
13. 몬스터 아이템 드랍 및 드랍테이블 설정 + 드랍 UI
	-> 드랍테이블 (몬스터 데이터를 넣어줄시 계산전용)
		-> 해당 몬스터가 가지고있는 드랍확률안에 진입시 모든 아이템의 확률을 더하여 누적확률 계산 실행  (해당 아이템 드랍확률 / 모든 아이템 드랍확률)
			-> 랜덤확률이 해당 누적확률안에 진입했을시 해당 아이템 드랍
			-> 인벤토리 아이템 데이터에 즉시 삽입
			-> 소지품(소비아이템)일경우 소지품 UI 재설정

22.05.02

---------------------------------------------------------------------------------------------------------------------
수정해야할 사항

1. 할 것    TODO

	
	4.11 소리
		-> SoundManager 만들것
			-> 플레이시킬 메서드는 만들었는데 Update일 경우에도 일정간격에 따라 호출해야하는데, 호출시간을 매개변수로 넣어야하나?
			
			-> SoundUI 만들기 (+ 메뉴판으로 돌아가기도 추가 + 플레이어 데이터 저장)
				-> 여기서에 Effect 볼륨, BGM 볼륨 연결해서 소리 조정하기
	
	-> 이상한 부분
		-> (Interaction 이 들어가있는) 특정 오브젝트에 발사체가 지나갈경우 가끔식 발사체가 공중에 고정되어버리는 버그; 
			-> 스크립트상 잡는 부분은 없음;;
			-> Trigger가 체크된 강체안에 Trigger가 체크되지않은 강체가 있다면 멈주나?
	
	-> UseItemData 는 모노를 상속안받고 있는데 왜 모노를 상속받았다고 나옴 ? -> SloBase 14번째 
		
	

// -> 쉐이더 작성 참고
		 - https://24dc-m.tistory.com/3?category=926420
		 - https://darkcatgame.tistory.com/8?category=1034169
	-> 쉐이더 디졸브
		- https://gdev.tistory.com/16
	

