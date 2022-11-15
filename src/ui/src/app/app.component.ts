import {Component, ElementRef, ViewChild} from '@angular/core';
import { gsap , TweenMax, TimelineMax } from 'gsap'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  data = [
    {
      name:"well",
      value: 2
    },
    {
      name:"fuck",
      value: 3
    },
    {
      name:"you",
      value: 4
    },
    {
      name:"nibba",
      value: 5
    }
  ]
}
