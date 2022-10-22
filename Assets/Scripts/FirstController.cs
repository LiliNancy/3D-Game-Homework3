using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



using test1;
using test2;
using test3;
using test4;
using test5;
public class FirstController : MonoBehaviour,ISceneController,IUserAction
{
    GameObject river;
    GameObject land1;
    GameObject land2; 
    boatCon boat;
    int statue=0;
    List<Cha> charactor=new List<Cha>();
    int speed=10;
    void Awake(){
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources(); 
    }
    public void LoadResources(){
        river = Instantiate(Resources.Load("Prefabs/river"), new Vector3(0,(float)-2.5,0), Quaternion.identity, null) as GameObject;
        land1 = Instantiate(Resources.Load("Prefabs/grass"), new Vector3(0,0,10), Quaternion.identity, null) as GameObject;
        land2 = Instantiate(Resources.Load("Prefabs/grass"), new Vector3(0,0,-10), Quaternion.identity, null) as GameObject;
        boat = new boatCon(Instantiate(Resources.Load("Prefabs/boat"), new Vector3(0,0,3), Quaternion.identity, null) as GameObject);
        for(int i=0;i<3;i++){
            Vector3 a=new Vector3(5-2*i,3,8);
            GameObject obj = Instantiate(Resources.Load("Prefabs/priest"),a, Quaternion.identity, null) as GameObject;
            charactor.Add(new Cha(obj,(int)(5-a.x)/2,0));
        }
        for(int i=0;i<3;i++){
            Vector3 a=new Vector3(-1-2*i,3,8);
            GameObject obj = Instantiate(Resources.Load("Prefabs/evil"),a, Quaternion.identity, null) as GameObject;
            charactor.Add(new Cha(obj,(int)(5-a.x)/2,1));
        }
    }
    public void Restart(){
        if(statue==1)return;
        for(int i=0;i<6;i++){
            charactor[i].cha.transform.position = new Vector3(5-2*i,3,8);
            charactor[i].Cwhere=0;
        }
        boat.roles[0]=null;
        boat.roles[1]=null;
        boat.boatm.transform.position = new Vector3(0,0,3);
        boat.boatWhere=0;
        statue=0;
    }
    public void ChooseCha(int i){
        if(statue!=0) return;
        for(int j=0+3*i;j<6;j++){
            if(boat.AddRoles(charactor[j])) break;
        }
    }
    public void MoveBoat(){
        if(statue!=0) return;
        if(boat.roles[0]==null&&boat.roles[1]==null) return;
        boat.boatWhere = 1-boat.boatWhere;
        statue =1;
    }
    public void RemoveCha(int i){
        if(statue!=0) return;
        boat.RemoveRoles(i);
    }
    public string check(){
        if(statue!=0) return " ";
        int re=0,rp=0,le=0,lp=0;
        for(int i=0;i<6;i++){
            if(charactor[i].kind==0){
                if(charactor[i].Cwhere==0) rp++;
                else if(charactor[i].Cwhere==-1&&boat.boatWhere==0) rp++;
                else lp++;
            }
            else{
                if(charactor[i].Cwhere==0) re++;
                else if(charactor[i].Cwhere==-1&&boat.boatWhere==0) re++;
                else le++;
            }
        }
        statue=2;
        if(re>rp&&rp!=0) {
            return "Game Over";  
        }
        else if(le>lp&&lp!=0) {
            return "Game Over";
        }
        else if(lp==3&&le==3) {
            return "You Win!";
        }
        statue=0;
        return " ";
    }
    void Start()
    {  
    }
    void Update(){
        if(statue==1){
            Vector3 target = new Vector3(0,0,3-6*boat.boatWhere);
            boat.boatm.transform.position = Vector3.MoveTowards(boat.boatm.transform.position,target,speed*Time.deltaTime);
            if(boat.roles[0]!=null)
                boat.roles[0].cha.transform.position = Vector3.MoveTowards(boat.roles[0].cha.transform.position,new Vector3(-1,(float)2.8,3-6*boat.boatWhere),speed*Time.deltaTime);
            if(boat.roles[1]!=null)
                boat.roles[1].cha.transform.position = Vector3.MoveTowards(boat.roles[1].cha.transform.position,new Vector3(1,(float)2.8,3-6*boat.boatWhere),speed*Time.deltaTime);
            if(boat.boatm.transform.position==target) statue=0;
        }
    }
}

