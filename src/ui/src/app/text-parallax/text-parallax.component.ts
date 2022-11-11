import {Component, OnInit} from '@angular/core';
import {gsap} from "gsap";
import{ ScrollTrigger } from "gsap/ScrollTrigger";


@Component({
  selector: 'app-text-parallax',
  templateUrl: './text-parallax.component.html',
  styleUrls: ['./text-parallax.component.css']
})
export class TextParallaxComponent implements OnInit {
  constructor() {}

  ngOnInit() {
    gsap.registerPlugin(ScrollTrigger);
    this.initScrollTriggers();
  }

  initScrollTriggers() {
    let sections:any = gsap.utils.toArray(".section");

    sections.forEach((section) => {
      let image = section.querySelector(".image");
      let title = section.querySelector(".title");
      let text = section.querySelector(".text");
      let time = gsap.timeline({
          scrollTrigger: {
            trigger: section,
            start: "top bottom",
            end: "+=100",
            scrub: 2,
            markers: true,
          }
        })
        .from(image, {
          opacity: 0,
          x: -120
        })

        .from(title, {
          opacity: 0,
          y: 120
        })

        .add(gsap.timeline(
          ScrollTrigger.batch(".text", {
            interval: 0.1, // time window (in seconds) for batching to occur.
            batchMax: 3,   // maximum batch size (targets). Can be function-based for dynamic values
            onEnter: batch => gsap.to(batch, {width: 400,scrub:2, stagger: {each: 0.06, grid: [1, 3]}, overwrite: true}),
            onLeave: batch => gsap.set(batch, {width: 1, overwrite: true}),
            onEnterBack: batch => gsap.to(batch, {width: 400, stagger: 0.06, overwrite: true}),
            onLeaveBack: batch => gsap.set(batch, {width: 1, overwrite: true})
          })
        ))

      // let texts:any = gsap.utils.selector(".section")
      // texts(".text").forEach((item) => {
      //   time.from(item, {
      //     opacity: 0,
      //     width: 1,
      //   })
      // })
    });
  }
}
