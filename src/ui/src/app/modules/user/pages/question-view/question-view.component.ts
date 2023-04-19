import {Component, Inject, Input, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {HttpClient} from "@angular/common/http";
import {TeacherService} from "../../../../core/services/user/teacher.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-question-view',
  templateUrl: './question-view.component.html',
  styleUrls: ['./question-view.component.css']
})
export class QuestionViewComponent implements OnInit {

  @Input() question: FormGroup|undefined;
  newQuestion : FormGroup;
  difficulties = ["Easy","Medium","Hard"];
  categories = ["1"];
  constructor(
    private formBuilder:FormBuilder,
    public teacherService:TeacherService,
    public dialogRef: MatDialogRef<QuestionViewComponent>,
    public snackBar:MatSnackBar,

    @Inject(MAT_DIALOG_DATA) public data: FormGroup,

  ) {
  }

  get wrongAnswerControlArray(): FormControl[]{
    return ((this.newQuestion?.get('wrongAnswers') as FormArray).controls as FormControl[])
  }

  submit(){
    if(this.newQuestion.valid){
      console.log(this.data)
      if(this.data === null){
        this.teacherService.add(this.newQuestion);
      }
      this.dialogRef.close()
    }else{
      this.snackBar.open("Please fill the required Fields", "Close",{duration:1000})
    }
  }

  close(){
    this.dialogRef.close()
  }

  ngOnInit(): void {
    if(this.data != undefined){
      this.newQuestion = this.data
    }else{
      this.newQuestion = this.formBuilder.group({
        exercise: ["",Validators.required ],
        answer: ["", Validators.required ],
        wrongAnswers: this.formBuilder.array([
          this.formBuilder.control("",Validators.required),
          this.formBuilder.control("",Validators.required),
          this.formBuilder.control("",Validators.required),
        ]),
        help: ["", Validators.required ],
        difficultyType: ["", Validators.required ],
        categoryType: ["", Validators.required ],
        multipleChoiceAnswers: [],
      })
    }
  }

}
