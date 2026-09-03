using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void OnClickExit()
    {
        // 에디터에서 테스트 중일 때는 로그만 찍고,
        Debug.Log("게임 종료!");
        // 실제로 빌드된 게임에서는 종료되도록 설정
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}