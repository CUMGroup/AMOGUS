import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-amogus-button',
  templateUrl: './amogus-button.component.html',
  styleUrls: ['./amogus-button.component.css']
})
export class AmogusButtonComponent implements OnInit {

  constructor() { }

  @Input() link: string;
  @Input() text: string;

  ngOnInit(): void {
  }

}
