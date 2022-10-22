using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using test1;
using test2;
using test3;
using test5;
namespace test4{
    public class boatCon{
        public GameObject boatm;
        public int boatWhere;
        public Cha[] roles=new Cha[2];
        IUserAction useraction;
        int speed = 10;
        public boatCon(GameObject obj){
            useraction = SSDirector.getInstance().currentSceneController as IUserAction;
            boatm=obj;
            boatWhere=0;
        }
        public bool AddRoles(Cha t){
            if(roles[0]!=null&&roles[1]!=null) return false;
            if(t.Cwhere!=boatWhere) return false;
            if(roles[0]!=null) {
                roles[1]=t;
                t.cha.transform.position = Vector3.MoveTowards(t.cha.transform.position,new Vector3(1,(float)2.8,3-boatWhere*6),speed);
                t.Cwhere=-1;
            }
            else{
                roles[0]=t;
                t.cha.transform.position = Vector3.MoveTowards(t.cha.transform.position,new Vector3(-1,(float)2.8f,3-boatWhere*6),speed);
                t.Cwhere=-1;
            }
            return true;
        }
        public void RemoveRoles(int i){
            if(roles[0]!=null&&roles[0].kind==i){
                roles[0].Cwhere=boatWhere;
                roles[0].cha.transform.position = Vector3.MoveTowards(roles[0].cha.transform.position,new Vector3(5-2*roles[0].num,(float)2.8,8-boatWhere*16),speed);
                Cha tep = roles[0];
                roles[0] = null;
                return;
            }
            if(roles[1]!=null&&roles[1].kind==i){
                roles[1].Cwhere=boatWhere;
                roles[1].cha.transform.position = Vector3.MoveTowards(roles[1].cha.transform.position,new Vector3(5-2*roles[1].num,(float)2.8,8-boatWhere*16),speed);
                Cha tep = roles[1];
                roles[1] = null;
            }
            return;
        }
    }
}
