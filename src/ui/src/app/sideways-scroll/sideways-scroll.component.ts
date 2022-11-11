import { Component, OnInit } from '@angular/core';
import {gsap} from "gsap";
import{ScrollTrigger} from "gsap/ScrollTrigger"

@Component({
  selector: 'app-sideways-scroll',
  templateUrl: './sideways-scroll.component.html',
  styleUrls: ['./sideways-scroll.component.css']
})
export class SidewaysScrollComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
    gsap.registerPlugin(ScrollTrigger);
    this.initalize()
  }

  initalize(){
    let sections = gsap.utils.toArray(".panel");

    gsap.to(sections, {
      xPercent: -100 * (sections.length - 1),
      ease: "none",
      scrollTrigger: {
        trigger: ".container",
        pin: true,
        scrub: 1,
        snap: 1 / (sections.length - 1),
        // base vertical scrolling on how wide the container is so it feels more natural.
        end: "+=3500",
      }
    });
  }

}
