import {Component, OnDestroy, OnInit} from '@angular/core';
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";
import {Router} from "@angular/router";
import {debounceTime} from "rxjs";


@Component({
  selector: 'app-text-parallax',
  templateUrl: './text-parallax.component.html',
  styleUrls: ['./text-parallax.component.scss']
})
export class TextParallaxComponent implements OnInit, OnDestroy {
  constructor() { }

  ngOnInit(){
    this.initScrollTriggers();
  }

  ngOnDestroy(): void {
    ScrollTrigger.getAll().forEach(t => t.kill())
  }

  async initScrollTriggers() {
    // when removing the revealUp css class has Opacity:0 -> hast to be changed when changing
    // Promise is required to not interrupt the lifecycle of angular -> results in misplaced triggers when routing
    await new Promise(f => setTimeout(f, 0)).then(()=>{
      gsap.registerPlugin(ScrollTrigger)
      gsap.utils.toArray(".revealUp").forEach((elem: any) => {
        ScrollTrigger.create({
          trigger: elem,
          start: "top 90%",
          end: "bottom 10%",
          onEnter: () => {
            gsap.fromTo(
              elem,
              {y: 100, autoAlpha: 0},
              {
                duration: 1.25,
                y: 0,
                autoAlpha: 1,
                ease: "back",
                overwrite: "auto",
              }
            );
          },
          onLeave: () => {
            gsap.fromTo(elem, {autoAlpha: 1}, {autoAlpha: 0, overwrite: "auto"});
          },
          onEnterBack: () => {
            gsap.fromTo(
              elem,
              {y: -100, autoAlpha: 0},
              {
                duration: 1.25,
                y: 0,
                autoAlpha: 1,
                ease: "back",
                overwrite: "auto"
              }
            );
          },
          onLeaveBack: () => {
            gsap.fromTo(elem, {autoAlpha: 1}, {autoAlpha: 0, overwrite: "auto"});
          }
        });
      });
    });
  }
}
