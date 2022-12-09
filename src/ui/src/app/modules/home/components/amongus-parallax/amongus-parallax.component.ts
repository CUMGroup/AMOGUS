import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { gsap } from "gsap";

@Component({
  selector: 'app-amongus-parallax',
  templateUrl: './amongus-parallax.component.html',
  styleUrls: ['./amongus-parallax.component.css']
})
export class AmongusParallaxComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  @ViewChild('container') container: ElementRef;
  @ViewChild('amogus') amogus: ElementRef;
  @ViewChild('rocket') rocket: ElementRef;

  rotation: number = 0;

  moveTimer: number = 20;
  move(e: any) {
    this.parallaxIt(e, ".amongus", 1000);
    //this.parallaxIt(e, ".background", -100);
  };

  parallaxIt(e: MouseEvent, target: string, movement: number) {
    const cont: any = this.container.nativeElement;
    let x = (e.pageX - cont.offsetLeft - cont.clientWidth / 2) / cont.clientWidth * movement;
    let y = (e.pageY - cont.offsetTop - cont.clientHeight / 2) / cont.clientHeight * movement;
    if (y < 0) y = 0;
    if (y > cont.clientHeight / 2) y = cont.clientHeight / 2;
    console.log(cont.clientHeight)
    let rota = 0;
    if (target != ".background") {
      this.rotation = this.rotation + 5
      rota = this.rotation
    }

    // for reseting if constant right movement exists (x+rota)
    // if(rota >= cont.clientWidth/2){
    //   this.rotation = 0;
    // }

    gsap.to(target, {
      duration: 5,
      x: x,
      y: y,
      rotate: rota,
    });

  }
  // let tmp = document.getElementById("test");
  // let matrix = window.getComputedStyle(tmp,null).getPropertyValue("transform");
  // let rotation: string[] = matrix.split('(');
  // console.log(rotation);
  // let rotations = rotation.split(',');
  // console.log(rotations);
  // Math.round(Math.atan2(b, a) * (180/Math.PI))
  // let angle = Math.round(Math.asin(Number(rotation)) * (180/Math.PI));
  // if(isNaN(angle)){ angle = 1}
  // console.log(angle);

}
