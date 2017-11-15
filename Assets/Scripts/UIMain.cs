using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class UIMain:MonoBehaviour{
    public Button button;
    void Start(){
         //方法一：  
        // button.onClick.AddListener(OnClickCallBack);  
        //方法二：  
        // button.onClick.AddListener(delegate (){onClick(button.gameObject);});  
        //方法三：  
        // EventTriggerListener.Get(button.gameObject).onClick = OnButtonClick; 
    }

    //方法一回调：
    private void OnClickCallBack()  
    {  
        Debug.Log(" AddListener ： OnClick testButton....");  
    }  

    //方法二回调：  
    private void onClick(GameObject go)  
    {  
        Debug.Log("delegate : OnClick testButton....");  
    }  
    //方法三回调：
    private void OnButtonClick(GameObject obj){
		//在这里监听按钮的点击事件
		if(obj == button.gameObject){
			Debug.Log ("DoSomeThings");
		}
	}
    /*
    * 方法四： 在控制面板上的组件最下方有一个“On Click ()”的区域 点击该区域的“+”号，可以添加一个触发项，
    * 触发项前面可以选择一个任意的游戏对象，当选择了游戏对象之后后面就可以选择该对象上的一个任意方法，选择好之后，
    * 当我们点击了该按钮对象之后就会调用到选择的游戏对象的制定方法。当然我们也可以自定义方法然后选择调用即可。
    */
    public void onClick(){
        Debug.Log ("onClick");

        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}

