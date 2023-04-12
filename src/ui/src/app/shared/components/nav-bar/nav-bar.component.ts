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

  // tl:any = gsap.timeline({defaults:{duration:2}})

  ngOnInit(): void {
    // this.tl
    //   .to(".item", {width:100, duration:0.3})
    //   .to(".menu", {left:0, duration:1})

    this.menuArray = [
      {
        text:"login/register",
        path:"/user/login",
      },
      {
        text:"stats",
        path:"/user/stats",
      },
      {
        text:"game",
        path:"/user/game-selection",
      },
      // {
      //   text:"text",
      //   path:"/home/text",
      // },
    ]
  }

  //needs work / simply proof of concept
  expand(){
    if(this.expanded){

      gsap.to(".upper",  {attr: {d: "M10,2 L2,2"}, x: 0, ease:"InOut"});
      gsap.to(".middle",  {autoAlpha: 1});
      gsap.to(".lower",  {attr: {d: "M10,8 L2,8"}, x: 0, ease:"InOut"});

      // this.tl.reverse()

      gsap.timeline()
        .to(".item", {width:0, duration:0.3})
        .to(".menu", {left:-200, duration:1})

      this.expanded = false;
    }else{

      gsap.to(".upper",  {attr: {d: "M8,2 L2,8"}, x: 1, ease:"InOut"});
      gsap.to(".middle",  {autoAlpha: 0});
      gsap.to(".lower",  {attr: {d: "M8,8 L2,2"}, x: 1, ease:"InOut"});

      // this.tl.play()

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
