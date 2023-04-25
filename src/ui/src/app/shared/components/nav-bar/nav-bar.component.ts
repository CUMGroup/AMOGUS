import { Component, OnInit } from '@angular/core';
import {gsap} from "gsap";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})

export class NavBarComponent implements OnInit {

  expanded = false;

  menuArray: any[];

  constructor() {}

  ngOnInit(): void {

    this.menuArray = [
      {
        text:"Login/Register",
        path:"/user/login",
      },
      {
        text:"Stats",
        path:"/user/stats",
      },
      {
        text:"Game",
        path:"/user/game-selection",
      },
      {
        text:"Create Questions",
        path:"/user/teacher-view",
      },
      {
        text:"How To Play",
        path:"/how-to",
      },
    ]
  }

  expand(){
    if(this.expanded){

      gsap.to(".upper",  {attr: {d: "M10,2 L2,2"}, x: 0, ease:"InOut"});
      gsap.to(".middle",  {autoAlpha: 1});
      gsap.to(".lower",  {attr: {d: "M10,8 L2,8"}, x: 0, ease:"InOut"});


      gsap.timeline()
        .to(".item", {width:0, duration:0.3})
        .to(".menu", {left:-200, duration:1})

      this.expanded = false;
    }else{

      gsap.to(".upper",  {attr: {d: "M8,2 L2,8"}, x: 1, ease:"InOut"});
      gsap.to(".middle",  {autoAlpha: 0});
      gsap.to(".lower",  {attr: {d: "M8,8 L2,2"}, x: 1, ease:"InOut"});


      gsap.timeline()
        .to(".item", {width:100, duration:0.3})
        .to(".menu", {left:0, duration:1})

      this.expanded = true;
    }
  }

  animate(name:string){
    gsap.to("." + name, {width:200,duration:0.4})
  }

  reverseAnimation(name:string){
    gsap.to("." + name, {width:0,duration:0.4})
  }
}
