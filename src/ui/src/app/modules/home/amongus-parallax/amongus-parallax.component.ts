import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {gsap} from "gsap";

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

  launch(){
    // let amog = this.amogus.nativeElement._gsap
    // let rock = this.rocket.nativeElement
    // let rx = Number((amog.x+"").split(".")[0])
    // let ry = Number((amog.y+"").split(".")[0])
    // let ax = Number((rock.x+"").split(".")[0])
    // let ay = Number((rock.y+"").split(".")[0])
    // console.log("rx: " + rx +", ry: " + ry + ", ax: " + ax + ", ay: " +ay)
    // let rot = (Math.atan2(ay-ry,ax-rx) * (180 / Math.PI));
    // gsap.to(".rocket", {rotation: rot})
    // gsap.to(".rocket",{duration:2,x:amog.x+amog.x,y:amog.y})

  }
  move(e:any) {
    this.parallaxIt(e, ".amongus", 1000);
    this.parallaxIt(e, ".background", -100);
  };

  parallaxIt(e:MouseEvent, target:string, movement:number) {
    const cont: any = this.container.nativeElement;
    const x = (e.pageX - cont.offsetLeft - cont.clientWidth / 2) / cont.clientWidth * movement;
    const y = (e.pageY - cont.offsetTop - cont.clientHeight / 2) / cont.clientHeight * movement;
    let rota = 0;
    if(target != ".background"){
      this.rotation = this.rotation + 5
      rota =this.rotation
    }
    console.log(this.rotation)

    // for reseting if constant right movement exists (x+rota)
    // if(rota >= cont.clientWidth/2){
    //   this.rotation = 0;
    // }

    gsap.to(target, {
      duration: 3,
      x: x,
      y: y,
      rotate:rota,
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
