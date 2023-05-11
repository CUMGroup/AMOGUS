import {Component, OnDestroy, OnInit} from '@angular/core';
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";


@Component({
  selector: 'app-text-parallax',
  templateUrl: './text-parallax.component.html',
  styleUrls: ['./text-parallax.component.scss']
})
export class TextParallaxComponent implements OnInit, OnDestroy {
  constructor() { }

  ngOnInit() {
    this.initScrollTriggers();
  }

  ngOnDestroy(): void {

  }

  initScrollTriggers() {
    // when removing the revealUp css class has Opacity:0 -> hast to be changed when changing
    gsap.registerPlugin(ScrollTrigger)
    gsap.utils.toArray(".revealUp").forEach((elem:any) => {
      ScrollTrigger.create({
        trigger: elem,
        start: "top 90%",
        end: "bottom 10%",
        onEnter: () => {
          gsap.fromTo(
            elem,
            { y: 100, autoAlpha: 0 },
            {
              duration: 1.25,
              y: 0,
              autoAlpha: 1,
              ease: "back",
              overwrite: "auto"
            }
          );
        },
        onLeave: () => {
          gsap.fromTo(elem, { autoAlpha: 1 }, { autoAlpha: 0, overwrite: "auto" });
        },
        onEnterBack: () => {
          gsap.fromTo(
            elem,
            { y: -100, autoAlpha: 0 },
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
          gsap.fromTo(elem, { autoAlpha: 1 }, { autoAlpha: 0, overwrite: "auto" });
        }
      });
    });
  }
}
