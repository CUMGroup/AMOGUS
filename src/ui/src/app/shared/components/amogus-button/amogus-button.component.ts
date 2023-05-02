import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-amogus-button',
  templateUrl: './amogus-button.component.html',
  styleUrls: ['./amogus-button.component.css']
})
export class AmogusButtonComponent implements OnInit {

  constructor() { }

  @Input() link?: string;

  @Output() click = new EventEmitter();

  ngOnInit(): void {
  }
}
