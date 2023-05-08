import { Component, OnInit } from '@angular/core';
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";


@Component({
  selector: 'app-text-parallax',
  templateUrl: './text-parallax.component.html',
  styleUrls: ['./text-parallax.component.scss']
})
export class TextParallaxComponent implements OnInit {
  constructor() { }

  ngOnInit() {
    gsap.registerPlugin(ScrollTrigger);
    this.initScrollTriggers();
  }

  initScrollTriggers() {
    let sections: any = gsap.utils.toArray(".section");

    sections.forEach((section) => {
      let title = section.querySelector(".title");
      let time = gsap.timeline({
        scrollTrigger: {
          trigger: section,
          start: "top bottom",
          end: "+=100",
          scrub: 2,
        }
      })
        .from(title, {
          opacity: 0,
          x: 120,
          toggleActions: "restart pause reverse pause",
        })
      time.add(gsap.timeline(
        ScrollTrigger.batch(".text", {
          interval: 0.3, // time window (in seconds) for batching to occur.
          batchMax: 10,   // maximum batch size (targets). Can be function-based for dynamic values
          onEnter: batch => gsap.to(batch, { opacity: 1, scrub: 2, stagger: 0.06,overwrite: true }),
        })
      ))
      time.play()
    });
  }
}
