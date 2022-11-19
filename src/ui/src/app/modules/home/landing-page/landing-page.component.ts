import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {gsap} from "gsap";
import {animate} from "@angular/animations";

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  @ViewChild('content') container: ElementRef;

  constructor() { }

  ngOnInit() {
    this.animate();
  }

  move(e:any) {
    this.parallaxIt(e, "#svg", 200);
    this.parallaxIt(e, ".left", -200);
    this.parallaxIt(e, ".center", -200);
    this.parallaxIt(e, ".right", -200);
  };

  parallaxIt(e:MouseEvent, target:string, movement:number) {
    const cont: any = this.container.nativeElement;
    const x = (e.pageX - cont.offsetLeft - cont.clientWidth / 2) / cont.clientWidth * movement;
    const y = (e.pageY - cont.offsetTop - cont.clientHeight / 2) / cont.clientHeight * movement;

    gsap.to(target, {
      duration: 1,
      x: x,
      y: y,
    });
  }

  animate(){
    gsap.fromTo(".left",{left:50,width:300,height:800},{duration:3, left:100,width:400,height:1000})
    gsap.fromTo(".center",{width:300, height:800},{duration:3,width:400,height:1000})
    gsap.fromTo(".right",{right:50,width:300,height:800},{duration:3, right:100,width:400,height:1000})
  }

}
