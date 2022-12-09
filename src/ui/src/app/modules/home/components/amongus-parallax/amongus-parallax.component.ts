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

  move(e: any) {
    this.parallaxIt(e, ".amongus", 1000);
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

    gsap.to(target, {
      duration: 5,
      x: x,
      y: y,
      rotate: rota,
    });

  }

}
