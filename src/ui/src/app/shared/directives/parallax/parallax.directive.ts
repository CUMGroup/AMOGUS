
import { Directive, Input, ElementRef, HostListener } from '@angular/core';

@Directive({
  selector: '[appParallax]'
})
export class ParallaxDirective {

  @Input('ratioTop') parallaxRatioTop: number = 0;
  @Input('ratioLeft') parallaxRatioLeft: number = 0;
  @Input('rotation') parallaxRotation: number = 0;

  initialRotation: number = 0;
  initialTop: number = 0;
  initialLeft: number = 0;

  constructor(private eleRef: ElementRef) {
    this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top;
    this.initialLeft = this.eleRef.nativeElement.getBoundingClientRect().left;
  }

  @HostListener("window:scroll", ["$event"])
  onWindowScroll(event) {
    this.eleRef.nativeElement.style.top = (this.initialTop - (window.scrollY * this.parallaxRatioTop)) + 'px';
    this.eleRef.nativeElement.style.left = (this.initialLeft - (window.scrollY * this.parallaxRatioLeft)) + 'px'

  }

}
